namespace ImagingInterface.Controllers
   {
   using System;
   using System.Drawing;
   using ImageMagick;
   using ImagingInterface.Models;
   using ImagingInterface.Views;
   using Microsoft.Practices.ServiceLocation;

   public class ImageController : IImageController
      {
      private IServiceLocator serviceLocator;

      public ImageController(IImageView imageView, IImageModel imageModel, IServiceLocator serviceLocator)
         {
         this.ImageView = imageView;
         this.ImageModel = imageModel;
         this.serviceLocator = serviceLocator;
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

      public byte[,,] ImageData
         {
         get
            {
            return this.ImageModel.ImageData;
            }
         }

      public bool LoadImage(byte[,,] imageData, string displayName)
         {
         // Clone the input image so that the internal image memory management stays inside this class
         this.ImageModel.ImageData = (byte[,,])imageData.Clone();

         this.ImageModel.DisplayName = displayName;
         this.ImageView.AssignImageModel(this.ImageModel);

         return true;
         }

      public bool LoadImage(string filename)
         {
         try
            {
            byte[] imageData;
            Size imageSize;

            using (MagickImage magickImage = new MagickImage(filename))
               {
               magickImage.Format = MagickFormat.Rgb;

               imageData = magickImage.ToByteArray();
               imageSize = new Size(magickImage.Width, magickImage.Height);
               }

            this.ImageModel.ImageData = new byte[imageSize.Height, imageSize.Width, 3];

            int imageDataIndex = 0;

            for (int y = 0; y < this.ImageModel.Size.Height; y++)
               {
               for (int x = 0; x < this.ImageModel.Size.Width; x++)
                  {
                  for (int channel = 0; channel < 3; channel++)
                     {
                     this.ImageModel.ImageData[y, x, channel] = imageData[imageDataIndex];

                     imageDataIndex++;
                     }
                  }
               }
            }
         catch (ArgumentException)
            {
            return false;
            }
         catch (MagickMissingDelegateErrorException)
            {
            return false;
            }

         this.ImageModel.DisplayName = filename;
         this.ImageView.AssignImageModel(this.ImageModel);

         return true;
         }

      public void UpdateImageData(byte[,,] imageData)
         {
         this.ImageModel.ImageData = (byte[,,])imageData.Clone();
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

         this.ImageView.Close();
         this.ImageModel.ImageData = null;
         }
      }
   }
