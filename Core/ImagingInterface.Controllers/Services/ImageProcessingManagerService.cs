namespace ImagingInterface.Controllers.Services
{
   using ImagingInterface.Plugins;

   public class ImageProcessingManagerService : IImageProcessingManagerService
   {
      private ImageManagerService imageManagerService;

      public ImageProcessingManagerService(ImageManagerService imageManagerService)
      {
         this.imageManagerService = imageManagerService;
      }

      // Apply processing to current active image synchronously
      public void AddOneShotImageProcessingToActiveImage(IImageProcessingService imageProcessingController)
      {
         int activeImageIndex = this.imageManagerService.ActiveImageIndex;
         IImageSource imageSource = this.imageManagerService.GetImageFromIndex(activeImageIndex);

         byte[,,] updatedImageData = imageProcessingController.ProcessImageData(imageSource.OriginalImageData, null, null);

         imageSource.UpdateImageData(updatedImageData);
      }
   }
}
