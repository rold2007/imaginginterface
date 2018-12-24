// <copyright file="ImageProcessingServiceBase.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Plugins
{
   using System.Drawing;

   public abstract class ImageProcessingServiceBase : IImageProcessingService
   {
      public ImageProcessingServiceBase()
      {
      }

      public virtual void ProcessImageData(IImageService imageService, byte[] overlayData)
      {
      }

      public virtual void SelectPixel(Point mouseClickPixel)
      {
      }
   }
}
