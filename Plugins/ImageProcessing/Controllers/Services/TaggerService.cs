// <copyright file="TaggerService.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Controllers.Services
{
   using System;
   using System.Collections.Generic;
   using System.Drawing;
   using ImageProcessing.ObjectDetection;
   using ImagingInterface.Plugins;

   public class TaggerService : IImageProcessingService
   {
      private static readonly string TaggerDisplayName = "Tagger";

      private Tagger tagger;

      public TaggerService(Tagger tagger)
      {
         this.tagger = tagger;
      }

      public string DisplayName
      {
         get
         {
            return TaggerService.TaggerDisplayName;
         }
      }

      public IEnumerable<string> Labels
      {
         get
         {
            SortedSet<string> labels = new SortedSet<string>(this.tagger.DataPoints.Keys);

            return labels;
         }
      }

      public SortedList<string, Color> LabelColors
      {
         get;
         set;
      }

      public void AddLabels(IEnumerable<string> labels)
      {
         foreach (string label in labels)
         {
            this.tagger.AddLabel(label);
         }
      }

      public byte[,,] ProcessImageData(byte[,,] imageData, byte[] overlayData)
      {
         int imageWidth = imageData.GetLength(1);
         int imageHeight = imageData.GetLength(0);
         int imageSize = imageWidth * imageHeight;

         foreach (string tag in this.tagger.DataPoints.Keys)
         {
            ////Color color = this.taggerModel.LabelColors[tag];
            Color color = this.LabelColors[tag];
            byte red = Convert.ToByte(color.R);
            byte green = Convert.ToByte(color.G);
            byte blue = Convert.ToByte(color.B);

            foreach (Point point in this.tagger.DataPoints[tag])
            {
               int pixelOffset = (point.Y * imageWidth * 4) + (point.X * 4);

               overlayData[pixelOffset] = red;
               overlayData[pixelOffset + 1] = green;
               overlayData[pixelOffset + 2] = blue;
               overlayData[pixelOffset + 3] = 255;
            }
         }

         return imageData;
      }
   }
}
