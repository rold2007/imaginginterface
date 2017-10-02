// <copyright file="ImageProcessingManagerService.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.Services
{
   using ImagingInterface.Plugins;

   public class ImageProcessingManagerService : IImageProcessingManagerService
   {
      private ImageManagerService imageManagerService;

      public ImageProcessingManagerService(ImageManagerService imageManagerService)
      {
         this.imageManagerService = imageManagerService;
      }

      // Apply processing to current active image synchronously
      public void AddOneShotImageProcessingToActiveImage(IImageProcessingService imageProcessingService)
      {
         int activeImageIndex = this.imageManagerService.ActiveImageIndex;

         if (activeImageIndex >= 0)
         {
            ImageService imageService = this.imageManagerService.GetImageServiceFromIndex(activeImageIndex);

            byte[,,] clonedImageData = imageService.ImageSource.OriginalImageData.Clone() as byte[,,];
            byte[] clonedOverlayData = imageService.OverlayImageData.Clone() as byte[];

            imageProcessingService.ProcessImageData(clonedImageData, clonedOverlayData);

            imageService.UpdateImageData(clonedImageData, clonedOverlayData);
         }
      }
   }
}
