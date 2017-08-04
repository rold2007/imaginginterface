namespace ImagingInterface.Controllers.Services
{
   using System;
   using System.Collections.Generic;
   using System.Diagnostics;
   using ImagingInterface.Plugins;

   public class ImageManagerService
   {
      private ImageSourceService imageSourceService;
      private int activeImageIndex;
      private List<IImageSource> imageSources;

      public ImageManagerService(ImageSourceService imageSourceService)
      {
         this.imageSourceService = imageSourceService;
         this.imageSources = new List<IImageSource>();

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
            return this.imageSources.Count;
         }
      }

      public int AddImage(IImageSource imageSource)
      {
         this.imageSources.Add(imageSource);

         this.ActiveImageIndex = this.ImageCount - 1;

         return this.ActiveImageIndex;
      }

      public void RemoveActiveImage()
      {
         this.imageSources.RemoveAt(this.ActiveImageIndex);

         if (this.ActiveImageIndex > 0)
         {
            this.ActiveImageIndex--;
         }
         else if(this.ImageCount == 0)
         {
            this.ActiveImageIndex = -1;
         }

         Debug.Assert(this.ImageCount >= 0, "Invalid image count.");
      }

      public IImageSource GetImageFromIndex(int activeImageIndex)
      {
         if (activeImageIndex < 0 || activeImageIndex >= this.ImageCount)
         {
            throw new ArgumentOutOfRangeException("activeImageIndex");
         }

         return this.imageSources[activeImageIndex];
      }
   }
}
