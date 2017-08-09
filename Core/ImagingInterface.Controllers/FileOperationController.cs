// <copyright file="FileOperationController.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers
{
   using System.Collections.Generic;
   using ImagingInterface.Controllers.Interfaces;
   using ImagingInterface.Controllers.Services;
   using ImagingInterface.Controllers.Views;
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
         if (imageView != null)
         {
            this.applicationLogic.RemoveImage(imageView);

            this.fileOperationService.RemoveImage(imageView.ImageSource);
         }
      }

      public void CloseAllFiles()
      {
         ////if (this.CloseAllFiles != null)
         ////   {
         ////   this.CloseAllFiles(this, EventArgs.Empty);

         ////   GC.Collect();
         ////   }
      }

      public void RequestDragDropFile(string[] data)
      {
         this.OpenFiles(data);
      }
   }
}
