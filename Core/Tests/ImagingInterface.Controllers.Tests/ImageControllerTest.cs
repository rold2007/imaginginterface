// <copyright file="ImageControllerTest.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.Tests
{
   using ImagingInterface.Controllers.Services;
   using Xunit;

   public class ImageControllerTest : ControllersBaseTest
   {
      public ImageControllerTest()
      {
         this.Container.Register<PluginManagerService>();
         this.Container.Register<ImageService>();
         this.Container.Register<ImageController>();
      }

      [Fact]
      public void Constructor()
      {
         ImageController imageController = this.Container.GetInstance<ImageController>();
      }

      [Fact]
      public void InitializeImageSourceController()
      {
         ImageController imageController = this.Container.GetInstance<ImageController>();

         // FileSourceController fileSourceController = this.ServiceLocator.GetInstance<FileSourceController>();

         // imageController.InitializeImageSourceController(fileSourceController);
      }

      [Fact]
      public void Close()
      {
         ImageController imageController = this.Container.GetInstance<ImageController>();

         imageController.Close();
      }

      [Fact]
      public void FullPath()
      {
         ////ImageController imageController = this.ServiceLocator.GetInstance<ImageController>();

         ////Assert.IsNull(imageController.DisplayName);
      }

      /*
      [Fact]
      public void LoadFile()
         {
         ////this.Container.RegisterSingleton<IImageView, ImageView>();
         this.Container.RegisterSingleton<IImageModel, ImageModel>();

         ////ImageView imageView = this.ServiceLocator.GetInstance<IImageView>() as ImageView;
         IImageModel imageModel = this.ServiceLocator.GetInstance<IImageModel>();
         string tempFileName = string.Empty;

         try
            {
            tempFileName = Path.GetRandomFileName() + ".png";

            using (UMat image = new UMat(1, 1, DepthType.Cv8U, 1))
               {
               image.Save(tempFileName);

               Assert.That(imageModel.DisplayName, Is.Null.Or.Empty);
               Assert.IsNull(imageModel.DisplayImageData);

               ImageController imageController = this.Container.GetInstance<ImageController>();
               IFileSourceController fileSourceController = this.Container.GetInstance<IFileSourceController>();

               fileSourceController.Filename = tempFileName;
               imageController.SetDisplayName(tempFileName);

               using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
                  {
                  imageController.InitializeImageSourceController(fileSourceController, fileSourceController.RawPluginModel);

                  imageControllerWrapper.WaitForDisplayUpdate();

                  Assert.That(imageModel.DisplayName, Is.Not.Null.Or.Empty);
                  Assert.IsNotNull(imageModel.DisplayImageData);
                  ////Assert.AreSame(imageModel, imageView.AssignedImageModel);

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

      [Fact]
      public void LoadFileInvalid()
         {
         ////ImageView imageView = new ImageView();
         ImageModel imageModel = new ImageModel();
         string tempFileName = string.Empty;

         try
            {
            tempFileName = Path.GetTempFileName();

            Assert.That(imageModel.DisplayName, Is.Null.Or.Empty);
            Assert.IsNull(imageModel.DisplayImageData);

            ImageController imageController = this.Container.GetInstance<ImageController>();
            IFileSourceController fileSourceController = this.Container.GetInstance<IFileSourceController>();

            fileSourceController.Filename = tempFileName;

            imageController.InitializeImageSourceController(fileSourceController, fileSourceController.RawPluginModel);

            Assert.That(imageModel.DisplayName, Is.Null.Or.Empty);
            Assert.IsNull(imageModel.DisplayImageData);
            ////Assert.IsNull(imageView.AssignedImageModel);
            }
         finally
            {
            if (!string.IsNullOrEmpty(tempFileName))
               {
               File.Delete(tempFileName);
               }
            }
         }

      [Fact]
      public void StartLiveUpdate()
         {
         ////this.Container.RegisterSingleton<IImageView, ImageView>();
         this.Container.RegisterSingleton<IImageModel, ImageModel>();

         ////ImageView imageView = this.ServiceLocator.GetInstance<IImageView>() as ImageView;
         IImageModel imageModel = this.ServiceLocator.GetInstance<IImageModel>();
         ImageSourceController imageSourceController = this.Container.GetInstance<IImageSourceController>() as ImageSourceController;
         ImageController imageController = this.Container.GetInstance<ImageController>();

         ////Assert.IsNotNull(imageView.AssignedImageModel);

         using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
            {
            imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

            imageControllerWrapper.WaitForDisplayUpdate();
            }

         ////Assert.IsNotNull(imageView.AssignedImageModel);
         ////Assert.IsNotNull(imageView.AssignedImageModel.DisplayImageData);
         }

      [Fact]
      public void IsGrayscale()
         {
         this.Container.RegisterSingleton<IImageModel, ImageModel>();
         ////this.Container.RegisterSingleton<IImageView, ImageView>();
         this.Container.RegisterSingleton<IImageSourceController, ImageSourceController>();

         ImageSourceController imageSourceController = this.Container.GetInstance<IImageSourceController>() as ImageSourceController;
         ImageController imageController = this.Container.GetInstance<ImageController>();
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

      [Fact]
      public void CloseWhileStartLiveUpdate()
         {
         ////this.Container.RegisterSingleton<IImageView, ImageView>();
         this.Container.RegisterSingleton<IImageModel, ImageModel>();

         ////ImageView imageView = this.ServiceLocator.GetInstance<IImageView>() as ImageView;
         IImageModel imageModel = this.ServiceLocator.GetInstance<IImageModel>();
         ImageSourceController imageSourceController = this.Container.GetInstance<IImageSourceController>() as ImageSourceController;
         ImageController imageController = this.Container.GetInstance<ImageController>();

         ////Assert.IsNotNull(imageView.AssignedImageModel);

         using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
            {
            imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

            imageControllerWrapper.WaitForDisplayUpdate();

            ////Assert.IsNotNull(imageView.AssignedImageModel);
            ////Assert.IsNotNull(imageView.AssignedImageModel.DisplayImageData);

            imageController.Closing += this.ImageController_Closing;

            imageController.Close();

            imageController.Closing -= this.ImageController_Closing;

            imageController.Close();

            imageControllerWrapper.WaitForClosed();
            }
         }

      [Fact]
      public void UpdatePeriod()
         {
         ////this.Container.RegisterSingleton<IImageView, ImageView>();
         this.Container.RegisterSingleton<IImageModel, ImageModel>();

         ////ImageView imageView = this.ServiceLocator.GetInstance<IImageView>() as ImageView;

         ////imageView.UpdateFrequency = 30;

         ImageController imageController = this.ServiceLocator.GetInstance<ImageController>();
         ImageSourceController imageSourceController = this.Container.GetInstance<IImageSourceController>() as ImageSourceController;

         using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
            {
            imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

            imageControllerWrapper.WaitForDisplayUpdate();
            }

         ////Thread.Sleep(Convert.ToInt32(2 * 1000 / imageView.UpdateFrequency));

         using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
            {
            imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

            imageControllerWrapper.WaitForDisplayUpdate();
            }

         imageSourceController.ImageData = new byte[1, 1, 1];
         ////imageView.UpdateFrequency = double.Epsilon;
         imageController = this.ServiceLocator.GetInstance<ImageController>();

         // Let the UpdateDisplayImageData() skip a frame
         using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
            {
            imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);
            imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

            imageControllerWrapper.WaitForDisplayUpdate();
            }
         }

      [Fact]
      public void ImageModelSize()
         {
         IImageModel imageModel = this.ServiceLocator.GetInstance<IImageModel>();

         imageModel.DisplayImageData = new byte[42, 54, 1];

         Assert.AreEqual(42, imageModel.Size.Height);
         Assert.AreEqual(54, imageModel.Size.Width);
         }

      [Fact]
      public void ZoomLevel()
         {
         ////this.Container.RegisterSingleton<IImageView, ImageView>();
         this.Container.RegisterSingleton<IImageModel, ImageModel>();

         ////ImageView imageView = this.ServiceLocator.GetInstance<IImageView>() as ImageView;
         ImageController imageController = this.ServiceLocator.GetInstance<ImageController>();
         IImageModel imageModel = this.ServiceLocator.GetInstance<IImageModel>();

         Assert.AreEqual(1, imageModel.ZoomLevel);

         ////imageView.TriggerZoomLevelIncreased();

         Assert.AreEqual(2.0, imageModel.ZoomLevel);

         ////imageView.TriggerZoomLevelDecreased();

         Assert.AreEqual(1.0, imageModel.ZoomLevel);

         ////imageView.TriggerZoomLevelDecreased();

         Assert.AreEqual(0.5, imageModel.ZoomLevel);
         }

      [Fact]
      public void PixelView()
         {
         ////this.Container.RegisterSingleton<IImageView, ImageView>();
         this.Container.RegisterSingleton<IImageModel, ImageModel>();

         ////ImageView imageView = this.ServiceLocator.GetInstance<IImageView>() as ImageView;
         ImageController imageController = this.ServiceLocator.GetInstance<ImageController>();
         IImageModel imageModel = this.ServiceLocator.GetInstance<IImageModel>();
         Point pixelPosition = new Point();

         imageModel.DisplayImageData = new byte[2, 2, 1];

         imageModel.DisplayImageData[1, 1, 0] = 255;

         ////imageView.TriggerPixelViewChanged(pixelPosition);

         ////Assert.AreEqual(pixelPosition, imageView.PixelPosition);
         ////Assert.AreEqual(0, imageView.Gray);
         ////Assert.IsNull(imageView.RGB);
         ////Assert.IsNull(imageView.HSV);

         pixelPosition = new Point(1, 1);

         ////imageView.TriggerPixelViewChanged(pixelPosition);

         ////Assert.AreEqual(pixelPosition, imageView.PixelPosition);
         ////Assert.AreEqual(255, imageView.Gray);
         ////Assert.IsNull(imageView.RGB);
         ////Assert.IsNull(imageView.HSV);

         imageModel.DisplayImageData = new byte[3, 3, 3];

         imageModel.DisplayImageData[0, 1, 0] = 0;
         imageModel.DisplayImageData[0, 1, 1] = 0;
         imageModel.DisplayImageData[0, 1, 2] = 255;
         imageModel.DisplayImageData[1, 0, 0] = 0;
         imageModel.DisplayImageData[1, 0, 1] = 255;
         imageModel.DisplayImageData[1, 0, 2] = 0;
         imageModel.DisplayImageData[1, 1, 0] = 255;
         imageModel.DisplayImageData[1, 1, 1] = 255;
         imageModel.DisplayImageData[1, 1, 2] = 255;
         imageModel.DisplayImageData[0, 2, 0] = 255;
         imageModel.DisplayImageData[0, 2, 1] = 0;
         imageModel.DisplayImageData[0, 2, 2] = 0;
         imageModel.DisplayImageData[1, 2, 0] = 255;
         imageModel.DisplayImageData[1, 2, 1] = 64;
         imageModel.DisplayImageData[1, 2, 2] = 128;

         pixelPosition = new Point(0, 0);

         ////imageView.TriggerPixelViewChanged(pixelPosition);

         ////Assert.AreEqual(pixelPosition, imageView.PixelPosition);
         ////Assert.AreEqual(0, imageView.Gray);
         ////Assert.AreEqual(0, imageView.RGB[0]);
         ////Assert.AreEqual(0, imageView.RGB[1]);
         ////Assert.AreEqual(0, imageView.RGB[2]);
         ////Assert.AreEqual(0.0, imageView.HSV[0]);
         ////Assert.AreEqual(0.0, imageView.HSV[1]);
         ////Assert.AreEqual(0.0, imageView.HSV[2]);

         pixelPosition = new Point(1, 0);

         ////imageView.TriggerPixelViewChanged(pixelPosition);

         ////Assert.AreEqual(pixelPosition, imageView.PixelPosition);
         ////Assert.AreEqual(0, imageView.Gray);
         ////Assert.AreEqual(0, imageView.RGB[0]);
         ////Assert.AreEqual(0, imageView.RGB[1]);
         ////Assert.AreEqual(255, imageView.RGB[2]);
         ////Assert.AreEqual(240.0, imageView.HSV[0]);
         ////Assert.AreEqual(1.0, imageView.HSV[1]);
         ////Assert.AreEqual(255.0, imageView.HSV[2]);

         pixelPosition = new Point(0, 1);

         ////imageView.TriggerPixelViewChanged(pixelPosition);

         ////Assert.AreEqual(pixelPosition, imageView.PixelPosition);
         ////Assert.AreEqual(0, imageView.Gray);
         ////Assert.AreEqual(0, imageView.RGB[0]);
         ////Assert.AreEqual(255, imageView.RGB[1]);
         ////Assert.AreEqual(0, imageView.RGB[2]);
         ////Assert.AreEqual(120.0, imageView.HSV[0]);
         ////Assert.AreEqual(1.0, imageView.HSV[1]);
         ////Assert.AreEqual(255.0, imageView.HSV[2]);

         pixelPosition = new Point(1, 1);

         ////imageView.TriggerPixelViewChanged(pixelPosition);

         ////Assert.AreEqual(pixelPosition, imageView.PixelPosition);
         ////Assert.AreEqual(0, imageView.Gray);
         ////Assert.AreEqual(255, imageView.RGB[0]);
         ////Assert.AreEqual(255, imageView.RGB[1]);
         ////Assert.AreEqual(255, imageView.RGB[2]);
         ////Assert.AreEqual(0.0, imageView.HSV[0]);
         ////Assert.AreEqual(0.0, imageView.HSV[1]);
         ////Assert.AreEqual(255.0, imageView.HSV[2]);

         pixelPosition = new Point(2, 0);

         ////imageView.TriggerPixelViewChanged(pixelPosition);

         ////Assert.AreEqual(pixelPosition, imageView.PixelPosition);
         ////Assert.AreEqual(0, imageView.Gray);
         ////Assert.AreEqual(255, imageView.RGB[0]);
         ////Assert.AreEqual(0, imageView.RGB[1]);
         ////Assert.AreEqual(0, imageView.RGB[2]);
         ////Assert.AreEqual(0.0, imageView.HSV[0]);
         ////Assert.AreEqual(1.0, imageView.HSV[1]);
         ////Assert.AreEqual(255.0, imageView.HSV[2]);

         pixelPosition = new Point(2, 1);

         ////imageView.TriggerPixelViewChanged(pixelPosition);

         ////Assert.AreEqual(pixelPosition, imageView.PixelPosition);
         ////Assert.AreEqual(0, imageView.Gray);
         ////Assert.AreEqual(255, imageView.RGB[0]);
         ////Assert.AreEqual(64, imageView.RGB[1]);
         ////Assert.AreEqual(128, imageView.RGB[2]);
         ////Assert.AreEqual(340.0, imageView.HSV[0]);
         ////Assert.AreEqual(0.749, imageView.HSV[1], 0.01);
         ////Assert.AreEqual(255.0, imageView.HSV[2]);
         }

      private void ImageController_Closing(object sender, CancelEventArgs e)
         {
         e.Cancel = true;
         }
      */
   }
}
