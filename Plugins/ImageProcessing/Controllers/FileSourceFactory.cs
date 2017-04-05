namespace ImageProcessing.Controllers
{
    using ImagingInterface.Controllers;
    using ImagingInterface.Plugins;

    public sealed class FileSourceFactory : IFileSourceFactory
    {
        public IFileSource CreateNew()
        {
            return new ImageProcessorFileSource();
        }
    }
}
