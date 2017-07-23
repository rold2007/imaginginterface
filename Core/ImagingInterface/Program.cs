namespace ImagingInterface
{
   using System;
   using System.Collections.Generic;
   using System.Diagnostics;
   using System.IO;
   using System.Reflection;
   using System.Windows.Forms;
   using ImagingInterface.BootStrapper;
   using ImagingInterface.Controllers;
   using ImagingInterface.Controllers.Services;
   using ImagingInterface.Plugins;
   using ImagingInterface.Views;
   using SimpleInjector;
   using SimpleInjector.Diagnostics;

   public static class Program
   {
      private static readonly string PluginsRootFolderName = "Plugins";

      private static List<string> pluginFolders = new List<string>();
      private static List<string> pluginLibraries = new List<string>();

      private static Container container = new Container();

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

            using (MainWindow mainWindow = Program.container.GetInstance<MainWindow>())
            {
               Application.Run(mainWindow);
            }

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

         if (System.IO.Directory.Exists(pluginRootFolder))
         {
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
         container.RegisterSingleton<IImageViewFactory, ImageViewFactory>();

         // Views
         container.Register<AboutBoxView>();
         container.Register<ImageManagerView>();
         container.Register<ImageView>();
         container.RegisterSingleton<Func<ImageView>>(() => { return container.GetInstance<ImageView>(); });
         container.Register<MainWindow>();
         container.Register<PluginManagerView>();

         // Controllers
         container.Register<MainController>();
         container.Register<FileOperationController>();
         container.Register<PluginOperationController>();
         container.Register<ImageManagerController>();
         container.Register<AboutBoxController>();
         container.Register<ImageController>();
         container.Register<PluginManagerController>();
         container.Register<PluginViewFactory>();
         container.RegisterSingleton<ImageSourceManager>();

         // Services
         Assembly servicesAssembly = typeof(ApplicationPropertiesService).Assembly;
         Type[] serviceTypes = servicesAssembly.GetExportedTypes();

         foreach (Type type in serviceTypes)
         {
            if (type.Namespace == "ImagingInterface.Controllers.Services")
            {
               if (type.IsVisible)
               {
                  container.Register(type, type, Lifestyle.Transient);
               }
            }
         }

         List<Type> packageWindowsFormsTypes = new List<Type>();
         List<Type> pluginControllerTypes = new List<Type>();
         List<Type> imageSourceTypes = new List<Type>();
         List<Assembly> pluginAssemblies = new List<Assembly>();

         foreach (string libraryFile in Program.pluginLibraries)
         {
            Assembly pluginAssembly = Assembly.LoadFrom(libraryFile);
            Type[] exportedTypes = pluginAssembly.GetExportedTypes();

            foreach (Type exportedType in exportedTypes)
            {
               if (Program.TypeValid(exportedType, typeof(IPackageWindowsForms)))
               {
                  packageWindowsFormsTypes.Add(exportedType);
               }

               if (Program.TypeValid(exportedType, typeof(IPluginController)))
               {
                  pluginControllerTypes.Add(exportedType);
               }

               if (Program.TypeValid(exportedType, typeof(IImageSource)))
               {
                  imageSourceTypes.Add(exportedType);
               }
            }

            pluginAssemblies.Add(pluginAssembly);
         }

         container.RegisterCollection<IPluginView>(pluginAssemblies);

         foreach (Type packageWindowsFormsType in packageWindowsFormsTypes)
         {
            IPackageWindowsForms packageWindowsForms = Activator.CreateInstance(packageWindowsFormsType) as IPackageWindowsForms;

            packageWindowsForms.RegisterServices(container);
         }

         container.RegisterCollection<IPluginController>(pluginControllerTypes);
         container.RegisterCollection<IImageSource>(imageSourceTypes);

         foreach (Type packageWindowsFormsType in packageWindowsFormsTypes)
         {
            IPackageWindowsForms packageWindowsForms = Activator.CreateInstance(packageWindowsFormsType) as IPackageWindowsForms;

            packageWindowsForms.SuppressDiagnosticWarning(container);
         }

         container.GetRegistration(typeof(AboutBoxView)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
         container.GetRegistration(typeof(ImageManagerView)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
         container.GetRegistration(typeof(ImageView)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
         container.GetRegistration(typeof(MainWindow)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
         container.GetRegistration(typeof(PluginManagerView)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");

         // Verify will also create the MainWindow and the ApplicationController
         container.Verify();

         DiagnosticResult[] diagnosticResults = Analyzer.Analyze(container);

         foreach (DiagnosticResult diagnosticResult in diagnosticResults)
         {
            if (diagnosticResult.DiagnosticType != DiagnosticType.DisposableTransientComponent)
            {
               throw new InvalidOperationException("IoC container was not initialized properly. Reason: " + diagnosticResult.Description);
            }
         }
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
