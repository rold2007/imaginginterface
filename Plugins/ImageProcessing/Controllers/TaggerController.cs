﻿// <copyright file="TaggerController.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Controllers
{
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Diagnostics;
   using System.Drawing;
   using ImageProcessing.Controllers.EventArguments;
   using ImageProcessing.Controllers.Services;
   using ImagingInterface.Plugins.EventArguments;

   public class TaggerController
   {
      private TaggerService taggerService;

      public TaggerController(TaggerService taggerService)
      {
         this.taggerService = taggerService;
      }

      public event CancelEventHandler Closing;

      public event EventHandler Closed;

      public event EventHandler<TagPointChangedEventArgs> TagPointChanged;

      public IEnumerable<string> Labels
      {
         get
         {
            return this.taggerService.Labels;
         }
      }

      public bool Active
      {
         get
         {
            return true;
         }
      }

      public string DisplayName
      {
         get
         {
            return this.taggerService.DisplayName;
         }
      }

      public void Initialize()
      {
         ////this.taggerView.SetTaggerModel(this.taggerModel);

         ////this.taggerView.LabelAdded += this.TaggerView_LabelAdded;

         ////this.imageManagerController.ActiveImageChanged += this.ImageManagerController_ActiveImageChanged;

         this.RegisterActiveImage();
      }

      public void Close()
      {
         CancelEventArgs cancelEventArgs = new CancelEventArgs();

         this.Closing?.Invoke(this, cancelEventArgs);

         if (!cancelEventArgs.Cancel)
         {
            ////this.taggerView.LabelAdded -= this.TaggerView_LabelAdded;

            ////this.imageManagerController.ActiveImageChanged -= this.ImageManagerController_ActiveImageChanged;

            this.UnregisterActiveImage();

            ////this.taggerView.Hide();

            ////this.taggerView.Close();

            this.Closed?.Invoke(this, EventArgs.Empty);
         }
      }

      public void AddLabel(string label)
      {
         this.AddLabels(new[] { label });
      }

      public void AddLabels(IEnumerable<string> labels)
      {
         this.taggerService.AddLabels(labels);
      }

      public void RemoveLabel(string label)
      {
         this.RemoveLabels(new[] { label });
      }

      public void RemoveLabels(IEnumerable<string> labels)
      {
         this.taggerService.RemoveLabels(labels);
      }

      public void AddPoint(string label, Point newPoint)
      {
         this.taggerService.AddPoint(label, newPoint);
      }

      public void RemovePoint(string label, Point point)
      {
         this.taggerService.RemovePoint(label, point);
      }

      public List<Point> GetPoints(string label)
      {
         return this.taggerService.GetPoints(label);
      }

      public Color TagColor(string tag)
      {
         ////return this.taggerModel.LabelColors[tag];
         throw new NotImplementedException("AAA");
      }

      private void ImageManagerController_ActiveImageChanged(object sender, EventArgs e)
      {
         this.UnregisterActiveImage();
         this.RegisterActiveImage();
      }

      private void RegisterActiveImage()
      {
         ////this.registeredImageController = this.imageManagerController.GetActiveImage();

         ////if (this.registeredImageController != null)
         {
            ////this.registeredImageController.SelectionChanged += this.RegisteredImageController_SelectionChanged;

            ////if (this.registeredImageController.LastDisplayedImage == null)
            {
               // Need to wait for the first display update
               ////this.registeredImageController.DisplayUpdated += this.RegisteredImageController_DisplayUpdated;
            }

            ////else
            {
               this.ExtractPoints();
            }
         }
      }

      private void UnregisterActiveImage()
      {
         // if (this.registeredImageController != null)
         {
            ////this.registeredImageController.SelectionChanged -= this.RegisteredImageController_SelectionChanged;
            ////this.registeredImageController.DisplayUpdated -= this.RegisteredImageController_DisplayUpdated;

            ////this.tagger.SavePoints();

            // this.registeredImageController = null;
         }
      }

      private void ExtractPoints()
      {
         Debug.Fail("Need to review the use of registeredImageController. The plugins shoudn't depend on ImagingInterface.Controllers, only on ImagingInterface.Plugins.Interface");

         ////this.tagger.LoadPoints(this.registeredImageController.DisplayName);

         ////this.AddLabels(this.tagger.DataPoints.Keys);

         ////this.registeredImageController.AddImageProcessingController(this, this.taggerModel.Clone() as IRawPluginModel);

         ////this.taggerView.UpdateLabelList();

         ////foreach (string tag in this.tagger.DataPoints.Keys)
         ////   {
         ////   foreach (Point point in this.tagger.DataPoints[tag])
         ////      {
         ////      this.TriggerTagPointChanged(tag, point, true);
         ////      }
         ////   }
      }

      ////private void RegisteredImageController_DisplayUpdated(object sender, DisplayUpdateEventArgs e)
      ////   {
      ////   ////this.registeredImageController.DisplayUpdated -= this.RegisteredImageController_DisplayUpdated;

      ////   this.ExtractPoints();
      ////   }

      private void RegisteredImageController_SelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         ////if (this.taggerModel.SelectedLabel != null)
         {
            if (e.Select)
            {
               ////if (this.AddPoint(this.taggerModel.SelectedLabel, e.PixelPosition))
               {
                  ////this.registeredImageController.AddImageProcessingController(this, this.taggerModel.Clone() as IRawPluginModel);
               }
            }
            else
            {
               ////if (this.RemovePoint(this.taggerModel.SelectedLabel, e.PixelPosition))
               {
                  ////this.registeredImageController.AddImageProcessingController(this, this.taggerModel.Clone() as IRawPluginModel);
               }
            }
         }
      }

      private void TriggerTagPointChanged(string label, Point tagPoint, bool added)
      {
         if (this.TagPointChanged != null)
         {
            Dictionary<string, List<Point>> tagPoints = new Dictionary<string, List<Point>>();
            List<Point> points = new List<Point>
            {
               tagPoint
            };
            tagPoints.Add(label, points);

            this.TagPointChanged(this, new TagPointChangedEventArgs(/*this.registeredImageController, */label, tagPoint, added));
         }
      }

      ////private void TaggerView_LabelAdded(object sender, EventArgs e)
      ////   {
      ////   this.AddLabels(new List<string>() { this.taggerModel.AddedLabel });
      ////   }

      private void AssignColors()
      {
         ////int labelIndex = 0;

         ////foreach (string label in this.taggerModel.Labels)
         ////   {
         ////   double hue = 360 * labelIndex / this.taggerModel.Labels.Count;
         ////   double[] hsv = new double[3] { hue, 1.0, 255.0 };

         ////   Color rgbColor = ColorConversion.FromHSV(hsv);

         ////   this.taggerModel.LabelColors[label] = rgbColor;

         ////   labelIndex++;
         ////   }
      }
   }
}
