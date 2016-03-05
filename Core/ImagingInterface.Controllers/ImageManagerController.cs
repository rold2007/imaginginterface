namespace ImagingInterface.Controllers
   {
   using System;
   using System.Collections.Generic;
   using ImagingInterface.Models;

   public class ImageManagerController
      {
      private ImageManagerModel imageManagerModel;

      public ImageManagerController(ImageManagerModel imageManagerModel)
         {
         this.imageManagerModel = imageManagerModel;
         }

      public event EventHandler ActiveImageChanged;

      public event EventHandler RemoveActiveImageIndex;

      public IImageManagerModel ImageManagerModel
         {
         get
            {
            return this.imageManagerModel;
            }
         }

      public void AddImage()
         {
         this.imageManagerModel.AddImage();

         this.TriggerActiveImageIndexChanged();
         }

      public IList<ImageController> GetAllImages()
         {
         return null;
         ////return this.imageControllers.Values.ToList();
         }

      public void RemoveActiveImage()
         {
         int activeImageIndex = this.imageManagerModel.ActiveImageIndex;

         this.imageManagerModel.RemoveActiveImage();

         this.TriggerRemoveActiveImageIndex();

         activeImageIndex = Math.Min(activeImageIndex, this.imageManagerModel.ImageCount - 1);

         // Restore the expected active image index as the removal could have changed it
         this.imageManagerModel.ActiveImageIndex = activeImageIndex;

         this.TriggerActiveImageIndexChanged();
         }

      public void SetActiveImageIndex(int activeImageIndex)
         {
         this.imageManagerModel.ActiveImageIndex = activeImageIndex;
         }

      private void TriggerActiveImageIndexChanged()
         {
         if (this.ActiveImageChanged != null)
            {
            this.ActiveImageChanged(this, EventArgs.Empty);
            }
         }

      private void TriggerRemoveActiveImageIndex()
         {
         if (this.RemoveActiveImageIndex != null)
            {
            this.RemoveActiveImageIndex(this, EventArgs.Empty);
            }
         }
      }
   }
