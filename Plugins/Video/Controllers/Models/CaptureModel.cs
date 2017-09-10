// <copyright file="CaptureModel.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace Video.Models
{
   using System;
   using System.Diagnostics;

   public class CaptureModel : ICloneable ////: ICaptureModel
   {
      public string DisplayName
      {
         get;
         set;
      }

      public byte[,,] LastImageData
      {
         get;
         set;
      }

      public bool LiveGrabRunning
      {
         get;
         set;
      }

      public Stopwatch TimeSinceLastGrab
      {
         get;
         set;
      }

      public object Clone()
      {
         return this.MemberwiseClone();
      }
   }
}
