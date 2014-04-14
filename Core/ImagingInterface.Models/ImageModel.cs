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

      public byte[, ,] DisplayImageData
         {
         get;
         set;
         }

      public System.Drawing.Size Size
         {
         get
            {
            return new System.Drawing.Size(this.DisplayImageData.GetLength(1), this.DisplayImageData.GetLength(0));
            }
         }

      public bool IsGrayscale
         {
         get
            {
            return this.DisplayImageData.GetLength(2) == 1;
            }
         }
      }
   }
