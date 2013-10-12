namespace ImagingInterface.Controllers
   {
   using System;
   using ImagingInterface.Views;
   using ImagingInterface.Views.EventArguments;
   using Microsoft.Practices.ServiceLocation;

   public class FileController : IFileController
      {
      private readonly IFileView fileView;

      public FileController(IFileView fileView)
         {
         this.fileView = fileView;

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
               IImageController imageController = ServiceLocator.Current.GetInstance<IImageController>();

               imageController.LoadFile(file);

               imageController.Show();
               }
            }
         }

      private void FileClose(object sender, EventArgs e)
         {
         IImageViewManagerController imageViewManagerController = ServiceLocator.Current.GetInstance<IImageViewManagerController>();
         IImageController activeImageController = imageViewManagerController.GetActiveImage();

         if (activeImageController != null)
            {
            activeImageController.Close();
            }
         }

      private void FileCloseAll(object sender, EventArgs e)
         {
         IImageViewManagerController imageViewManagerController = ServiceLocator.Current.GetInstance<IImageViewManagerController>();
         IImageController activeImageController;

         activeImageController = imageViewManagerController.GetActiveImage();

         while (activeImageController != null)
            {
            activeImageController.Close();

            activeImageController = imageViewManagerController.GetActiveImage();
            }

         GC.Collect();
         }

      private void DragDropFile(object sender, DragDropEventArgs e)
         {
         foreach (string file in e.Data)
            {
            IImageController imageController = ServiceLocator.Current.GetInstance<IImageController>();

            imageController.LoadFile(file);

            imageController.Show();
            }
         }
      }
   }
