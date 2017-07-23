namespace ImagingInterface
{
   using System;
   using ImagingInterface.Views;

   public class ImageViewFactory : IImageViewFactory
    {
        private Func<ImageView> imageViewFactory;

        public ImageViewFactory(Func<ImageView> imageViewFactory)
        {
            this.imageViewFactory = imageViewFactory;
        }

        public ImageView CreateNew()
        {
            return this.imageViewFactory();
        }
    }
}
