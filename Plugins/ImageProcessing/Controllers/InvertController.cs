﻿namespace ImageProcessing.Controllers
   {
   using System;
   using System.ComponentModel;
   using System.Diagnostics;
   using System.Drawing;
   using Emgu.CV;
   using Emgu.CV.Structure;
   using ImageProcessing.Controllers.EventArguments;
   using ImageProcessing.Models;
   using ImagingInterface.Controllers;
   using ImagingInterface.Plugins;

   public class InvertController : IImageProcessingController
      {
      private static readonly string InvertDisplayName = "Invert"; // ncrunch: no coverage
      ////private IInvertView invertView;
      private IInvertModel invertModel;
      private ImageManagerController imageManagerController;

      public InvertController(IInvertModel invertModel, ImageManagerController imageManagerController)
         {
         ////this.invertView = invertView;
         this.invertModel = invertModel;
         this.imageManagerController = imageManagerController;

         this.invertModel.DisplayName = InvertController.InvertDisplayName;
         }

      public event CancelEventHandler Closing;

      public event EventHandler Closed;

      ////public IRawPluginView RawPluginView
      ////   {
      ////   get
      ////      {
      ////      return this.invertView;
      ////      }
      ////   }

      public IRawPluginModel RawPluginModel
         {
         get
            {
            return this.invertModel;
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
         ////this.invertView.Invert += this.InvertView_Invert;
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
            ////this.invertView.Invert -= this.InvertView_Invert;

            ////this.invertView.Hide();

            ////this.invertView.Close();

            if (this.Closed != null)
               {
               this.Closed(this, EventArgs.Empty);
               }
            }
         }

      public byte[,,] ProcessImageData(byte[,,] imageData, byte[] overlayData, IRawPluginModel rawPluginModel)
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

      private void InvertView_Invert(object sender, InvertEventArgs e)
         {
         ImageController imageController = this.imageManagerController.GetActiveImage();

         if (imageController != null)
            {
            if (e.Invert)
               {
               ////imageController.AddImageProcessingController(this, null);
               }
            else
               {
               ////imageController.RemoveImageProcessingController(this, null);
               }
            }
         }
      }
   }
