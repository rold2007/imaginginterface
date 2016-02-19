namespace ImagingInterface.Controllers
   {
   using System;
   using ImagingInterface.Controllers.EventArguments;
   using ImagingInterface.Plugins;
   using Microsoft.Practices.ServiceLocation;

   public class FileOperationController
      {
      ////private readonly IFileOperationView fileOperationView;
      private IServiceLocator serviceLocator;

      public FileOperationController(IServiceLocator serviceLocator)
         {
         ////this.fileOperationView = fileOperationView;
         this.serviceLocator = serviceLocator;

         ////this.fileOperationView.FileOpen += this.FileOpen;
         ////this.fileOperationView.FileClose += this.FileClose;
         ////this.fileOperationView.FileCloseAll += this.FileCloseAll;
         ////this.fileOperationView.DragDropFile += this.DragDropFile;
         }

      private void FileOpen(object sender, EventArgs e)
         {
         ////string[] files = this.fileOperationView.OpenFile();

         ////if (files != null && files.Length != 0)
         ////   {
         ////   foreach (string file in files)
         ////      {
         ////      IFileSourceController fileSourceController = this.serviceLocator.GetInstance<IFileSourceController>();
         ////      ImageController imageController = this.serviceLocator.GetInstance<ImageController>();
         ////      ImageManagerController imageManagerController = this.serviceLocator.GetInstance<ImageManagerController>();

         ////      fileSourceController.Filename = file;
         ////      imageController.SetDisplayName(file);

         ////      imageController.InitializeImageSourceController(fileSourceController, fileSourceController.RawPluginModel);

         ////      imageManagerController.AddImage(imageController);
         ////      }
         ////   }
         }

      private void FileClose(object sender, EventArgs e)
         {
         ImageManagerController imageViewManagerController = this.serviceLocator.GetInstance<ImageManagerController>();
         ImageController activeImageController = imageViewManagerController.GetActiveImage();

         if (activeImageController != null)
            {
            activeImageController.Close();
            }
         }

      private void FileCloseAll(object sender, EventArgs e)
         {
         ImageManagerController imageViewManagerController = this.serviceLocator.GetInstance<ImageManagerController>();

         foreach (ImageController imageController in imageViewManagerController.GetAllImages())
            {
            imageController.Close();
            }

         GC.Collect();
         }

      private void DragDropFile(object sender, DragDropEventArgs e)
         {
         if (e.Data != null)
            {
            foreach (string file in e.Data)
               {
               IFileSourceController fileSourceController = this.serviceLocator.GetInstance<IFileSourceController>();
               ImageController imageController = this.serviceLocator.GetInstance<ImageController>();
               ImageManagerController imageManagerController = this.serviceLocator.GetInstance<ImageManagerController>();

               fileSourceController.Filename = file;
               imageController.SetDisplayName(file);

               imageController.InitializeImageSourceController(fileSourceController, fileSourceController.RawPluginModel);

               imageManagerController.AddImage(imageController);
               }
            }
         }
      }
   }
