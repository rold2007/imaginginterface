namespace ImagingInterface.Controllers
   {
   using ImagingInterface.Models;
   using ImagingInterface.Views;

   public class ImageController : IImageController
      {
      private readonly IImageModel imageModel;
      private readonly IImageView imageView;

      public ImageController(IImageView imageView, IImageModel imageModel)
         {
         this.imageView = imageView;
         this.imageModel = imageModel;
         }

      public void LoadFile(string file)
         {
         this.imageModel.LoadFile(file);
         }

      public void Show()
         {
         this.imageView.Show(this.imageModel);
         }
      }
   }
