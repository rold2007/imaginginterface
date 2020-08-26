// <copyright file="InvertService.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Controllers.Services
{
   using System.Diagnostics;
   using System.Diagnostics.CodeAnalysis;
   using ImagingInterface.Plugins;
   using ImagingInterface.Plugins.Utilities;

   public class InvertService : IImageProcessingService
   {
      private const string InvertDisplayName = "Invert"; // ncrunch: no coverage

      private IImageProcessingManagerService imageProcessingService;

      public InvertService(IImageProcessingManagerService imageProcessingService)
      {
         this.imageProcessingService = imageProcessingService;
      }

      [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Will be fixed when done refactoring.")]
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

      [SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", Justification = "Too much work for now.")]
      public void ProcessImageData(byte[,,] imageData, byte[] overlayData)
      {
         if (this.ApplyInvert)
         {
            if (imageData.GetLength(2) == 1)
            {
               ////using (Image<Gray, byte> invertedImage = new Image<Gray, byte>(imageData))
               ////{
               ////   invertedImage._Not();

               ////   ArrayUtils.ArrayCopy(invertedImage.Data, imageData);
               ////}
            }
            else
            {
               Debug.Assert(imageData.GetLength(2) == 3, "For now only 3-bands images are supported.");

               ////using (Image<Bgr, byte> invertedImage = new Image<Bgr, byte>(imageData))
               ////{
               ////   invertedImage._Not();

               ////   ArrayUtils.ArrayCopy(invertedImage.Data, imageData);
               ////}
            }
         }
      }
   }
}
