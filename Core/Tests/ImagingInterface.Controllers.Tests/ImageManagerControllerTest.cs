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

      [Test]
      public void AddImage()
         {
         ////this.Container.RegisterSingleton<IImageView, ImageView>();
         this.Container.RegisterSingleton<IImageModel, ImageModel>();

         ////IImageManagerView imageManagerView = this.ServiceLocator.GetInstance<IImageManagerView>();
         ImageManagerController imageViewManagerController = this.ServiceLocator.GetInstance<ImageManagerController>();
         ////IImageView imageView = this.Container.GetInstance<IImageView>();
         ImageController imageController = this.Container.GetInstance<ImageController>();
         IImageModel imageModel = this.Container.GetInstance<IImageModel>();

         ////Assert.IsNull(imageManagerView.GetActiveImageView());
         Assert.IsNull(imageViewManagerController.GetActiveImage());

         imageViewManagerController.AddImage(imageController);

         ////Assert.AreSame(imageView, imageManagerView.GetActiveImageView());
         Assert.AreSame(imageController, imageViewManagerController.GetActiveImage());
         }

      [Test]
      public void GetActiveImage()
         {
         ////this.Container.RegisterSingleton<IImageView, ImageView>();
         this.Container.RegisterSingleton<IImageModel, ImageModel>();

         ImageManagerController imageViewManagerController = this.ServiceLocator.GetInstance<ImageManagerController>();
         ImageController imageController = this.Container.GetInstance<ImageController>();

         Assert.IsNull(imageViewManagerController.GetActiveImage());

         imageViewManagerController.AddImage(imageController);

         Assert.AreSame(imageController, imageViewManagerController.GetActiveImage());

         imageController.Close();

         Assert.IsNull(imageViewManagerController.GetActiveImage());
         }

      [Test]
      public void RemoveImageController()
         {
         ////this.Container.RegisterSingleton<IImageView, ImageView>();
         this.Container.RegisterSingleton<IImageModel, ImageModel>();

         ////IImageManagerView imageManagerView = this.ServiceLocator.GetInstance<IImageManagerView>();
         ImageManagerController imageViewManagerController = this.ServiceLocator.GetInstance<ImageManagerController>();
         ////IImageView imageView = this.Container.GetInstance<IImageView>();
         ImageController imageController = this.Container.GetInstance<ImageController>();

         imageViewManagerController.AddImage(imageController);

         ////Assert.AreSame(imageView, imageManagerView.GetActiveImageView());
         Assert.AreSame(imageController, imageViewManagerController.GetActiveImage());

         imageController.Close();

         ////Assert.IsNull(imageManagerView.GetActiveImageView());
         Assert.IsNull(imageViewManagerController.GetActiveImage());
         }
      }
   }
