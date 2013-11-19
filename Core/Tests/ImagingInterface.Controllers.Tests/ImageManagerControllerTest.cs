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
         ImageViewManager imageViewManager = new ImageViewManager();
         ImageManagerController imageViewManagerController = new ImageManagerController(imageViewManager);
         }

      [Test]
      public void AddImageController()
         {
         ImageViewManager imageViewManager = new ImageViewManager();
         ImageManagerController imageViewManagerController = new ImageManagerController(imageViewManager);
         IImageController imageController = this.Container.GetInstance<IImageController>();

         Assert.IsNull(imageViewManager.GetActiveImageView());
         Assert.IsNull(imageViewManagerController.GetActiveImageController());

         imageViewManagerController.AddImageController(imageController);

         Assert.AreSame(imageController.ImageView, imageViewManager.GetActiveImageView());
         Assert.AreSame(imageController, imageViewManagerController.GetActiveImageController());
         }

      [Test]
      public void GetActiveImageController()
         {
         ImageViewManager imageViewManager = new ImageViewManager();
         ImageManagerController imageViewManagerController = new ImageManagerController(imageViewManager);
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
         ImageViewManager imageViewManager = new ImageViewManager();
         ImageManagerController imageViewManagerController = new ImageManagerController(imageViewManager);
         IImageController imageController = this.Container.GetInstance<IImageController>();

         imageViewManagerController.AddImageController(imageController);

         Assert.AreSame(imageController.ImageView, imageViewManager.GetActiveImageView());
         Assert.AreSame(imageController, imageViewManagerController.GetActiveImageController());

         imageViewManagerController.RemoveImageController(imageController);

         Assert.IsNull(imageViewManager.GetActiveImageView());
         Assert.IsNull(imageViewManagerController.GetActiveImageController());
         }

      private class ImageView : IImageView
         {
         public IImageModel AssignedImageModel
            {
            get;
            private set;
            }

         public void AssignImageModel(IImageModel imageModel)
            {
            this.AssignedImageModel = imageModel;
            }

         public void Close()
            {
            }
         }

      private class ImageViewManager : IImageManagerView
         {
         private List<IImageView> allImageViews;

         public ImageViewManager()
            {
            this.allImageViews = new List<IImageView>();
            }

         public void AddImageView(IImageView imageView, IImageModel imageModel)
            {
            this.allImageViews.Add(imageView);
            }

         public IImageView GetActiveImageView()
            {
            if (this.allImageViews.Count == 0)
               {
               return null;
               }
            else
               {
               return this.allImageViews[0];
               }
            }

         public void RemoveImageView(IImageView imageView)
            {
            this.allImageViews.Remove(imageView);
            }
         }
      }
   }
