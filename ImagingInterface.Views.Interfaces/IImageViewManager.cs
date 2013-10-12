namespace ImagingInterface.Views
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using System.Windows.Forms;
   using ImagingInterface.Models;

   public interface IImageViewManager
      {
      void AddImageView(IImageView imageView, IImageModel imageModel);

      IImageView GetActiveImage();
      
      void RemoveImageView(IImageView imageView);
      }
   }
