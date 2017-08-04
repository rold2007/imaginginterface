namespace ImagingInterface.Controllers.Views
{
   using ImagingInterface.Plugins;

   public interface IImageView
   {
      IImageSource ImageSource
      {
         get;
      }
   }
}
