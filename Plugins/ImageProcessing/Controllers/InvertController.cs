namespace ImageProcessing.Controllers
   {
   using System;
   using System.Collections.Generic;
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
      private static string invertDisplayName = "Invert";
      private IInvertView invertView;
      private IInvertModel invertModel;
      private IImageManagerController imageManagerController;

      public InvertController(IInvertView invertView, IInvertModel invertModel, IImageManagerController imageManagerController)
         {
         this.invertView = invertView;
         this.invertModel = invertModel;
         this.imageManagerController = imageManagerController;

         this.invertModel.DisplayName = InvertController.invertDisplayName;

         this.invertView.Invert += this.InvertView_Invert;
         }

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

      private void InvertView_Invert(object sender, EventArgs e)
         {
         IImageController imageController = this.imageManagerController.GetActiveImageController();

         if (imageController != null)
            {
            if (imageController.Image != null)
               {
               using (Image<Bgr, byte> invertedImage = imageController.Image.Convert<Bgr, byte>())
                  {
                  invertedImage._Not();

                  imageController.Image.ConvertFrom<Bgr, byte>(invertedImage);
                  }

               imageController.UpdateImage();
               }
            }
         }
      }
   }
