namespace ImagingInterface
   {
   using System;
   using System.Collections.Generic;
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
         Program.InitializePluginFolders();

         AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(LoadFromSameFolder);

         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);

         Program.Bootstrap();

         Application.Run(Program.serviceLocator.GetInstance<MainWindow>());
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

         container.RegisterSingle<IServiceLocator>(Program.serviceLocator);

         // Views
         // Need to register singleton instances for all things pertaining MainWindow
         container.RegisterSingle<MainWindow>();
         container.RegisterSingle<ImageManagerView>();
         container.RegisterSingle<IMainView>(Program.GetMainWindow);
         container.RegisterSingle<IFileOperationView>(Program.GetMainWindow);
         container.RegisterSingle<IImageManagerView>(Program.GetImageManagerView);
         container.RegisterSingle<IPluginOperationsView>(Program.GetMainWindow);
         container.RegisterSingle<IPluginManagerView>(Program.GetPluginManagerView);
         container.Register<IImageView, ImageView>();

         // Controllers
         container.RegisterSingle<IMainController, MainController>();
         container.RegisterSingle<IFileOperationController, FileOperationController>();
         container.RegisterSingle<IImageManagerController, ImageManagerController>();
         container.RegisterSingle<IPluginOperationController, PluginOperationController>();
         container.RegisterSingle<IPluginManagerController, PluginManagerController>();
         container.Register<IImageController, ImageController>();

         // Models
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

         container.RegisterAll<IPluginController>(pluginsTypes);

         // Verify will also create thw MainWindow and the ApplicationController
         container.Verify();

         DiagnosticResult[] diagnosticResults = Analyzer.Analyze(container);

         if (diagnosticResults.Length != 0)
            {
            throw new InvalidOperationException("IoC container was not initialized properly.");
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
      }
   }
