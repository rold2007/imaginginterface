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

      [Test]
      public void StartLiveUpdate()
         {
         SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());

         this.Container.RegisterSingle<IImageView, ImageView>();
         this.Container.RegisterSingle<IImageModel, ImageModel>();

         ImageView imageView = this.ServiceLocator.GetInstance<IImageView>() as ImageView;
         IImageModel imageModel = this.ServiceLocator.GetInstance<IImageModel>();
         ImageSourceController imageSourceController = this.Container.GetInstance<IImageSourceController>() as ImageSourceController;
         IImageController imageController = this.Container.GetInstance<IImageController>();

         Assert.AreEqual(0, imageSourceController.NextImageDataCalls);
         Assert.IsNull(imageView.AssignedImageModel);

         imageController.StartLiveUpdate(imageSourceController);

         Stopwatch stopwatch = Stopwatch.StartNew();

         while (stopwatch.ElapsedMilliseconds < 1000 && imageView.AssignedImageModel == null)
            {
            Thread.Sleep(10);
            }

         Assert.AreNotEqual(0, imageSourceController.NextImageDataCalls);
         Assert.IsNotNull(imageView.AssignedImageModel);

         // Make sure a Start after Stop doesn't throw any exception
         imageController.StopLiveUpdate();
         imageController.StartLiveUpdate(imageSourceController);
         imageController.StopLiveUpdate();
         }

      [Test]
      public void StopLiveUpdate()
         {
         SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());

         ImageSourceController imageSourceController = this.Container.GetInstance<IImageSourceController>() as ImageSourceController;
         IImageController imageController = this.Container.GetInstance<IImageController>();

         Assert.AreEqual(0, imageSourceController.NextImageDataCalls);

         imageController.StartLiveUpdate(imageSourceController);

         Stopwatch stopwatch = Stopwatch.StartNew();

         while (stopwatch.ElapsedMilliseconds < 1000 && imageView.AssignedImageModel == null)
            {
            Thread.Sleep(10);
            }

         Assert.AreNotEqual(0, imageSourceController.NextImageDataCalls);

         bool liveUpdateStopped = false;

         imageController.LiveUpdateStopped += (sender, eventArgs) => { liveUpdateStopped = true; };

         imageController.StopLiveUpdate();

         stopwatch = Stopwatch.StartNew();

         while (stopwatch.ElapsedMilliseconds < 1000 && !liveUpdateStopped)
            {
            Thread.Sleep(10);
            }

         Assert.IsTrue(liveUpdateStopped);

         int nextImageDataCalls = imageSourceController.NextImageDataCalls;

         Thread.Sleep(500);

         Assert.AreEqual(nextImageDataCalls, imageSourceController.NextImageDataCalls);
         }

      [Test]
      public void IsGrayscale()
         {
         this.Container.RegisterSingle<IImageModel, ImageModel>();

         IImageModel imageModel = this.ServiceLocator.GetInstance<IImageModel>();
         IImageController imageController = this.Container.GetInstance<IImageController>();

         imageController.LoadImage(new byte[1, 1, 1], "Dummy");

         Assert.IsTrue(imageModel.IsGrayscale);

         imageController.LoadImage(new byte[1, 1, 3], "Dummy");

         Assert.IsFalse(imageModel.IsGrayscale);

         imageController.LoadImage(new byte[1, 1, 4], "Dummy");

         Assert.IsFalse(imageModel.IsGrayscale);
         }

      [Test]
      public void CloseWhileStartLiveUpdate()
         {
         SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());

         this.Container.RegisterSingle<IImageView, ImageView>();
         this.Container.RegisterSingle<IImageModel, ImageModel>();

         ImageView imageView = this.ServiceLocator.GetInstance<IImageView>() as ImageView;
         IImageModel imageModel = this.ServiceLocator.GetInstance<IImageModel>();
         ImageSourceController imageSourceController = this.Container.GetInstance<IImageSourceController>() as ImageSourceController;
         IImageController imageController = this.Container.GetInstance<IImageController>();

         Assert.AreEqual(0, imageSourceController.NextImageDataCalls);
         Assert.IsNull(imageView.AssignedImageModel);

         imageController.StartLiveUpdate(imageSourceController);

         Stopwatch stopwatch = Stopwatch.StartNew();

         while (stopwatch.ElapsedMilliseconds < 1000 && imageView.AssignedImageModel == null)
            {
            Thread.Sleep(10);
            }

         Assert.AreNotEqual(0, imageSourceController.NextImageDataCalls);
         Assert.IsNotNull(imageView.AssignedImageModel);

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
