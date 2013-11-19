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
   using NUnit.Framework;

   [TestFixture]
   public class ImageControllerTest : ControllersBaseTest
      {
      private ImageViewManager imageViewManager;
      private ImageManagerController imageViewManagerController;

      [SetUp]
      public void SetUp()
         {
         this.imageViewManager = new ImageViewManager();
         this.imageViewManagerController = new ImageManagerController(this.imageViewManager);

         this.Container.RegisterSingle<IImageManagerView>(this.imageViewManager);
         this.Container.RegisterSingle<IImageManagerController>(this.imageViewManagerController);
         }

      [Test]
      public void Constructor()
         {
         ImageView imageView = new ImageView();
         ImageModel imageModel = new ImageModel();
         ImageController imageController = new ImageController(imageView, imageModel, this.ServiceLocator);

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

               using (ImageController imageController = new ImageController(imageView, imageModel, this.ServiceLocator))
                  {
                  bool loadResult = imageController.LoadImage(tempFileName);

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

            using (ImageController imageController = new ImageController(imageView, imageModel, this.ServiceLocator))
               {
               bool loadResult = imageController.LoadImage(tempFileName);

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

         using (ImageController imageController = new ImageController(imageView, imageModel, this.ServiceLocator))
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

         using (ImageController imageController = new ImageController(imageView, imageModel, this.ServiceLocator))
            {
            // Make sure we can call Close() right away without crashing
            imageController.Remove();

            imageController.Add();

            imageController.Remove();

            Assert.IsNull(this.imageViewManagerController.GetActiveImageController());
            }
         }
      }
   }
