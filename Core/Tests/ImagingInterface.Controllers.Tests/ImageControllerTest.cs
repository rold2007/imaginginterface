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
         IImageController imageController = this.ServiceLocator.GetInstance<IImageController>();
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
               Assert.IsNull(imageModel.ImageData);

               ImageController imageController = this.Container.GetInstance<ImageController>();

               bool loadResult = imageController.LoadImage(tempFileName);

               Assert.IsTrue(loadResult);
               Assert.IsNotNullOrEmpty(imageModel.DisplayName);
               Assert.IsNotNull(imageModel.ImageData);
               Assert.AreSame(imageModel, imageView.AssignedImageModel);

               imageController.Close();

               // imageModel.Image is set to null when imageController is closed
               Assert.IsNull(imageModel.ImageData);
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
            Assert.IsNull(imageModel.ImageData);

            IImageController imageController = this.Container.GetInstance<IImageController>();

            bool loadResult = imageController.LoadImage(tempFileName);

            Assert.IsFalse(loadResult);
            Assert.IsNullOrEmpty(imageModel.DisplayName);
            Assert.IsNull(imageModel.ImageData);
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
