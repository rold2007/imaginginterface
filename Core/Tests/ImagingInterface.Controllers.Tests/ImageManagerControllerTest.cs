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
   using ImagingInterface.Models;
   using ImagingInterface.Views;
   using ImagingInterface.Views.EventArguments;
   using Microsoft.Practices.ServiceLocation;
   using NUnit.Framework;

   [TestFixture]
   public class ImageManagerControllerTest : ControllersBaseTest
      {
      [Test]
      public void Constructor()
         {
         IImageManagerController imageViewManagerController = this.ServiceLocator.GetInstance<IImageManagerController>();

         Assert.IsNotNull(imageViewManagerController);
         }

      [Test]
      public void AddImageController()
         {
         IImageManagerView imageManagerView = this.ServiceLocator.GetInstance<IImageManagerView>();
         IImageManagerController imageViewManagerController = this.ServiceLocator.GetInstance<IImageManagerController>();
         IImageController imageController = this.Container.GetInstance<IImageController>();

         Assert.IsNull(imageManagerView.GetActiveImageView());
         Assert.IsNull(imageViewManagerController.GetActiveImageController());

         imageViewManagerController.AddImageController(imageController);

         Assert.AreSame(imageController.ImageView, imageManagerView.GetActiveImageView());
         Assert.AreSame(imageController, imageViewManagerController.GetActiveImageController());
         }

      [Test]
      public void GetActiveImageController()
         {
         IImageManagerController imageViewManagerController = this.ServiceLocator.GetInstance<IImageManagerController>();
         IImageController imageController = this.Container.GetInstance<IImageController>();

         Assert.IsNull(imageViewManagerController.GetActiveImageController());

         imageViewManagerController.AddImageController(imageController);

         Assert.AreSame(imageController, imageViewManagerController.GetActiveImageController());

         imageViewManagerController.RemoveImageController(imageController);

         Assert.IsNull(imageViewManagerController.GetActiveImageController());
         }

      [Test]
      public void RemoveImageController()
         {
         IImageManagerView imageManagerView = this.ServiceLocator.GetInstance<IImageManagerView>();
         IImageManagerController imageViewManagerController = this.ServiceLocator.GetInstance<IImageManagerController>();
         IImageController imageController = this.Container.GetInstance<IImageController>();

         imageViewManagerController.AddImageController(imageController);

         Assert.AreSame(imageController.ImageView, imageManagerView.GetActiveImageView());
         Assert.AreSame(imageController, imageViewManagerController.GetActiveImageController());

         imageViewManagerController.RemoveImageController(imageController);

         Assert.IsNull(imageManagerView.GetActiveImageView());
         Assert.IsNull(imageViewManagerController.GetActiveImageController());
         }
      }
   }
