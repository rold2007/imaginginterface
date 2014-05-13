namespace ImagingInterface.Controllers.Tests
   {
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Diagnostics;
   using System.Drawing;
   using System.IO;
   using System.Linq;
   using System.Text;
   using System.Threading;
   using System.Threading.Tasks;
   using Emgu.CV;
   using Emgu.CV.Structure;
   using ImagingInterface.Controllers.EventArguments;
   using ImagingInterface.Controllers.Tests.Mocks;
   using ImagingInterface.Controllers.Tests.Views;
   using ImagingInterface.Models;
   using ImagingInterface.Plugins;
   using ImagingInterface.Tests.Common;
   using ImagingInterface.Tests.Common.Mocks;
   using ImagingInterface.Tests.Common.Views;
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
               Assert.IsNull(imageModel.DisplayImageData);

               ImageController imageController = this.Container.GetInstance<ImageController>();
               IFileSourceController fileSourceController = this.Container.GetInstance<IFileSourceController>();

               fileSourceController.Filename = tempFileName;
               imageController.SetDisplayName(tempFileName);

               using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
                  {
                  imageController.InitializeImageSourceController(fileSourceController, fileSourceController.RawPluginModel);

                  imageControllerWrapper.WaitForDisplayUpdate();

                  Assert.IsNotNullOrEmpty(imageModel.DisplayName);
                  Assert.IsNotNull(imageModel.DisplayImageData);
                  Assert.AreSame(imageModel, imageView.AssignedImageModel);

                  imageController.Close();

                  imageControllerWrapper.WaitForClosed();

                  // imageModel.DisplayImageData is set to null when imageController is closed
                  Assert.IsNull(imageModel.DisplayImageData);
                  }
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
            Assert.IsNull(imageModel.DisplayImageData);

            IImageController imageController = this.Container.GetInstance<IImageController>();
            IFileSourceController fileSourceController = this.Container.GetInstance<IFileSourceController>();

            fileSourceController.Filename = tempFileName;

            imageController.InitializeImageSourceController(fileSourceController, fileSourceController.RawPluginModel);

            Assert.IsNullOrEmpty(imageModel.DisplayName);
            Assert.IsNull(imageModel.DisplayImageData);
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

      [Test]
      public void StartLiveUpdate()
         {
         this.Container.RegisterSingle<IImageView, ImageView>();
         this.Container.RegisterSingle<IImageModel, ImageModel>();

         ImageView imageView = this.ServiceLocator.GetInstance<IImageView>() as ImageView;
         IImageModel imageModel = this.ServiceLocator.GetInstance<IImageModel>();
         ImageSourceController imageSourceController = this.Container.GetInstance<IImageSourceController>() as ImageSourceController;
         IImageController imageController = this.Container.GetInstance<IImageController>();

         Assert.IsNotNull(imageView.AssignedImageModel);

         using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
            {
            imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

            imageControllerWrapper.WaitForDisplayUpdate();
            }

         Assert.IsNotNull(imageView.AssignedImageModel);
         Assert.IsNotNull(imageView.AssignedImageModel.DisplayImageData);
         }

      [Test]
      public void IsGrayscale()
         {
         this.Container.RegisterSingle<IImageModel, ImageModel>();
         this.Container.RegisterSingle<IImageView, ImageView>();
         this.Container.RegisterSingle<IImageSourceController, ImageSourceController>();

         ImageSourceController imageSourceController = this.Container.GetInstance<IImageSourceController>() as ImageSourceController;
         IImageController imageController = this.Container.GetInstance<IImageController>();
         IImageModel imageModel = this.ServiceLocator.GetInstance<IImageModel>();

         using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
            {
            imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

            imageControllerWrapper.WaitForDisplayUpdate();

            Assert.IsTrue(imageModel.IsGrayscale);
            }

         using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
            {
            imageSourceController.ImageData = new byte[1, 1, 3];
            imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

            imageControllerWrapper.WaitForDisplayUpdate();

            Assert.IsFalse(imageModel.IsGrayscale);
            }

         using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
            {
            imageSourceController.ImageData = new byte[1, 1, 4];
            imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

            imageControllerWrapper.WaitForDisplayUpdate();

            Assert.IsFalse(imageModel.IsGrayscale);
            }
         }

      [Test]
      public void CloseWhileStartLiveUpdate()
         {
         this.Container.RegisterSingle<IImageView, ImageView>();
         this.Container.RegisterSingle<IImageModel, ImageModel>();

         ImageView imageView = this.ServiceLocator.GetInstance<IImageView>() as ImageView;
         IImageModel imageModel = this.ServiceLocator.GetInstance<IImageModel>();
         ImageSourceController imageSourceController = this.Container.GetInstance<IImageSourceController>() as ImageSourceController;
         IImageController imageController = this.Container.GetInstance<IImageController>();

         Assert.IsNotNull(imageView.AssignedImageModel);

         using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
            {
            imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

            imageControllerWrapper.WaitForDisplayUpdate();

            Assert.IsNotNull(imageView.AssignedImageModel);
            Assert.IsNotNull(imageView.AssignedImageModel.DisplayImageData);

            imageController.Closing += this.ImageController_Closing;

            imageController.Close();

            imageController.Closing -= this.ImageController_Closing;

            imageController.Close();

            imageControllerWrapper.WaitForClosed();
            }
         }

      [Test]
      public void UpdatePeriod()
         {
         this.Container.RegisterSingle<IImageView, ImageView>();
         this.Container.RegisterSingle<IImageModel, ImageModel>();

         ImageView imageView = this.ServiceLocator.GetInstance<IImageView>() as ImageView;

         imageView.UpdateFrequency = 30;

         IImageController imageController = this.ServiceLocator.GetInstance<IImageController>();
         IImageSourceController imageSourceController = this.Container.GetInstance<IImageSourceController>();

         using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
            {
            imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

            imageControllerWrapper.WaitForDisplayUpdate();
            }

         Thread.Sleep(Convert.ToInt32(2 * 1000 / imageView.UpdateFrequency));

         using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
            {
            imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

            imageControllerWrapper.WaitForDisplayUpdate();
            }
         }

      [Test]
      public void ImageModelSize()
         {
         IImageModel imageModel = this.ServiceLocator.GetInstance<IImageModel>();

         imageModel.DisplayImageData = new byte[42, 54, 1];

         Assert.AreEqual(42, imageModel.Size.Height);
         Assert.AreEqual(54, imageModel.Size.Width);
         }

      private void ImageController_Closing(object sender, CancelEventArgs e)
         {
         e.Cancel = true;
         }
      }
   }
