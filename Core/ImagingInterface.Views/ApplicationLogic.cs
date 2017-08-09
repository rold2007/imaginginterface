// <copyright file="ApplicationLogic.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Views
{
   using System;
   using System.Collections.Generic;
   using ImagingInterface.Controllers.Interfaces;
   using ImagingInterface.Controllers.Views;
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

      public void ManageNewImageSources(IEnumerable<IImageSource> imageSources)
      {
         this.ValidateImageManagerView();

         this.AddImageViewToCurrentImageManagerView(imageSources);
      }

      public void RemoveImage(IImageView imageView)
      {
         this.ValidateImageManagerView();

         this.RemoveImageViewFromCurrentImageManagerView(imageView);
      }

      private void ValidateImageManagerView()
      {
         if (this.imageManagerView == null)
         {
            throw new InvalidOperationException("The ImageManagerView was not initialized.");
         }
      }

      private void AddImageViewToCurrentImageManagerView(IEnumerable<IImageSource> imageSources)
      {
         foreach (IImageSource imageSource in imageSources)
         {
            ImageView imageView = this.imageViewFactory.CreateNew();

            imageView.ImageSource = imageSource;

            this.imageManagerView.AddImageToNewtab(imageView);
         }
      }

      private void RemoveImageViewFromCurrentImageManagerView(IImageView imageView)
      {
         ImageView imageViewObject = imageView as ImageView;

         if (imageViewObject == null)
         {
            throw new ArgumentException("imageView");
         }

         this.imageManagerView.RemoveImageView(imageViewObject);

         imageViewObject.Close();
      }
   }
}
