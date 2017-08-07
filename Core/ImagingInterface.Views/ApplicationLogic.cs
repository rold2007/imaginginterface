namespace ImagingInterface.Views
{
   using System;
   using System.Collections.Generic;
   using System.Diagnostics;
   using ImagingInterface.Controllers.Interfaces;
   using ImagingInterface.Plugins;

   public class ApplicationLogic : IApplicationLogic
   {
      private IImageViewFactory imageViewFactory;
      private ImageManagerView imageManagerView;

      public ApplicationLogic(IImageViewFactory imageViewFactory)
      {
         this.imageViewFactory = imageViewFactory;
      }

      public void AddImageManagerView(ImageManagerView imageManagerView)
      {
         if (this.imageManagerView != null)
         {
            throw new InvalidOperationException("The ImageManagerView is already initialized.");
         }

         this.imageManagerView = imageManagerView;
      }

      public void AddImageViewToCurrentImageManagerView(IEnumerable<IImageSource> imageSources)
      {
      }

      public void ManageNewImageSources(IEnumerable<IImageSource> imageSources)
      {
         if (this.imageManagerView == null)
         {
            throw new InvalidOperationException("The ImageManagerView was not initialized.");
         }

         foreach (IImageSource imageSource in imageSources)
         {
            ImageView imageView = this.imageViewFactory.CreateNew();

            imageView.ImageSource = imageSource;

            this.imageManagerView.AddImageToNewtab(imageView);
         }
      }
   }
}
