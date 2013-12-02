namespace ImagingInterface.Controllers
   {
   using System;
   using ImagingInterface.Views;
   using ImagingInterface.Views.EventArguments;
   using Microsoft.Practices.ServiceLocation;

   public class FileController : IFileController
      {
      private readonly IFileView fileView;
      private IServiceLocator serviceLocator;

      public FileController(IFileView fileView, IServiceLocator serviceLocator)
         {
         this.fileView = fileView;
         this.serviceLocator = serviceLocator;

         this.fileView.FileOpen += this.FileOpen;
         this.fileView.FileClose += this.FileClose;
         this.fileView.FileCloseAll += this.FileCloseAll;
         this.fileView.DragDropFile += this.DragDropFile;
         }

      private void FileOpen(object sender, EventArgs e)
         {
         string[] files = this.fileView.OpenFile();

         if (files != null && files.Length != 0)
            {
            foreach (string file in files)
               {
               IImageController imageController = this.serviceLocator.GetInstance<IImageController>();

               if (imageController.LoadImage(file))
                  {
                  imageController.Add();
                  }
               }
            }
         }

      private void FileClose(object sender, EventArgs e)
         {
         IImageManagerController imageViewManagerController = this.serviceLocator.GetInstance<IImageManagerController>();
         IImageController activeImageController = imageViewManagerController.GetActiveImageController();

         if (activeImageController != null)
            {
            activeImageController.Remove();
            (activeImageController as IDisposable).Dispose();
            }
         }

      private void FileCloseAll(object sender, EventArgs e)
         {
         IImageManagerController imageViewManagerController = this.serviceLocator.GetInstance<IImageManagerController>();
         IImageController activeImageController;

         activeImageController = imageViewManagerController.GetActiveImageController();

         while (activeImageController != null)
            {
            activeImageController.Remove();
            (activeImageController as IDisposable).Dispose();

            activeImageController = imageViewManagerController.GetActiveImageController();
            }

         GC.Collect();
         }

      private void DragDropFile(object sender, DragDropEventArgs e)
         {
         if (e.Data != null)
            {
            foreach (string file in e.Data)
               {
               IImageController imageController = this.serviceLocator.GetInstance<IImageController>();

               if (imageController.LoadImage(file))
                  {
                  imageController.Add();
                  }
               }
            }
         }
      }
   }
