namespace Video.Controllers.Tests.Views
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
      private IRawImageView activeRawImageView;

      public void AddImage(IRawImageView rawImageView, IRawImageModel rawImageModel)
         {
         this.activeRawImageView = rawImageView;
         }

      public IRawImageView GetActiveImageView()
         {
         return this.activeRawImageView;
         }

      public void RemoveImage(IRawImageView rawImageView)
         {
         if (rawImageView == this.activeRawImageView)
            {
            this.activeRawImageView = null;
            }
         }
      }
   }
