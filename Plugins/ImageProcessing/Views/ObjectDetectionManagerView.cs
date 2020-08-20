// <copyright file="ObjectDetectionManagerView.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Views
{
   using System.Collections.Generic;
   using System.Drawing;
   using System.Drawing.Imaging;
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
            return ObjectDetectionManagerController.DisplayName;
         }
      }

      public void Close()
      {
      }

      public void SelectPixel(IImageSource imageSource, Point pixelPosition)
      {
         this.objectDetectionManagerController.SelectPixel(imageSource, pixelPosition);
      }

      private void LabelTextBox_TextChanged(object sender, System.EventArgs e)
      {
         if (!string.IsNullOrWhiteSpace(this.labelTextBox.Text))
         {
            this.addButton.Enabled = true;
         }
         else
         {
            this.addButton.Enabled = false;
         }
      }

      private void AddButton_Click(object sender, System.EventArgs e)
      {
         this.objectDetectionManagerController.AddLabel(this.labelTextBox.Text);

         this.UpdateLabelList();
      }

      private void RemoveButton_Click(object sender, System.EventArgs e)
      {
         List<string> labels = new List<string>();

         foreach (ListViewItem listViewItem in this.labelsListView.SelectedItems)
         {
            labels.Add(listViewItem.Text);
         }

         this.objectDetectionManagerController.RemoveLabels(labels);

         this.UpdateLabelList();
      }

      private void UpdateLabelList()
      {
         this.labelsListView.Items.Clear();
         this.ClearImageList();

         int imageIndex = 0;

         foreach (string label in this.objectDetectionManagerController.Labels)
         {
            Color color = this.objectDetectionManagerController.TagColor(label);
            Bitmap bitmap = new Bitmap(16, 16, PixelFormat.Format24bppRgb);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
               Color colorWithAlpha = Color.FromArgb(255, color);

               using (SolidBrush solidBrush = new SolidBrush(colorWithAlpha))
               {
                  graphics.FillRectangle(solidBrush, 0, 0, 15, 15);
               }
            }

            this.imageList.Images.Add(bitmap);

            ListViewItem listViewItem = new ListViewItem(label, imageIndex)
            {
               Name = label,
            };
            this.labelsListView.Items.Add(listViewItem);

            imageIndex++;
         }

         this.objectDetectionManagerController.SelectLabel(null);
         this.UpdateRemoveButtonState();
      }

      private void ClearImageList()
      {
         foreach (Image image in this.imageList.Images)
         {
            image.Dispose();
         }

         this.imageList.Images.Clear();
      }

      private void UpdateRemoveButtonState()
      {
         if (this.labelsListView.SelectedItems.Count > 0)
         {
            this.removeButton.Enabled = true;
         }
         else
         {
            this.removeButton.Enabled = false;
         }
      }

      private void LabelsListView_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         if (this.labelsListView.SelectedItems.Count > 0)
         {
            this.objectDetectionManagerController.SelectLabel(this.labelsListView.SelectedItems[0].Text);
         }
         else
         {
            this.objectDetectionManagerController.SelectLabel(null);
         }

         this.UpdateRemoveButtonState();
      }

      private void TrainButton_Click(object sender, System.EventArgs e)
      {
         this.objectDetectionManagerController.TrainModel();
      }
   }
}
