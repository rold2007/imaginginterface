namespace ImagingInterface.Controllers
   {
   using Emgu.CV;
   using Emgu.CV.Structure;
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

      public void LoadFile(string filename)
         {
         this.imageModel.Image = new Image<Bgra, byte>(filename);
         }

      public void Show()
         {
         this.imageView.Show(this.imageModel);
         }
      }
   }
