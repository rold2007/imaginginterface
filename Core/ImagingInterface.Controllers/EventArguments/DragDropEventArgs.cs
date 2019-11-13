// <copyright file="DragDropEventArgs.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.EventArguments
{
   using System;
   using System.Collections.ObjectModel;

   public class DragDropEventArgs : EventArgs
   {
      private string[] data;

      public DragDropEventArgs(string[] data)
      {
         this.data = data;
      }

      public ReadOnlyCollection<string> Data
      {
         get
         {
            return new ReadOnlyCollection<string>(this.data);
         }
      }
   }
}
