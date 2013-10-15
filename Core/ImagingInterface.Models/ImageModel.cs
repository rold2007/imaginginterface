namespace ImagingInterface.Models
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using Emgu.CV;
   using Emgu.CV.Structure;

   public class ImageModel : IImageModel
      {
      public ImageModel()
         {
         }

      public string DisplayName
         {
         get;
         set;
         }

      public IImage Image
         {
         get;
         set;
         }
      }
   }
