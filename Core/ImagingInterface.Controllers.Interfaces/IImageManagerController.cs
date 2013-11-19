namespace ImagingInterface.Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Controllers;

   public interface IImageManagerController
      {
      void AddImageController(IImageController imageController);

      IImageController GetActiveImageController();
      
      void RemoveImageController(IImageController imageController);
      }
   }
