// <copyright file="RotateView.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Views
{
   using System;
   using System.Drawing;
   using System.Windows.Forms;
   using ImageProcessing.Controllers;
   using ImagingInterface.Plugins;

   public partial class RotateView : UserControl, IPluginView
   {
      private RotateController rotateController;

      public RotateView(RotateController rotateController)
      {
         this.InitializeComponent();

         this.rotateController = rotateController;
      }

      public string DisplayName
      {
         get
         {
            return this.rotateController.DisplayName;
         }
      }

      public void Close()
      {
      }

      public void SelectPixel(IImageSource imageSource, Point pixelPosition)
      {
      }

      private void SetRotationAngle(double angle)
      {
         this.rotationAngleTrackBar.Value = Convert.ToInt32(angle * 100);
         this.rotationAngleNumericUpDown.Value = Convert.ToDecimal(angle);

         this.rotateController.SetRotationAngle(angle);
      }

      private void RotationAngleTrackBar_Scroll(object sender, EventArgs e)
      {
         double angle = Convert.ToDouble(this.rotationAngleTrackBar.Value / 100);

         this.SetRotationAngle(angle);
      }

      private void RotationAngleNumericUpDown_ValueChanged(object sender, EventArgs e)
      {
         double angle = Convert.ToDouble(this.rotationAngleNumericUpDown.Value);

         this.SetRotationAngle(angle);
      }
   }
}
