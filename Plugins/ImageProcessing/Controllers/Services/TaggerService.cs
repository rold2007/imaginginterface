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
   using ImagingInterface.Plugins.Utilities;
   using Shouldly;

   public class TaggerService : IImageProcessingService
   {
      private static readonly string TaggerDisplayName = "Tagger";

      private Dictionary<IImageSource, Tagger> taggers;
      private IImageProcessingManagerService imageProcessingManagerService;
      ////private IImageSource activeImageSource;
      private SortedList<string, Color> labelColors;
      private Dictionary<IImageSource, string> savedDataPoints;

      public TaggerService(IImageProcessingManagerService imageProcessingManagerService)
      {
         this.taggers = new Dictionary<IImageSource, Tagger>();
         this.imageProcessingManagerService = imageProcessingManagerService;
         this.labelColors = new SortedList<string, Color>();
         this.savedDataPoints = new Dictionary<IImageSource, string>();
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
            SortedSet<string> labels = new SortedSet<string>(this.CurrentImageSourceTagger.DataPoints.Keys);

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

      public string SelectedLabel
      {
         get;
         private set;
      }

      private IImageSource ActiveImageSource
      {
         get
         {
            IImageService imageService = this.imageProcessingManagerService.ActiveImageService;

            if (imageService == null)
            {
               return null;
            }
            else
            {
               return imageService.ImageSource;
            }
         }
      }

      private Tagger CurrentImageSourceTagger
      {
         get
         {
            Tagger currentImageSourceTagger = this.taggers[this.ActiveImageSource];

            currentImageSourceTagger.ShouldNotBeNull();

            return currentImageSourceTagger;
         }
      }

      public void CloseImage(IImageSource imageSource)
      {
         this.taggers.Remove(imageSource);
      }

      public void Activate()
      {
         this.imageProcessingManagerService.ActiveImageProcessingService = this;
      }

      public void AddLabels(IEnumerable<string> labels)
      {
         The list of labels should be specific to TaggerService.Tagger should allow to add a point and create a new label at the same time.
          So AddLabels should not be applied to CurrentImageSourceTagger
         this.CurrentImageSourceTagger.AddLabels(labels);

         this.AssignColors(labels);
      }

      public void RemoveLabels(IEnumerable<string> labels)
      {
         this.CurrentImageSourceTagger.RemoveLabels(labels);
      }

      public void SelectLabel(string label)
      {
         if (label != null)
         {
            this.Labels.ShouldContain(label);
         }

         this.SelectedLabel = label;
      }

      public void AddPoint(string label, Point newPoint)
      {
         this.CurrentImageSourceTagger.AddPoint(label, newPoint);
      }

      public void RemovePoint(string label, Point point)
      {
         this.CurrentImageSourceTagger.RemovePoint(label, point);
      }

      public List<Point> GetPoints(string label)
      {
         IReadOnlyDictionary<string, List<Point>> dataPoints = this.CurrentImageSourceTagger.DataPoints;
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

      public void ProcessImageData(byte[,,] imageData, byte[] overlayData)
      {
         int imageWidth = imageData.GetLength(1);
         int imageHeight = imageData.GetLength(0);
         int imageSize = imageWidth * imageHeight;

         foreach (string tag in this.CurrentImageSourceTagger.DataPoints.Keys)
         {
            Color color = this.LabelColors[tag];

            byte red = Convert.ToByte(color.R);
            byte green = Convert.ToByte(color.G);
            byte blue = Convert.ToByte(color.B);

            foreach (Point point in this.CurrentImageSourceTagger.DataPoints[tag])
            {
               int pixelOffset = (point.Y * imageWidth * 4) + (point.X * 4);

               overlayData[pixelOffset] = red;
               overlayData[pixelOffset + 1] = green;
               overlayData[pixelOffset + 2] = blue;
               overlayData[pixelOffset + 3] = 255;
            }
         }
      }

      public void SelectPixel(IImageSource imageSource, Point pixelPosition)
      {
         if (this.SelectedLabel != null)
         {
            this.CurrentImageSourceTagger.AddPoint(this.SelectedLabel, pixelPosition);

            this.imageProcessingManagerService.AddOneShotImageProcessingToActiveImage(this);
         }
      }

      public void ActiveImageSourceChanged(IImageSource imageSource)
      {
         // TODO: Currently working on this
         // It doesn't save and restore points properly. All points are shared with all images
         // It doesn't save and restore points properly. All points are shared with all images
         // It doesn't save and restore points properly. All points are shared with all images
         // It doesn't save and restore points properly. All points are shared with all images
         // It doesn't save and restore points properly. All points are shared with all images
         // It doesn't save and restore points properly. All points are shared with all images
         string savedDataPoints;

         // Save the current data points
         ////if (this.activeImageSource != null)
         {
            savedDataPoints = this.CurrentImageSourceTagger.SavePoints();

            ////this.savedDataPoints[this.activeImageSource] = savedDataPoints;
         }

         ////this.activeImageSource = imageSource;

         if (imageSource != null)
         {
            ////if (this.savedDataPoints.TryGetValue(this.activeImageSource, out savedDataPoints))
            ////{
            ////   this.tagger.LoadPoints(savedDataPoints);
            ////}
            ////else
            ////{
            ////   this.tagger.RemoveAllPoints();
            ////}

            this.imageProcessingManagerService.AddOneShotImageProcessingToActiveImage(this);
         }
         else
         {
            this.CurrentImageSourceTagger.RemoveAllPoints();
         }
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
