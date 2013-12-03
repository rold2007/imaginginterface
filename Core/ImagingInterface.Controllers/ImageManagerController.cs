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

   public class ImageManagerController : IImageManagerController
      {
      private IImageManagerView imageManagerView;
      private Dictionary<IImageView, IImageController> imageControllers;

      public ImageManagerController(IImageManagerView imageManagerView, IMainController mainController)
         {
         this.imageManagerView = imageManagerView;
         this.imageControllers = new Dictionary<IImageView, IImageController>();

         mainController.AddImageManagerView(this.imageManagerView);
         }

      public void AddImageController(IImageController imageController)
         {
         this.imageManagerView.AddImageView(imageController.ImageView, imageController.ImageModel);
         this.imageControllers.Add(imageController.ImageView, imageController);
         }

      public IImageController GetActiveImageController()
         {
         IImageView activeImageView = this.imageManagerView.GetActiveImageView();

         if (activeImageView != null)
            {
            return this.imageControllers[activeImageView];
            }
         else
            {
            return null;
            }
         }

      public void RemoveImageController(IImageController imageController)
         {
         this.imageManagerView.RemoveImageView(imageController.ImageView);
         this.imageControllers.Remove(imageController.ImageView);
         }
      }
   }
