// <copyright file="ImageService.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.Services
{
   using System;
   using System.Drawing;
   using ImageProcessor.Imaging.Colors;
   using ImagingInterface.Plugins;

   public class ImageService
   {
      private IImageSource imageSource;
      private PluginManagerService pluginManagerService;

      public ImageService(PluginManagerService pluginManagerService)
      {
         this.pluginManagerService = pluginManagerService;
         this.ZoomLevel = 1.0;
      }

      public IImageSource ImageSource
      {
         get
         {
            return this.imageSource;
         }

         set
         {
            if (this.imageSource == null)
            {
               this.imageSource = value;
            }
            else
            {
               throw new InvalidOperationException("Changing the image source isn't supported for now.");
            }
         }
      }

      public string DisplayName
      {
         get
         {
            return this.ImageSource.ImageName;
         }
      }

      ////public byte[,,] SourceImageData
      ////   {
      ////   get;
      ////   set;
      ////   }

      public byte[,,] DisplayImageData
      {
         get
         {
            return (byte[,,])this.ImageSource.UpdatedImageData.Clone();
         }

         ////set;
      }

      ////public byte[] OverlayImageData
      ////   {
      ////   get;
      ////   set;
      ////   }

      public Size Size
      {
         get
         {
            ////return new Size(this.DisplayImageData.GetLength(1), this.DisplayImageData.GetLength(0));
            return new Size(this.ImageSource.UpdatedImageData.GetLength(1), this.ImageSource.UpdatedImageData.GetLength(0));
         }
      }

      public bool IsGrayscale
      {
         get
         {
            ////return this.DisplayImageData.GetLength(2) == 1;
            return this.ImageSource.UpdatedImageData.GetLength(2) == 1;
         }
      }

      public double ZoomLevel
      {
         get;
         set;
      }

      public RgbaColor GetRgbaPixel(Point pixelPosition)
      {
         RgbaColor rgbaColor;

         if (this.IsGrayscale)
         {
            byte gray = this.DisplayImageData[pixelPosition.Y, pixelPosition.X, 0];

            rgbaColor = RgbaColor.FromRgba(gray, gray, gray);
         }
         else
         {
            rgbaColor = RgbaColor.FromRgba(
               this.DisplayImageData[pixelPosition.Y, pixelPosition.X, 0],
               this.DisplayImageData[pixelPosition.Y, pixelPosition.X, 1],
               this.DisplayImageData[pixelPosition.Y, pixelPosition.X, 2]);
         }

         return rgbaColor;
      }

      public void SelectPixel(Point mouseClickPixel)
      {
         ////IImageProcessingService imageProcessingPluginService = this.pluginManagerService.GetPluginFromIndex(this.pluginManagerService.ActivePluginIndex);

         ////imageProcessingPluginService.SelectPixel(mouseClickPixel);

         this.pluginManagerService.SelectPixel(this.imageSource, mouseClickPixel);
      }
   }
}
