namespace ImagingInterface.Models
   {
   using System;
   using System.Collections.Generic;
   using System.Drawing;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;

   public interface IImageModel : IRawImageModel
      {
      byte[, ,] DisplayImageData
         {
         get;
         set;
         }

      Size Size
         {
         get;
         }

      bool IsGrayscale
         {
         get;
         }
      }
   }
