namespace ImagingInterface.Controllers.Tests
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading;
   using System.Threading.Tasks;
   using CommonServiceLocator.SimpleInjectorAdapter;
   using ImagingInterface.Controllers.Tests.Mocks;
   using ImagingInterface.Controllers.Tests.Views;
   using ImagingInterface.Models;
   using ImagingInterface.Plugins;
   using ImagingInterface.Tests.Common;
   using ImagingInterface.Tests.Common.Mocks;
   using ImagingInterface.Tests.Common.Views;
   using ImagingInterface.Views;
   using Microsoft.Practices.ServiceLocation;
   using NUnit.Framework;
   using SimpleInjector;

   public abstract class ControllersBaseTest : BaseTest
      {
      public Container Container
         {
         get;
         private set;
         }

      public IServiceLocator ServiceLocator
         {
         get
            {
            return this.Container.GetInstance<IServiceLocator>();
            }
         }

      [SetUp]
      protected override void SetUp()
         {
         base.SetUp();

         this.Bootstrap();
         }

      private void Bootstrap()
         {
         this.Container = new Container();

         // This is needed for some tests to register a mock class
         this.Container.Options.AllowOverridingRegistrations = true;

         // Service
         this.Container.RegisterSingleton<IServiceLocator, SimpleInjectorServiceLocatorAdapter>();

         // Views
         this.Container.RegisterSingleton<IMainView, MainView>();
         this.Container.RegisterSingleton<IFileOperationView, FileOperationView>();
         this.Container.RegisterSingleton<IImageManagerView, ImageManagerView>();
         this.Container.RegisterSingleton<IPluginOperationView, PluginOperationsView>();
         this.Container.RegisterSingleton<IPluginManagerView, PluginManagerView>();
         this.Container.RegisterSingleton<IHelpOperationView, HelpOperationView>();
         this.Container.RegisterSingleton<IAboutBoxView, AboutBoxView>();
         this.Container.Register<IImageView, ImageView>();
         this.Container.RegisterSingleton<PluginView1>();
         this.Container.RegisterSingleton<PluginView2>();

         // Controllers
         this.Container.RegisterSingleton<IMainController, MainController>();
         this.Container.RegisterSingleton<IFileOperationController, FileOperationController>();
         this.Container.RegisterSingleton<IImageManagerController, ImageManagerController>();
         this.Container.RegisterSingleton<IPluginOperationController, PluginOperationController>();
         this.Container.RegisterSingleton<IPluginManagerController, PluginManagerController>();
         this.Container.RegisterSingleton<IHelpOperationController, HelpOperationController>();
         this.Container.RegisterSingleton<IAboutBoxController, AboutBoxController>();
         this.Container.Register<IImageController, ImageController>();
         this.Container.Register<IImageSourceController, ImageSourceController>();
         this.Container.Register<IFileSourceController, FileSourceController>();
         this.Container.Register<IImageProcessingController, ImageProcessingController>();

         // Models
         this.Container.RegisterSingleton<IAboutBoxModel, AboutBoxModel>();
         this.Container.Register<IImageModel, ImageModel>();
         this.Container.Register<IFileSourceModel, FileSourceModel>();
         this.Container.RegisterSingleton<PluginModel1>();

         // Plugins
         this.Container.RegisterCollection<IPluginController>(new Type[] { typeof(PluginController1), typeof(PluginController2) });
         }
      }
   }
