﻿namespace ImagingInterface.Models
   {
   using System;
   using System.Collections.Generic;
   using System.Drawing;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImageProcessor.Imaging.Colors;
   using ImagingInterface.Plugins;

   public class ImageModel : IImageModel
      {
      private IImageSource imageSource;

      public ImageModel()
         {
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
            return (byte[,,])this.ImageSource.ImageData.Clone();
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
            return new Size(this.ImageSource.ImageData.GetLength(1), this.ImageSource.ImageData.GetLength(0));
            }
         }

      public bool IsGrayscale
         {
         get
            {
            ////return this.DisplayImageData.GetLength(2) == 1;
            return this.ImageSource.ImageData.GetLength(2) == 1;
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
      }
   }