namespace ImagingInterface.Views
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using System.Windows.Forms;
   using ImagingInterface.Models;

   public interface IImageManagerView
      {
      void AddImageView(IRawImageView imageView, IRawImageModel imageModel);

      IRawImageView GetActiveImageView();
      
      void RemoveImageView(IRawImageView imageView);
      }
   }
