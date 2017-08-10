// <copyright file="ICaptureWrapper.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace Video.Controllers
{
   using Emgu.CV;

   public interface ICaptureWrapper
      {
      int Width
         {
         get;
         }

      int Height
         {
         get;
         }

      bool CaptureAllocated
         {
         get;
         }

      double FramePeriod
         {
         get;
         }

      bool Grab();

      UMat RetrieveFrame();
      }
   }
