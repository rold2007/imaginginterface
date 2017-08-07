// <copyright file="IImageProcessingManagerService.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Plugins
{
   public interface IImageProcessingManagerService
   {
      void AddOneShotImageProcessingToActiveImage(IImageProcessingService imageProcessingController);
   }
}
