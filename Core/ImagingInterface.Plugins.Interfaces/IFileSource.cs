// <copyright file="IFileSource.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Plugins
   {
   public interface IFileSource : IImageSource
   {
      string Filename
      {
         get;
      }

      bool LoadFile(string file);
   }
}
