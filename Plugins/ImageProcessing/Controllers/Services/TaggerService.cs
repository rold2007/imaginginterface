// <copyright file="TaggerService.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Controllers.Services
{
   using System;
   using System.Collections.Generic;
   using System.Drawing;
   using System.Linq;
   using ImageProcessing.ObjectDetection;
   using ImagingInterface.Plugins;
   using ImagingInterface.Plugins.Utilities;
   using Shouldly;

   public class TaggerService : IImageProcessingService
   {
      private static readonly string TaggerDisplayName = "Tagger";

      private SortedList<string, Color> labelColors;

      public TaggerService(IImageProcessingManagerService imageProcessingManagerService)
      {
         Taggers = new Dictionary<IImageService, Tagger>();
         ImageProcessingManagerService = imageProcessingManagerService;
         labelColors = new SortedList<string, Color>();
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
            return CurrentImageServiceTagger.Labels;
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

      private Dictionary<IImageService, Tagger> Taggers
      {
         get;
         set;
      }

      private IImageProcessingManagerService ImageProcessingManagerService
      {
         get;
         set;
      }

      private IImageService ActiveImageService
      {
         get
         {
            return ImageProcessingManagerService.ActiveImageService;
         }
      }

      private Tagger CurrentImageServiceTagger
      {
         get
         {
            IImageService activeImageService = ActiveImageService;

            if (activeImageService != null)
            {
               if (!Taggers.ContainsKey(activeImageService))
               {
                  Taggers.Add(activeImageService, new Tagger());
               }

               Tagger currentImageSourceTagger = Taggers[activeImageService];

               currentImageSourceTagger.ShouldNotBeNull();

               return currentImageSourceTagger;
            }
            else
            {
               return null;
            }
         }
      }

      public void CloseImage(IImageService imageService)
      {
         Taggers.Remove(imageService);
      }

      public void Activate()
      {
         ImageProcessingManagerService.ActiveImageProcessingService = this;
      }

      public void AddLabels(IEnumerable<string> labels)
      {
         CurrentImageServiceTagger.AddLabels(labels);

         this.AssignColors(labels);
      }

      public void RemoveLabels(IEnumerable<string> labels)
      {
         CurrentImageServiceTagger.RemoveLabels(labels);

         if (labels.Contains(SelectedLabel))
         {
            SelectLabel(null);
         }
      }

      public void SelectLabel(string label)
      {
         if (label != null)
         {
            this.Labels.ShouldNotBeNull();
            this.Labels.ShouldContain(label);
         }

         this.SelectedLabel = label;
      }

      public bool AddPoint(string label, Point newPoint)
      {
         return CurrentImageServiceTagger.AddPoint(label, newPoint);
      }

      public bool RemovePoint(string label, Point point)
      {
         return CurrentImageServiceTagger.RemovePoint(label, point);
      }

      public void RemoveAllPoints()
      {
         CurrentImageServiceTagger.RemoveAllPoints();
      }

      public IList<Point> GetPoints(string label)
      {
         IReadOnlyDictionary<string, List<Point>> dataPoints = CurrentImageServiceTagger.DataPoints;

         return dataPoints[label];
      }

      public void ProcessImageData(IImageService imageService, byte[] overlayData)
      {
         byte[,,] imageData = imageService.ImageSource.OriginalImageData;

         int imageWidth = imageData.GetLength(1);
         int imageHeight = imageData.GetLength(0);
         int imageSize = imageWidth * imageHeight;

         foreach (string tag in this.CurrentImageServiceTagger.DataPoints.Keys)
         {
            Color color = this.LabelColors[tag];

            byte red = Convert.ToByte(color.R);
            byte green = Convert.ToByte(color.G);
            byte blue = Convert.ToByte(color.B);

            foreach (Point point in this.CurrentImageServiceTagger.DataPoints[tag])
            {
               int pixelOffset = (point.Y * imageWidth * 4) + (point.X * 4);

               if (pixelOffset < overlayData.Count())
               {
                  overlayData[pixelOffset] = red;
                  overlayData[pixelOffset + 1] = green;
                  overlayData[pixelOffset + 2] = blue;
                  overlayData[pixelOffset + 3] = 255;
               }
            }
         }
      }

      public void SelectPixel(Point pixelPosition)
      {
         if (this.SelectedLabel != null)
         {
            this.CurrentImageServiceTagger.AddPoint(this.SelectedLabel, pixelPosition);

            ImageProcessingManagerService.AddOneShotImageProcessingToActiveImage(this);
         }
      }

      /*
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
      //*/

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
