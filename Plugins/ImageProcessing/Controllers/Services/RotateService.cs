// <copyright file="RotateService.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Controllers.Services
{
   using Emgu.CV;
   using Emgu.CV.Structure;
   using ImagingInterface.Plugins;
   using ImagingInterface.Plugins.Utilities;
   using Shouldly;

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

      public void Rotate()
      {
         this.imageProcessingService.AddOneShotImageProcessingToActiveImage(this);
      }

      public void ProcessImageData(byte[,,] imageData, byte[] overlayData)
      {
         if (imageData.GetLength(2) == 1)
         {
            using (Image<Gray, byte> convertedImage = new Image<Gray, byte>(imageData), rotatedImage = convertedImage.Rotate(this.Angle, new Gray(0.0)))
            {
               ArrayUtils.ArrayCopy(rotatedImage.Data, imageData);
            }
         }
         else
         {
            imageData.GetLength(2).ShouldBe(3, "For now only 3-bands images are supported.");

            using (Image<Rgb, byte> convertedImage = new Image<Rgb, byte>(imageData), rotatedImage = convertedImage.Rotate(this.Angle, new Rgb(0.0, 0.0, 0.0)))
            {
               ArrayUtils.ArrayCopy(rotatedImage.Data, imageData);
            }
         }
      }
   }
}
