namespace ImagingInterface.Controllers
{
    using System;
    using ImagingInterface.Plugins;

    public class ImageSourceRemovedEventArgs : EventArgs
    {
        public ImageSourceRemovedEventArgs(IImageSource imageSource)
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
