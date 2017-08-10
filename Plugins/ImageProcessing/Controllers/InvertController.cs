// <copyright file="InvertController.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Controllers
{
   using System;
   using System.ComponentModel;
   using System.Diagnostics;
   using Emgu.CV;
   using Emgu.CV.Structure;
   using ImageProcessing.Controllers.EventArguments;
   using ImageProcessing.Models;
   using ImagingInterface.Plugins;

   public class InvertController : IImageProcessingService
      {
      private static readonly string InvertDisplayName = "Invert"; // ncrunch: no coverage
      private InvertModel invertModel = new InvertModel();

      public InvertController()
         {
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

      public bool Active
         {
         get
            {
            return true;
            }
         }

      public string DisplayName
      {
         get
         {
            return this.invertModel.DisplayName;
         }
      }

      public void Initialize()
         {
         ////this.invertView.Invert += this.InvertView_Invert;
         }

      public void Close()
         {
         CancelEventArgs cancelEventArgs = new CancelEventArgs();

         this.Closing?.Invoke(this, cancelEventArgs);

         if (!cancelEventArgs.Cancel)
            {
            ////this.invertView.Invert -= this.InvertView_Invert;

            ////this.invertView.Hide();

            ////this.invertView.Close();

            this.Closed?.Invoke(this, EventArgs.Empty);
         }
         }

      public byte[,,] ProcessImageData(byte[,,] imageData, byte[] overlayData)
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
         ////ImageController imageController = this.imageManagerController.GetActiveImage();

         ////if (imageController != null)
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
