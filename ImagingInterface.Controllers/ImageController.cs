namespace ImagingInterface.Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.Diagnostics;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Views;
   using ImagingInterface.Models;

   public class ImageController : IImageController
      {
      private readonly IImageModel imageModel;
      private readonly IImageView imageView;

      public ImageController(IImageView imageView, IImageModel imageModel)
         {
         this.imageView = imageView;
         this.imageModel = imageModel;
         }

      public void LoadFile(string file)
         {
         this.imageModel.LoadFile(file);
         }

      public void Show()
         {
         this.imageView.Show(this.imageModel);
         }
      }
   }
