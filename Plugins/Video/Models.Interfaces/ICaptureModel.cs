// <copyright file="ICaptureModel.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace Video.Models
{
   using System;
   using System.Diagnostics;
   using ImagingInterface.Plugins;

   public interface ICaptureModel : ICloneable
      {
      byte[,,] LastImageData
         {
         get;
         set;
         }

      bool LiveGrabRunning
         {
         get;
         set;
         }

      Stopwatch TimeSinceLastGrab
         {
         get;
         set;
         }
      }
   }
