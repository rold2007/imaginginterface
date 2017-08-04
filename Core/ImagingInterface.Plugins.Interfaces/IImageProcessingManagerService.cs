namespace ImagingInterface.Plugins
{
   public interface IImageProcessingManagerService
   {
      void AddOneShotImageProcessingToActiveImage(IImageProcessingService imageProcessingController);
   }
}
