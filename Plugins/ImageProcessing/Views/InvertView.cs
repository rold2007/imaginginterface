// <copyright file="InvertView.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Views
{
   using System;
   using System.Windows.Forms;
   using ImageProcessing.Controllers;
   using ImageProcessing.Controllers.EventArguments;
   using ImagingInterface.Plugins;

   public partial class InvertView : UserControl, IPluginView
   {
      private InvertController invertController;

      public InvertView(InvertController invertController)
      {
         this.InitializeComponent();

         this.invertController = invertController;
      }

      public event EventHandler<InvertEventArgs> Invert;

      public string DisplayName
      {
         get
         {
            return this.invertController.RawPluginModel.DisplayName;
         }
      }

      public bool Active
      {
         get
         {
            return this.invertController.Active;
         }
      }

      public void Close()
      {
      }

      private void InvertCheckBox_CheckedChanged(object sender, EventArgs e)
      {
         if (this.Invert != null)
         {
            this.Invert(this, new InvertEventArgs(this.InvertCheckBox.Checked));
         }
      }
   }
}
