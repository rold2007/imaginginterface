﻿namespace ImagingInterface.Tests.Common.Mocks
{
   using System;
   using System.ComponentModel;
   using ImagingInterface.Plugins;

   public class ImageSourceController : IImageSource
   {
      public ImageSourceController()
      {
         this.OriginalImageData = new byte[50, 50, 1];
         this.UpdatedImageData = this.OriginalImageData.Clone() as byte[,,];
      }

      public event EventHandler ImageDataUpdated;

      public event CancelEventHandler Closing;

      public event EventHandler Closed;

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

      public IRawPluginView RawPluginView
      {
         get;
         private set;
      }

      public IRawPluginModel RawPluginModel
      {
         get;
         set;
      }

      public bool Active
      {
         get
         {
            return true;
         }
      }

      public bool IsDynamic(IRawPluginModel rawPluginModel)
      {
         return false;
      }

      public byte[,,] NextImageData(IRawPluginModel rawPluginModel)
      {
         return this.OriginalImageData;
      }

      public void Initialize()
      {
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

      public void Disconnected()
      {
      }

      public void UpdateImageData(byte[,,] updatedImageData)
      {
      }
   }
}
