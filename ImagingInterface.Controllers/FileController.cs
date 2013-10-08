namespace ImagingInterface.Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Controllers;
   using ImagingInterface.Views;
   using Microsoft.Practices.ServiceLocation;
   using System.Diagnostics;

   public class FileController : IFileController
      {
      private readonly IFileView fileView;

      public FileController(IFileView fileView)
         {
         this.fileView = fileView;

         this.fileView.FileOpen += this.FileOpen;
         }

      public void FileOpen()
         {
         string[] files = this.fileView.OpenFile();

         foreach (string file in files)
            {
            IImageController imageController = ServiceLocator.Current.GetInstance<IImageController>();

            imageController.LoadFile(file);

            imageController.Show();
            }
         }
      }
   }
