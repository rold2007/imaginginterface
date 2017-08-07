// <copyright file="ImageSourceAddedEventArgs.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.EventArguments
{
    using System;
    using ImagingInterface.Plugins;

    public class ImageSourceAddedEventArgs : EventArgs
    {
        public ImageSourceAddedEventArgs(IImageSource imageSource)
        {
            this.ImageSource = imageSource;
        }

        public IImageSource ImageSource
        {
            get;
            private set;
        }
    }
}
