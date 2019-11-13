// <copyright file="CaptureWrapper.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace Video.Controllers
{
   using System;
   using Emgu.CV;
   using Emgu.CV.CvEnum;

   public class CaptureWrapper : IDisposable
   {
      private VideoCapture capture;
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

      public UMat RetrieveFrame()
      {
         double nextFrame = this.capture.GetCaptureProperty(CapProp.PosFrames);

         // This seems to be the condition to detect if a camera is already in use by another application
         // It might not work for all types of cameras though...
         if (nextFrame == -1)
         {
            UMat outputImage = null;

            try
            {
               outputImage = new UMat();

               if (this.capture.Retrieve(outputImage))
               {
                  CvInvoke.CvtColor(outputImage, outputImage, ColorConversion.Bgr2Rgb);
               }
            }
            finally
            {
               if (outputImage != null)
               {
                  outputImage.Dispose();
               }
            }

            return outputImage;
         }
         else
         {
            UMat outputImage = new UMat(this.Width, this.Height, DepthType.Cv8U, 3);

            return outputImage;
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
               this.capture = new VideoCapture();

               double frameRate = this.capture.GetCaptureProperty(CapProp.Fps);

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
