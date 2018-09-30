// <copyright file="IImageService.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Plugins
{
   public interface IImageService
   {
      IImageSource ImageSource
      {
         get;
      }
   }
}
