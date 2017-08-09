// <copyright file="ImageViewFactory.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.Views
{
   using System;
   using ImagingInterface.Plugins;

   public class ImageViewFactory : IImageViewFactory
    {
        private Func<IImageView> imageViewFactory;

        public ImageViewFactory(Func<IImageView> imageViewFactory)
        {
            this.imageViewFactory = imageViewFactory;
        }

        public IImageView CreateNew()
        {
            return this.imageViewFactory();
        }
    }
}
