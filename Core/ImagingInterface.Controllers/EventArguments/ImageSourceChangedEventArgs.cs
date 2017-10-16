// <copyright file="ImageSourceChangedEventArgs.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.EventArguments
{
    using System;
    using ImagingInterface.Plugins;

    public class ImageSourceChangedEventArgs : EventArgs
    {
        public ImageSourceChangedEventArgs(IImageSource imageSource)
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
