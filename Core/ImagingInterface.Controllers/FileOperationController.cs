namespace ImagingInterface.Controllers
   {
   using System;
   using System.Collections.Generic;
   using ImagingInterface.Controllers.EventArguments;
   using ImagingInterface.Models;
   using ImagingInterface.Plugins;

   public class FileOperationController
      {
      private FileOperationModel fileOperationModel;

      public FileOperationController(FileOperationModel fileOperationModel)
         {
         this.fileOperationModel = fileOperationModel;
         }

      public IList<IFileSource> OpenFiles(string[] files)
         {
         List<IFileSource> fileSourceControllers = null;

         if (files != null && files.Length != 0)
            {
            fileSourceControllers = new List<IFileSource>();

            foreach (string file in files)
               {
               IFileSource fileSourceController = this.fileOperationModel.OpenFile(file);

               fileSourceControllers.Add(fileSourceController);
               }
            }

         return fileSourceControllers;
         }

      public void RequestCloseFile()
         {
         ////if (this.CloseFile != null)
         ////   {
         ////   this.CloseFile(this, EventArgs.Empty);
         ////   }
         }

      public void RequestCloseAllFiles()
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
