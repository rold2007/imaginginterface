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
   public class ImageControllerTests : ControllersBaseTests
      {
      private ImageViewManager imageViewManager;
      private ImageViewManagerController imageViewManagerController;

      [SetUp]
      public void SetUp()
         {
         this.imageViewManager = new ImageViewManager();
         this.imageViewManagerController = new ImageViewManagerController(this.imageViewManager);

         this.Container.RegisterSingle<IImageViewManager>(this.imageViewManager);
         this.Container.RegisterSingle<IImageViewManagerController>(this.imageViewManagerController);
         }

      [Test]
      public void Constructor()
         {
         ImageView imageView = new ImageView();
         ImageModel imageModel = new ImageModel();
         ImageController imageController = new ImageController(imageView, imageModel);

         Assert.AreSame(imageView, imageController.ImageView);
         Assert.AreSame(imageModel, imageController.ImageModel);
         }

      [Test]
      public void LoadFile()
         {
         ImageView imageView = new ImageView();
         ImageModel imageModel = new ImageModel();
         string tempFileName = string.Empty;

         try
            {
            tempFileName = Path.GetRandomFileName() + ".png";

            using (Image<Gray, byte> image = new Image<Gray, byte>(1, 1))
               {
               image.Save(tempFileName);

               Assert.IsNullOrEmpty(imageModel.DisplayName);
               Assert.IsNull(imageModel.Image);

               using (ImageController imageController = new ImageController(imageView, imageModel))
                  {
                  bool loadResult = imageController.LoadFile(tempFileName);

                  Assert.IsTrue(loadResult);
                  Assert.IsNotNullOrEmpty(imageModel.DisplayName);
                  Assert.IsNotNull(imageModel.Image);
                  Assert.AreSame(imageModel, imageView.AssignedImageModel);
                  }

               Assert.IsNull(imageModel.Image);
               }
            }
         finally
            {
            if (!string.IsNullOrEmpty(tempFileName))
               {
               File.Delete(tempFileName);
               }
            }
         }

      [Test]
      public void LoadFileInvalid()
         {
         ImageView imageView = new ImageView();
         ImageModel imageModel = new ImageModel();
         string tempFileName = string.Empty;

         try
            {
            tempFileName = Path.GetTempFileName();

            Assert.IsNullOrEmpty(imageModel.DisplayName);
            Assert.IsNull(imageModel.Image);

            using (ImageController imageController = new ImageController(imageView, imageModel))
               {
               bool loadResult = imageController.LoadFile(tempFileName);

               Assert.IsFalse(loadResult);
               Assert.IsNullOrEmpty(imageModel.DisplayName);
               Assert.IsNull(imageModel.Image);
               Assert.IsNull(imageView.AssignedImageModel);
               }
            }
         finally
            {
            if (!string.IsNullOrEmpty(tempFileName))
               {
               File.Delete(tempFileName);
               }
            }
         }

      [Test]
      public void Add()
         {
         ImageView imageView = new ImageView();
         ImageModel imageModel = new ImageModel();

         using (ImageController imageController = new ImageController(imageView, imageModel))
            {
            Assert.IsNull(this.imageViewManagerController.GetActiveImageController());

            imageController.Add();

            Assert.AreSame(imageController, this.imageViewManagerController.GetActiveImageController());
            }
         }

      [Test]
      public void Remove()
         {
         ImageView imageView = new ImageView();
         ImageModel imageModel = new ImageModel();

         using (ImageController imageController = new ImageController(imageView, imageModel))
            {
            // Make sure we can call Close() right away without crashing
            imageController.Remove();

            imageController.Add();

            imageController.Remove();

            Assert.IsNull(this.imageViewManagerController.GetActiveImageController());
            }
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

      private class ImageViewManager : IImageViewManager
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
