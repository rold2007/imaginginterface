// <copyright file="ObjectDetectionController.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Controllers
{
   using System;
   using System.ComponentModel;
   using System.Diagnostics.CodeAnalysis;
   using ImageProcessing.Controllers.EventArguments;
   using ImageProcessing.ObjectDetection;
   using ImagingInterface.Plugins;

   public class ObjectDetectionController : IImageProcessingService
   {
      private const string ObjectDetectionDisplayName = "Object detection"; // ncrunch: no coverage

      private ObjectDetector objectDetector;

      public ObjectDetectionController(ObjectDetector objectDetection)
      {
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

      public void Close()
      {
         CancelEventArgs cancelEventArgs = new CancelEventArgs();

         this.Closing?.Invoke(this, cancelEventArgs);

         if (!cancelEventArgs.Cancel)
         {
            this.Closed?.Invoke(this, EventArgs.Empty);

            throw new Exception("Need to review how to dispose this.objectDetector.");
         }
      }

      [SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", Justification = "Too much work for now.")]
      public void ProcessImageData(byte[,,] imageData, byte[] overlayData)
      {
      }

      private void TaggerController_TagPointChanged(object sender, TagPointChangedEventArgs e)
      {
         if (e.Added)
         {
            this.objectDetector.Add(e.Label, e.TagPoint);
         }
         else
         {
            this.objectDetector.Remove(e.Label, e.TagPoint);
         }
      }
   }
}
