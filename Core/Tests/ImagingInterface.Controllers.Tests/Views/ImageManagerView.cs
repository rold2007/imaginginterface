namespace ImagingInterface.Controllers.Tests.Views
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
      private List<IRawImageView> allRawImageViews;

      public ImageManagerView()
         {
         this.allRawImageViews = new List<IRawImageView>();
         }

      public void AddImageView(IRawImageView rawImageView, IRawImageModel rawImageModel)
         {
         this.allRawImageViews.Add(rawImageView);
         }

      public IRawImageView GetActiveImageView()
         {
         if (this.allRawImageViews.Count == 0)
            {
            return null;
            }
         else
            {
            return this.allRawImageViews[0];
            }
         }

      public void RemoveImageView(IRawImageView rawImageView)
         {
         this.allRawImageViews.Remove(rawImageView);
         }
      }
   }
