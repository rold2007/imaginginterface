namespace Video.Controllers.Tests.Mocks
   {
   using System;
   using Emgu.CV;
   using Emgu.CV.Structure;
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

      public bool GrabFail
         {
         set;
         get;
         }

      public void Dispose()
         {
         this.Dispose(true);
         GC.SuppressFinalize(this);
         }

      public bool Grab()
         {
         if (GrabFail)
            {
            return false;
            }
         else
            {
            this.grabStarted = true;

            return true;
            }
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
