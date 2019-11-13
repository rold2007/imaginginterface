// <copyright file="IImageProcessingService.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Plugins
{
   using System.Diagnostics.CodeAnalysis;

   public interface IImageProcessingService
   {
      [SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", Justification = "Too much work for now.")]
      void ProcessImageData(byte[,,] imageData, byte[] overlayData);
   }
}
