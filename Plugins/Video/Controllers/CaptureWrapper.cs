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
            }
         }
      }
   }
