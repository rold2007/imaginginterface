namespace ImagingInterface.Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using Emgu.CV;
   using Emgu.CV.Structure;
   using ImagingInterface.Models;
   using ImagingInterface.Views;

   public interface IImageController
      {
      Image<Bgra, byte> Image
         {
         get;
         }

      bool LoadImage(Image<Bgra, byte> image, string displayName);

      bool LoadImage(string file);

      void UpdateImage();

      void Add();

      void Close();
      }
   }
