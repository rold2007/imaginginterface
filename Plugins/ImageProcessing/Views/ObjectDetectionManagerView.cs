﻿namespace ImageProcessing.Views
{
   using System.Windows.Forms;
   using ImageProcessing.Controllers;
   using ImagingInterface.Plugins;

   public partial class ObjectDetectionManagerView : UserControl, IObjectDetectionManagerView
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
            return this.objectDetectionManagerController.RawPluginModel.DisplayName;
         }
      }

      public bool Active
      {
         get
         {
            return this.objectDetectionManagerController.Active;
         }
      }

      public void Close()
      {
      }

      public void AddView(IRawPluginView rawPluginView)
      {
         Control pluginViewControl = rawPluginView as Control;

         this.flowLayoutPanel.Controls.Add(pluginViewControl);
      }
   }
}
