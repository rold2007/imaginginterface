// <copyright file="CaptureWrapperMock.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace Video.Controllers.Tests.Mocks
   {
   using System;
   using Emgu.CV;
   using Emgu.CV.CvEnum;
   using Emgu.CV.Structure;
   using Video.Controllers;

   public class CaptureWrapperMock
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
         get;
         set;
         }

      public void Dispose()
         {
         this.Dispose(true);
         GC.SuppressFinalize(this);
         }

      public bool Grab()
         {
         if (this.GrabFail)
            {
            return false;
            }
         else
            {
            this.grabStarted = true;

            return true;
            }
         }

      public UMat QueryGrayFrame()
         {
         return this.RetrieveFrame();
         }

      public UMat RetrieveFrame()
         {
         if (this.grabStarted)
            {
            UMat image = new UMat(1, 1, DepthType.Cv8U, 3);

            image.SetTo(new MCvScalar(255.0));

            return image;
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
