namespace ImagingInterface.Controllers.EventArguments
{
   using System;
   using ImagingInterface.Controllers.Views;

   public class ImageViewRemovedEventArgs : EventArgs
   {
      public ImageViewRemovedEventArgs(IImageView imageView)
      {
         this.ImageView = imageView;
      }

      public IImageView ImageView
      {
         get;
         private set;
      }
   }
}
