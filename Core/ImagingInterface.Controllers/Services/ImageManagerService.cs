﻿// <copyright file="ImageManagerService.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.Services
{
   using System;
   using System.Collections.Generic;
   using System.Diagnostics;
   using ImagingInterface.Plugins;
   using Shouldly;

   public class ImageManagerService
   {
      private int activeImageIndex;
      private List<ImageService> imageServices;

      public ImageManagerService()
      {
         this.imageServices = new List<ImageService>();

         this.ActiveImageIndex = -1;
      }

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
               if (value != -1)
               {
                  throw new ArgumentOutOfRangeException();
               }
            }
            else if (value < 0 || value >= this.ImageCount)
            {
               throw new ArgumentOutOfRangeException();
            }

            this.activeImageIndex = value;
         }
      }

      public int ImageCount
      {
         get
         {
            return this.imageServices.Count;
         }
      }

      public int AddImage()
      {
         this.imageServices.Add(null);

         this.ActiveImageIndex = this.ImageCount - 1;

         return this.ActiveImageIndex;
      }

      public void RemoveActiveImage()
      {
         this.imageServices.RemoveAt(this.ActiveImageIndex);

         if (this.ActiveImageIndex > 0)
         {
            this.ActiveImageIndex--;
         }
         else if (this.ImageCount == 0)
         {
            this.ActiveImageIndex = -1;
         }

         Debug.Assert(this.ImageCount >= 0, "Invalid image count.");
      }

      public void AssignImageService(ImageService imageService)
      {
         this.ActiveImageIndex.ShouldBeInRange(0, this.imageServices.Count - 1);
         this.imageServices[this.ActiveImageIndex].ShouldBeNull();

         this.imageServices[this.ActiveImageIndex] = imageService;
      }

      public ImageService GetImageServiceFromIndex(int imageIndex)
      {
         imageIndex.ShouldBeInRange(0, this.imageServices.Count - 1);
         this.imageServices[imageIndex].ShouldNotBeNull();

         return this.imageServices[imageIndex];
      }
   }
}
