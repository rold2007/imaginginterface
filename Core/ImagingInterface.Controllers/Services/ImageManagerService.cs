// <copyright file="ImageManagerService.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.Services
{
   using System;
   using System.Collections.Generic;
   using System.Diagnostics;
   using ImagingInterface.Controllers.EventArguments;
   using Shouldly;

   public class ImageManagerService
   {
      private int activeImageIndex;
      private List<ImageService> imageServices;
      private ImageProcessingManagerService imageProcessingManagerService;

      public ImageManagerService(ImageProcessingManagerService imageProcessingService)
      {
         this.imageServices = new List<ImageService>();
         this.imageProcessingManagerService = imageProcessingService;

         this.ActiveImageIndex = -1;
      }

      ////public event EventHandler<ImageSourceChangedEventArgs> ActiveImageSourceChanged;

      public int ActiveImageIndex
      {
         get
         {
            return this.activeImageIndex;
         }

         set
         {
            if (this.ImageCount == 0)
            {
               value.ShouldBe(-1);
            }
            else if (value < 0 || value >= this.ImageCount)
            {
               value.ShouldBeGreaterThanOrEqualTo(0);
               value.ShouldBeLessThan(this.ImageCount);
            }

            if (this.activeImageIndex != value)
            {
               this.activeImageIndex = value;

               if (this.activeImageIndex >= 0)
               {
                  ////this.ActiveImageSourceChanged?.Invoke(this, new ImageSourceChangedEventArgs(this.imageServices[this.activeImageIndex].ImageSource));
               }
            }
         }
      }

      public int ImageCount
      {
         get
         {
            return this.imageServices.Count;
         }
      }

      public int AddImage(ImageService imageService)
      {
         ////this.imageServices.Add(imageService);
         this.imageServices.Add(null);

         this.ActiveImageIndex = this.ImageCount - 1;

         return this.ActiveImageIndex;
      }

      // TODO: Add unit tests for this method as its logic has changed a lot
      public void RemoveImage(int imageIndex)
      {
         this.ImageCount.ShouldBeGreaterThan(0);
         imageIndex.ShouldBeGreaterThanOrEqualTo(0);
         imageIndex.ShouldBeLessThan(this.imageServices.Count);

         this.imageServices.RemoveAt(imageIndex);

         if (this.ActiveImageIndex > imageIndex)
         {
            this.ActiveImageIndex.ShouldBeGreaterThan(0);

            this.ActiveImageIndex--;
         }
         else if (this.ActiveImageIndex == imageIndex)
         {
            if (this.ActiveImageIndex >= this.ImageCount)
            {
               this.ActiveImageIndex--;
            }
         }

         if (this.ActiveImageIndex < 0)
         {
            this.imageProcessingManagerService.ActiveImageService = null;
         }
      }

      // TODO: This method is awful. Is there any way to remove it ???
      public ImageService GetImageServiceFromIndex(int imageIndex)
      {
         imageIndex.ShouldBeInRange(0, this.imageServices.Count - 1);
         this.imageServices[imageIndex].ShouldNotBeNull();

         return this.imageServices[imageIndex];
      }
   }
}