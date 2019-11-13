// <copyright file="CaptureWrapperMock.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace Video.Controllers.Tests.Mocks
{
   using System;
   using System.Diagnostics.CodeAnalysis;
   using Emgu.CV;
   using Emgu.CV.CvEnum;
   using Emgu.CV.Structure;

   public class CaptureWrapperMock : IDisposable
   {
      private bool grabStarted;

      ~CaptureWrapperMock()
      {
         this.Dispose(false);
      }

      [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Will be fixed when done refactoring.")]
      public int Width
      {
         get
         {
            return 1;
         }
      }

      [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Will be fixed when done refactoring.")]
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

      [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Will be fixed when done refactoring.")]
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
