// <copyright file="IImageProcessingManagerService.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Plugins
{
   public interface IImageProcessingManagerService
   {
      IImageService ActiveImageService
      {
         get;
      }

      IImageProcessingService ActiveImageProcessingService
      {
         get;
         set;
      }

      void AddOneShotImageProcessingToActiveImage(IImageProcessingService imageProcessingController);
   }
}
