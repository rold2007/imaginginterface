namespace ImagingInterface.Controllers.Tests
   {
   using System;
   using System.Collections.Generic;
   using System.IO;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using Emgu.CV;
   using Emgu.CV.Structure;
   using ImagingInterface.Controllers.EventArguments;
   using ImagingInterface.Models;
   using ImagingInterface.Plugins;
   using Microsoft.Practices.ServiceLocation;
   using NUnit.Framework;

   [TestFixture]
   public class ImageManagerControllerTest : ControllersBaseTest
      {
      [Test]
      public void Constructor()
         {
         ImageManagerController imageViewManagerController = this.ServiceLocator.GetInstance<ImageManagerController>();

         Assert.IsNotNull(imageViewManagerController);
         }

      //[Test]
      //public void AddImage()
      //   {
      //   ImageManagerController imageManagerController = this.ServiceLocator.GetInstance<ImageManagerController>();
      //   ImageController imageController = this.Container.GetInstance<ImageController>();

      //   Assert.AreEqual(-1, imageManagerController.ActiveImageIndex);

      //   imageManagerController.AddImage(null);

      //   Assert.AreEqual(0, imageManagerController.ActiveImageIndex);
      //   }

      //[Test]
      //public void AddRemoveImage()
      //   {
      //   ImageManagerController imageManagerController = this.ServiceLocator.GetInstance<ImageManagerController>();

      //   imageManagerController.AddImage(null);
      //   imageManagerController.AddImage(null);
      //   Assert.AreEqual(1, imageManagerController.ActiveImageIndex);

      //   imageManagerController.AddImage(null);
      //   Assert.AreEqual(2, imageManagerController.ActiveImageIndex);

      //   imageManagerController.RemoveActiveImage();
      //   Assert.AreEqual(1, imageManagerController.ActiveImageIndex);

      //   imageManagerController.AddImage(null);
      //   Assert.AreEqual(2, imageManagerController.ActiveImageIndex);

      //   imageManagerController.SetActiveImageIndex(1);
      //   Assert.AreEqual(1, imageManagerController.ActiveImageIndex);
      //   imageManagerController.RemoveActiveImage();
      //   Assert.AreEqual(1, imageManagerController.ActiveImageIndex);

      //   imageManagerController.RemoveActiveImage();
      //   Assert.AreEqual(0, imageManagerController.ActiveImageIndex);

      //   imageManagerController.RemoveActiveImage();
      //   Assert.AreEqual(-1, imageManagerController.ActiveImageIndex);
      //   }

      //[Test]
      //public void RemoveAllImages()
      //   {
      //   ImageManagerController imageManagerController = this.ServiceLocator.GetInstance<ImageManagerController>();

      //   imageManagerController.AddImage(null);
      //   imageManagerController.AddImage(null);

      //   imageManagerController.RemoveAllImages();

      //   Assert.AreEqual(0, imageManagerController.ImageCount);
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
