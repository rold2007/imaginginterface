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
      public IImage Image
         {
         get;
         private set;
         }

      public ImageModel()
         {
         }

      public void LoadFile(string filename)
         {
         this.Image = new Image<Bgra, byte>(filename);
         }
      }
   }
