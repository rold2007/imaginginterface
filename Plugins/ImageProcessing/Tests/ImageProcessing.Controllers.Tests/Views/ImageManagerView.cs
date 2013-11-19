namespace ImageProcessing.Controllers.Tests.Views
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Models;
   using ImagingInterface.Views;

   public class ImageManagerView : IImageManagerView
      {
      private IImageView activeImageView;

      public void AddImageView(IImageView imageView, IImageModel imageModel)
         {
         this.activeImageView = imageView;
         }

      public IImageView GetActiveImageView()
         {
         return this.activeImageView;
         }

      public void RemoveImageView(IImageView imageView)
         {
         if (imageView == this.activeImageView)
            {
            this.activeImageView = null;
            }
         }
      }
   }
