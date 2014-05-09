namespace ImagingInterface.Controllers
   {
   using System;
   using ImagingInterface.Plugins;
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
               IFileSourceController fileSourceController = this.serviceLocator.GetInstance<IFileSourceController>();
               IImageController imageController = this.serviceLocator.GetInstance<IImageController>();
               IImageManagerController imageManagerController = this.serviceLocator.GetInstance<IImageManagerController>();

               fileSourceController.Filename = file;
               imageController.SetDisplayName(file);

               imageController.InitializeImageSourceController(fileSourceController, fileSourceController.RawPluginModel);

               imageManagerController.AddImage(imageController);
               }
            }
         }

      private void FileClose(object sender, EventArgs e)
         {
         IImageManagerController imageViewManagerController = this.serviceLocator.GetInstance<IImageManagerController>();
         IImageController activeImageController = imageViewManagerController.GetActiveImage();

         if (activeImageController != null)
            {
            activeImageController.Close();
            }
         }

      private void FileCloseAll(object sender, EventArgs e)
         {
         IImageManagerController imageViewManagerController = this.serviceLocator.GetInstance<IImageManagerController>();

         foreach (IImageController imageController in imageViewManagerController.GetAllImages())
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
               IImageController imageController = this.serviceLocator.GetInstance<IImageController>();
               IImageManagerController imageManagerController = this.serviceLocator.GetInstance<IImageManagerController>();

               fileSourceController.Filename = file;
               imageController.SetDisplayName(file);

               imageController.InitializeImageSourceController(fileSourceController, fileSourceController.RawPluginModel);

               imageManagerController.AddImage(imageController);
               }
            }
         }
      }
   }
