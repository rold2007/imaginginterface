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
      IImage Image
         {
         get;
         set;
         }
      }
   }
