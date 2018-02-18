// <copyright file="ImageManagerController.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers
{
   using System;
   using System.Collections.Generic;
   using ImagingInterface.Controllers.Services;

   public class ImageManagerController
   {
      private ImageManagerService imageManagerService;

      public ImageManagerController(ImageManagerService imageManagerService)
      {
         this.imageManagerService = imageManagerService;
      }

      public int ActiveImageIndex
      {
         get
         {
            return this.imageManagerService.ActiveImageIndex;
         }
      }

      public int ImageCount
      {
         get
         {
            return this.imageManagerService.ImageCount;
         }
      }

      public IList<ImageController> GetAllImages()
      {
         throw new NotImplementedException();
         ////return this.imageControllers.Values.ToList();
      }

      public void RemoveImage(int imageIndex)
      {
         ////int activeImageIndex = this.imageManagerService.ActiveImageIndex;

         this.imageManagerService.RemoveImage(imageIndex);

         ////activeImageIndex = Math.Min(activeImageIndex, this.imageManagerService.ImageCount - 1);

         // Restore the expected active image index as the removal could have changed it
         ////this.imageManagerService.ActiveImageIndex = activeImageIndex;
      }

      public void RemoveAllImages()
      {
         while (this.imageManagerService.ImageCount > 0)
         {
            this.RemoveImage(0);
         }
      }

      public void SetActiveImageIndex(int activeImageIndex)
      {
         if (activeImageIndex >= 0)
         {
            this.imageManagerService.ActiveImageIndex = activeImageIndex;
         }
      }

      public void AddImage()
      {
         this.imageManagerService.AddImage(null);
      }
   }
}
