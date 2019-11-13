// <copyright file="ObjectDetectionView.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Views
{
   using System;
   using System.Diagnostics.CodeAnalysis;
   using System.Drawing;
   using System.Windows.Forms;
   using ImageProcessing.Controllers;
   using ImagingInterface.Plugins;

   public partial class ObjectDetectionView : UserControl
   {
      private ObjectDetectionController objectDetectionController;

      public ObjectDetectionView(ObjectDetectionController objectDetectionController)
      {
         this.InitializeComponent();

         this.objectDetectionController = objectDetectionController;
      }

      public event EventHandler Train;

      public event EventHandler Test;

      public string DisplayName
      {
         get
         {
            return this.objectDetectionController.DisplayName;
         }
      }

      [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Will be fixed when done refactoring.")]
      public void Close()
      {
      }

      [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Too much work for now.")]
      [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Will be fixed when done refactoring.")]
      public void SelectPixel(IImageSource imageSource, Point pixelPosition)
      {
      }

      private void TrainButton_Click(object sender, EventArgs e)
      {
         this.Train?.Invoke(this, EventArgs.Empty);
      }

      private void DetectButton_Click(object sender, EventArgs e)
      {
         this.Test?.Invoke(this, EventArgs.Empty);
      }
   }
}
