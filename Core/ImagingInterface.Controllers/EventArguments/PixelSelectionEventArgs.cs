// <copyright file="PixelSelectionEventArgs.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.EventArguments
{
   using System;
   using System.Drawing;
   using ImagingInterface.Plugins;

   public class PixelSelectionEventArgs : EventArgs
      {
      public PixelSelectionEventArgs(Point pixelPosition, IImageSource imageSource, bool select)
         {
         this.PixelPosition = pixelPosition;
         this.ImageSource = imageSource;
         this.Select = select;
         }

      public Point PixelPosition
         {
         get;
         private set;
         }

      public IImageSource ImageSource
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
