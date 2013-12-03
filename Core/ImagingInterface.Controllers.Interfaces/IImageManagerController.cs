namespace ImagingInterface.Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Controllers;
   using ImagingInterface.Models;
   using ImagingInterface.Views;

   public interface IImageManagerController
      {
      void AddImageController(IImageController imageController, IRawImageView rawImageView, IRawImageModel rawImageModel);

      IImageController GetActiveImageController();
      
      void RemoveImageController(IRawImageView rawImageView);
      }
   }
