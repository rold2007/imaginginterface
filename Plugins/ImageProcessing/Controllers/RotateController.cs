namespace ImageProcessing.Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
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

      public event CancelEventHandler Closing;

      public event EventHandler Closed;

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

      public void Close()
         {
         CancelEventArgs cancelEventArgs = new CancelEventArgs();

         if (this.Closing != null)
            {
            this.Closing(this, cancelEventArgs);
            }

         this.rotateView.Close();

         if (this.Closed != null)
            {
            this.Closed(this, EventArgs.Empty);
            }
         }

      private void RotateView_Rotate(object sender, EventArgs e)
         {
         IImageController imageController = this.imageManagerController.GetActiveImage();

         using (Image<Rgb, byte> convertedImage = new Image<Rgb, byte>(imageController.ImageData), rotatedImage = convertedImage.Rotate(90.0, new Rgb()))
            {
            imageController.UpdateImageData(rotatedImage.Data);
            }
         }
      }
   }
