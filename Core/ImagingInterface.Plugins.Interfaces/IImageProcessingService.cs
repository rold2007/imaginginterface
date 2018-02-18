// <copyright file="IImageProcessingService.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Plugins
{
   using System.Drawing;

   public interface IImageProcessingService
   {
      void CloseImage(IImageSource imageSource);

      void ProcessImageData(byte[,,] imageData, byte[] overlayData);

      void SelectPixel(IImageSource imageSource, Point mouseClickPixel);
   }
}
