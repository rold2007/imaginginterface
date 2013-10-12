namespace ImagingInterface.Controllers
   {
   using Emgu.CV;
   using Emgu.CV.Structure;
   using ImagingInterface.Models;
   using ImagingInterface.Views;
   using Microsoft.Practices.ServiceLocation;

   public class ImageController : IImageController
      {
      public IImageModel ImageModel
         {
         get;
         private set;
         }

      public IImageView ImageView
         {
         get;
         private set;
         }

      public ImageController(IImageView imageView, IImageModel imageModel)
         {
         this.ImageView = imageView;
         this.ImageModel = imageModel;
         }

      public void LoadFile(string filename)
         {
         this.ImageModel.DisplayName = filename;
         this.ImageModel.Image = new Image<Bgra, byte>(filename);
         this.ImageView.AssignImage(this.ImageModel);
         }

      public void Show()
         {
         IImageViewManagerController imageViewManagerController = ServiceLocator.Current.GetInstance<IImageViewManagerController>();

         imageViewManagerController.AddImage(this);
         }

      public void Close()
         {
         IImageViewManagerController imageViewManagerController = ServiceLocator.Current.GetInstance<IImageViewManagerController>();

         imageViewManagerController.RemoveImage(this);

         IImage image = this.ImageModel.Image;

         this.ImageModel.Image = null;
         this.ImageView.AssignImage(this.ImageModel);
         
         image.Dispose();

         this.ImageView.Close();
         this.ImageView = null;
         this.ImageModel = null;
         }
      }
   }
