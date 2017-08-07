// <copyright file="CudafyGridSizeChangedEventArgs.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Controllers.EventArguments
{
   using System;

   public class CudafyGridSizeChangedEventArgs : EventArgs
      {
      public CudafyGridSizeChangedEventArgs(int x, int y, int z)
         {
         this.X = x;
         this.Y = y;
         this.Z = z;
         }

      public int X
         {
         get;
         private set;
         }

      public int Y
         {
         get;
         private set;
         }

      public int Z
         {
         get;
         private set;
         }
      }
   }
