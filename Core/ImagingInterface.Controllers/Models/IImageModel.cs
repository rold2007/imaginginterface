namespace ImagingInterface.Models
   {
   using System;
   using System.Collections.Generic;
   using System.Drawing;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Plugins;

   public interface IImageModel : IRawImageModel
      {
      byte[, ,] SourceImageData
         {
         get;
         set;
         }

      byte[, ,] DisplayImageData
         {
         get;
         set;
         }

      byte[] OverlayImageData
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

      double ZoomLevel
         {
         get;
         set;
         }
      }
   }
