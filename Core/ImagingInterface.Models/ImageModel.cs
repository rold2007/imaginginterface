namespace ImagingInterface.Models
   {
   using System;
   using System.Collections.Generic;
   using System.Drawing;
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

      public byte[, ,] SourceImageData
         {
         get;
         set;
         }

      public byte[, ,] DisplayImageData
         {
         get;
         set;
         }

      public Size Size
         {
         get
            {
            return new Size(this.DisplayImageData.GetLength(1), this.DisplayImageData.GetLength(0));
            }
         }

      public bool IsGrayscale
         {
         get
            {
            return this.DisplayImageData.GetLength(2) == 1;
            }
         }

      public double ZoomLevel
         {
         get;
         set;
         }
      }
   }
