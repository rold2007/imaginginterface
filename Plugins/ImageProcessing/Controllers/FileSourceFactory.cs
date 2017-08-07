// <copyright file="FileSourceFactory.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Controllers
{
    using ImagingInterface.Plugins;

    public sealed class FileSourceFactory : IFileSourceFactory
    {
        public IFileSource CreateNew()
        {
            return new ImageProcessorFileSource();
        }
    }
}
