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
      private IImageProcessingManagerService imageProcessingService;

      public TaggerService(Tagger tagger, IImageProcessingManagerService imageProcessingService)
      {
         this.tagger = tagger;
         this.imageProcessingService = imageProcessingService;
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
         this.tagger.AddLabels(labels);
      }

      public void RemoveLabels(IEnumerable<string> labels)
      {
         this.tagger.RemoveLabels(labels);
      }

      public void AddPoint(string label, Point newPoint)
      {
         this.tagger.AddPoint(label, newPoint);
      }

      public void RemovePoint(string label, Point point)
      {
         this.tagger.RemovePoint(label, point);
      }

      public List<Point> GetPoints(string label)
      {
         IReadOnlyDictionary<string, List<Point>> dataPoints = this.tagger.DataPoints;
         List<Point> points;

         dataPoints.TryGetValue(label, out points);

         if (points == null)
         {
            return new List<Point>();
         }
         else
         {
            return points;
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
            ////Color color = this.LabelColors[tag];

            Color color = Color.Red;
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

      public void SelectPixel(IImageSource imageSource, string label, Point pixelPosition)
      {
         this.tagger.AddPoint(label, pixelPosition);

         this.imageProcessingService.AddOneShotImageProcessingToActiveImage(this);
      }
   }
}
