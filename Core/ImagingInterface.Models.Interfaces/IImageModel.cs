namespace ImagingInterface.Models
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using Emgu.CV;
   using Emgu.CV.Structure;

   public interface IImageModel
      {
      string DisplayName
         {
         get;
         set;
         }

      Image<Bgra, byte> Image
         {
         get;
         set;
         }
      }
   }
