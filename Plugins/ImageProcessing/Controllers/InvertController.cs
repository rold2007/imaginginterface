namespace ImageProcessing.Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Diagnostics;
   using System.Drawing;
   using System.Drawing.Imaging;
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

   public class InvertController : IInvertController, IImageProcessingController
      {
      private static readonly string InvertDisplayName = "Invert"; // ncrunch: no coverage
      private IInvertView invertView;
      private IInvertModel invertModel;
      private IImageManagerController imageManagerController;

      public InvertController(IInvertView invertView, IInvertModel invertModel, IImageManagerController imageManagerController)
         {
         this.invertView = invertView;
         this.invertModel = invertModel;
         this.imageManagerController = imageManagerController;

         this.invertModel.DisplayName = InvertController.InvertDisplayName;

         this.invertView.Invert += this.InvertView_Invert;
         }

      public event CancelEventHandler Closing;

      public event EventHandler Closed;

      public IRawPluginView RawPluginView
         {
         get
            {
            return this.invertView;
            }
         }

      public IRawPluginModel RawPluginModel
         {
         get
            {
            return this.invertModel;
            }
         }

      public void Close()
         {
         CancelEventArgs cancelEventArgs = new CancelEventArgs();

         if (this.Closing != null)
            {
            this.Closing(this, cancelEventArgs);
            }

         this.invertView.Hide();

         if (!cancelEventArgs.Cancel)
            {
            this.invertView.Close();

            if (this.Closed != null)
               {
               this.Closed(this, EventArgs.Empty);
               }
            }
         }

      public byte[, ,] ProcessImageData(byte[, ,] imageData, IRawPluginModel rawPluginModel)
         {
         if (imageData.GetLength(2) == 1)
            {
            using (Image<Gray, byte> invertedImage = new Image<Gray, byte>(imageData))
               {
               invertedImage._Not();

               return invertedImage.Data;
               }
            }
         else
            {
            Debug.Assert(imageData.GetLength(2) == 3, "For now only 3-bands images are supported.");

            using (Image<Bgr, byte> invertedImage = new Image<Bgr, byte>(imageData))
               {
               invertedImage._Not();

               return invertedImage.Data;
               }
            }
         }

      private void InvertView_Invert(object sender, EventArgs e)
         {
         IImageController imageController = this.imageManagerController.GetActiveImage();

         if (imageController != null)
            {
            imageController.AddImageProcessingController(this, this, null);
            }
         }
      }
   }
