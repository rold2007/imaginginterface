namespace ImagingInterface.Controllers.Tests.Mocks
{
    using System;
    using ImagingInterface.Plugins;

    public class FileSourceFactory : IFileSourceFactory
    {
        public IFileSource CreateNew()
        {
            return new FileSourceController();
        }
    }
}
