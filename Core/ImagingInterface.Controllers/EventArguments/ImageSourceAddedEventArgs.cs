namespace ImagingInterface.Controllers
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
