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
   using ImagingInterface.Controllers.Tests.Views;
   using ImagingInterface.Models;
   using ImagingInterface.Tests.Common.Views;
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
      public void AddImage()
         {
         this.Container.RegisterSingle<IImageView, ImageView>();
         this.Container.RegisterSingle<IImageModel, ImageModel>();

         IImageManagerView imageManagerView = this.ServiceLocator.GetInstance<IImageManagerView>();
         IImageManagerController imageViewManagerController = this.ServiceLocator.GetInstance<IImageManagerController>();
         IImageView imageView = this.Container.GetInstance<IImageView>();
         IImageController imageController = this.Container.GetInstance<IImageController>();
         IImageModel imageModel = this.Container.GetInstance<IImageModel>();

         Assert.IsNull(imageManagerView.GetActiveImageView());
         Assert.IsNull(imageViewManagerController.GetActiveImage());

         imageViewManagerController.AddImage(imageController);

         Assert.AreSame(imageView, imageManagerView.GetActiveImageView());
         Assert.AreSame(imageController, imageViewManagerController.GetActiveImage());
         }

      [Test]
      public void GetActiveImage()
         {
         this.Container.RegisterSingle<IImageView, ImageView>();
         this.Container.RegisterSingle<IImageModel, ImageModel>();

         IImageManagerController imageViewManagerController = this.ServiceLocator.GetInstance<IImageManagerController>();
         IImageController imageController = this.Container.GetInstance<IImageController>();

         Assert.IsNull(imageViewManagerController.GetActiveImage());

         imageViewManagerController.AddImage(imageController);

         Assert.AreSame(imageController, imageViewManagerController.GetActiveImage());

         imageController.Close();

         Assert.IsNull(imageViewManagerController.GetActiveImage());
         }

      [Test]
      public void RemoveImageController()
         {
         this.Container.RegisterSingle<IImageView, ImageView>();
         this.Container.RegisterSingle<IImageModel, ImageModel>();

         IImageManagerView imageManagerView = this.ServiceLocator.GetInstance<IImageManagerView>();
         IImageManagerController imageViewManagerController = this.ServiceLocator.GetInstance<IImageManagerController>();
         IImageView imageView = this.Container.GetInstance<IImageView>();
         IImageController imageController = this.Container.GetInstance<IImageController>();

         imageViewManagerController.AddImage(imageController);

         Assert.AreSame(imageView, imageManagerView.GetActiveImageView());
         Assert.AreSame(imageController, imageViewManagerController.GetActiveImage());

         imageController.Close();

         Assert.IsNull(imageManagerView.GetActiveImageView());
         Assert.IsNull(imageViewManagerController.GetActiveImage());
         }
      }
   }
