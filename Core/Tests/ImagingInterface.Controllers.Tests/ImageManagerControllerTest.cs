// <copyright file="ImageManagerControllerTest.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.Tests
{
   using ImagingInterface.Controllers.Services;
   using Xunit;

   public class ImageManagerControllerTest : ControllersBaseTest
   {
      public ImageManagerControllerTest()
      {
         this.Container.Register<ImageManagerController>();
         this.Container.RegisterSingleton<ImageManagerService>();
         this.Container.RegisterSingleton<PluginManagerService>();
      }

      [Fact]
      public void Constructor()
      {
         ////ImageManagerController imageViewManagerController = this.Container.GetInstance<ImageManagerController>();
      }

      [Fact]
      public void Constructor2()
      {
         ////ImageManagerController imageViewManagerController = this.Container.GetInstance<ImageManagerController>();

         // IImageSource imageSource1 = this.ServiceLocator.GetInstance<IImageSource>();
         // IImageSource imageSource2 = this.ServiceLocator.GetInstance<IImageSource>();
         // IImageSource imageSource3 = this.ServiceLocator.GetInstance<IImageSource>();
         // int activeImageIndex;

         // imageViewManagerController.AddImage(imageSource1);
         // activeImageIndex = imageViewManagerController.ActiveImageIndex;
         // imageViewManagerController.SetActiveImageIndex(-1);
         // imageViewManagerController.AddImage(imageSource2);
         // imageViewManagerController.AddImage(imageSource3);
         // activeImageIndex = imageViewManagerController.ActiveImageIndex;
      }

      // [Fact]
      // public void AddImage()
      //   {
      //   ImageManagerController imageManagerController = this.ServiceLocator.GetInstance<ImageManagerController>();
      //   ImageController imageController = this.Container.GetInstance<ImageController>();

      // Assert.AreEqual(-1, imageManagerController.ActiveImageIndex);

      // imageManagerController.AddImage(null);

      // Assert.AreEqual(0, imageManagerController.ActiveImageIndex);
      //   }

      // [Fact]
      // public void AddRemoveImage()
      //   {
      //   ImageManagerController imageManagerController = this.ServiceLocator.GetInstance<ImageManagerController>();

      // imageManagerController.AddImage(null);
      //   imageManagerController.AddImage(null);
      //   Assert.AreEqual(1, imageManagerController.ActiveImageIndex);

      // imageManagerController.AddImage(null);
      //   Assert.AreEqual(2, imageManagerController.ActiveImageIndex);

      // imageManagerController.RemoveActiveImage();
      //   Assert.AreEqual(1, imageManagerController.ActiveImageIndex);

      // imageManagerController.AddImage(null);
      //   Assert.AreEqual(2, imageManagerController.ActiveImageIndex);

      // imageManagerController.SetActiveImageIndex(1);
      //   Assert.AreEqual(1, imageManagerController.ActiveImageIndex);
      //   imageManagerController.RemoveActiveImage();
      //   Assert.AreEqual(1, imageManagerController.ActiveImageIndex);

      // imageManagerController.RemoveActiveImage();
      //   Assert.AreEqual(0, imageManagerController.ActiveImageIndex);

      // imageManagerController.RemoveActiveImage();
      //   Assert.AreEqual(-1, imageManagerController.ActiveImageIndex);
      //   }

      // [Fact]
      // public void RemoveAllImages()
      //   {
      //   ImageManagerController imageManagerController = this.ServiceLocator.GetInstance<ImageManagerController>();

      // imageManagerController.AddImage(null);
      //   imageManagerController.AddImage(null);

      // imageManagerController.RemoveAllImages();

      // Assert.AreEqual(0, imageManagerController.ImageCount);
      //   }

      ////[Fact]
      ////public void GetActiveImage()
      ////   {
      ////   ImageManagerController imageViewManagerController = this.ServiceLocator.GetInstance<ImageManagerController>();
      ////   ImageController imageController = this.Container.GetInstance<ImageController>();

      ////   Assert.IsNull(imageViewManagerController.GetActiveImage());

      ////   imageViewManagerController.AddImage(imageController);

      ////   Assert.AreSame(imageController, imageViewManagerController.GetActiveImage());

      ////   imageController.Close();

      ////   Assert.IsNull(imageViewManagerController.GetActiveImage());
      ////   }
   }
}
