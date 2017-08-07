// <copyright file="FileSourceFactory.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.Tests.Mocks
{
   using ImagingInterface.Plugins;

   public class FileSourceFactory : IFileSourceFactory
    {
        public IFileSource CreateNew()
        {
            return new FileSourceController();
        }
    }
}
