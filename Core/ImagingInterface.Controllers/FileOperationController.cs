namespace ImagingInterface.Controllers
{
   using ImagingInterface.Controllers.Interfaces;
   using ImagingInterface.Controllers.Services;
   using ImagingInterface.Plugins;
   using System.Collections.Generic;

   public class FileOperationController
   {
      private ImageSourceService imageSourceService;
      private IApplicationLogic applicationLogic;

      public FileOperationController(ImageSourceService imageSourceService, IApplicationLogic applicationLogic)
      {
         this.imageSourceService = imageSourceService;
         this.applicationLogic = applicationLogic;
      }

      public void OpenFiles(string[] files)
      {
         if (files != null)
         {
            IEnumerable<IImageSource> imageSources = this.imageSourceService.AddImageFiles(files);

            this.applicationLogic.ManageNewImageSources(imageSources);
         }
      }

      public void CloseFile(IImageSource imageSource)
      {
         if (imageSource != null)
         {
            this.imageSourceService.RemoveImageSource(imageSource);
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
