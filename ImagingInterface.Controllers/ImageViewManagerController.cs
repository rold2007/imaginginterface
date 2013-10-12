namespace ImagingInterface.Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Models;
   using ImagingInterface.Views;
   using Microsoft.Practices.ServiceLocation;

   public class ImageViewManagerController : IImageViewManagerController
      {
      private IImageViewManager imageViewManager;
      private Dictionary<IImageView, IImageController> imageControllers;

      public ImageViewManagerController(IImageViewManager imageViewManager)
         {
         this.imageViewManager = imageViewManager;
         this.imageControllers = new Dictionary<IImageView, IImageController>();
         }

      public void AddImage(IImageController imageController)
         {
         this.imageViewManager.AddImageView(imageController.ImageView, imageController.ImageModel);
         this.imageControllers.Add(imageController.ImageView, imageController);
         }

      public IImageController GetActiveImage()
         {
         IImageViewManager imageViewManager = ServiceLocator.Current.GetInstance<IImageViewManager>();
         IImageView activeImageView = imageViewManager.GetActiveImage();

         if (activeImageView != null)
            {
            return this.imageControllers[activeImageView];
            }
         else
            {
            return null;
            }
         }

      public void RemoveImage(IImageController imageController)
         {
         this.imageViewManager.RemoveImageView(imageController.ImageView);
         this.imageControllers.Remove(imageController.ImageView);
         }
      }
   }
