// <copyright file="PluginManagerService.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.Services
{
   using System;
   using System.Drawing;
   using ImagingInterface.Controllers.EventArguments;
   using ImagingInterface.Plugins;
   using Shouldly;

   public class PluginManagerService
   {
      private int activePluginIndex;
      private ImageManagerService imageManagerService;
      private ImageProcessingManagerService imageProcessingService;

      public PluginManagerService(ImageManagerService imageManagerService, ImageProcessingManagerService imageProcessingService)
      {
         this.ActivePluginIndex = -1;
         this.imageManagerService = imageManagerService;
         this.imageProcessingService = imageProcessingService;
         this.PluginCount = 0;

         ////this.imageManagerService.ActiveImageSourceChanged += this.ImageManagerService_ActiveImageSourceChanged;
      }

      ////public event EventHandler<PixelSelectionEventArgs> ActiveImagePixelSelected;

      ////public event EventHandler<ImageSourceChangedEventArgs> ActiveImageSourceChanged;

      public int ActivePluginIndex
      {
         get
         {
            return this.activePluginIndex;
         }

         set
         {
            if (this.PluginCount == 0)
            {
               if (value != -1)
               {
                  throw new ArgumentOutOfRangeException();
               }
            }
            else if (value < 0 || value >= this.PluginCount)
            {
               throw new ArgumentOutOfRangeException();
            }

            this.activePluginIndex = value;
         }
      }

      public int PluginCount
      {
         get;
         private set;
      }

      public int AddPlugin()
      {
         this.PluginCount++;

         this.ActivePluginIndex = this.PluginCount - 1;

         ////IImageSource activeImageSource;

         ////if (this.imageManagerService.ActiveImageIndex > 0)
         ////{
         ////   activeImageSource = this.imageManagerService.GetImageServiceFromIndex(this.imageManagerService.ActiveImageIndex).ImageSource;
         ////}
         ////else
         ////{
         ////   activeImageSource = null;
         ////}

         ////this.ImageSourceChanged(activeImageSource);

         return this.ActivePluginIndex;
      }

      public void RemoveActivePlugin()
      {
         this.PluginCount--;

         if (this.ActivePluginIndex > 0)
         {
            this.ActivePluginIndex--;
         }
         else if (this.PluginCount == 0)
         {
            this.ActivePluginIndex = -1;

            this.imageProcessingService.ActiveImageProcessingService = null;
         }

         this.PluginCount.ShouldBeGreaterThanOrEqualTo(0, "Invalid plugin count.");
      }

      ////public void SelectPixel(IImageSource imageSource, Point mouseClickPixel)
      ////{
      ////   System.Diagnostics.Debug.Fail("Not implemented.");

      ////   if (this.PluginCount > 0)
      ////   {
      ////      ////this.ActiveImagePixelSelected?.Invoke(this, new PixelSelectionEventArgs(mouseClickPixel, true));
      ////   }
      ////}

      ////public void ImageSourceChanged(IImageSource imageSource)
      ////{
      ////   this.ImageManagerService_ActiveImageSourceChanged(this, new ImageSourceChangedEventArgs(imageSource));
      ////}

      ////private void ImageManagerService_ActiveImageSourceChanged(object sender, ImageSourceChangedEventArgs e)
      ////{
      ////   if (this.PluginCount > 0)
      ////   {
      ////      this.ActiveImageSourceChanged?.Invoke(sender, e);
      ////   }
      ////}
   }
}
