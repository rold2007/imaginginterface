// <copyright file="IImageSource.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Plugins
{
   using System;
   using System.Diagnostics.CodeAnalysis;

   public interface IImageSource
   {
      event EventHandler ImageDataUpdated;

      [SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", Justification = "Too much work for now.")]
      [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Too much work for now.")]
      byte[,,] OriginalImageData
      {
         get;
      }

      [SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", Justification = "Too much work for now.")]
      [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Too much work for now.")]
      byte[,,] UpdatedImageData
      {
         get;
      }

      string ImageName
      {
         get;
      }

      [SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", Justification = "Too much work for now.")]
      void UpdateImageData(byte[,,] updatedImageData);

      void Disconnected();
   }
}
