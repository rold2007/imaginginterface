// <copyright file="IImageProcessingService.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Plugins
{
   public interface IImageProcessingService
   {
      byte[,,] ProcessImageData(byte[,,] imageData, byte[] overlayData, IRawPluginModel rawPluginModel);
   }
}
