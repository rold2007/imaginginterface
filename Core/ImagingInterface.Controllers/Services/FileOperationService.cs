// <copyright file="FileOperationService.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.Services
{
   using System.Collections.Generic;
   using ImagingInterface.Plugins;

   public class FileOperationService
   {
      private ImageSourceService imageSourceService;

      public FileOperationService(ImageSourceService imageSourceService)
      {
         this.imageSourceService = imageSourceService;
      }

      public IEnumerable<IImageSource> AddImages(string[] files)
      {
         IEnumerable<IImageSource> imageSources = this.imageSourceService.AddImageFiles(files);

         return imageSources;
      }

      public void RemoveImage(IImageSource imageSource)
      {
         this.imageSourceService.RemoveImageSource(imageSource);
      }
   }
}
