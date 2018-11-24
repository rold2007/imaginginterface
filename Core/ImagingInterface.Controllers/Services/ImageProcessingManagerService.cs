// <copyright file="ImageProcessingManagerService.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.Services
{
   using System.Collections.Generic;
   using System.Drawing;
   using ImagingInterface.Plugins;
   using Shouldly;

   public class ImageProcessingManagerService : IImageProcessingManagerService
   {
      ////private ImageManagerService imageManagerService;
      private List<ImageManagerService> imageManagerServices;
      private List<PluginManagerService> pluginManagerServices;

      private int activeImageManagerService;
      private int activePluginManagerService;

      private ImageService activeImageService;

      public ImageProcessingManagerService(/*ImageManagerService imageManagerService*/)
      {
         ////this.imageManagerService = imageManagerService;

         this.imageManagerServices = new List<ImageManagerService>();
         this.pluginManagerServices = new List<PluginManagerService>();

         this.activeImageManagerService = -1;
         this.activePluginManagerService = -1;
      }

      public IImageService ActiveImageService
      {
         get
         {
            return this.activeImageService;
         }

         set
         {
            this.activeImageService = value as ImageService;
         }
      }

      public IImageProcessingService ActiveImageProcessingService
      {
         get;
         set;
      }

      public void AddImageManagerService(ImageManagerService imageManagerService)
      {
         this.imageManagerServices.Add(imageManagerService);

         // The active manager logic is kept to the bare minimum until a more complex logic becomes necessary
         this.activeImageManagerService = this.imageManagerServices.Count - 1;
      }

      public void AddPluginManagerService(PluginManagerService pluginManagerService)
      {
         this.pluginManagerServices.Add(pluginManagerService);

         // The active manager logic is kept to the bare minimum until a more complex logic becomes necessary
         this.activePluginManagerService = this.pluginManagerServices.Count - 1;
      }

      // Apply processing to current active image synchronously
      public void AddOneShotImageProcessingToActiveImage(IImageProcessingService imageProcessingService)
      {
         if (this.ActiveImageService != null)
         {
            ImageService imageService = this.activeImageService;

            imageProcessingService.ProcessImageData(imageService, imageService.OverlayImageData);

            imageService.UpdateImageData(imageService.ImageSource.OriginalImageData, imageService.OverlayImageData);
         }

         ////int activeImageIndex = this.imageManagerService.ActiveImageIndex;

         ////if (activeImageIndex >= 0)
         ////{
         ////   ImageService imageService = this.imageManagerService.GetImageServiceFromIndex(activeImageIndex);

         ////   byte[,,] clonedImageData = imageService.ImageSource.OriginalImageData.Clone() as byte[,,];
         ////   byte[] clonedOverlayData = imageService.OverlayImageData.Clone() as byte[];

         ////   imageProcessingService.ProcessImageData(clonedImageData, clonedOverlayData);

         ////   imageService.UpdateImageData(clonedImageData, clonedOverlayData);
         ////}
      }

      public void SelectPixel(Point mouseClickPixel)
      {
         if (this.ActiveImageProcessingService != null)
         ////if (this.activePluginManagerService >= 0)
         {
            ////this.activePluginManagerService.ShouldBeLessThan(this.pluginManagerServices.Count);

            this.ActiveImageProcessingService.SelectPixel(mouseClickPixel);
         }
      }
   }
}
