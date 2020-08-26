// <copyright file="ObjectDetectionManagerController.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Controllers
{
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Drawing;
   using ImageProcessing.Controllers.Services;
   using ImageProcessing.ObjectDetection;
   using ImagingInterface.Plugins;
   using Shouldly;

   public class ObjectDetectionManagerController
   {
      private const string ObjectDetectionDisplayName = "Object detection"; // ncrunch: no coverage
      private TaggerService taggerService;
      private ObjectDetector objectDetector;

      public ObjectDetectionManagerController(TaggerService taggerService, ObjectDetector objectDetection)
      {
         this.taggerService = taggerService;
         this.objectDetector = objectDetection;
      }

      public event CancelEventHandler Closing;

      public event EventHandler Closed;

      public static string DisplayName
      {
         get
         {
            return ObjectDetectionDisplayName;
         }
      }

      public IEnumerable<string> Labels
      {
         get
         {
            return this.taggerService.Labels;
         }
      }

      public string SelectedLabel
      {
         get;
         private set;
      }

      public void Close()
      {
         CancelEventArgs cancelEventArgs = new CancelEventArgs();

         this.Closing?.Invoke(this, cancelEventArgs);

         if (!cancelEventArgs.Cancel)
         {
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

      public void RemoveLabels(IEnumerable<string> labels)
      {
         this.taggerService.RemoveLabels(labels);
      }

      public Color TagColor(string label)
      {
         return this.taggerService.LabelColors[label];
      }

      public void SelectLabel(string label)
      {
         if (label != null)
         {
            this.taggerService.Labels.ShouldContain(label);
         }

         this.SelectedLabel = label;
      }

      public void SelectPixel(IImageSource imageSource, Point pixelPosition)
      {
         if (this.SelectedLabel != null)
         {
            if (this.taggerService.SelectPixel(this.SelectedLabel, imageSource, pixelPosition))
            {
               this.objectDetector.Add(this.SelectedLabel, imageSource, pixelPosition);
            }
            else
            {
               this.objectDetector.Remove(this.SelectedLabel, imageSource, pixelPosition);
            }
         }
      }

      public void TrainModel()
      {
         this.objectDetector.Train(null);
      }
   }
}
