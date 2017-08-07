// <copyright file="PixelViewChangedEventArgs.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.EventArguments
{
   using System;
   using System.Drawing;

   public class PixelViewChangedEventArgs : EventArgs
      {
      public PixelViewChangedEventArgs(Point pixelPosition)
         {
         this.PixelPosition = pixelPosition;
         }

      public Point PixelPosition
         {
         get;
         private set;
         }
      }
   }
