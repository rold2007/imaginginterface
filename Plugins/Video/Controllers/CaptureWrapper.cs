namespace Video.Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using Emgu.CV;
   using Emgu.CV.CvEnum;
   using Emgu.CV.Structure;
   using Emgu.Util;

   public class CaptureWrapper : ICaptureWrapper
      {
      private Capture capture;
      private double framePeriod;

      ~CaptureWrapper()
         {
         this.Dispose(false);
         }

      public int Width
         {
         get
            {
            return this.capture.Width;
            }
         }

      public int Height
         {
         get
            {
            return this.capture.Height;
            }
         }

      public bool CaptureAllocated
         {
         get
            {
            return this.capture == null ? false : true;
            }
         }

      public double FramePeriod
         {
         get
            {
            return this.framePeriod;
            }
         }

      public void Dispose()
         {
         this.Dispose(true);
         GC.SuppressFinalize(this);
         }

      public bool Grab()
         {
         this.AllocateCapture();

         return this.capture.Grab();
         }

      public Image<Gray, byte> RetrieveGrayFrame()
         {
         double frameRate = this.capture.GetCaptureProperty(CAP_PROP.CV_CAP_PROP_FPS);

         return this.capture.RetrieveGrayFrame();
         }

      protected virtual void Dispose(bool disposing)
         {
         if (disposing)
            {
            if (this.capture != null)
               {
               this.capture.Dispose();
               this.capture = null;
               }
            }
         }

      private void AllocateCapture()
         {
         if (this.capture == null)
            {
            this.capture = new Capture();

            double frameRate = this.capture.GetCaptureProperty(CAP_PROP.CV_CAP_PROP_FPS);

            if (frameRate > 0.0)
               {
               this.framePeriod = 1000 / frameRate;
               }
            else
               {
               this.framePeriod = 1000.0 / 30.0;
               }
            }
         }
      }
   }
