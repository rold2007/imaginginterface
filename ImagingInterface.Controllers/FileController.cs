namespace ImagingInterface.Controllers
   {
   using System;
   using ImagingInterface.Views;
   using Microsoft.Practices.ServiceLocation;

   public class FileController : IFileController
      {
      private readonly IFileView fileView;

      public FileController(IFileView fileView)
         {
         this.fileView = fileView;

         this.fileView.FileOpen += this.FileOpen;
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
      }
   }
