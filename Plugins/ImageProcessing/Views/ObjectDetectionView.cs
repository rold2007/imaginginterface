// <copyright file="ObjectDetectionView.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Views
{
   using System;
   using System.Drawing;
   using System.Windows.Forms;
   using ImageProcessing.Controllers;
   using ImagingInterface.Plugins;

   public partial class ObjectDetectionView : UserControl, IPluginView
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

      public void Close()
      {
      }

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
