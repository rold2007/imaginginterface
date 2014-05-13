namespace ImagingInterface.Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using ImagingInterface.Plugins;
   using ImagingInterface.Views;

   public class ImageManagerController : IImageManagerController
      {
      private IImageManagerView imageManagerView;
      private Dictionary<IRawImageView, IImageController> imageControllers;

      public ImageManagerController(IImageManagerView imageManagerView, IMainController mainController)
         {
         this.imageManagerView = imageManagerView;
         this.imageControllers = new Dictionary<IRawImageView, IImageController>();

         mainController.AddImageManager(this, this.imageManagerView);
         }

      public void AddImage(IImageController imageController)
         {
         imageController.Closed += this.ImageController_Closed;

         this.imageManagerView.AddImage(imageController.RawImageView, imageController.RawImageModel);
         this.imageControllers.Add(imageController.RawImageView, imageController);
         }

      public IImageController GetActiveImage()
         {
         IRawImageView activeRawImageView = this.imageManagerView.GetActiveImageView();

         if (activeRawImageView != null)
            {
            return this.imageControllers[activeRawImageView];
            }
         else
            {
            return null;
            }
         }

      public IList<IImageController> GetAllImages()
         {
         return this.imageControllers.Values.ToList();
         }

      private void RemoveImage(IImageController imageController)
         {
         imageController.Closed -= this.ImageController_Closed;

         this.imageManagerView.RemoveImage(imageController.RawImageView);
         this.imageControllers.Remove(imageController.RawImageView);
         }

      private void ImageController_Closed(object sender, EventArgs e)
         {
         this.RemoveImage(sender as IImageController);
         }
      }
   }
