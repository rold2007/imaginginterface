// <copyright file="IFileSourceFactory.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Plugins
{
    public interface IFileSourceFactory
    {
        IFileSource CreateNew();
    }
}
