// <copyright file="IImageProcessingService.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Plugins
{
   using System.Drawing;

   public interface IImageProcessingService
   {
      void CloseImage(IImageService imageService);

      void ProcessImageData(IImageService imageService, byte[] overlayData);

      void SelectPixel(Point mouseClickPixel);
   }
}
