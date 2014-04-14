namespace ImagingInterface.Controllers.Tests
   {
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Diagnostics;
   using System.IO;
   using System.Linq;
   using System.Text;
   using System.Threading;
   using System.Threading.Tasks;
   using Emgu.CV;
   using Emgu.CV.Structure;
   using ImagingInterface.Controllers.Tests.Mocks;
   using ImagingInterface.Controllers.Tests.Views;
   using ImagingInterface.Models;
   using ImagingInterface.Plugins;
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

               imageController.InitializeImageSourceController(fileSourceController, fileSourceController.RawPluginModel);

               Assert.IsNotNullOrEmpty(imageModel.DisplayName);
               Assert.IsNotNull(imageModel.DisplayImageData);
               Assert.AreSame(imageModel, imageView.AssignedImageModel);

               imageController.Close();

               // imageModel.DisplayImageData is set to null when imageController is closed
               Assert.IsNull(imageModel.DisplayImageData);
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

         imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

         imageController.StartLiveUpdate();

         imageView.WaitForDisplayUpdate();

         Assert.IsNotNull(imageView.AssignedImageModel);
         Assert.IsNotNull(imageView.AssignedImageModel.DisplayImageData);

         // Make sure a Start after Stop doesn't throw any exception
         imageController.StopLiveUpdate();
         imageController.StartLiveUpdate();
         imageController.StopLiveUpdate();
         }

      [Test]
      public void StopLiveUpdate()
         {
         this.Container.RegisterSingle<IImageView, ImageView>();

         ImageView imageView = this.ServiceLocator.GetInstance<IImageView>() as ImageView;
         ImageSourceController imageSourceController = this.Container.GetInstance<IImageSourceController>() as ImageSourceController;
         IImageController imageController = this.Container.GetInstance<IImageController>();

         Assert.IsNotNull(imageView.AssignedImageModel);

         imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

         imageController.StartLiveUpdate();

         imageView.WaitForDisplayUpdate();

         Assert.IsNotNull(imageView.AssignedImageModel);
         Assert.IsNotNull(imageView.AssignedImageModel.DisplayImageData);

         AutoResetEvent autoResetEvent = new AutoResetEvent(false);

         imageController.LiveUpdateStopped += (sender, eventArgs) => { autoResetEvent.Set(); };

         imageController.StopLiveUpdate();

         while (!autoResetEvent.WaitOne(10))
            {
            }

         imageView.DisplayUpdated = false;

         Thread.Sleep(500);

         // There should not be any update once the live update is stopped
         Assert.IsFalse(imageView.DisplayUpdated);
         }

      [Test]
      public void IsGrayscale()
         {
         this.Container.RegisterSingle<IImageModel, ImageModel>();
         this.Container.RegisterSingle<IImageView, ImageView>();

         ImageView imageView = this.ServiceLocator.GetInstance<IImageView>() as ImageView;
         IImageModel imageModel = this.ServiceLocator.GetInstance<IImageModel>();
         ImageSourceController imageSourceController = this.Container.GetInstance<IImageSourceController>() as ImageSourceController;
         IImageController imageController = this.Container.GetInstance<IImageController>();

         imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

         Stopwatch stopwatch = Stopwatch.StartNew();

         imageView.WaitForDisplayUpdate();

         Assert.IsTrue(imageModel.IsGrayscale);

         imageSourceController = this.Container.GetInstance<IImageSourceController>() as ImageSourceController;
         imageSourceController.ImageData = new byte[1, 1, 3];
         imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

         imageView.WaitForDisplayUpdate();

         Assert.IsFalse(imageModel.IsGrayscale);

         imageSourceController = this.Container.GetInstance<IImageSourceController>() as ImageSourceController;
         imageSourceController.ImageData = new byte[1, 1, 4];
         imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

         imageView.WaitForDisplayUpdate();

         Assert.IsFalse(imageModel.IsGrayscale);
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

         imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

         imageController.StartLiveUpdate();

         imageView.WaitForDisplayUpdate();

         Assert.IsNotNull(imageView.AssignedImageModel);
         Assert.IsNotNull(imageView.AssignedImageModel.DisplayImageData);

         imageController.Closing += this.ImageController_Closing;

         imageController.Close();

         imageController.Closing -= this.ImageController_Closing;
         }

      private void ImageController_Closing(object sender, CancelEventArgs e)
         {
         IImageController imageController = sender as IImageController;

         e.Cancel = true;

         imageController.StopLiveUpdate();
         }
      }
   }
