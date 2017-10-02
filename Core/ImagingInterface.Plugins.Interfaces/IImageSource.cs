// <copyright file="IImageSource.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Plugins
{
   using System;

   public interface IImageSource
   {
      event EventHandler ImageDataUpdated;

      byte[,,] OriginalImageData
      {
         get;
      }

      byte[,,] UpdatedImageData
      {
         get;
      }

      string ImageName
      {
         get;
      }

      void UpdateImageData(byte[,,] updatedImageData);

      void Disconnected();
   }
}
