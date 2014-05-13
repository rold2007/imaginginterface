namespace ImagingInterface.Plugins
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;

   public interface IImageManagerController
      {
      void AddImage(IImageController imageController);

      IImageController GetActiveImage();

      IList<IImageController> GetAllImages();
      }
   }
