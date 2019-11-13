// <copyright file="FileSourceController.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.Tests.Mocks
{
   using System;
   using System.ComponentModel;
   using System.Diagnostics.CodeAnalysis;
   using ImagingInterface.Plugins;

   public class FileSourceController : IFileSource
   {
      public FileSourceController()
      {
      }

      public event EventHandler ImageDataUpdated;

      public event CancelEventHandler Closing;

      public event EventHandler Closed;

      ////public IRawPluginModel RawPluginModel
      ////   {
      ////   get
      ////      {
      ////      return this.fileSourceModel;
      ////      }
      ////   }

      [SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", Justification = "Too much work for now.")]
      [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Too much work for now.")]
      public byte[,,] OriginalImageData
      {
         get;
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
            return "Mock";
         }
      }

      [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Will be fixed when done refactoring.")]
      public bool Active
      {
         get
         {
            return false;
         }
      }

      public string Filename
      {
         get;
         private set;
      }

      [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Will be fixed when done refactoring.")]
      public void Initialize()
      {
      }

      public bool LoadFile(string file)
      {
         if (file == "ValidFile")
         {
            this.Filename = file;

            return true;
         }
         else
         {
            return false;
         }
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

      ////public bool IsDynamic(IRawPluginModel rawPluginModel)
      ////{
      ////   return false;
      ////}

      ////public byte[,,] NextImageData(IRawPluginModel rawPluginModel)
      ////{
      ////   ////if (this.fileSourceModel.ImageData == null)
      ////   ////   {
      ////   ////   this.fileSourceModel.ImageData = new byte[1, 1, 1];
      ////   ////   }

      ////   ////return this.fileSourceModel.ImageData;
      ////   return null;
      ////}

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
