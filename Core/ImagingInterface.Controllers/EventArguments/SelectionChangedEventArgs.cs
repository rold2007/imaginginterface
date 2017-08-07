// <copyright file="SelectionChangedEventArgs.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.EventArguments
{
   using System;
   using System.Drawing;

   public class SelectionChangedEventArgs : EventArgs
      {
      public SelectionChangedEventArgs(Point pixelPosition, bool select)
         {
         this.PixelPosition = pixelPosition;
         this.Select = select;
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
