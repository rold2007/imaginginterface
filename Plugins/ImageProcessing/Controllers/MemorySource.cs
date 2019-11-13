// <copyright file="MemorySource.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Controllers
{
   using System;
   using System.ComponentModel;
   using System.Diagnostics.CodeAnalysis;
   using ImagingInterface.Plugins;

   public class MemorySource : IMemorySource
   {
      ////public string DisplayName
      ////   {
      ////   get; // ncrunch: no coverage
      ////   set; // ncrunch: no coverage
      ////   }

      ////public byte[,,] ImageData
      ////   {
      ////   get;
      ////   set;
      ////   }

      public MemorySource()
      {
      }

      public event EventHandler ImageDataUpdated;

      public event CancelEventHandler Closing;

      public event EventHandler Closed;

      ////public IRawPluginModel RawPluginModel
      ////   {
      ////   get
      ////      {
      ////      return this.memorySourceModel;
      ////      }
      ////   }

      [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Will be fixed when done refactoring.")]
      public bool Active
      {
         get
         {
            return false;
         }
      }

      [SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", Justification = "Too much work for now.")]
      [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Too much work for now.")]
      public byte[,,] OriginalImageData
      {
         get;
         private set;
      }

      [SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", Justification = "Too much work for now.")]
      [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Too much work for now.")]
      public byte[,,] UpdatedImageData
      {
         get;
         set;
      }

      public string ImageName
      {
         get
         {
            return "Memory";
         }
      }

      ////public bool IsDynamic(IRawPluginModel rawPluginModel)
      ////{
      ////   return false;
      ////}

      ////public byte[,,] NextImageData(IRawPluginModel rawPluginModel)
      ////{
      ////   ////IMemorySourceModel memorySourceModel = rawPluginModel as IMemorySourceModel;

      ////   ////Debug.Assert(memorySourceModel.ImageData != null, "The image data should never be null.");

      ////   ////return memorySourceModel.ImageData;
      ////   return null;
      ////}

      [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Will be fixed when done refactoring.")]
      public void Initialize()
      {
      }

      public void Close()
      {
         CancelEventArgs cancelEventArgs = new CancelEventArgs();

         this.Closing?.Invoke(this, cancelEventArgs);

         if (!cancelEventArgs.Cancel)
         {
            this.Closed?.Invoke(this, EventArgs.Empty);
         }
      }

      public void Disconnected()
      {
      }

      [SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", Justification = "Too much work for now.")]
      public void UpdateImageData(byte[,,] updatedImageData)
      {
      }

      private void TriggerImageDataUpdated()
      {
         this.ImageDataUpdated?.Invoke(this, EventArgs.Empty);
      }
   }
}
