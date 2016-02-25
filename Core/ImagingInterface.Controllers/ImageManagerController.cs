namespace ImagingInterface.Controllers
   {
   using System;
   using System.Collections.Generic;
   using ImagingInterface.Models;

   public class ImageManagerController
      {
      private ImageManagerModel imageManagerModel;

      public ImageManagerController(ImageManagerModel imageManagerModel/*, MainController mainController*/)
         {
         ////this.imageManagerView = imageManagerView;
         ////this.imageControllers = new Dictionary<IRawImageView, ImageController>();
         this.imageManagerModel = imageManagerModel;

         ////this.imageManagerView.ActiveImageChanged += this.ImageManagerView_ActiveImageChanged;

         ////mainController.AddImageManager(this);
         }

      public event EventHandler ActiveImageChanged;

      public void AddImage(ImageController imageController)
         {
         ////imageController.Closed += this.ImageController_Closed;

         ////this.imageControllers.Add(imageController.RawImageView, imageController);
         ////this.imageManagerView.AddImage(imageController.RawImageView, imageController.RawImageModel);

         this.imageManagerModel.ImageControllers.Add(imageController);
         ////this.imageManagerModel.ActiveImageController = imageController;
         }

      /*
      public ImageController GetActiveImage()
         {
         ////IRawImageView activeRawImageView = this.imageManagerView.GetActiveImageView();

         ////if (activeRawImageView != null)
         ////   {
         ////   return this.imageControllers[activeRawImageView];
         ////   }
         ////else
            {
            return this.imageManagerModel.ActiveImageController;
            }
         }*/

      public IList<ImageController> GetAllImages()
         {
         return null;
         ////return this.imageControllers.Values.ToList();
         }

      public void RemoveImage(ImageController imageController)
         {
         ////imageController.Closed -= this.ImageController_Closed;

         ////this.imageManagerView.RemoveImage(imageController.RawImageView);
         ////this.imageControllers.Remove(imageController.RawImageView);
         this.imageManagerModel.ImageControllers.Remove(imageController);
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
