namespace ImagingInterface.Models
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using Emgu.CV;

   public interface IImageModel
      {
      string DisplayName
         {
         get;
         set;
         }

      IImage Image
         {
         get;
         set;
         }
      }
   }
