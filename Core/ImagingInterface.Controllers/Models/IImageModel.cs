namespace ImagingInterface.Models.Interfaces
{
   using System.Drawing;
   using ImageProcessor.Imaging.Colors;
   using ImagingInterface.Plugins;

   public interface IImageModel
      {
      string DisplayName
         {
         get;
         }

      IImageSource ImageSource
      {
         get;
         set;
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
         set;
      }

      RgbaColor GetRgbaPixel(Point pixelPosition);
      }
   }
