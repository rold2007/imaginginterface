// <copyright file="ControllersBaseTest.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.Tests
{
   using NUnit.Framework;

   public abstract class ControllersBaseTest
      {
      // public Container Container
      //   {
      //   get;
      //   private set;
      //   }
      [SetUp]
      protected void SetUp()
         {
         // base.SetUp();

         // this.Bootstrap();
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
         ////this.Container.RegisterSingleton<IFileOperationView, FileOperationView>();
         ////this.Container.RegisterSingleton<IImageManagerView, ImageManagerView>();
         ////this.Container.RegisterSingleton<IPluginOperationView, PluginOperationsView>();
         ////this.Container.RegisterSingleton<IPluginManagerView, PluginManagerView>();
         ////this.Container.RegisterSingleton<IHelpOperationView, HelpOperationView>();
         ////this.Container.Register<IImageView, ImageView>();
         ////this.Container.RegisterSingleton<PluginView1>();
         ////this.Container.RegisterSingleton<PluginView2>();

         // Controllers
         this.Container.Register<MainController>();
         this.Container.Register<FileOperationController>();
         this.Container.Register<ImageManagerController>();
         this.Container.Register<PluginOperationController>();
         this.Container.Register<PluginManagerController>();
         this.Container.Register<AboutBoxController>();
         this.Container.Register<ImageController>();
         this.Container.Register<IImageSource, ImageSourceController>();
         this.Container.Register<IFileSource, FileSourceController>();
         this.Container.Register<IImageProcessingController, ImageProcessingController>();

         // Models
         this.Container.Register<AboutBoxModel>();
         this.Container.Register<IImageModel, ImageModel>();
         this.Container.Register<PluginModel1>();

         // Plugins
         this.Container.RegisterCollection<IPluginController>(new Type[] { typeof(PluginController1), typeof(PluginController2) });
         }*/
      }
   }
