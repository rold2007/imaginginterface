namespace Video.Controllers.Tests.Mocks
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
   using Video.Controllers;

   public class CaptureWrapperMock : ICaptureWrapper
      {
      private bool grabStarted;

      ~CaptureWrapperMock()
         {
         this.Dispose(false);
         }

      public int Width
         {
         get
            {
            return 1;
            }
         }

      public int Height
         {
         get
            {
            return 1;
            }
         }

      public bool CaptureAllocated
         {
         get
            {
            return this.grabStarted;
            }
         }

      public double FramePeriod
         {
         get
            {
            return 33;
            }
         }

      public void Dispose()
         {
         this.Dispose(true);
         GC.SuppressFinalize(this);
         }

      public bool Grab()
         {
         this.grabStarted = true;

         return true;
         }

      public Image<Gray, byte> QueryGrayFrame()
         {
         return this.RetrieveGrayFrame();
         }

      public Image<Gray, byte> RetrieveGrayFrame()
         {
         if (this.grabStarted)
            {
            return new Image<Gray, byte>(1, 1, new Gray(255.0));
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
            }
         }
      }
   }
