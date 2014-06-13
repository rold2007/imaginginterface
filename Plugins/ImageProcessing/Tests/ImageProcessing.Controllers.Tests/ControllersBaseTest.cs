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
   using ImageProcessing.Controllers.Tests.Views;
   using ImageProcessing.Models;
   using ImageProcessing.Views;
   using ImagingInterface.Controllers;
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
         this.Container.Register<ICudafyView, CudafyView>();
         this.Container.Register<IImageView, ImageView>();
         this.Container.Register<ITaggerView, TaggerView>();

         // Controllers
         this.Container.RegisterSingle<IMainController, MainController>();
         this.Container.RegisterSingle<IImageManagerController, ImageManagerController>();
         this.Container.Register<IInvertController, InvertController>();
         this.Container.Register<IRotateController, RotateController>();
         this.Container.Register<ICudafyController, CudafyController>();
         this.Container.Register<IImageController, ImageController>();
         this.Container.Register<IImageSourceController, ImageSourceController>();
         this.Container.Register<IFileSourceController, FileSourceController>();
         this.Container.Register<IMemorySourceController, MemorySourceController>();
         this.Container.Register<ITaggerController, TaggerController>();

         // Models
         this.Container.Register<IInvertModel, InvertModel>();
         this.Container.Register<IRotateModel, RotateModel>();
         this.Container.Register<ICudafyModel, CudafyModel>();
         this.Container.Register<IImageModel, ImageModel>();
         this.Container.Register<IFileSourceModel, FileSourceModel>();
         this.Container.Register<IMemorySourceModel, MemorySourceModel>();
         this.Container.Register<ITaggerModel, TaggerModel>();
         }
      }
   }
