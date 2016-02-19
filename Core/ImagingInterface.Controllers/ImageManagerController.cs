namespace ImagingInterface.Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using ImagingInterface.Plugins;

   public class ImageManagerController
      {
      ////private IImageManagerView imageManagerView;
      private Dictionary<IRawImageView, ImageController> imageControllers;

      public ImageManagerController(MainController mainController)
         {
         ////this.imageManagerView = imageManagerView;
         this.imageControllers = new Dictionary<IRawImageView, ImageController>();

         ////this.imageManagerView.ActiveImageChanged += this.ImageManagerView_ActiveImageChanged;

         mainController.AddImageManager(this);
         }

      public event EventHandler ActiveImageChanged;

      public void AddImage(ImageController imageController)
         {
         imageController.Closed += this.ImageController_Closed;

         ////this.imageControllers.Add(imageController.RawImageView, imageController);
         ////this.imageManagerView.AddImage(imageController.RawImageView, imageController.RawImageModel);
         }

      public ImageController GetActiveImage()
         {
         ////IRawImageView activeRawImageView = this.imageManagerView.GetActiveImageView();

         ////if (activeRawImageView != null)
         ////   {
         ////   return this.imageControllers[activeRawImageView];
         ////   }
         ////else
            {
            return null;
            }
         }

      public IList<ImageController> GetAllImages()
         {
         return this.imageControllers.Values.ToList();
         }

      private void RemoveImage(ImageController imageController)
         {
         imageController.Closed -= this.ImageController_Closed;

         ////this.imageManagerView.RemoveImage(imageController.RawImageView);
         ////this.imageControllers.Remove(imageController.RawImageView);
         }

      private void ImageController_Closed(object sender, EventArgs e)
         {
         this.RemoveImage(sender as ImageController);
         }

      private void ImageManagerView_ActiveImageChanged(object sender, EventArgs e)
         {
         if (this.ActiveImageChanged != null)
            {
            this.ActiveImageChanged(this, EventArgs.Empty);
            }
         }
      }
   }
