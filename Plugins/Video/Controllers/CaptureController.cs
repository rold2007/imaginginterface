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
   using ImagingInterface.Plugins;
   using Microsoft.Practices.ServiceLocation;
   using Video.Models;
   using Video.Views;

   public class CaptureController : ICaptureController, IImageSourceController, IDisposable
      {
      private static readonly string CaptureDisplayName = "Capture";
      private ICaptureView captureView;
      private ICaptureModel captureModel;
      private IServiceLocator serviceLocator;
      private ICaptureWrapper captureWrapper;
      private IImageController liveGrabImageController;
      private bool closeImageControllerAfterLiveUpdate;
      private bool closeCaptureControllerAfterLiveUpdate;
      private ICaptureModel liveGrabCaptureModel;

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
            if (this.liveGrabCaptureModel != null)
               {
               this.closeCaptureControllerAfterLiveUpdate = true;
               this.StopLiveGrab();
               }
            else
               {
               this.captureView.Close();

               if (this.Closed != null)
                  {
                  this.Closed(this, EventArgs.Empty);
                  }

               this.captureView = null;
               this.Dispose();
               }
            }
         }

      public string DisplayName(IRawPluginModel rawPluginModel)
         {
         ICaptureModel captureModel = rawPluginModel as ICaptureModel;

         if (captureModel.LiveGrabRunning)
            {
            return "LiveGrab";
            }
         else
            {
            return "SnapShot";
            }
         }

      public byte[,,] NextImageData(IRawPluginModel rawPluginModel)
         {
         if (rawPluginModel != null)
            {
            ICaptureModel captureModel = rawPluginModel as ICaptureModel;

            Debug.Assert(captureModel != null, "Something went wrong with the cast.");

            if (captureModel.LiveGrabRunning)
               {
               Debug.Assert(this.captureWrapper.CaptureAllocated, "The capture should have been allocated before launching NextImageData. This is done by calling the Grab() method.");

               using (Image<Gray, byte> image = this.captureWrapper.RetrieveGrayFrame())
                  {
                  // Keep a copy of the last grabbed image to give it when when the grab is stopped and an image processing is applied
                  captureModel.LastImageData = image.Data;
                  }
               }

            return captureModel.LastImageData;
            }
         else
            {
            return null;
            }
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
            }
         }

      private void CaptureView_Start(object sender, EventArgs e)
         {
         if (this.liveGrabCaptureModel == null)
            {
            this.liveGrabCaptureModel = this.captureModel.Clone() as ICaptureModel;

            this.liveGrabCaptureModel.LiveGrabRunning = true;

            this.captureView.UpdateLiveGrabStatus(false, true);

            if (this.captureWrapper.Grab())
               {
               this.GrabFirstFrame();

               if (this.liveGrabImageController == null)
                  {
                  this.liveGrabImageController = this.serviceLocator.GetInstance<IImageController>();

                  this.liveGrabImageController.Closing += this.LiveGrabImageController_Closing;
                  this.liveGrabImageController.Closed += this.LiveGrabImageController_Closed;
                  this.liveGrabImageController.LiveUpdateStopped += this.LiveGrabImageController_LiveUpdateStopped;

                  this.liveGrabImageController.InitializeImageSourceController(this, this.liveGrabCaptureModel);

                  IImageManagerController imageManagerController = this.serviceLocator.GetInstance<IImageManagerController>();

                  imageManagerController.AddImage(this.liveGrabImageController);

                  this.liveGrabImageController.StartLiveUpdate();
                  }
               else
                  {
                  this.liveGrabImageController.StartLiveUpdate();
                  }
               }
            }
         else
            {
            Trace.WriteLine("Cannot start more than one live grab");
            }
         }

      private void LiveGrabImageController_Closing(object sender, CancelEventArgs e)
         {
         if (this.liveGrabCaptureModel != null)
            {
            this.closeImageControllerAfterLiveUpdate = true;

            e.Cancel = true;

            this.StopLiveGrab();
            }
         }

      private void LiveGrabImageController_Closed(object sender, EventArgs e)
         {
         this.liveGrabImageController.Closing -= this.LiveGrabImageController_Closing;
         this.liveGrabImageController.Closed -= this.LiveGrabImageController_Closed;
         this.liveGrabImageController.LiveUpdateStopped -= this.LiveGrabImageController_LiveUpdateStopped;
         this.liveGrabImageController = null;
         this.closeImageControllerAfterLiveUpdate = false;
         }

      private void LiveGrabImageController_LiveUpdateStopped(object sender, EventArgs e)
         {
         this.liveGrabCaptureModel = null;

         if (this.closeImageControllerAfterLiveUpdate)
            {
            this.liveGrabImageController.Close();
            }

         if (this.closeCaptureControllerAfterLiveUpdate)
            {
            this.Close();
            }
         else
            {
            if (this.liveGrabImageController != null)
               {
               this.captureView.UpdateLiveGrabStatus(this.liveGrabImageController.CanLiveUpdate, false);
               }
            else
               {
               this.captureView.UpdateLiveGrabStatus(true, false);
               }
            }
         }

      private void CaptureView_Stop(object sender, EventArgs e)
         {
         this.StopLiveGrab();
         }

      private void StopLiveGrab()
         {
         if (this.liveGrabCaptureModel != null)
            {
            this.captureView.UpdateLiveGrabStatus(false, false);

            this.liveGrabImageController.StopLiveUpdate();
            }
         else
            {
            Trace.WriteLine("No live grab to stop");
            }
         }

      private void CaptureView_SnapShot(object sender, EventArgs e)
         {
         if (this.captureWrapper.Grab())
            {
            this.GrabFirstFrame();

            using (Image<Gray, byte> image = this.captureWrapper.RetrieveGrayFrame())
               {
               IImageController imageController = this.serviceLocator.GetInstance<IImageController>();

               ICaptureModel captureModel = this.captureModel.Clone() as ICaptureModel;

               captureModel.LiveGrabRunning = false;
               captureModel.LastImageData = image.Data;

               imageController.InitializeImageSourceController(this, captureModel);

               IImageManagerController imageManagerController = this.serviceLocator.GetInstance<IImageManagerController>();

               imageManagerController.AddImage(imageController);
               }
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
