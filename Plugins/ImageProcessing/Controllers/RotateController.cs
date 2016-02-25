namespace ImageProcessing.Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Diagnostics;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using Emgu.CV;
   using Emgu.CV.CvEnum;
   using Emgu.CV.Structure;
   using Emgu.Util;
   using ImageProcessing.Controllers;
   using ImageProcessing.Controllers.EventArguments;
   using ImageProcessing.Models;
   using ImagingInterface.Controllers;
   using ImagingInterface.Plugins;

   public class RotateController : IImageProcessingController
      {
      private static readonly string RotateDisplayName = "Rotate"; // ncrunch: no coverage
      ////private IRotateView rotateView;
      private IRotateModel rotateModel;
      private ImageManagerController imageManagerController;

      public RotateController(IRotateModel rotateModel, ImageManagerController imageManagerController)
         {
         ////this.rotateView = rotateView;
         this.rotateModel = rotateModel;
         this.imageManagerController = imageManagerController;

         this.rotateModel.DisplayName = RotateDisplayName;
         }

      public event CancelEventHandler Closing;

      public event EventHandler Closed;

      ////public IRawPluginView RawPluginView
      ////   {
      ////   get
      ////      {
      ////      return this.rotateView;
      ////      }
      ////   }

      public IRawPluginModel RawPluginModel
         {
         get
            {
            return this.rotateModel;
            }
         }

      public bool Active
         {
         get
            {
            return true;
            }
         }

      public void Initialize()
         {
         ////this.rotateView.Rotate += this.RotateView_Rotate;
         }

      public void Close()
         {
         CancelEventArgs cancelEventArgs = new CancelEventArgs();

         if (this.Closing != null)
            {
            this.Closing(this, cancelEventArgs);
            }

         if (!cancelEventArgs.Cancel)
            {
            ////this.rotateView.Rotate -= this.RotateView_Rotate;

            ////this.rotateView.Hide();

            ////this.rotateView.Close();

            if (this.Closed != null)
               {
               this.Closed(this, EventArgs.Empty);
               }
            }
         }

      public byte[, ,] ProcessImageData(byte[, ,] imageData, byte[] overlayData, IRawPluginModel rawPluginModel)
         {
         IRotateModel rotateModel = rawPluginModel as IRotateModel;

         if (imageData.GetLength(2) == 1)
            {
            using (Image<Gray, byte> convertedImage = new Image<Gray, byte>(imageData), rotatedImage = convertedImage.Rotate(rotateModel.Angle, new Gray()))
               {
               return rotatedImage.Data;
               }
            }
         else
            {
            Debug.Assert(imageData.GetLength(2) == 3, "For now only 3-bands images are supported.");

            using (Image<Bgr, byte> convertedImage = new Image<Bgr, byte>(imageData), rotatedImage = convertedImage.Rotate(rotateModel.Angle, new Bgr()))
               {
               return rotatedImage.Data;
               }
            }
         }

      private void RotateView_Rotate(object sender, RotateEventArgs e)
         {
         if (this.rotateModel.Angle != e.Angle)
            {
            this.rotateModel.Angle = e.Angle;

            ImageController imageController = this.imageManagerController.GetActiveImage();

            if (imageController != null)
               {
               ////imageController.AddImageProcessingController(this, this.rotateModel.Clone() as IRawPluginModel);
               }
            }
         }
      }
   }
