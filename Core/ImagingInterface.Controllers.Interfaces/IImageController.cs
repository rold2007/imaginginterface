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
      byte[,,] ImageData
         {
         get;
         }

      bool LoadImage(byte[,,] imageData, string displayName);

      bool LoadImage(string file);

      void UpdateImageData(byte[,,] imageData);

      void Add();

      void Close();
      }
   }
