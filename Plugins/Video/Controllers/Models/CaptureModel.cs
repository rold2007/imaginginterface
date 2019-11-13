// <copyright file="CaptureModel.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace Video.Models
{
   using System;
   using System.Diagnostics;
   using System.Diagnostics.CodeAnalysis;

   public class CaptureModel : ICloneable ////: ICaptureModel
   {
      public string DisplayName
      {
         get;
         set;
      }

      ////[SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", Justification = "Too much work for now.")]
      ////public byte[,,] LastImageData
      ////{
      ////   get;
      ////   set;
      ////}

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
