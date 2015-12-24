namespace ImagingInterface
   {
   using System;
   using System.Collections.Generic;
   using System.Diagnostics;
   using System.IO;
   using System.Linq;
   using System.Reflection;
   using System.Windows.Forms;
   using CommonServiceLocator.SimpleInjectorAdapter;
   using ImagingInterface.BootStrapper;
   using ImagingInterface.Controllers;
   using ImagingInterface.Models;
   using ImagingInterface.Plugins;
   using ImagingInterface.Views;
   using Microsoft.Practices.ServiceLocation;
   using SimpleInjector;
   using SimpleInjector.Diagnostics;

   public static class Program
      {
      private static readonly string PluginsRootFolderName = "Plugins";

      private static List<string> pluginFolders = new List<string>();
      private static List<string> pluginLibraries = new List<string>();

      private static IServiceLocator serviceLocator;

      /// <summary>
      /// The main entry point for the application.
      /// </summary>
      [STAThread]
      public static void Main()
         {
         TraceSource traceSource = new TraceSource("Critical", SourceLevels.Critical);
         Trace.AutoFlush = true;

         try
            {
            TextWriterTraceListener textWriterTraceListener = new TextWriterTraceListener("log.txt");

            traceSource.Listeners.Add(textWriterTraceListener);

            Program.InitializePluginFolders();

            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(LoadFromSameFolder);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Program.Bootstrap();

            ValidateAssemblyUniqueness();

            Application.Run(Program.serviceLocator.GetInstance<MainWindow>());

            // Make sure that using the plugins we don't load some DLL from elsewhere
            ValidateAssemblyUniqueness();
            }
         catch (Exception e)
            {
            traceSource.TraceEvent(TraceEventType.Critical, 0, e.ToString());
            }
         }

      private static void InitializePluginFolders()
         {
         string pluginRootFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Program.PluginsRootFolderName);
         IEnumerable<string> pluginFolders = System.IO.Directory.EnumerateDirectories(pluginRootFolder, "*", System.IO.SearchOption.AllDirectories);

         foreach (string pluginFolder in pluginFolders)
            {
            IEnumerable<string> libraryFiles = System.IO.Directory.EnumerateFiles(pluginFolder, "*.dll", System.IO.SearchOption.AllDirectories);
            bool pluginLibraryPresent = false;

            foreach (string libraryFile in libraryFiles)
               {
               Program.pluginLibraries.Add(libraryFile);
               pluginLibraryPresent = true;
               }

            if (pluginLibraryPresent)
               {
               Program.pluginFolders.Add(pluginFolder);
               }
            }
         }

      // Helps SimpleInjector load some plugin assemblies, else we get a FileNotFoundException
      private static System.Reflection.Assembly LoadFromSameFolder(object sender, ResolveEventArgs args)
         {
         foreach (string pluginFolder in Program.pluginFolders)
            {
            string assemblyPath = Path.Combine(pluginFolder, new AssemblyName(args.Name).Name + ".dll");

            if (File.Exists(assemblyPath))
               {
               Assembly assembly = Assembly.LoadFrom(assemblyPath);

               return assembly;
               }
            }

         return null;
         }

      private static void Bootstrap()
         {
         Container container = new Container();

         // Service
         Program.serviceLocator = new SimpleInjectorServiceLocatorAdapter(container);

         container.RegisterSingleton<IServiceLocator>(Program.serviceLocator);

         // Views
         // Need to register singleton instances for all things pertaining MainWindow
         container.RegisterSingleton<MainWindow>();
         container.RegisterSingleton<ImageManagerView>();
         container.RegisterSingleton<PluginManagerView>();
         container.RegisterSingleton<IMainView>(Program.GetMainWindow);
         container.RegisterSingleton<IFileOperationView>(Program.GetMainWindow);
         container.RegisterSingleton<IImageManagerView>(Program.GetImageManagerView);
         container.RegisterSingleton<IPluginOperationView>(Program.GetMainWindow);
         container.RegisterSingleton<IPluginManagerView>(Program.GetPluginManagerView);
         container.RegisterSingleton<IHelpOperationView>(Program.GetMainWindow);
         container.RegisterSingleton<IAboutBoxView, AboutBoxView>();
         container.Register<IImageView, ImageView>();

         // Controllers
         container.RegisterSingleton<IMainController, MainController>();
         container.RegisterSingleton<IFileOperationController, FileOperationController>();
         container.RegisterSingleton<IImageManagerController, ImageManagerController>();
         container.RegisterSingleton<IPluginOperationController, PluginOperationController>();
         container.RegisterSingleton<IPluginManagerController, PluginManagerController>();
         container.RegisterSingleton<IHelpOperationController, HelpOperationController>();
         container.RegisterSingleton<IAboutBoxController, AboutBoxController>();
         container.Register<IImageController, ImageController>();

         // Models
         container.RegisterSingleton<IAboutBoxModel, AboutBoxModel>();
         container.Register<IImageModel, ImageModel>();

         List<Type> packageWindowsFormsTypes = new List<Type>();
         List<Type> pluginsTypes = new List<Type>();

         foreach (string libraryFile in Program.pluginLibraries)
            {
            Assembly assembly = Assembly.LoadFrom(libraryFile);
            Type[] exportedTypes = assembly.GetExportedTypes();

            foreach (Type exportedType in exportedTypes)
               {
               if (Program.TypeValid(exportedType, typeof(IPackageWindowsForms)))
                  {
                  packageWindowsFormsTypes.Add(exportedType);
                  }

               if (Program.TypeValid(exportedType, typeof(IPluginController)))
                  {
                  pluginsTypes.Add(exportedType);
                  }
               }
            }

         foreach (Type packageWindowsFormsType in packageWindowsFormsTypes)
            {
            IPackageWindowsForms packageWindowsForms = Activator.CreateInstance(packageWindowsFormsType) as IPackageWindowsForms;

            packageWindowsForms.RegisterServices(container);
            }

         container.RegisterCollection<IPluginController>(pluginsTypes);

         // Verify will also create thw MainWindow and the ApplicationController
         container.Verify();

         DiagnosticResult[] diagnosticResults = Analyzer.Analyze(container);

         foreach (DiagnosticResult diagnosticResult in diagnosticResults)
            {
            if (diagnosticResult.DiagnosticType != DiagnosticType.DisposableTransientComponent)
               {
               throw new InvalidOperationException("IoC container was not initialized properly.");
               }
            }
         }

      private static MainWindow GetMainWindow()
         {
         return Program.serviceLocator.GetInstance<MainWindow>();
         }

      private static ImageManagerView GetImageManagerView()
         {
         return Program.serviceLocator.GetInstance<ImageManagerView>();
         }

      private static PluginManagerView GetPluginManagerView()
         {
         return Program.serviceLocator.GetInstance<PluginManagerView>();
         }

      private static bool TypeValid(Type currentType, Type validationType)
         {
         if (validationType.IsAssignableFrom(currentType))
            {
            if (!currentType.IsAbstract)
               {
               if (!currentType.IsGenericTypeDefinition)
                  {
                  return true;
                  }
               }
            }

         return false;
         }

      private static void ValidateAssemblyUniqueness()
         {
         // Make sure that no dependent DLL are loaded from the plugin folders
         // This happens when a reference is added to a plugin project but the "Copy Local"
         // property isn't set to false. Having the same DLL loaded multiple times from
         // different paths can lead to weird crashes so it is better to avoid it.
         Assembly[] loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
         HashSet<string> loadedAssemblyFullNames = new HashSet<string>();

         foreach (Assembly loadedAssembly in loadedAssemblies)
            {
            string loadedAssemblyFullName = loadedAssembly.FullName;

            if (loadedAssemblyFullNames.Contains(loadedAssemblyFullName))
               {
               // When this exception is thrown, change the "Copy Local" property of the
               // referenced DLL for the plugin with the issue
               throw new InvalidOperationException(string.Format("The DLL {0} was already loaded. Don't load the same DLL twice from different paths.", loadedAssemblyFullName));
               }

            loadedAssemblyFullNames.Add(loadedAssemblyFullName);
            }
         }
      }
   }
