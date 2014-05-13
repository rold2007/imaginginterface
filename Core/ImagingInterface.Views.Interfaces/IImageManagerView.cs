namespace ImagingInterface.Views
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using System.Windows.Forms;
   using ImagingInterface.Plugins;

   public interface IImageManagerView
      {
      void AddImage(IRawImageView imageView, IRawImageModel imageModel);

      IRawImageView GetActiveImageView();
      
      void RemoveImage(IRawImageView imageView);
      }
   }
