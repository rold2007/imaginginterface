// <copyright file="Program.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface
{
   using System;
   using System.Collections.Generic;
   using System.Diagnostics;
   using System.Diagnostics.CodeAnalysis;
   using System.IO;
   using System.Reflection;
   using System.Threading.Tasks;
   using System.Windows.Forms;
   using ImageProcessor.Configuration;
   using ImageProcessor.Imaging.Formats;
   using ImagingInterface.Controllers;
   using ImagingInterface.Controllers.Interfaces;
   using ImagingInterface.Controllers.Services;
   using ImagingInterface.Controllers.Views;
   using ImagingInterface.Plugins;
   using ImagingInterface.Views;
   using SimpleInjector;
   using SimpleInjector.Diagnostics;

   public static class Program
   {
      private const string PluginsRootFolderName = "Plugins";

      private static List<string> pluginFolders = new List<string>();
      private static List<string> pluginLibraries = new List<string>();

      private static Container container = new Container();

      /// <summary>
      /// The main entry point for the application.
      /// </summary>
      [STAThread]
      [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Managed through trace.")]
      public static void Main()
      {
         LoadSupportedImageFormatsAsynchronously();

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
         catch (SimpleInjector.DiagnosticVerificationException e)
         {
            traceSource.TraceEvent(TraceEventType.Critical, 0, e.ToString());

            throw;
         }
         catch (Exception e)
         {
            traceSource.TraceEvent(TraceEventType.Critical, 0, e.ToString());

            throw;
         }
      }

      // Need to call this explicitely otherwise the first image load is too slow (~2-3s)
      // This is because ImageProcessor load all Accord.Net dll and scan the available classes.
      public static async void LoadSupportedImageFormatsAsynchronously()
      {
         Task task = Task.Run(() =>
         {
            IEnumerable<ISupportedImageFormat> supportedImageFormats = ImageProcessorBootstrapper.Instance.SupportedImageFormats;
         });

         await task.ConfigureAwait(false);
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
         container.RegisterSingleton<IApplicationLogic, ApplicationLogic>();
         container.RegisterSingleton<IImageViewFactory, ImageViewFactory>();
         container.RegisterSingleton<IPluginViewFactory, PluginViewFactory>();

         // Views
         container.Register<AboutBoxView>();
         container.Register<ImageManagerView>();
         container.Register<ImageView>();
         container.RegisterInstance<Func<IImageView>>(() => { return container.GetInstance<ImageView>(); });
         container.Register<MainWindow>();
         container.Register<PluginManagerView>();

         // Controllers
         container.Register<AboutBoxController>();
         container.Register<FileOperationController>();
         container.Register<ImageManagerController>();
         container.Register<ImageController>();
         container.Register<IImageProcessingManagerService, ImageProcessingManagerService>();
         container.Register<MainController>();
         container.Register<PluginManagerController>();
         container.Register<PluginOperationController>();

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

         // Override the current registration
         Debug.Assert(container.Options.AllowOverridingRegistrations == false, "The default behavior should be disabled. Someone forgot to reset this value.");

         container.Options.AllowOverridingRegistrations = true;
         container.RegisterSingleton<ImageManagerService>();
         container.RegisterSingleton<ImageSourceService>();
         container.RegisterSingleton<PluginManagerService>();
         container.Options.AllowOverridingRegistrations = false;

         List<Assembly> pluginAssemblies = new List<Assembly>();

         foreach (string libraryFile in Program.pluginLibraries)
         {
            Assembly pluginAssembly = Assembly.LoadFrom(libraryFile);

            pluginAssemblies.Add(pluginAssembly);
         }

         container.RegisterPackages(pluginAssemblies);

         container.Collection.Register<IPluginView>(pluginAssemblies);

         SuppressDiagnosticWarning();

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

      private static void SuppressDiagnosticWarning()
      {
         InstanceProducer[] currentRegistrations = container.GetCurrentRegistrations();

         foreach (InstanceProducer instanceProducer in currentRegistrations)
         {
            if (instanceProducer.Lifestyle == Lifestyle.Transient)
            {
               if (instanceProducer.ServiceType.IsPublic)
               {
                  if (!instanceProducer.ServiceType.IsAbstract)
                  {
                     if (!instanceProducer.ServiceType.IsGenericTypeDefinition)
                     {
                        if (typeof(IDisposable).IsAssignableFrom(instanceProducer.ServiceType))
                        {
                           bool validType = false;

                           if (typeof(Control).IsAssignableFrom(instanceProducer.ServiceType))
                           {
                              validType = true;
                           }
                           else if (typeof(IPluginView).IsAssignableFrom(instanceProducer.ServiceType))
                           {
                              validType = true;
                           }

                           if (validType)
                           {
                              instanceProducer.Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
                           }
                        }
                     }
                  }
               }
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
               throw new InvalidOperationException(FormattableString.Invariant($"The DLL {loadedAssemblyFullName} was already loaded. Don't load the same DLL twice from different paths."));
            }

            loadedAssemblyFullNames.Add(loadedAssemblyFullName);
         }
      }
   }
}
