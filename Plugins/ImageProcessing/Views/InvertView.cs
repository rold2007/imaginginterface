// <copyright file="InvertView.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Views
{
   using System;
   using System.Drawing;
   using System.Windows.Forms;
   using ImageProcessing.Controllers;
   using ImagingInterface.Plugins;

   public partial class InvertView : UserControl, IPluginView
   {
      private InvertController invertController;

      public InvertView(InvertController invertController)
      {
         this.InitializeComponent();

         this.invertController = invertController;
      }

      public string DisplayName
      {
         get
         {
            return this.invertController.DisplayName;
         }
      }

      public void Close()
      {
      }

      public void SelectPixel(Point pixelPosition)
      {
      }

      public void ActiveImageSourceChanged(IImageSource imageSource)
      {
      }

      public void Activate()
      {
      }

      private void InvertCheckBox_CheckedChanged(object sender, EventArgs e)
      {
         this.invertController.Invert(this.InvertCheckBox.Checked);
      }
   }
}
