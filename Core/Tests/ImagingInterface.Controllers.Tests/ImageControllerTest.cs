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
      [Test]
      public void Constructor()
         {
         this.Container.RegisterSingle<IImageView, ImageView>();
         this.Container.RegisterSingle<IImageModel, ImageModel>();

         IImageController imageController = this.ServiceLocator.GetInstance<IImageController>();

         Assert.AreSame(this.ServiceLocator.GetInstance<IImageView>(), imageController.ImageView);
         Assert.AreSame(this.ServiceLocator.GetInstance<IImageModel>(), imageController.ImageModel);

         imageController = null;

         // Force wait on ImageController finalizer. This may not always work.
         // It was only added for code coverage purpose, skip it if it becomes annoying
         GC.Collect();
         GC.WaitForPendingFinalizers();
         }

      [Test]
      public void LoadFile()
         {
         this.Container.RegisterSingle<IImageView, ImageView>();
         this.Container.RegisterSingle<IImageModel, ImageModel>();

         ImageView imageView = this.ServiceLocator.GetInstance<IImageView>() as ImageView;
         IImageModel imageModel = this.ServiceLocator.GetInstance<IImageModel>();
         string tempFileName = string.Empty;

         try
            {
            tempFileName = Path.GetRandomFileName() + ".png";

            using (Image<Gray, byte> image = new Image<Gray, byte>(1, 1))
               {
               image.Save(tempFileName);

               Assert.IsNullOrEmpty(imageModel.DisplayName);
               Assert.IsNull(imageModel.Image);

               using (ImageController imageController = this.Container.GetInstance<ImageController>())
                  {
                  bool loadResult = imageController.LoadImage(tempFileName);

                  Assert.IsTrue(loadResult);
                  Assert.IsNotNullOrEmpty(imageModel.DisplayName);
                  Assert.IsNotNull(imageModel.Image);
                  Assert.AreSame(imageModel, imageView.AssignedImageModel);
                  }

               // imageModel.Image is set to null when imageController is disposed of.
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

            IImageController imageController = this.Container.GetInstance<IImageController>();

            bool loadResult = imageController.LoadImage(tempFileName);

            Assert.IsFalse(loadResult);
            Assert.IsNullOrEmpty(imageModel.DisplayName);
            Assert.IsNull(imageModel.Image);
            Assert.IsNull(imageView.AssignedImageModel);
            }
         finally
            {
            if (!string.IsNullOrEmpty(tempFileName))
               {
               File.Delete(tempFileName);
               }
            }
         }
      }
   }
