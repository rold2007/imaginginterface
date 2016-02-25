namespace ImagingInterface.Controllers
   {
   using System;
   using System.Collections.Generic;
   using ImagingInterface.Controllers.EventArguments;
   using ImagingInterface.Plugins;
   using Microsoft.Practices.ServiceLocation;

   public class FileOperationController
      {
      private IServiceLocator serviceLocator;

      public FileOperationController(IServiceLocator serviceLocator)
         {
         this.serviceLocator = serviceLocator;
         }

      public event EventHandler<OpenFileEventArgs> OpenFile;

      public event EventHandler CloseFile;

      public event EventHandler CloseAllFiles;

      public void RequestOpenFile(string[] files)
         {
         if (files != null && files.Length != 0)
            {
            List<IFileSourceController> fileSourceControllers = new List<IFileSourceController>();

            foreach (string file in files)
               {
               IFileSourceController fileSourceController = this.serviceLocator.GetInstance<IFileSourceController>();

               fileSourceController.Filename = file;

               fileSourceControllers.Add(fileSourceController);
               }

            if (this.OpenFile != null)
               {
               this.OpenFile(this, new OpenFileEventArgs(fileSourceControllers));
               }
            }
         }

      public void RequestCloseFile()
         {
         if (this.CloseFile != null)
            {
            this.CloseFile(this, EventArgs.Empty);
            }
         }

      public void RequestCloseAllFiles()
         {
         if (this.CloseAllFiles != null)
            {
            this.CloseAllFiles(this, EventArgs.Empty);

            GC.Collect();
            }
         }

      public void RequestDragDropFile(string[] data)
         {
         this.RequestOpenFile(data);
         }
      }
   }
