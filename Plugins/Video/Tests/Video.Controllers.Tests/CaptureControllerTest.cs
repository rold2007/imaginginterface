// <copyright file="CaptureControllerTest.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace Video.Controllers.Tests
   {
   using System;
   using System.Threading;
   using ImagingInterface.Controllers;
   using ImagingInterface.Plugins;
   using NUnit.Framework;
   using Video.Controllers.Tests.Mocks;
   using Video.Models;
   using Video.Views;

   [TestFixture]
   public class CaptureControllerTest : ControllersBaseTest
      {
      [Test]
      public void Constructor()
         {
         ////this.Container.RegisterSingleton<ICaptureView, CaptureView>();

         ////CaptureView captureView = this.ServiceLocator.GetInstance<ICaptureView>() as CaptureView;
         CaptureController captureController = null;

         try
            {
            ////captureController = this.ServiceLocator.GetInstance<CaptureController>();

            ////captureController.Initialize();

            ////Assert.IsTrue(captureView.AllowGrab);
            ////Assert.IsFalse(captureView.LiveGrabRunning);
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
      public void DisplayName()
         {
         // this.Container.RegisterSingleton<CaptureModel, CaptureModel>();

         // CaptureController captureController = this.ServiceLocator.GetInstance<CaptureController>();
         // CaptureModel captureModel = this.ServiceLocator.GetInstance<CaptureModel>();

         // Assert.AreEqual("Capture", captureModel.DisplayName);
         }

      [Test]
      public void Active()
         {
         // CaptureController captureController = null;

         // try
         //   {
         //   captureController = this.ServiceLocator.GetInstance<CaptureController>();

         // Assert.IsTrue(captureController.Active);
         //   }
         // finally
         //   {
         //   if (captureController != null)
         //      {
         //      captureController.Close();
         //      }
         //   }
         }

      [Test]
      public void Close()
         {
         ////this.Container.RegisterSingleton<ICaptureView, CaptureView>();

         ////CaptureView captureView = this.ServiceLocator.GetInstance<ICaptureView>() as CaptureView;
         // bool closingCalled = false;
         // bool closeCalled = false;

         // CaptureController captureController = null;

         // try
         //   {
         //   captureController = this.ServiceLocator.GetInstance<CaptureController>();

         // captureController.Closing += (sender, eventArgs) => { closingCalled = true; };
         //   captureController.Closed += (sender, eventArgs) => { closeCalled = true; };
         //   }
         // finally
         //   {
         //   if (captureController != null)
         //      {
         //      captureController.Close();
         //      }

         // Assert.IsTrue(closingCalled);
         //   Assert.IsTrue(closeCalled);
         //   ////Assert.IsTrue(captureView.CloseCalled);
         //   }
         }

      [Test]
      public void NextImageData()
         {
         ////this.Container.RegisterSingleton<ICaptureView, CaptureView>();

         ////CaptureView captureView = this.ServiceLocator.GetInstance<ICaptureView>() as CaptureView;
         ////CaptureController captureController = null;
         ////ICaptureModel captureModel = this.ServiceLocator.GetInstance<ICaptureModel>();

         ////try
         ////   {
         ////   captureController = this.ServiceLocator.GetInstance<CaptureController>();

         ////   captureController.Initialize();

         ////   ////IImageSourceController imageSourceController = captureController as IImageSourceController;

         ////   ////byte[, ,] imageData = imageSourceController.NextImageData(captureModel);

         ////   ////Assert.IsNotNull(imageData);

         ////   ////captureView.TriggerStart();

         ////   ICaptureWrapper captureWrapper = this.ServiceLocator.GetInstance<ICaptureWrapper>();
         ////   ImageManagerController imageManagerController = this.ServiceLocator.GetInstance<ImageManagerController>();

         ////   ////using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageManagerController.GetActiveImage()))
         ////   ////   {
         ////   ////   imageControllerWrapper.WaitForDisplayUpdate();
         ////   ////   }

         ////   Thread.Sleep(Convert.ToInt32(Math.Ceiling(2 * captureWrapper.FramePeriod)));

         ////   ////using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageManagerController.GetActiveImage()))
         ////   ////   {
         ////   ////   imageControllerWrapper.WaitForDisplayUpdate();
         ////   ////   }
         ////   }
         ////finally
         ////   {
         ////   if (captureController != null)
         ////      {
         ////      captureController.Close();
         ////      }
         ////   }
         }

      [Test]
      public void NextImageDataFail()
         {
         ////this.Container.RegisterSingleton<ICaptureView, CaptureView>();
         ////this.Container.RegisterSingleton<ICaptureWrapper, CaptureWrapperMock>();

         ////////CaptureView captureView = this.ServiceLocator.GetInstance<ICaptureView>() as CaptureView;
         ////CaptureController captureController = null;
         ////ICaptureModel captureModel = this.ServiceLocator.GetInstance<ICaptureModel>();
         ////CaptureWrapperMock captureWrapper = this.ServiceLocator.GetInstance<ICaptureWrapper>() as CaptureWrapperMock;

         ////captureWrapper.GrabFail = true;

         ////try
         ////   {
         ////   captureController = this.ServiceLocator.GetInstance<CaptureController>();

         ////   captureController.Initialize();

         ////   ////IImageSourceController imageSourceController = captureController as IImageSourceController;

         ////   ////byte[, ,] imageData = imageSourceController.NextImageData(captureModel);

         ////   ////Assert.AreEqual(640, imageData.GetLength(1));
         ////   ////Assert.AreEqual(480, imageData.GetLength(0));
         ////   }
         ////finally
         ////   {
         ////   if (captureController != null)
         ////      {
         ////      captureController.Close();
         ////      }
         ////   }
         }

      [Test]
      public void StartLiveGrab()
         {
         ////this.Container.RegisterSingleton<ICaptureView, CaptureView>();

         ////CaptureView captureView = this.ServiceLocator.GetInstance<ICaptureView>() as CaptureView;
         ////ICaptureWrapper captureWrapper = this.ServiceLocator.GetInstance<ICaptureWrapper>();
         ////ICaptureModel captureModel = this.ServiceLocator.GetInstance<ICaptureModel>();
         ////CaptureController captureController = null;

         ////try
         ////   {
         ////   captureController = this.ServiceLocator.GetInstance<CaptureController>();

         ////   captureController.Initialize();

         ////   ////IImageSourceController imageSourceController = captureController as IImageSourceController;

         ////   ////byte[, ,] imageData = imageSourceController.NextImageData(captureModel);

         ////   ////Assert.IsNotNull(imageData);

         ////   // Not much is tested in this test appart from making sure we have a full code coverage
         ////   // If a bug is detected the test should be updated accordingly but note that it involves dealing
         ////   // with multithreading...
         ////   ////captureView.TriggerStart();
         ////   ////captureView.TriggerStop();
         ////   ////captureView.TriggerStart();

         ////   // Make sure we can try to trigger two start in a row without crashing
         ////   ////captureView.TriggerStart();

         ////   ImageManagerController imageManagerController = ServiceLocator.GetInstance<ImageManagerController>();

         ////   // Use all images in case more than one image controller is created
         ////   using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageManagerController.GetAllImages()))
         ////      {
         ////      // Wait for all display updates
         ////      imageControllerWrapper.WaitForDisplayUpdate();

         ////      foreach (ImageController imageController in imageManagerController.GetAllImages())
         ////         {
         ////         imageController.Close();
         ////         }

         ////      // Wait for all asynchronous close
         ////      imageControllerWrapper.WaitForClosed();
         ////      }
         ////   }
         ////finally
         ////   {
         ////   if (captureController != null)
         ////      {
         ////      captureController.Close();
         ////      }
         ////   }
         }

      [Test]
      public void StopLiveGrab()
         {
         ////this.Container.RegisterSingleton<ICaptureView, CaptureView>();

         ////CaptureView captureView = this.ServiceLocator.GetInstance<ICaptureView>() as CaptureView;
         ////CaptureController captureController = null;
         ////ImageManagerController imageManagerController = this.ServiceLocator.GetInstance<ImageManagerController>();

         ////try
         ////   {
         ////   captureController = this.ServiceLocator.GetInstance<CaptureController>();

         ////   captureController.Initialize();

         ////   // Not much is tested in this test appart from making sure we have a full code coverage
         ////   // If a bug is detected the test should be updated accordingly but note that it involves dealing
         ////   // with multithreading...
         ////   ////captureView.TriggerStart();

         ////   ////ImageController activeImageController = imageManagerController.GetActiveImage();

         ////   ////using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageManagerController.GetActiveImage()))
         ////      {
         ////      ////captureView.TriggerStop();
         ////      ////captureView.TriggerStop();

         ////      ////captureView.TriggerStart();
         ////      ////captureView.TriggerStop();

         ////      // Wait for all display updates
         ////      ////imageControllerWrapper.WaitForDisplayUpdate();

         ////      foreach (ImageController imageController in imageManagerController.GetAllImages())
         ////         {
         ////         imageController.Close();
         ////         }

         ////      // Wait for all asynchronous close
         ////      ////imageControllerWrapper.WaitForClosed();
         ////      }
         ////   }
         ////finally
         ////   {
         ////   if (captureController != null)
         ////      {
         ////      captureController.Close();
         ////      }
         ////   }
         }

      [Test]
      public void SnapShot()
         {
         ////this.Container.RegisterSingleton<ICaptureView, CaptureView>();

         ////ImageManagerController imageManagerController = this.ServiceLocator.GetInstance<ImageManagerController>();
         ////////CaptureView captureView = this.ServiceLocator.GetInstance<ICaptureView>() as CaptureView;
         ////CaptureController captureController = null;

         ////try
         ////   {
         ////   captureController = this.ServiceLocator.GetInstance<CaptureController>();

         ////   captureController.Initialize();

         ////   // Not much is tested in this test appart from making sure we have a full code coverage
         ////   // If a bug is detected the test should be updated accordingly but note that it involves dealing
         ////   // with multithreading...
         ////   ////captureView.TriggerSnapShot();

         ////   ////ImageController imageController = imageManagerController.GetActiveImage();

         ////   ////using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
         ////   ////   {
         ////   ////   imageControllerWrapper.WaitForDisplayUpdate();

         ////   ////   imageController.Close();

         ////   ////   imageControllerWrapper.WaitForClosed();
         ////   ////   }

         ////   ////captureView.TriggerSnapShot();

         ////   ////imageController = imageManagerController.GetActiveImage();

         ////   ////using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
         ////      {
         ////      // Closing the image controller right away should trigger the SnapShot_Closed method in CaptureController
         ////      // This execution is dependant on the close being called before the display is updated, if needed
         ////      // the threads should be synchronized in some wait to make sure the display is not called before the close.
         ////      ////imageController.Close();

         ////      ////imageControllerWrapper.WaitForClosed();

         ////      ////ImageController activeImageController = imageManagerController.GetActiveImage();

         ////      ////Assert.IsNull(activeImageController);
         ////      }
         ////   }
         ////finally
         ////   {
         ////   if (captureController != null)
         ////      {
         ////      captureController.Close();
         ////      }
         ////   }
         }

      [Test]
      public void Closing()
         {
         ////this.Container.RegisterSingleton<ICaptureView, CaptureView>();

         ////CaptureView captureView = this.ServiceLocator.GetInstance<ICaptureView>() as CaptureView;
         ////ImageManagerController imageManagerController = this.ServiceLocator.GetInstance<ImageManagerController>();

         ////CaptureController captureController = null;

         ////// Use try/finally instead of using to deal with warning CA2202: Do not dispose objects multiple times
         ////try
         ////   {
         ////   captureController = this.ServiceLocator.GetInstance<CaptureController>();

         ////   captureController.Initialize();

         ////   // Not much is tested in this test appart from making sure we have a full code coverage
         ////   // If a bug is detected the test should be updated accordingly but note that it involves dealing
         ////   // with multithreading...
         ////   ////captureView.TriggerStart();

         ////   ////ImageController activeImageController = imageManagerController.GetActiveImage();

         ////   ////using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(activeImageController))
         ////   ////   {
         ////   ////   captureController.Close();
         ////   ////   captureController = null;

         ////   ////   imageControllerWrapper.WaitForDisplayUpdate();
         ////   ////   }

         ////   ////using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(activeImageController))
         ////   ////   {
         ////   ////   imageControllerWrapper.WaitForDisplayUpdate();

         ////   ////   activeImageController.Close();

         ////   ////   imageControllerWrapper.WaitForClosed();
         ////   ////   }
         ////   }
         ////finally
         ////   {
         ////   if (captureController != null)
         ////      {
         ////      captureController.Dispose();
         ////      }
         ////   }

         ////captureController = null;

         ////// Use try/finally instead of using to deal with warning CA2202: Do not dispose objects multiple times
         ////try
         ////   {
         ////   captureController = this.ServiceLocator.GetInstance<CaptureController>();

         ////   captureController.Initialize();

         ////   // Make sure we can try to trigger two start in a row without crashing
         ////   ////captureView.TriggerStart();

         ////   ////ImageController activeImageController = imageManagerController.GetActiveImage();

         ////   ////using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(activeImageController))
         ////   ////   {
         ////   ////   // Wait for all display updates
         ////   ////   imageControllerWrapper.WaitForDisplayUpdate();

         ////   ////   captureController.Close();
         ////   ////   captureController = null;

         ////   ////   activeImageController.Close();

         ////   ////   // Wait for all asynchronous close
         ////   ////   imageControllerWrapper.WaitForClosed();
         ////   ////   }
         ////   }
         ////finally
         ////   {
         ////   if (captureController != null)
         ////      {
         ////      captureController.Dispose();
         ////      }
         ////   }

         ////// Test some Dispose code
         ////using (captureController = this.ServiceLocator.GetInstance<CaptureController>())
         ////   {
         ////   }
         }

      [Test]
      public void CaptureModelClone()
         {
         ////ICaptureModel captureModel = this.ServiceLocator.GetInstance<ICaptureModel>();

         ////captureModel.Clone();
         }
      }
   }
