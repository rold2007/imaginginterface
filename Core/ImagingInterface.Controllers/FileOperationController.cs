// <copyright file="FileOperationController.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers
{
   using System.Collections.Generic;
   using System.Linq;
   using ImagingInterface.Controllers.Interfaces;
   using ImagingInterface.Controllers.Services;
   using ImagingInterface.Plugins;

   public class FileOperationController
   {
      private FileOperationService fileOperationService;
      private IApplicationLogic applicationLogic;

      public FileOperationController(FileOperationService fileOperationService, IApplicationLogic applicationLogic)
      {
         this.fileOperationService = fileOperationService;
         this.applicationLogic = applicationLogic;
      }

      public void OpenFiles(string[] files)
      {
         if (files != null)
         {
            IEnumerable<IImageSource> imageSources = this.fileOperationService.AddImages(files);

            this.applicationLogic.ManageNewImageSources(imageSources);
         }
      }

      public void CloseFile(IImageView imageView)
      {
         this.RemoveImage(imageView);
      }

      public void CloseFiles(IEnumerable<IImageView> imageViews)
      {
         // Copy to allow to remove items in the IEnumerable
         IEnumerable<IImageView> imageViewsClone = imageViews.ToList();

         foreach (IImageView imageView in imageViewsClone)
         {
            this.RemoveImage(imageView);
         }
      }

      public void RequestDragDropFile(string[] data)
      {
         this.OpenFiles(data);
      }

      private void RemoveImage(IImageView imageView)
      {
         if (imageView != null)
         {
            this.applicationLogic.RemoveImage(imageView);

            this.fileOperationService.RemoveImage(imageView.ImageSource);
         }
      }
   }
}
