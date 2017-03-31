namespace ImagingInterface.Controllers
{
    using System;
    using System.Collections.Generic;
    using ImagingInterface.Models;
    using ImagingInterface.Plugins;

    public class ImageManagerController
    {
        private ImageManagerModel imageManagerModel;

        public ImageManagerController(ImageManagerModel imageManagerModel)
        {
            this.imageManagerModel = imageManagerModel;
        }

        //public event EventHandler ImageAdded;

        //public event EventHandler ActiveImageChanged;

        //public event EventHandler RemoveActiveImageIndex;

        public int ActiveImageIndex
        {
            get
            {
                return this.imageManagerModel.ActiveImageIndex;
            }
        }

        public int ImageCount
        {
            get
            {
                return this.imageManagerModel.ImageCount;
            }
        }

        //public IImageManagerModel ImageManagerModel
        //   {
        //   get
        //      {
        //      return this.imageManagerModel;
        //      }
        //   }

        public void AddImage(IImageSource imageSource)
        {
            this.imageManagerModel.AddImage();

            //this.TriggerActiveImageIndexChanged();
        }

        public void AddImages(IList<IImageSource> imageSources)
        {
            foreach (IImageSource imageSource in imageSources)
            {
                this.imageManagerModel.AddImage();

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
            int activeImageIndex = this.imageManagerModel.ActiveImageIndex;

            this.imageManagerModel.RemoveActiveImage();

            //this.TriggerRemoveActiveImageIndex();

            activeImageIndex = Math.Min(activeImageIndex, this.imageManagerModel.ImageCount - 1);

            // Restore the expected active image index as the removal could have changed it
            this.imageManagerModel.ActiveImageIndex = activeImageIndex;

            //this.TriggerActiveImageIndexChanged();
        }

        public void RemoveAllImages()
        {
            while (this.imageManagerModel.ImageCount > 0)
            {
                this.RemoveActiveImage();
            }
        }

        public void SetActiveImageIndex(int activeImageIndex)
        {
            if (activeImageIndex >= 0)
            {
                this.imageManagerModel.ActiveImageIndex = activeImageIndex;
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
