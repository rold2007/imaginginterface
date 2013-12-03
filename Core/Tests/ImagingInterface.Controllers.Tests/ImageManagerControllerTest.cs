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
         this.Container.RegisterSingle<IImageView, ImageView>();
         this.Container.RegisterSingle<IImageModel, ImageModel>();

         IImageManagerView imageManagerView = this.ServiceLocator.GetInstance<IImageManagerView>();
         IImageManagerController imageViewManagerController = this.ServiceLocator.GetInstance<IImageManagerController>();
         IImageView imageView = this.Container.GetInstance<IImageView>();
         IImageController imageController = this.Container.GetInstance<IImageController>();
         IImageModel imageModel = this.Container.GetInstance<IImageModel>();

         Assert.IsNull(imageManagerView.GetActiveImageView());
         Assert.IsNull(imageViewManagerController.GetActiveImageController());

         imageViewManagerController.AddImageController(imageController, imageView, imageModel);

         Assert.AreSame(imageView, imageManagerView.GetActiveImageView());
         Assert.AreSame(imageController, imageViewManagerController.GetActiveImageController());
         }

      [Test]
      public void GetActiveImageController()
         {
         this.Container.RegisterSingle<IImageView, ImageView>();
         this.Container.RegisterSingle<IImageModel, ImageModel>();

         IImageManagerController imageViewManagerController = this.ServiceLocator.GetInstance<IImageManagerController>();
         IImageView imageView = this.Container.GetInstance<IImageView>();
         IImageController imageController = this.Container.GetInstance<IImageController>();
         IImageModel imageModel = this.Container.GetInstance<IImageModel>();

         Assert.IsNull(imageViewManagerController.GetActiveImageController());

         imageViewManagerController.AddImageController(imageController, imageView, imageModel);

         Assert.AreSame(imageController, imageViewManagerController.GetActiveImageController());

         imageViewManagerController.RemoveImageController(imageView);

         Assert.IsNull(imageViewManagerController.GetActiveImageController());
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
         IImageModel imageModel = this.Container.GetInstance<IImageModel>();

         imageViewManagerController.AddImageController(imageController, imageView, imageModel);

         Assert.AreSame(imageView, imageManagerView.GetActiveImageView());
         Assert.AreSame(imageController, imageViewManagerController.GetActiveImageController());

         imageViewManagerController.RemoveImageController(imageView);

         Assert.IsNull(imageManagerView.GetActiveImageView());
         Assert.IsNull(imageViewManagerController.GetActiveImageController());
         }
      }
   }
