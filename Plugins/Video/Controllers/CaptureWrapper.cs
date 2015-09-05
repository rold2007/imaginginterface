﻿namespace Video.Controllers
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
         if (this.AllocateCapture())
            {
            return this.capture.Grab();
            }
         else
            {
            return false;
            }
         }

      public Image<Gray, byte> RetrieveGrayFrame()
         {
         double nextFrame = this.capture.GetCaptureProperty(CAP_PROP.CV_CAP_PROP_POS_FRAMES);

         // This seems to be the condition to detect if a camera is already in use by another application
         // It might not work for all types of cameras though...
         if (nextFrame == -1)
            {
            return this.capture.RetrieveGrayFrame();
            }
         else
            {
            return new Image<Gray, byte>(this.Width, this.Height);
            }
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

      private bool AllocateCapture()
         {
         if (!this.CaptureAllocated)
            {
            try
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
            catch (NullReferenceException)
               {
               return false;
               }
            }

         return true;
         }
      }
   }
