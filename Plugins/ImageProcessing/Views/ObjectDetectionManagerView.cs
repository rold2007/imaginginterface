// <copyright file="ObjectDetectionManagerView.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Views
{
   using System.Drawing;
   using System.Windows.Forms;
   using ImageProcessing.Controllers;
   using ImagingInterface.Plugins;

   public partial class ObjectDetectionManagerView : UserControl, IPluginView
   {
      private ObjectDetectionManagerController objectDetectionManagerController;

      public ObjectDetectionManagerView(ObjectDetectionManagerController objectDetectionManagerController)
      {
         this.InitializeComponent();

         this.objectDetectionManagerController = objectDetectionManagerController;
      }

      public string DisplayName
      {
         get
         {
            return this.objectDetectionManagerController.DisplayName;
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

      ////public void AddView(IRawPluginView rawPluginView)
      ////{
      ////   Control pluginViewControl = rawPluginView as Control;

      ////   this.flowLayoutPanel.Controls.Add(pluginViewControl);
      ////}
   }
}
