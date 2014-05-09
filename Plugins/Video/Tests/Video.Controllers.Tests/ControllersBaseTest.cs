namespace Video.Controllers.Tests
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading;
   using System.Threading.Tasks;
   using CommonServiceLocator.SimpleInjectorAdapter;
   using ImagingInterface.Controllers;
   using ImagingInterface.Models;
   using ImagingInterface.Tests.Common;
   using ImagingInterface.Views;
   using Microsoft.Practices.ServiceLocation;
   using NUnit.Framework;
   using SimpleInjector;
   using Video.Controllers;
   using Video.Controllers.Tests.Mocks;
   using Video.Controllers.Tests.Views;
   using Video.Models;
   using Video.Views;

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
         ContainerOptions containerOptions = new ContainerOptions();

         // This is needed for some tests to register a mock class
         containerOptions.AllowOverridingRegistrations = true;

         this.Container = new Container(containerOptions);

         // Service
         this.Container.RegisterSingle<IServiceLocator, SimpleInjectorServiceLocatorAdapter>();

         // Views
         this.Container.RegisterSingle<IMainView, MainView>();
         this.Container.RegisterSingle<IImageManagerView, ImageManagerView>();
         this.Container.Register<ICaptureView, CaptureView>();
         this.Container.Register<IImageView, ImageView>();

         // Controllers
         this.Container.RegisterSingle<IMainController, MainController>();
         this.Container.RegisterSingle<IImageManagerController, ImageManagerController>();
         this.Container.Register<ICaptureController, CaptureController>();
         this.Container.Register<IImageController, ImageController>();
         this.Container.Register<ICaptureWrapper, CaptureWrapperMock>();

         // Models
         this.Container.Register<ICaptureModel, CaptureModel>();
         this.Container.Register<IImageModel, ImageModel>();
         }
      }
   }
