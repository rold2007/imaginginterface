// <copyright file="InvertService.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Controllers.Services
{
   using System.Diagnostics;
   using Emgu.CV;
   using Emgu.CV.Structure;
   using ImagingInterface.Plugins;

   public class InvertService : IImageProcessingService
   {
      private static readonly string InvertDisplayName = "Invert"; // ncrunch: no coverage

      private IImageProcessingManagerService imageProcessingService;

      public InvertService(IImageProcessingManagerService imageProcessingService)
      {
         this.imageProcessingService = imageProcessingService;
      }

      public string DisplayName
      {
         get
         {
            return InvertService.InvertDisplayName;
         }
      }

      public bool ApplyInvert
      {
         get;
         set;
      }

      public void Invert()
      {
         this.imageProcessingService.AddOneShotImageProcessingToActiveImage(this);
      }

      public byte[,,] ProcessImageData(byte[,,] imageData, byte[] overlayData)
      {
         if (this.ApplyInvert)
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
         else
         {
            return imageData;
         }
      }
   }
}
