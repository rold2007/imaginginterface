// <copyright file="DragDropEventArgs.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.EventArguments
{
   using System;

   public class DragDropEventArgs : EventArgs
      {
      public DragDropEventArgs(string[] data)
         {
         this.Data = data;
         }

      public string[] Data
         {
         get;
         private set;
         }
      }
   }
