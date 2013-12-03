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
      private IServiceLocator serviceLocator;

      public ImageController(IImageView imageView, IImageModel imageModel, IServiceLocator serviceLocator)
         {
         this.ImageView = imageView;
         this.ImageModel = imageModel;
         this.serviceLocator = serviceLocator;
         }

      // ncrunch: no coverage start
      ~ImageController()
         {
         this.Dispose(false);
         }
      //// ncrunch: no coverage end

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

      public Image<Bgra, byte> Image
         {
         get
            {
            return this.ImageModel.Image;
            }
         }

      public void Dispose()
         {
         this.Dispose(true);
         GC.SuppressFinalize(this);
         }

      public bool LoadImage(Image<Bgra, byte> image, string displayName)
         {
         // Clone the input image so that the internal image memory management stays inside this class
         this.ImageModel.Image = image.Clone();

         this.ImageModel.DisplayName = displayName;
         this.ImageView.AssignImageModel(this.ImageModel);

         return true;
         }

      public bool LoadImage(string filename)
         {
         try
            {
            this.ImageModel.Image = new Image<Bgra, byte>(filename);
            }
         catch (ArgumentException)
            {
            return false;
            }

         this.ImageModel.DisplayName = filename;
         this.ImageView.AssignImageModel(this.ImageModel);

         return true;
         }

      public void UpdateImage()
         {
         this.ImageView.AssignImageModel(this.ImageModel);
         }

      public void Add()
         {
         IImageManagerController imageViewManagerController = this.serviceLocator.GetInstance<IImageManagerController>();

         imageViewManagerController.AddImageController(this, this.ImageView, this.ImageModel);
         }

      public void Close()
         {
         IImageManagerController imageViewManagerController = this.serviceLocator.GetInstance<IImageManagerController>();

         imageViewManagerController.RemoveImageController(this.ImageView);

         this.Dispose();
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
      }
   }
