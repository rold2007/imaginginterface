namespace ImageProcessing.Controllers.Tests
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading;
   using System.Threading.Tasks;
   using CommonServiceLocator.SimpleInjectorAdapter;
   using ImageProcessing.Controllers;
   using ImageProcessing.Controllers.Tests.Mocks;
   using ImageProcessing.Controllers.Tests.Views;
   using ImageProcessing.Models;
   using ImageProcessing.Views;
   using ImagingInterface.Controllers;
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
      protected void SetUp()
         {
         this.InitializeSynchronizationContext();

         this.Bootstrap();
         }

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
         this.Container.RegisterSingle<IImageManagerView, ImageManagerView>();
         this.Container.Register<IInvertView, InvertView>();
         this.Container.Register<IRotateView, RotateView>();
         this.Container.Register<IImageView, ImageView>();

         // Controllers
         this.Container.RegisterSingle<IMainController, MainController>();
         this.Container.RegisterSingle<IImageManagerController, ImageManagerController>();
         this.Container.Register<IInvertController, InvertController>();
         this.Container.Register<IRotateController, RotateController>();
         this.Container.Register<IImageController, ImageController>();
         this.Container.Register<IImageSourceController, ImageSourceController>();

         // Models
         this.Container.Register<IInvertModel, InvertModel>();
         this.Container.Register<IRotateModel, RotateModel>();
         this.Container.Register<IImageModel, ImageModel>();
         }

      private void InitializeSynchronizationContext()
         {
         SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
         }
      }
   }
