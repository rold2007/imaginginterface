namespace ImagingInterface.Models
   {
   using System.Drawing;
   using ImageProcessor.Imaging.Colors;

   public interface IImageModel
      {
      string DisplayName
         {
         get;
         }

      ////byte[, ,] SourceImageData
      ////   {
      ////   get;
      ////   set;
      ////   }

      byte[,,] DisplayImageData
         {
         get;
         ////set;
         }

      ////byte[] OverlayImageData
      ////   {
      ////   get;
      ////   set;
      ////   }

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
         }

      RgbaColor GetRgbaPixel(Point pixelPosition);
      }
   }
