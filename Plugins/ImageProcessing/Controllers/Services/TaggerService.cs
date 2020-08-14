// <copyright file="TaggerService.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Controllers.Services
{
   using System;
   using System.Collections.Generic;
   using System.Diagnostics.CodeAnalysis;
   using System.Drawing;
   using ImageProcessing.ObjectDetection;
   using ImagingInterface.Plugins;
   using ImagingInterface.Plugins.Utilities;
   using Shouldly;

   public class TaggerService : IImageProcessingService
   {
      private const string TaggerDisplayName = "Tagger";

      private Tagger tagger;
      private IImageProcessingManagerService imageProcessingService;
      private SortedList<string, Color> labelColors;
      private Dictionary<IImageSource, string> savedDataPoints;

      public TaggerService(Tagger tagger, IImageProcessingManagerService imageProcessingService)
      {
         this.tagger = tagger;
         this.imageProcessingService = imageProcessingService;
         this.labelColors = new SortedList<string, Color>();
         this.savedDataPoints = new Dictionary<IImageSource, string>();
      }

      [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Will be fixed when done refactoring.")]
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

      public IReadOnlyDictionary<string, Color> LabelColors
      {
         get
         {
            return this.labelColors as IReadOnlyDictionary<string, Color>;
         }
      }

      public void AddLabels(IEnumerable<string> labels)
      {
         this.tagger.AddLabels(labels);

         this.AssignColors(labels);
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

      [SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", Justification = "Too much work for now.")]
      public void ProcessImageData(byte[,,] imageData, byte[] overlayData)
      {
         int imageWidth = imageData.GetLength(1);
         int imageHeight = imageData.GetLength(0);
         int imageSize = imageWidth * imageHeight;

         foreach (string tag in this.tagger.DataPoints.Keys)
         {
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
      }

      public void SelectPixel(string label, Point pixelPosition)
      {
         this.tagger.AddPoint(label, pixelPosition);

         this.imageProcessingService.AddOneShotImageProcessingToActiveImage(this);
      }

      public void ActiveImageSourceChanged(IImageSource imageSource)
      {
         string savedDataPoints;

         // Save the current data points
         if (this.activeImageSource != null)
         {
            savedDataPoints = this.tagger.SavePoints();

            this.savedDataPoints[this.activeImageSource] = savedDataPoints;
         }

         this.activeImageSource = imageSource;

         if (this.savedDataPoints.TryGetValue(this.activeImageSource, out savedDataPoints))
         {
            this.tagger.LoadPoints(savedDataPoints);
         }
         else
         {
            this.tagger.RemoveAllPoints();
         }

         this.imageProcessingService.AddOneShotImageProcessingToActiveImage(this);
      }

      private void AssignColors(IEnumerable<string> labels)
      {
         foreach (string label in labels)
         {
            if (!this.LabelColors.ContainsKey(label))
            {
               int labelHashCode = label.GetHashCode();
               double hue = 360 * ((double)Math.Abs(labelHashCode)) / int.MaxValue;
               double[] hsv = new double[3] { hue, 1.0, 255.0 };

               Color rgbColor = ColorConversion.FromHSV(hsv);

               this.labelColors[label] = rgbColor;
            }
         }
      }
   }
}
