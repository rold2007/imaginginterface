namespace ImagingInterface.Models
   {
   using System;
   using System.Diagnostics;

   public class ImageManagerModel : IImageManagerModel
      {
      private int activeImageIndex;

      public ImageManagerModel()
         {
         this.ImageCount = 0;
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
         get;
         private set;
         }

      public int AddImage()
         {
         this.ImageCount++;
         this.ActiveImageIndex = this.ImageCount - 1;

         return this.ActiveImageIndex;
         }

      public void RemoveActiveImage()
         {
         this.ImageCount--;

         Debug.Assert(this.ImageCount >= 0, "Invalid image count.");
         }

      public void TryUpdateActiveImageIndex()
         {
         if (this.ImageCount == 0)
            {
            this.ActiveImageIndex = -1;
            }
         else
            {
            this.ActiveImageIndex = Math.Min(this.ActiveImageIndex, this.ImageCount - 1);
            }
         }
      }
   }
