namespace Video.Controllers.Tests
   {
   using System;
   using System.Collections.Generic;
   using System.Diagnostics;
   using System.Linq;
   using System.Text;
   using System.Threading;
   using System.Threading.Tasks;
   using System.Windows.Forms;
   using ImagingInterface.Controllers;
   using ImagingInterface.Plugins;
   using ImagingInterface.Tests.Common;
   using NUnit.Framework;
   using Video.Controllers.Tests.Views;
   using Video.Models;
   using Video.Views;

   [TestFixture]
   public class CaptureControllerTest : ControllersBaseTest
      {
      [Test]
      public void Constructor()
         {
         this.Container.RegisterSingle<ICaptureView, CaptureView>();

         CaptureView captureView = this.ServiceLocator.GetInstance<ICaptureView>() as CaptureView;
         ICaptureController captureController = null;

         try
            {
            captureController = this.ServiceLocator.GetInstance<ICaptureController>();

            Assert.IsTrue(captureView.AllowGrab);
            Assert.IsFalse(captureView.LiveGrabRunning);
            }
         finally
            {
            if (captureController != null)
               {
               captureController.Close();
               }
            }
         }

      [Test]
      public void RawPluginView()
         {
         this.Container.RegisterSingle<ICaptureView, CaptureView>();

         ICaptureView captureView = this.ServiceLocator.GetInstance<ICaptureView>();
         ICaptureController captureController = null;

         try
            {
            captureController = this.ServiceLocator.GetInstance<ICaptureController>();

            Assert.AreEqual(captureView, captureController.RawPluginView);
            }
         finally
            {
            if (captureController != null)
               {
               captureController.Close();
               }
            }
         }

      [Test]
      public void RawPluginModel()
         {
         this.Container.RegisterSingle<ICaptureModel, CaptureModel>();

         ICaptureModel captureModel = this.ServiceLocator.GetInstance<ICaptureModel>();
         ICaptureController captureController = null;

         try
            {
            captureController = this.ServiceLocator.GetInstance<ICaptureController>();

            Assert.AreEqual(captureModel, captureController.RawPluginModel);
            }
         finally
            {
            if (captureController != null)
               {
               captureController.Close();
               }
            }
         }

      [Test]
      public void Close()
         {
         this.Container.RegisterSingle<ICaptureView, CaptureView>();

         CaptureView captureView = this.ServiceLocator.GetInstance<ICaptureView>() as CaptureView;
         bool closingCalled = false;
         bool closeCalled = false;

         ICaptureController captureController = null;

         try
            {
            captureController = this.ServiceLocator.GetInstance<ICaptureController>();

            captureController.Closing += (sender, eventArgs) => { closingCalled = true; };
            captureController.Closed += (sender, eventArgs) => { closeCalled = true; };
            }
         finally
            {
            if (captureController != null)
               {
               captureController.Close();
               }

            Assert.IsTrue(closingCalled);
            Assert.IsTrue(closeCalled);
            Assert.IsTrue(captureView.CloseCalled);
            }
         }

      [Test]
      public void NextImageData()
         {
         this.Container.RegisterSingle<ICaptureView, CaptureView>();

         CaptureView captureView = this.ServiceLocator.GetInstance<ICaptureView>() as CaptureView;
         ICaptureController captureController = null;
         ICaptureModel captureModel = this.ServiceLocator.GetInstance<ICaptureModel>();

         try
            {
            captureController = this.ServiceLocator.GetInstance<ICaptureController>();

            IImageSourceController imageSourceController = captureController as IImageSourceController;

            byte[, ,] imageData = imageSourceController.NextImageData(captureModel);

            Assert.IsNotNull(imageData);

            captureView.TriggerStart();

            ICaptureWrapper captureWrapper = this.ServiceLocator.GetInstance<ICaptureWrapper>();
            IImageManagerController imageManagerController = this.ServiceLocator.GetInstance<IImageManagerController>();

            using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageManagerController.GetActiveImage()))
               {
               imageControllerWrapper.WaitForDisplayUpdate();
               }

            Thread.Sleep(Convert.ToInt32(Math.Ceiling(2 * captureWrapper.FramePeriod)));

            using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageManagerController.GetActiveImage()))
               {
               imageControllerWrapper.WaitForDisplayUpdate();
               }
            }
         finally
            {
            if (captureController != null)
               {
               captureController.Close();
               }
            }
         }

      [Test]
      public void StartLiveGrab()
         {
         this.Container.RegisterSingle<ICaptureView, CaptureView>();

         CaptureView captureView = this.ServiceLocator.GetInstance<ICaptureView>() as CaptureView;
         ICaptureWrapper captureWrapper = this.ServiceLocator.GetInstance<ICaptureWrapper>();
         ICaptureModel captureModel = this.ServiceLocator.GetInstance<ICaptureModel>();
         ICaptureController captureController = null;

         try
            {
            captureController = this.ServiceLocator.GetInstance<ICaptureController>();

            IImageSourceController imageSourceController = captureController as IImageSourceController;

            byte[, ,] imageData = imageSourceController.NextImageData(captureModel);

            Assert.IsNotNull(imageData);

            // Not much is tested in this test appart from making sure we have a full code coverage
            // If a bug is detected the test should be updated accordingly but note that it involves dealing
            // with multithreading...
            captureView.TriggerStart();
            captureView.TriggerStop();
            captureView.TriggerStart();

            // Make sure we can try to trigger two start in a row without crashing
            captureView.TriggerStart();

            IImageManagerController imageManagerController = ServiceLocator.GetInstance<IImageManagerController>();

            // Use all images in case more than one image controller is created
            using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageManagerController.GetAllImages()))
               {
               // Wait for all display updates
               imageControllerWrapper.WaitForDisplayUpdate();

               foreach (IImageController imageController in imageManagerController.GetAllImages())
                  {
                  imageController.Close();
                  }

               // Wait for all asynchronous close
               imageControllerWrapper.WaitForClosed();
               }
            }
         finally
            {
            if (captureController != null)
               {
               captureController.Close();
               }
            }
         }

      [Test]
      public void StopLiveGrab()
         {
         this.Container.RegisterSingle<ICaptureView, CaptureView>();

         CaptureView captureView = this.ServiceLocator.GetInstance<ICaptureView>() as CaptureView;
         ICaptureController captureController = null;
         IImageManagerController imageManagerController = this.ServiceLocator.GetInstance<IImageManagerController>();

         try
            {
            captureController = this.ServiceLocator.GetInstance<ICaptureController>();

            // Not much is tested in this test appart from making sure we have a full code coverage
            // If a bug is detected the test should be updated accordingly but note that it involves dealing
            // with multithreading...
            captureView.TriggerStart();

            IImageController activeImageController = imageManagerController.GetActiveImage();

            using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageManagerController.GetActiveImage()))
               {
               captureView.TriggerStop();
               captureView.TriggerStop();

               captureView.TriggerStart();
               captureView.TriggerStop();

               // Wait for all display updates
               imageControllerWrapper.WaitForDisplayUpdate();

               foreach (IImageController imageController in imageManagerController.GetAllImages())
                  {
                  imageController.Close();
                  }

               // Wait for all asynchronous close
               imageControllerWrapper.WaitForClosed();
               }
            }
         finally
            {
            if (captureController != null)
               {
               captureController.Close();
               }
            }
         }

      [Test]
      public void SnapShot()
         {
         this.Container.RegisterSingle<ICaptureView, CaptureView>();

         IImageManagerController imageManagerController = this.ServiceLocator.GetInstance<IImageManagerController>();
         CaptureView captureView = this.ServiceLocator.GetInstance<ICaptureView>() as CaptureView;
         ICaptureController captureController = null;

         try
            {
            captureController = this.ServiceLocator.GetInstance<ICaptureController>();

            // Not much is tested in this test appart from making sure we have a full code coverage
            // If a bug is detected the test should be updated accordingly but note that it involves dealing
            // with multithreading...
            captureView.TriggerSnapShot();

            IImageController imageController = imageManagerController.GetActiveImage();

            using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
               {
               imageControllerWrapper.WaitForDisplayUpdate();

               imageController.Close();

               imageControllerWrapper.WaitForClosed();
               }

            captureView.TriggerSnapShot();

            imageController = imageManagerController.GetActiveImage();

            using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
               {
               // Closing the image controller right away should trigger the SnapShot_Closed method in CaptureController
               // This execution is dependant on the close being called before the display is updated, if needed
               // the threads should be synchronized in some wait to make sure the display is not called before the close.
               imageController.Close();

               imageControllerWrapper.WaitForClosed();

               IImageController activeImageController = imageManagerController.GetActiveImage();

               Assert.IsNull(activeImageController);
               }
            }
         finally
            {
            if (captureController != null)
               {
               captureController.Close();
               }
            }
         }

      [Test]
      public void Closing()
         {
         this.Container.RegisterSingle<ICaptureView, CaptureView>();

         CaptureView captureView = this.ServiceLocator.GetInstance<ICaptureView>() as CaptureView;
         IImageManagerController imageManagerController = this.ServiceLocator.GetInstance<IImageManagerController>();

         using (CaptureController captureController = this.ServiceLocator.GetInstance<ICaptureController>() as CaptureController)
            {
            // Not much is tested in this test appart from making sure we have a full code coverage
            // If a bug is detected the test should be updated accordingly but note that it involves dealing
            // with multithreading...
            captureView.TriggerStart();

            IImageController activeImageController = imageManagerController.GetActiveImage();

            using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(activeImageController))
               {
               captureController.Close();

               imageControllerWrapper.WaitForDisplayUpdate();
               }

            using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(activeImageController))
               {
               imageControllerWrapper.WaitForDisplayUpdate();

               activeImageController.Close();

               imageControllerWrapper.WaitForClosed();
               }
            }

         using (CaptureController captureController = this.ServiceLocator.GetInstance<ICaptureController>() as CaptureController)
            {
            // Make sure we can try to trigger two start in a row without crashing
            captureView.TriggerStart();

            IImageController activeImageController = imageManagerController.GetActiveImage();

            using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(activeImageController))
               {
               // Wait for all display updates
               imageControllerWrapper.WaitForDisplayUpdate();

               captureController.Close();

               activeImageController.Close();

               // Wait for all asynchronous close
               imageControllerWrapper.WaitForClosed();
               }
            }

         // Test some Dispose code
         using (CaptureController captureController = this.ServiceLocator.GetInstance<ICaptureController>() as CaptureController)
            {
            }
         }

      [Test]
      public void CaptureModelClone()
         {
         ICaptureModel captureModel = this.ServiceLocator.GetInstance<ICaptureModel>();

         captureModel.Clone();
         }
      }
   }
