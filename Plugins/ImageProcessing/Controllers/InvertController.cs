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

   public class InvertController : IInvertController
      {
      private static readonly string InvertDisplayName = "Invert";
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

         this.invertView.Close();

         if (this.Closed != null)
            {
            this.Closed(this, EventArgs.Empty);
            }
         }

      private void InvertView_Invert(object sender, EventArgs e)
         {
         IImageController imageController = this.imageManagerController.GetActiveImage();

         if (imageController != null)
            {
            if (imageController.ImageData != null)
               {
               using (Image<Rgb, byte> invertedImage = new Image<Rgb, byte>(imageController.ImageData))
                  {
                  invertedImage._Not();

                  imageController.UpdateImageData(invertedImage.Data);
                  }
               }
            }
         }
      }
   }
