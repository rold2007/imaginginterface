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
      byte[,,] ImageData
         {
         get;
         set;
         }

      System.Drawing.Size Size
         {
         get;
         }

      bool IsGrayscale
         {
         get;
         }
      }
   }
