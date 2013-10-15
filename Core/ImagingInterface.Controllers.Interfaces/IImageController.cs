namespace ImagingInterface.Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Models;
   using ImagingInterface.Views;

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

      bool LoadFile(string file);

      void Add();

      void Remove();
      }
   }
