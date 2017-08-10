// <copyright file="RotateService.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Controllers.Services
{
   using System.Diagnostics;
   using Emgu.CV;
   using Emgu.CV.Structure;
   using ImagingInterface.Plugins;

   public class RotateService : IImageProcessingService
   {
      private static readonly string RotateDisplayName = "Rotate"; // ncrunch: no coverage

      private IImageProcessingManagerService imageProcessingService;

      public RotateService(IImageProcessingManagerService imageProcessingService)
      {
         this.imageProcessingService = imageProcessingService;
      }

      public string DisplayName
      {
         get
         {
            return RotateService.RotateDisplayName;
         }
      }

      public double Angle
      {
         get;
         set;
      }

      public void Rotate(double angle)
      {
         this.imageProcessingService.AddOneShotImageProcessingToActiveImage(this);
      }

      public byte[,,] ProcessImageData(byte[,,] imageData, byte[] overlayData)
      {
         if (imageData.GetLength(2) == 1)
         {
            using (Image<Gray, byte> convertedImage = new Image<Gray, byte>(imageData), rotatedImage = convertedImage.Rotate(this.Angle, new Gray(0.0)))
            {
               return rotatedImage.Data;
            }
         }
         else
         {
            Debug.Assert(imageData.GetLength(2) == 3, "For now only 3-bands images are supported.");

            using (Image<Bgr, byte> convertedImage = new Image<Bgr, byte>(imageData), rotatedImage = convertedImage.Rotate(this.Angle, new Bgr(0.0, 0.0, 0.0)))
            {
               return rotatedImage.Data;
            }
         }
      }
   }
}
