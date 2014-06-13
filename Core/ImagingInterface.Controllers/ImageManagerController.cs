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

         this.imageManagerView.ActiveImageChanged += this.ImageManagerView_ActiveImageChanged;

         mainController.AddImageManager(this, this.imageManagerView);
         }

      public event EventHandler ActiveImageChanged;

      public void AddImage(IImageController imageController)
         {
         imageController.Closed += this.ImageController_Closed;

         this.imageControllers.Add(imageController.RawImageView, imageController);
         this.imageManagerView.AddImage(imageController.RawImageView, imageController.RawImageModel);
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

      private void ImageManagerView_ActiveImageChanged(object sender, EventArgs e)
         {
         if (this.ActiveImageChanged != null)
            {
            this.ActiveImageChanged(this, EventArgs.Empty);
            }
         }
      }
   }
