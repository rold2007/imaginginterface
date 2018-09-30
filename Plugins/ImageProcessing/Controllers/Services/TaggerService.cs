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

      private SortedSet<string> labels;
      private SortedList<string, Color> labelColors;

      public TaggerService(IImageProcessingManagerService imageProcessingManagerService)
      {
         Taggers = new Dictionary<IImageSource, Tagger>();
         ImageProcessingManagerService = imageProcessingManagerService;
         labelColors = new SortedList<string, Color>();
         SavedDataPoints = new Dictionary<IImageSource, string>();
         labels = new SortedSet<string>();
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

      private Dictionary<IImageSource, Tagger> Taggers
      {
         get;
         set;
      }

      private IImageProcessingManagerService ImageProcessingManagerService
      {
         get;
         set;
      }

      private Dictionary<IImageSource, string> SavedDataPoints
      {
         get;
         set;
      }

      private IImageSource ActiveImageSource
      {
         get
         {
            IImageService imageService = ImageProcessingManagerService.ActiveImageService;

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
            IImageSource activeImageSource = ActiveImageSource;

            if (activeImageSource != null)
            {
               if (!Taggers.ContainsKey(activeImageSource))
               {
                  Taggers.Add(activeImageSource, new Tagger());
               }

               Tagger currentImageSourceTagger = Taggers[activeImageSource];

               currentImageSourceTagger.ShouldNotBeNull();

               return currentImageSourceTagger;
            }
            else
            {
               return null;
            }
         }
      }

      public void CloseImage(IImageSource imageSource)
      {
         Taggers.Remove(imageSource);
      }

      public void Activate()
      {
         ImageProcessingManagerService.ActiveImageProcessingService = this;
      }

      public int AddLabels(IEnumerable<string> addedLabels)
      {
         int addedCount = 0;

         foreach (string label in addedLabels)
         {
            if (labels.Add(label))
            {
               addedCount++;
            }
         }

         this.AssignColors(labels);

         return addedCount;
      }

      public void RemoveLabels(IEnumerable<string> labels)
      {
         foreach (string label in labels)
         {
            this.labels.Remove(label);
         }
      }

      public void SelectLabel(string label)
      {
         if (label != null)
         {
            this.Labels.ShouldContain(label);
         }

         this.SelectedLabel = label;
      }

      public bool AddPoint(string label, Point newPoint)
      {
         return CurrentImageSourceTagger.AddPoint(label, newPoint);
      }

      public bool RemovePoint(string label, Point point)
      {
         return CurrentImageSourceTagger.RemovePoint(label, point);
      }

      public void RemoveAllPoints()
      {
         CurrentImageSourceTagger.RemoveAllPoints();
      }

      public List<Point> GetPoints(string label)
      {
         if (CurrentImageSourceTagger != null)
         {
            IReadOnlyDictionary<string, List<Point>> dataPoints = CurrentImageSourceTagger.DataPoints;
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
         else
         {
            return new List<Point>();
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

            ImageProcessingManagerService.AddOneShotImageProcessingToActiveImage(this);
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

            ImageProcessingManagerService.AddOneShotImageProcessingToActiveImage(this);
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
