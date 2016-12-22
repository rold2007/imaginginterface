namespace ImagingInterface.Controllers
{
   using System;
   using System.Collections.Generic;
   using ImagingInterface.Controllers.EventArguments;
   using ImagingInterface.Models;
   using ImagingInterface.Plugins;
   using Microsoft.Practices.ServiceLocation;

   public class FileOperationController
   {
      private FileOperationModel fileOperationModel;
      private IServiceLocator serviceLocator;

      public FileOperationController(FileOperationModel fileOperationModel, IServiceLocator serviceLocator)
      {
         this.fileOperationModel = fileOperationModel;
         this.serviceLocator = serviceLocator;
      }

      public IList<IFileSource> OpenFiles(string[] files)
      {
         List<IFileSource> fileSources = null;

         if (files != null && files.Length != 0)
         {
            fileSources = new List<IFileSource>();

            foreach (string file in files)
            {
               IFileSource fileSource = this.OpenFile(file);

               if (fileSource != null)
               {
                  fileSources.Add(fileSource);
               }
            }
         }

         return fileSources;
      }

      ////public void CloseFile(IFileSource fileSourceController)
      ////{
      ////}

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

      private IFileSource OpenFile(string file)
      {
         IFileSource fileSource = this.serviceLocator.GetInstance<IFileSource>();

         if (fileSource.LoadFile(file))
         {
            return fileSource;
         }
         else
         {
            return null;
         }
      }
   }
}
