// <copyright file="CaptureController.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace Video.Controllers
{
   using System;
   using System.ComponentModel;
   using System.Diagnostics;
   using Emgu.CV;
   using Emgu.CV.Structure;
   using Video.Models;

   public class CaptureController
   {
      private static readonly string CaptureDisplayName = "Capture"; // ncrunch: no coverage
      private CaptureModel captureModel = new CaptureModel();
      private CaptureWrapper captureWrapper;
      ////private ImageController liveGrabImageController;
      private bool grabbingLive;
      private bool stopping;

      public CaptureController(CaptureWrapper captureWrapper)
      {
         this.captureWrapper = captureWrapper;

         this.captureModel.DisplayName = CaptureController.CaptureDisplayName;
      }

      ~CaptureController()
      { // ncrunch: no coverage
         this.Dispose(false); // ncrunch: no coverage
      } // ncrunch: no coverage

      public event CancelEventHandler Closing;

      public event EventHandler Closed;

      ////public IRawPluginView RawPluginView
      ////   {
      ////   get
      ////      {
      ////      return this.captureView;
      ////      }
      ////   }

      public string DisplayName
      {
         get
         {
            return this.captureModel.DisplayName;
         }
      }

      public void Dispose()
      {
         this.Dispose(true);
         GC.SuppressFinalize(this);
      }

      public void Initialize()
      {
         ////this.captureView.Start += this.CaptureView_Start;
         ////this.captureView.Stop += this.CaptureView_Stop;
         ////this.captureView.SnapShot += this.CaptureView_SnapShot;

         ////this.captureView.UpdateLiveGrabStatus(true, false);
      }

      public void Close()
      {
         CancelEventArgs cancelEventArgs = new CancelEventArgs();

         this.Closing?.Invoke(this, cancelEventArgs);

         if (!cancelEventArgs.Cancel)
         {
            Debug.Assert(this.grabbingLive == false, "The grab live should have been stopped by the image controller.");

            this.Closed?.Invoke(this, EventArgs.Empty);

            Debug.Fail("Review the use of liveGrabImageController. Plugins shouldn't depend on ImagingInterface.Controllers.");

            ////if (this.liveGrabImageController != null)
            {
               ////this.liveGrabImageController.Closed -= this.LiveGrabImageController_Closed;
               ////this.liveGrabImageController = null;
            }

            // Do not reset this.captureView before calling the Closed event as objects
            // registered to this event may depend on this.captureView
            ////this.captureView.Close();
            this.UnregisterCaptureViewEvents();
            ////this.captureView = null;
            this.Dispose();
         }
      }

      ////public bool IsDynamic(IRawPluginModel rawPluginModel)
      ////{
      ////   CaptureModel captureModel = rawPluginModel as CaptureModel;

      ////   return captureModel.LiveGrabRunning;
      ////}

      ////public byte[,,] NextImageData(IRawPluginModel rawPluginModel)
      ////{
      ////   Debug.Assert(rawPluginModel != null, "We should never send a null parameter to InitializeImageSourceController.");

      ////   CaptureModel captureModel = rawPluginModel as CaptureModel;

      ////   Debug.Assert(captureModel != null, "Something went wrong with the cast.");

      ////   // Initialize first grab
      ////   if (captureModel.LastImageData == null)
      ////   {
      ////      if (this.captureWrapper.Grab())
      ////      {
      ////         this.GrabFirstFrame();
      ////      }

      ////      if (this.captureWrapper.CaptureAllocated)
      ////      {
      ////         using (UMat image = this.captureWrapper.RetrieveFrame())
      ////         using (Image<Rgb, byte> imageData = image.ToImage<Rgb, byte>())
      ////         {
      ////            captureModel.LastImageData = imageData.Data;
      ////            captureModel.TimeSinceLastGrab = Stopwatch.StartNew();
      ////         }
      ////      }
      ////   }
      ////   else
      ////   {
      ////      if (captureModel.LiveGrabRunning)
      ////      {
      ////         Debug.Assert(this.captureWrapper.CaptureAllocated, "The capture should have been allocated before launching NextImageData. This is done by calling the Grab() method.");

      ////         long timeSinceLastGrab = captureModel.TimeSinceLastGrab.ElapsedMilliseconds;

      ////         if (timeSinceLastGrab > this.captureWrapper.FramePeriod)
      ////         {
      ////            using (UMat image = this.captureWrapper.RetrieveFrame())
      ////            using (Image<Rgb, byte> imageData = image.ToImage<Rgb, byte>())
      ////            {
      ////               captureModel.TimeSinceLastGrab = Stopwatch.StartNew();

      ////               // Keep a copy of the last grabbed image to give it when when the grab is stopped and an image processing is applied
      ////               captureModel.LastImageData = imageData.Data;
      ////            }
      ////         }
      ////         else
      ////         {
      ////            // Need to throttle the grab otherwise it uses 100% CPU for nothing
      ////            int waitTime = Convert.ToInt32(this.captureWrapper.FramePeriod - timeSinceLastGrab - 5);

      ////            if (waitTime > 0)
      ////            {
      ////               System.Threading.Thread.Sleep(waitTime);
      ////            }
      ////         }
      ////      }
      ////   }

      ////   if (captureModel.LastImageData != null)
      ////   {
      ////      return captureModel.LastImageData;
      ////   }
      ////   else
      ////   {
      ////      using (Image<Rgb, byte> errorImage = new Image<Rgb, byte>(640, 480))
      ////      {
      ////         errorImage.Draw("Unable to initialize camera.", new Point(0, 32), FontFace.HersheyPlain, 1.0, new Rgb(Color.Red));

      ////         return errorImage.Data;
      ////      }
      ////   }
      ////}

      public void Disconnected()
      {
         this.ResetGrabLive();
      }

      protected virtual void Dispose(bool disposing)
      {
         if (disposing)
         {
            if (this.captureWrapper != null)
            {
               this.captureWrapper.Dispose();
               this.captureWrapper = null;
            }

            ////if (this.captureView != null)
            {
               this.UnregisterCaptureViewEvents();

               ////this.captureView = null;
            }
         }
      }

      private void CaptureView_Start(object sender, EventArgs e)
      {
         if (!this.grabbingLive)
         {
            this.grabbingLive = true;

            ////this.captureView.UpdateLiveGrabStatus(false, true);

            ////if (this.liveGrabImageController == null)
            {
               // this.liveGrabImageController = this.serviceLocator.GetInstance<ImageController>();

               ////this.liveGrabImageController.Closed += this.LiveGrabImageController_Closed;

               ////this.liveGrabImageController.SetDisplayName("LiveGrab");

               // ImageManagerController imageManagerController = this.serviceLocator.GetInstance<ImageManagerController>();

               ////imageManagerController.AddImage(this.liveGrabImageController);
            }

            // CaptureModel liveGrabCaptureModel = this.serviceLocator.GetInstance<CaptureModel>();

            // liveGrabCaptureModel.LiveGrabRunning = true;

            ////this.liveGrabImageController.InitializeImageSourceController(this, liveGrabCaptureModel);
         }
      }

      ////private void LiveGrabImageController_DisplayUpdated(object sender, DisplayUpdateEventArgs e)
      ////{
         ////CaptureModel captureModel = e.RawPluginModel as CaptureModel;

         ////// Received the new model indicating there will be no more live update
         ////if (!captureModel.LiveGrabRunning)
         ////{
         ////   this.ResetGrabLive();
         ////}
      ////}

      private void ResetGrabLive()
      {
         this.grabbingLive = false;
         this.stopping = false;

         ////if (this.liveGrabImageController != null)
         ////   {
         ////   this.liveGrabImageController.DisplayUpdated -= this.LiveGrabImageController_DisplayUpdated;
         ////   }

         ////this.captureView.UpdateLiveGrabStatus(true, false);
      }

      private void SnapShot_Closed(object sender, EventArgs e)
      {
         ////ImageController imageController = sender as ImageController;

         this.SnapShotFinished(/*imageController*/);
      }

      ////private void SnapShot_DisplayUpdated(object sender, DisplayUpdateEventArgs e)
      ////{
      ////   ImageController imageController = sender as ImageController;

      ////   this.SnapShotFinished(imageController);
      ////}

      private void SnapShotFinished(/*ImageController imageController*/)
      {
         ////imageController.Closed -= this.SnapShot_Closed;
         ////imageController.DisplayUpdated -= this.SnapShot_DisplayUpdated;

         ////this.captureView.UpdateLiveGrabStatus(true, false);
      }

      private void UnregisterCaptureViewEvents()
      {
         ////this.captureView.Start -= this.CaptureView_Start;
         ////this.captureView.Stop -= this.CaptureView_Stop;
         ////this.captureView.SnapShot -= this.CaptureView_SnapShot;
      }

      private void LiveGrabImageController_Closed(object sender, EventArgs e)
      {
         ////this.liveGrabImageController.Closed -= this.LiveGrabImageController_Closed;

         ////this.liveGrabImageController = null;
      }

      private void CaptureView_Stop(object sender, EventArgs e)
      {
         this.StopLiveGrab();
      }

      private void StopLiveGrab()
      {
         if (this.grabbingLive)
         {
            if (!this.stopping)
            {
               ////this.liveGrabImageController.DisplayUpdated += this.LiveGrabImageController_DisplayUpdated;

               ////this.captureView.UpdateLiveGrabStatus(false, false);

               // CaptureModel liveGrabCaptureModel = this.serviceLocator.GetInstance<CaptureModel>();

               // liveGrabCaptureModel.LiveGrabRunning = false;

               ////this.liveGrabImageController.InitializeImageSourceController(this, liveGrabCaptureModel);
            }

            this.stopping = true;
         }
      }

      private void CaptureView_SnapShot(object sender, EventArgs e)
      {
         // CaptureModel captureModel = this.serviceLocator.GetInstance<CaptureModel>();

         // captureModel.LiveGrabRunning = false;

         // ImageController imageController = this.serviceLocator.GetInstance<ImageController>();

         ////imageController.SetDisplayName("Snapshot");
         ////imageController.InitializeImageSourceController(this, captureModel);

         // ImageManagerController imageManagerController = this.serviceLocator.GetInstance<ImageManagerController>();

         ////imageManagerController.AddImage(imageController);

         // Register the closed event because the image controller can be closed without a last display update
         ////imageController.Closed += this.SnapShot_Closed;
         ////imageController.DisplayUpdated += this.SnapShot_DisplayUpdated;

         // Prevent any other grab until snapshot is finished to prevent many thread calling NextImageData
         ////this.captureView.UpdateLiveGrabStatus(false, false);
      }

      private void GrabFirstFrame()
      {
         if (this.captureWrapper.CaptureAllocated)
         {
            // The first grab is usually blank, skip it
            bool blank = true;
            Stopwatch stopwatch = Stopwatch.StartNew();

            // Don't wait more than a second
            while (blank && stopwatch.ElapsedMilliseconds < 1000)
            {
               using (UMat image = this.captureWrapper.RetrieveFrame())
               {
                  MCvScalar abc = CvInvoke.Sum(image);

                  if (abc.V0 != 0)
                  {
                     blank = false;
                  }
               }
            }
         }
      }
   }
}
