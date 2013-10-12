namespace ImagingInterface.Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Views;
   using ImagingInterface.Models;

   public interface IImageController
      {
      IImageView ImageView
         {
         get;
         }

      IImageModel ImageModel
         {
         get;
         }

      void LoadFile(string file);

      void Show();

      void Close();
      }
   }
