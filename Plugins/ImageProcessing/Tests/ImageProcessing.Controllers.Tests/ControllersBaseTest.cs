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
   using ImageProcessing.ObjectDetection;
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
         this.Container = new Container();

         // This is needed for some tests to register a mock class
         this.Container.Options.AllowOverridingRegistrations = true;

         // Service
         this.Container.RegisterSingleton<IServiceLocator, SimpleInjectorServiceLocatorAdapter>();

         // Views
         this.Container.RegisterSingleton<IMainView, MainView>();
         this.Container.RegisterSingleton<IImageManagerView, ImageManagerView>();
         this.Container.Register<IInvertView, InvertView>();
         this.Container.Register<IRotateView, RotateView>();
         this.Container.Register<ICudafyView, CudafyView>();
         this.Container.Register<IImageView, ImageView>();
         this.Container.RegisterSingleton<ITaggerView, TaggerView>();
         this.Container.RegisterSingleton<IObjectDetectionView, ObjectDetectionView>();
         this.Container.Register<IObjectDetectionManagerView, ObjectDetectionManagerView>();

         // Controllers
         this.Container.RegisterSingleton<IMainController, MainController>();
         this.Container.RegisterSingleton<IImageManagerController, ImageManagerController>();
         this.Container.Register<IInvertController, InvertController>();
         this.Container.Register<IRotateController, RotateController>();
         this.Container.Register<ICudafyController, CudafyController>();
         this.Container.Register<IImageController, ImageController>();
         this.Container.Register<IImageSourceController, ImageSourceController>();
         this.Container.Register<IFileSourceController, FileSourceController>();
         this.Container.Register<IMemorySourceController, MemorySourceController>();
         this.Container.Register<ITaggerController, TaggerController>();
         this.Container.Register<IObjectDetectionController, ObjectDetectionController>();
         this.Container.Register<IObjectDetectionManagerController, ObjectDetectionManagerController>();

         // Models
         this.Container.Register<IInvertModel, InvertModel>();
         this.Container.Register<IRotateModel, RotateModel>();
         this.Container.Register<ICudafyModel, CudafyModel>();
         this.Container.Register<IImageModel, ImageModel>();
         this.Container.Register<IFileSourceModel, FileSourceModel>();
         this.Container.Register<IMemorySourceModel, MemorySourceModel>();
         this.Container.RegisterSingleton<ITaggerModel, TaggerModel>();
         this.Container.RegisterSingleton<IObjectDetectionModel, ObjectDetectionModel>();
         this.Container.Register<IObjectDetectionManagerModel, ObjectDetectionManagerModel>();

         // Processing
         this.Container.RegisterSingleton<IObjectDetector, ObjectDetector>();
         this.Container.RegisterSingleton<ITagger, Tagger>();
         }
      }
   }
