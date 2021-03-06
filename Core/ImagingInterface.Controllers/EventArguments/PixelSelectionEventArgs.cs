﻿// <copyright file="PixelSelectionEventArgs.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.EventArguments
{
   using System;
   using System.Drawing;
   using ImagingInterface.Plugins;

   public class PixelSelectionEventArgs : EventArgs
   {
      public PixelSelectionEventArgs(IImageSource imageSource, Point pixelPosition, bool select)
      {
         this.ImageSource = imageSource;
         this.PixelPosition = pixelPosition;
         this.Select = select;
      }

      public IImageSource ImageSource
      {
         get;
         private set;
      }

      public Point PixelPosition
      {
         get;
         private set;
      }

      public bool Select
      {
         get;
         private set;
      }
   }
}
