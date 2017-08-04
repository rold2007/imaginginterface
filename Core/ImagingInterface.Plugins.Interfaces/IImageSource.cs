namespace ImagingInterface.Plugins
{
   using System;

   public interface IImageSource
   {
      event EventHandler ImageDataUpdated;

      byte[,,] OriginalImageData
      {
         get;
      }

      byte[,,] UpdatedImageData
      {
         get;
      }

      string ImageName
      {
         get;
      }

      bool IsDynamic(IRawPluginModel rawPluginModel);

      byte[,,] NextImageData(IRawPluginModel rawPluginModel);

      void UpdateImageData(byte[,,] updatedImageData);

      void Disconnected();
   }
}
