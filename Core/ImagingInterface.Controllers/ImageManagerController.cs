namespace ImagingInterface.Controllers
{
   using System;
   using System.Collections.Generic;
   using System.Windows;
   using ImagingInterface.Controllers.EventArguments;
   using ImagingInterface.Controllers.Services;
   using ImagingInterface.Controllers.Views;
   using ImagingInterface.Plugins;

   public class ImageManagerController
   {
      private ImageManagerService imageManagerService;
      private Dictionary<IImageSource, List<IImageView>> imageSourceImageViews;

      public ImageManagerController(ImageManagerService imageManagerService)
      {
         this.imageManagerService = imageManagerService;
         this.imageSourceImageViews = new Dictionary<IImageSource, List<IImageView>>();
      }

      //public event EventHandler ActiveImageChanged;

      //public event EventHandler RemoveActiveImageIndex;

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

      //public IImageManagerModel ImageManagerModel
      //   {
      //   get
      //      {
      //      return this.imageManagerModel;
      //      }
      //   }

      public void AddImage(IImageSource imageSource, IImageView imageView)
      {
         List<IImageView> imageViews;

         if (!this.imageSourceImageViews.TryGetValue(imageSource, out imageViews))
         {
            imageViews = new List<IImageView>();

            this.imageSourceImageViews.Add(imageSource, imageViews);
         }

         imageViews.Add(imageView);

         this.imageManagerService.AddImage(imageSource);

         //this.TriggerActiveImageIndexChanged();
      }

      public void AddImages(IList<IImageSource> imageSources)
      {
         foreach (IImageSource imageSource in imageSources)
         {
            this.imageManagerService.AddImage(imageSource);

            //ImageView imageView = this.serviceLocator.GetInstance<ImageView>();

            //imageView.SetImageSource(fileSource);

            //this.imageManagerView.AddImageView(imageView);

            //this.TriggerImageAdded();
            //this.TriggerActiveImageIndexChanged();
         }
      }

      public IList<ImageController> GetAllImages()
      {
         throw new NotImplementedException();

         return null;
         ////return this.imageControllers.Values.ToList();
      }

      public void RemoveActiveImage()
      {
         int activeImageIndex = this.imageManagerService.ActiveImageIndex;

         this.imageManagerService.RemoveActiveImage();

         //this.TriggerRemoveActiveImageIndex();

         activeImageIndex = Math.Min(activeImageIndex, this.imageManagerService.ImageCount - 1);

         // Restore the expected active image index as the removal could have changed it
         this.imageManagerService.ActiveImageIndex = activeImageIndex;

         //this.TriggerActiveImageIndexChanged();
      }

      public void RemoveAllImages()
      {
         while (this.imageManagerService.ImageCount > 0)
         {
            this.RemoveActiveImage();
         }
      }

      public void SetActiveImageIndex(int activeImageIndex)
      {
         if (activeImageIndex >= 0)
         {
            this.imageManagerService.ActiveImageIndex = activeImageIndex;
         }
      }

      //private void TriggerImageAdded()
      //{
      //   this.ImageAdded?.Invoke(this, EventArgs.Empty);
      //}

      //private void TriggerActiveImageIndexChanged()
      //{
      //   if (this.ActiveImageChanged != null)
      //   {
      //      this.ActiveImageChanged(this, EventArgs.Empty);
      //   }
      //}

      //private void TriggerRemoveActiveImageIndex()
      //{
      //   if (this.RemoveActiveImageIndex != null)
      //   {
      //      this.RemoveActiveImageIndex(this, EventArgs.Empty);
      //   }
      //}
   }
}
