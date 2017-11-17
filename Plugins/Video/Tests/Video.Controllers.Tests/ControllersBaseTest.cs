// <copyright file="ControllersBaseTest.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace Video.Controllers.Tests
{
   public abstract class ControllersBaseTest
      {
      protected void SetUp()
         {
         // base.SetUp();

         ////this.Bootstrap();
         }

      /*
      private void Bootstrap()
         {
         this.Container = new Container();

         // This is needed for some tests to register a mock class
         this.Container.Options.AllowOverridingRegistrations = true;

         // Service
         //this.Container.RegisterSingleton<IServiceLocator, SimpleInjectorServiceLocatorAdapter>();

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
         ////this.Container.Register<IMemorySourceController, MemorySourceController>();

         // Models
         this.Container.Register<CaptureModel, CaptureModel>();
         this.Container.Register<IImageModel, ImageModel>();
         ////this.Container.Register<IMemorySourceModel, MemorySourceModel>();
         }*/
      }
   }
