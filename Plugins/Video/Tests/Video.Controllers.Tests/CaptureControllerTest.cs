namespace Video.Controllers.Tests
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading;
   using System.Threading.Tasks;
   using ImagingInterface.Controllers;
   using ImagingInterface.Plugins;
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
         ICaptureController captureController = null;

         try
            {
            captureController = this.ServiceLocator.GetInstance<ICaptureController>();

            IImageSourceController imageSourceController = captureController as IImageSourceController;

            byte[,,] imageData = imageSourceController.NextImageData(null);

            Assert.IsNull(imageData);
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

         ICaptureController captureController = null;

         try
            {
            captureController = this.ServiceLocator.GetInstance<ICaptureController>();

            IImageSourceController imageSourceController = captureController as IImageSourceController;

            byte[,,] imageData = imageSourceController.NextImageData(null);

            Assert.IsNull(imageData);

            // Not much is tested in this test appart from making sure we have a full code coverage
            // If a bug is detected the test should be updated accordingly but note that it involves dealing
            // with multithreading...
            captureView.TriggerStart();

            captureView.TriggerStop();

            captureView.TriggerStart();
            captureView.TriggerStart();
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

            activeImageController.Close();

            captureView.TriggerStop();
            captureView.TriggerStop();

            captureView.TriggerStart();
            captureView.TriggerStop();
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

         CaptureView captureView = this.ServiceLocator.GetInstance<ICaptureView>() as CaptureView;
         ICaptureController captureController = null;

         try
            {
            captureController = this.ServiceLocator.GetInstance<ICaptureController>();

            // Not much is tested in this test appart from making sure we have a full code coverage
            // If a bug is detected the test should be updated accordingly but note that it involves dealing
            // with multithreading...
            captureView.TriggerSnapShot();
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
         this.Container.RegisterSingle<IImageController, ImageController>();

         CaptureView captureView = this.ServiceLocator.GetInstance<ICaptureView>() as CaptureView;
         IImageController imageController = this.ServiceLocator.GetInstance<IImageController>();
         ICaptureController captureController = null;

         try
            {
            captureController = this.ServiceLocator.GetInstance<ICaptureController>();

            // Not much is tested in this test appart from making sure we have a full code coverage
            // If a bug is detected the test should be updated accordingly but note that it involves dealing
            // with multithreading...
            captureView.TriggerStart();

            imageController.Close();
            }
         finally
            {
            if (captureController != null)
               {
               captureController.Close();
               }
            }
         }
      }
   }
