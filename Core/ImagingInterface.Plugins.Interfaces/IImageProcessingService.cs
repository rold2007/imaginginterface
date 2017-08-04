namespace ImagingInterface.Plugins
{
   public interface IImageProcessingService
   {
      byte[,,] ProcessImageData(byte[,,] imageData, byte[] overlayData, IRawPluginModel rawPluginModel);
   }
}
