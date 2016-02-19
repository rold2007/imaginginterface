namespace Video.Controllers.Tests
   {
   using CommonServiceLocator.SimpleInjectorAdapter;
   using ImageProcessing.Controllers;
   using ImageProcessing.Models;
   using ImagingInterface.Controllers;
   using ImagingInterface.Models;
   using ImagingInterface.Plugins;
   using ImagingInterface.Tests.Common;
   using Microsoft.Practices.ServiceLocation;
   using NUnit.Framework;
   using SimpleInjector;
   using Video.Controllers;
   using Video.Controllers.Tests.Mocks;
   using Video.Models;

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
         ////this.Container.RegisterSingleton<IMainView, MainView>();
         ////this.Container.RegisterSingleton<IImageManagerView, ImageManagerView>();
         ////this.Container.Register<ICaptureView, CaptureView>();
         ////this.Container.Register<IImageView, ImageView>();

         // Controllers
         this.Container.Register<MainController>();
         this.Container.Register<ImageManagerController>();
         this.Container.Register<CaptureController>();
         this.Container.Register<ImageController>();
         this.Container.Register<ICaptureWrapper, CaptureWrapperMock>();
         this.Container.Register<IMemorySourceController, MemorySourceController>();

         // Models
         this.Container.Register<ICaptureModel, CaptureModel>();
         this.Container.Register<IImageModel, ImageModel>();
         this.Container.Register<IMemorySourceModel, MemorySourceModel>();
         }
      }
   }
