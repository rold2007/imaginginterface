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
      private Dictionary<IRawImageView, IImageController> imageControllers;

      public ImageManagerController(IImageManagerView imageManagerView, IMainController mainController)
         {
         this.imageManagerView = imageManagerView;
         this.imageControllers = new Dictionary<IRawImageView, IImageController>();

         mainController.AddImageManagerView(this.imageManagerView);
         }

      public void AddImageController(IImageController imageController, IRawImageView rawImageView, IRawImageModel rawImageModel)
         {
         this.imageManagerView.AddImageView(rawImageView, rawImageModel);
         this.imageControllers.Add(rawImageView, imageController);
         }

      public IImageController GetActiveImageController()
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

      public void RemoveImageController(IRawImageView rawImageView)
         {
         this.imageManagerView.RemoveImageView(rawImageView);
         this.imageControllers.Remove(rawImageView);
         }
      }
   }
