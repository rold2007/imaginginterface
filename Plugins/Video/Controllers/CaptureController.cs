namespace Video.Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Diagnostics;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using Emgu.CV;
   using Emgu.CV.Structure;
   using ImagingInterface.Controllers;
   using ImagingInterface.Controllers.EventArguments;
   using ImagingInterface.Plugins;
   using Microsoft.Practices.ServiceLocation;
   using Video.Models;
   using Video.Views;

   public class CaptureController : ICaptureController, IImageSourceController, IDisposable
      {
      private static readonly string CaptureDisplayName = "Capture"; // ncrunch: no coverage
      private ICaptureView captureView;
      private ICaptureModel captureModel;
      private IServiceLocator serviceLocator;
      private ICaptureWrapper captureWrapper;
      private IImageController liveGrabImageController;
      private bool isGrabbingLive;
      private bool isClosing;
      private bool isStopping;

      public CaptureController(ICaptureView captureView, ICaptureModel captureModel, IServiceLocator serviceLocator, ICaptureWrapper captureWrapper)
         {
         this.captureView = captureView;
         this.captureModel = captureModel;
         this.serviceLocator = serviceLocator;
         this.captureWrapper = captureWrapper;

         this.captureModel.DisplayName = CaptureController.CaptureDisplayName;

         this.captureView.Start += this.CaptureView_Start;
         this.captureView.Stop += this.CaptureView_Stop;
         this.captureView.SnapShot += this.CaptureView_SnapShot;

         this.captureView.UpdateLiveGrabStatus(true, false);
         }

      ~CaptureController()
         { // ncrunch: no coverage
         this.Dispose(false); // ncrunch: no coverage
         } // ncrunch: no coverage

      public event CancelEventHandler Closing;

      public event EventHandler Closed;

      public IRawPluginView RawPluginView
         {
         get
            {
            return this.captureView;
            }
         }

      public IRawPluginModel RawPluginModel
         {
         get
            {
            return this.captureModel;
            }
         }

      public void Dispose()
         {
         this.Dispose(true);
         GC.SuppressFinalize(this);
         }

      public void Close()
         {
         CancelEventArgs cancelEventArgs = new CancelEventArgs();

         if (this.Closing != null)
            {
            this.Closing(this, cancelEventArgs);
            }

         if (!cancelEventArgs.Cancel)
            {
            if (this.isGrabbingLive)
               {
               this.isClosing = true;
               this.StopLiveGrab();
               }
            else
               {
               if (this.Closed != null)
                  {
                  this.Closed(this, EventArgs.Empty);
                  }

               if (this.liveGrabImageController != null)
                  {
                  this.UnregisterImageControllerEvents();
                  this.liveGrabImageController = null;
                  }

               // Do not reset this.captureView before calling the Closed event as objects
               // registered to this event may depend on this.captureView
               this.captureView.Close();
               this.UnregisterCaptureViewEvents();
               this.captureView = null;
               this.Dispose();
               }
            }
         }

      public bool IsDynamic(IRawPluginModel rawPluginModel)
         {
         ICaptureModel captureModel = rawPluginModel as ICaptureModel;

         return captureModel.LiveGrabRunning;
         }

      public byte[, ,] NextImageData(IRawPluginModel rawPluginModel)
         {
         Debug.Assert(rawPluginModel != null, "We should never send a null parameter to InitializeImageSourceController.");

         ICaptureModel captureModel = rawPluginModel as ICaptureModel;

         Debug.Assert(captureModel != null, "Something went wrong with the cast.");

         // Initialize first grab
         if (captureModel.LastImageData == null)
            {
            if (this.captureWrapper.Grab())
               {
               this.GrabFirstFrame();
               }

            using (Image<Gray, byte> image = this.captureWrapper.RetrieveGrayFrame())
               {
               captureModel.LastImageData = image.Data;
               captureModel.TimeSinceLastGrab = Stopwatch.StartNew();
               }
            }
         else
            {
            if (captureModel.LiveGrabRunning)
               {
               Debug.Assert(this.captureWrapper.CaptureAllocated, "The capture should have been allocated before launching NextImageData. This is done by calling the Grab() method.");

               if (captureModel.TimeSinceLastGrab.ElapsedMilliseconds > this.captureWrapper.FramePeriod)
                  {
                  using (Image<Gray, byte> image = this.captureWrapper.RetrieveGrayFrame())
                     {
                     captureModel.TimeSinceLastGrab = Stopwatch.StartNew();

                     // Keep a copy of the last grabbed image to give it when when the grab is stopped and an image processing is applied
                     captureModel.LastImageData = image.Data;
                     }
                  }
               }
            }

         return captureModel.LastImageData;
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

            if (this.captureView != null)
               {
               this.UnregisterCaptureViewEvents();

               this.captureView = null;
               }
            }
         }

      private void CaptureView_Start(object sender, EventArgs e)
         {
         if (!this.isClosing)
            {
            if (!this.isGrabbingLive)
               {
               this.isGrabbingLive = true;

               this.captureView.UpdateLiveGrabStatus(false, true);

               if (this.liveGrabImageController == null)
                  {
                  this.liveGrabImageController = this.serviceLocator.GetInstance<IImageController>();

                  this.liveGrabImageController.Closed += this.LiveGrabImageController_Closed;

                  this.liveGrabImageController.SetDisplayName("LiveGrab");

                  IImageManagerController imageManagerController = this.serviceLocator.GetInstance<IImageManagerController>();

                  imageManagerController.AddImage(this.liveGrabImageController);
                  }

               ICaptureModel liveGrabCaptureModel = this.serviceLocator.GetInstance<ICaptureModel>();

               liveGrabCaptureModel.LiveGrabRunning = true;

               this.liveGrabImageController.InitializeImageSourceController(this, liveGrabCaptureModel);
               }
            }
         }

      private void LiveGrabImageController_DisplayUpdated(object sender, DisplayUpdateEventArgs e)
         {
         ICaptureModel captureModel = e.RawPluginModel as ICaptureModel;

         // Received the new model indicating there will be no more live update
         if (!captureModel.LiveGrabRunning)
            {
            this.isGrabbingLive = false;
            this.isStopping = false;

            this.liveGrabImageController.DisplayUpdated -= this.LiveGrabImageController_DisplayUpdated;

            this.captureView.UpdateLiveGrabStatus(true, false);

            if (this.isClosing)
               {
               this.Close();
               }
            }
         }

      private void SnapShot_Closed(object sender, EventArgs e)
         {
         IImageController imageController = sender as IImageController;

         this.SnapShotFinished(imageController);
         } // ncrunch: no coverage

      private void SnapShot_DisplayUpdated(object sender, DisplayUpdateEventArgs e)
         {
         IImageController imageController = sender as IImageController;

         this.SnapShotFinished(imageController);
         }

      private void SnapShotFinished(IImageController imageController)
         {
         imageController.Closed -= this.SnapShot_Closed;
         imageController.DisplayUpdated -= this.SnapShot_DisplayUpdated;

         this.captureView.UpdateLiveGrabStatus(true, false);
         }

      private void UnregisterImageControllerEvents()
         {
         this.liveGrabImageController.Closed -= this.LiveGrabImageController_Closed;

         if (this.isStopping)
            {
            // The image controller can be closed without a last display update so we need to unregister trhe event here
            this.liveGrabImageController.DisplayUpdated -= this.LiveGrabImageController_DisplayUpdated;
            }
         }

      private void UnregisterCaptureViewEvents()
         {
         this.captureView.Start -= this.CaptureView_Start;
         this.captureView.Stop -= this.CaptureView_Stop;
         this.captureView.SnapShot -= this.CaptureView_SnapShot;
         }

      private void LiveGrabImageController_Closed(object sender, EventArgs e)
         {
         this.UnregisterImageControllerEvents();
         this.liveGrabImageController = null;
         this.isGrabbingLive = false;
         this.isStopping = false;
         this.captureView.UpdateLiveGrabStatus(true, false);

         if (this.isClosing)
            {
            this.Close();
            }
         }

      private void CaptureView_Stop(object sender, EventArgs e)
         {
         if (!this.isClosing)
            {
            this.StopLiveGrab();
            }
         }

      private void StopLiveGrab()
         {
         if (this.isGrabbingLive)
            {
            if (!this.isStopping)
               {
               this.liveGrabImageController.DisplayUpdated += this.LiveGrabImageController_DisplayUpdated;

               this.captureView.UpdateLiveGrabStatus(false, false);

               ICaptureModel liveGrabCaptureModel = this.serviceLocator.GetInstance<ICaptureModel>();

               liveGrabCaptureModel.LiveGrabRunning = false;

               this.liveGrabImageController.InitializeImageSourceController(this, liveGrabCaptureModel);
               }

            this.isStopping = true;
            }
         }

      private void CaptureView_SnapShot(object sender, EventArgs e)
         {
         if (!this.isClosing)
            {
            ICaptureModel captureModel = this.serviceLocator.GetInstance<ICaptureModel>();

            captureModel.LiveGrabRunning = false;

            IImageController imageController = this.serviceLocator.GetInstance<IImageController>();

            imageController.SetDisplayName("Snapshot");
            imageController.InitializeImageSourceController(this, captureModel);

            IImageManagerController imageManagerController = this.serviceLocator.GetInstance<IImageManagerController>();

            imageManagerController.AddImage(imageController);

            // Register the closed event because the image controller can be closed without a last display update
            imageController.Closed += this.SnapShot_Closed;
            imageController.DisplayUpdated += this.SnapShot_DisplayUpdated;

            // Prevent any other grab until snapshot is finished to prevent many thread calling NextImageData
            this.captureView.UpdateLiveGrabStatus(false, false);
            }
         }

      private void GrabFirstFrame()
         {
         // The first grab is usually blank, skip it
         bool blank = true;
         Stopwatch stopwatch = Stopwatch.StartNew();

         // Don't wait more than a second
         while (blank && stopwatch.ElapsedMilliseconds < 1000)
            {
            using (Image<Gray, byte> image = this.captureWrapper.RetrieveGrayFrame())
               {
               if (image.GetSum().Intensity != 0)
                  {
                  blank = false;
                  }
               }
            }
         }
      }
   }
