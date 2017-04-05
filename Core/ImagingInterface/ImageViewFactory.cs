namespace ImagingInterface
{
    using ImagingInterface.Views;
    using SimpleInjector;

    public class ImageViewFactory : IImageViewFactory
    {
        private Container container;

        public ImageViewFactory(Container container)
        {
            this.container = container;
        }

        public ImageView CreateNew()
        {
            return this.container.GetInstance<ImageView>();
        }
    }
}
