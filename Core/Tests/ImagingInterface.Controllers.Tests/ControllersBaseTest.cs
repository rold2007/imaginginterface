namespace ImagingInterface.Controllers.Tests
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using CommonServiceLocator.SimpleInjectorAdapter;
   using ImagingInterface.Controllers.Tests.Mocks;
   using ImagingInterface.Controllers.Tests.Views;
   using ImagingInterface.Models;
   using ImagingInterface.Plugins;
   using ImagingInterface.Views;
   using Microsoft.Practices.ServiceLocation;
   using NUnit.Framework;
   using SimpleInjector;

   public abstract class ControllersBaseTest
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
      protected void Bootstrap()
         {
         ContainerOptions containerOptions = new ContainerOptions();

         // This is needed for some tests to register a mock class
         containerOptions.AllowOverridingRegistrations = true;

         this.Container = new Container(containerOptions);

         // Service
         this.Container.RegisterSingle<IServiceLocator, SimpleInjectorServiceLocatorAdapter>();

         // Views
         this.Container.RegisterSingle<IMainView, MainView>();
         this.Container.RegisterSingle<IFileOperationView, FileOperationView>();
         this.Container.RegisterSingle<IImageManagerView, ImageManagerView>();
         this.Container.RegisterSingle<IPluginOperationView, PluginOperationsView>();
         this.Container.RegisterSingle<IPluginManagerView, PluginManagerView>();
         this.Container.RegisterSingle<IHelpOperationView, HelpOperationView>();
         this.Container.RegisterSingle<IAboutBoxView, AboutBoxView>();
         this.Container.Register<IImageView, ImageView>();
         this.Container.Register<IPluginView, PluginView>();

         // Controllers
         this.Container.RegisterSingle<IMainController, MainController>();
         this.Container.RegisterSingle<IFileOperationController, FileOperationController>();
         this.Container.RegisterSingle<IImageManagerController, ImageManagerController>();
         this.Container.RegisterSingle<IPluginOperationController, PluginOperationController>();
         this.Container.RegisterSingle<IPluginManagerController, PluginManagerController>();
         this.Container.RegisterSingle<IHelpOperationController, HelpOperationController>();
         this.Container.RegisterSingle<IAboutBoxController, AboutBoxController>();
         this.Container.Register<IImageController, ImageController>();
         this.Container.Register<IImageSourceController, ImageSourceController>();

         // Models
         this.Container.RegisterSingle<IAboutBoxModel, AboutBoxModel>();
         this.Container.Register<IImageModel, ImageModel>();

         // Plugins
         this.Container.RegisterAll<IPluginController>(new Type[] { typeof(PluginController1), typeof(PluginController2) });
         }
      }
   }
