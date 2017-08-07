// <copyright file="ImageManagerControllerTest.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.Tests
{
   using ImagingInterface.Controllers.Services;
   using ImagingInterface.Controllers.Tests.Mocks;
   using NUnit.Framework;

   [TestFixture]
   public class ImageManagerControllerTest : ControllersBaseTest
   {
      [Test]
      public void Constructor()
      {
         FileSourceFactory fileSourceFactory = new FileSourceFactory();
         ImageSourceService imageSourceService = new ImageSourceService(fileSourceFactory);
         ImageManagerService imageManagerService = new ImageManagerService(imageSourceService);
         ImageManagerController imageViewManagerController = new ImageManagerController(imageManagerService);
      }

      [Test]
      public void Constructor2()
      {
         FileSourceFactory fileSourceFactory = new FileSourceFactory();
         ImageSourceService imageSourceService = new ImageSourceService(fileSourceFactory);
         ImageManagerService imageManagerService = new ImageManagerService(imageSourceService);
         ImageManagerController imageViewManagerController = new ImageManagerController(imageManagerService);

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

      // [Test]
      // public void AddImage()
      //   {
      //   ImageManagerController imageManagerController = this.ServiceLocator.GetInstance<ImageManagerController>();
      //   ImageController imageController = this.Container.GetInstance<ImageController>();

      // Assert.AreEqual(-1, imageManagerController.ActiveImageIndex);

      // imageManagerController.AddImage(null);

      // Assert.AreEqual(0, imageManagerController.ActiveImageIndex);
      //   }

      // [Test]
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

      // [Test]
      // public void RemoveAllImages()
      //   {
      //   ImageManagerController imageManagerController = this.ServiceLocator.GetInstance<ImageManagerController>();

      // imageManagerController.AddImage(null);
      //   imageManagerController.AddImage(null);

      // imageManagerController.RemoveAllImages();

      // Assert.AreEqual(0, imageManagerController.ImageCount);
      //   }

      ////[Test]
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
