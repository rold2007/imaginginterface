namespace ImagingInterface.Controllers
   {
   using System;
   using Emgu.CV;
   using Emgu.CV.Structure;
   using ImagingInterface.Models;
   using ImagingInterface.Views;
   using Microsoft.Practices.ServiceLocation;

   public class ImageController : IImageController, IDisposable
      {
      private bool imageControllerShown = false;
      private bool imageControllerClosed = true;
      
      public ImageController(IImageView imageView, IImageModel imageModel)
         {
         this.ImageView = imageView;
         this.ImageModel = imageModel;
         }

      ~ImageController()
         {
         this.Dispose(false);
         }

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

      public void Dispose()
         {
         this.Dispose(true);
         GC.SuppressFinalize(this);
         }

      public void LoadFile(string filename)
         {
         this.imageControllerClosed = false;

         this.ImageModel.DisplayName = filename;
         this.ImageModel.Image = new Image<Bgra, byte>(filename);
         this.ImageView.AssignImageModel(this.ImageModel);
         }

      public void Add()
         {
         IImageViewManagerController imageViewManagerController = ServiceLocator.Current.GetInstance<IImageViewManagerController>();

         imageViewManagerController.AddImageController(this);

         this.imageControllerShown = true;
         }

      public void Remove()
         {
         this.Hide();

         if (!this.imageControllerClosed)
            {
            IImage image = this.ImageModel.Image;

            this.ImageModel.Image = null;
            this.ImageView.AssignImageModel(this.ImageModel);

            image.Dispose();

            this.ImageView.Close();
            this.ImageView = null;
            this.ImageModel = null;
            this.imageControllerClosed = true;
            }
         }

      protected virtual void Dispose(bool disposing)
         {
         if (disposing)
            {
            if (this.ImageModel != null && this.ImageModel.Image != null)
               {
               this.ImageModel.Image.Dispose();
               this.ImageModel.Image = null;
               }
            }
         }

      private void Hide()
         {
         if (this.imageControllerShown)
            {
            IImageViewManagerController imageViewManagerController = ServiceLocator.Current.GetInstance<IImageViewManagerController>();

            imageViewManagerController.RemoveImageController(this);
            }
         }
      }
   }
