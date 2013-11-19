namespace ImagingInterface.Controllers.Tests.Views
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Models;
   using ImagingInterface.Views;

   public class ImageViewManager : IImageManagerView
      {
      private List<IImageView> allImageViews;

      public ImageViewManager()
         {
         this.allImageViews = new List<IImageView>();
         }

      public void AddImageView(IImageView imageView, IImageModel imageModel)
         {
         this.allImageViews.Add(imageView);
         }

      public IImageView GetActiveImageView()
         {
         if (this.allImageViews.Count == 0)
            {
            return null;
            }
         else
            {
            return this.allImageViews[0];
            }
         }

      public void RemoveImageView(IImageView imageView)
         {
         this.allImageViews.Remove(imageView);
         }
      }
   }
