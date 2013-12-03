namespace ImagingInterface.Controllers
   {
   using System;
   using ImagingInterface.Views;
   using ImagingInterface.Views.EventArguments;
   using Microsoft.Practices.ServiceLocation;

   public class FileOperationController : IFileOperationController
      {
      private readonly IFileOperationView fileOperationView;
      private IServiceLocator serviceLocator;

      public FileOperationController(IFileOperationView fileOperationView, IServiceLocator serviceLocator)
         {
         this.fileOperationView = fileOperationView;
         this.serviceLocator = serviceLocator;

         this.fileOperationView.FileOpen += this.FileOpen;
         this.fileOperationView.FileClose += this.FileClose;
         this.fileOperationView.FileCloseAll += this.FileCloseAll;
         this.fileOperationView.DragDropFile += this.DragDropFile;
         }

      private void FileOpen(object sender, EventArgs e)
         {
         string[] files = this.fileOperationView.OpenFile();

         if (files != null && files.Length != 0)
            {
            foreach (string file in files)
               {
               IImageController imageController = this.serviceLocator.GetInstance<IImageController>();

               if (imageController.LoadImage(file))
                  {
                  imageController.Add();
                  }
               else
                  {
                  imageController.Close();
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
            activeImageController.Close();
            }
         }

      private void FileCloseAll(object sender, EventArgs e)
         {
         IImageManagerController imageViewManagerController = this.serviceLocator.GetInstance<IImageManagerController>();
         IImageController activeImageController;

         activeImageController = imageViewManagerController.GetActiveImageController();

         while (activeImageController != null)
            {
            activeImageController.Close();

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
               else
                  {
                  imageController.Close();
                  }
               }
            }
         }
      }
   }
