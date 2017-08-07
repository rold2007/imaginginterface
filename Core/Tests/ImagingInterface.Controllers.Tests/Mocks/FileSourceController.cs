// <copyright file="FileSourceController.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.Tests.Mocks
{
   using System;
   using System.ComponentModel;
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

      public byte[,,] OriginalImageData
      {
         get;
      }

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

         if (this.Closing != null)
         {
            this.Closing(this, cancelEventArgs);
         }

         if (!cancelEventArgs.Cancel)
         {
            if (this.Closed != null)
            {
               this.Closed(this, EventArgs.Empty);
            }
         }
      }

      public bool IsDynamic(IRawPluginModel rawPluginModel)
      {
         return false;
      }

      public byte[,,] NextImageData(IRawPluginModel rawPluginModel)
      {
         ////if (this.fileSourceModel.ImageData == null)
         ////   {
         ////   this.fileSourceModel.ImageData = new byte[1, 1, 1];
         ////   }

         ////return this.fileSourceModel.ImageData;
         return null;
      }

      public void Disconnected()
      {
      }

      public void UpdateImageData(byte[,,] updatedImageData)
      {
      }
   }
}
