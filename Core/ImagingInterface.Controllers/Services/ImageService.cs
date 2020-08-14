// <copyright file="ImageService.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.Services
{
   using System;
   using System.Diagnostics.CodeAnalysis;
   using System.Drawing;
   using ImageProcessor.Imaging.Colors;
   using ImagingInterface.Plugins;

   public class ImageService
   {
      private IImageSource imageSource;
      private ImageManagerService imageManagerService;
      private PluginManagerService pluginManagerService;

      public ImageService(ImageManagerService imageManagerService, PluginManagerService pluginManagerService)
      {
         this.imageManagerService = imageManagerService;
         this.pluginManagerService = pluginManagerService;
         this.ZoomLevel = 1.0;
      }

      [SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", Justification = "Not shown to end user.")]
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

               // Prepare overlay
               int width = this.imageSource.OriginalImageData.GetLength(1);
               int height = this.imageSource.OriginalImageData.GetLength(0);

               // Allocated enough for RGBA format
               this.OverlayImageData = new byte[width * height * 4];
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

      [SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", Justification = "Too much work for now.")]
      [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Too much work for now.")]
      public byte[,,] DisplayImageData
      {
         get
         {
            return (byte[,,])this.ImageSource.UpdatedImageData.Clone();
         }
      }

      [SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", Justification = "Too much work for now.")]
      [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Too much work for now.")]
      public byte[] OverlayImageData
      {
         get;
         private set;
      }

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

      public void AssignToImageManager()
      {
         this.imageManagerService.AddImage(this);
      }

      public void UnAssignFromImageManager()
      {
         this.imageManagerService.RemoveImage(this);
      }

      [SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", Justification = "Too much work for now.")]
      public void UpdateImageData(byte[,,] updatedImageData, byte[] updatedOverlayData)
      {
         this.OverlayImageData = updatedOverlayData;

         this.imageSource.UpdateImageData(updatedImageData);
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
         this.pluginManagerService.SelectPixel(this.imageSource, mouseClickPixel);
      }
   }
}
