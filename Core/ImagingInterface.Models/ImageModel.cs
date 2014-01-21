namespace ImagingInterface.Models
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;

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

      public byte[,,] ImageData
         {
         get;
         set;
         }

      public System.Drawing.Size Size
         {
         get
            {
            return new System.Drawing.Size(this.ImageData.GetLength(1), this.ImageData.GetLength(0));
            }
         }

      public bool IsGrayscale
         {
         get
            {
            return this.ImageData.GetLength(2) == 1;
            }
         }
      }
   }
