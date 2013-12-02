namespace ImageProcessing.Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using Emgu.CV;
   using Emgu.CV.CvEnum;
   using Emgu.CV.Structure;
   using Emgu.Util;
   using ImageProcessing.Controllers;
   using ImageProcessing.Models;
   using ImageProcessing.Views;
   using ImagingInterface.Controllers;
   using ImagingInterface.Plugins;

   public class RotateController : IRotateController
      {
      private static readonly string RotateDisplayName = "Rotate";
      private IRotateView rotateView;
      private IRotateModel rotateModel;
      private IImageManagerController imageManagerController;

      public RotateController(IRotateView rotateView, IRotateModel rotateModel, IImageManagerController imageManagerController)
         {
         this.rotateView = rotateView;
         this.rotateModel = rotateModel;
         this.imageManagerController = imageManagerController;

         this.rotateModel.DisplayName = RotateDisplayName;

         this.rotateView.Rotate += this.RotateView_Rotate;
         }

      public IRawPluginView RawPluginView
         {
         get
            {
            return this.rotateView;
            }
         }

      public IRawPluginModel RawPluginModel
         {
         get
            {
            return this.rotateModel;
            }
         }

      private void RotateView_Rotate(object sender, EventArgs e)
         {
         IImageController imageController = this.imageManagerController.GetActiveImageController();

         using (Image<Bgr, byte> convertedImage = imageController.Image.Convert<Bgr, byte>(), rotatedImage = convertedImage.Rotate(90.0, new Bgr()))
            {
            imageController.Image.ConvertFrom<Bgr, byte>(rotatedImage);
            }

         imageController.UpdateImage();
         }
      }
   }
