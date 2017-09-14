// <copyright file="TaggerView.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Views
{
   using System;
   using System.Collections.Generic;
   using System.Drawing;
   using System.Drawing.Imaging;
   using System.Windows.Forms;
   using ImageProcessing.Controllers;
   using ImagingInterface.Plugins;

   public partial class TaggerView : UserControl, IPluginView
   {
      ////private TaggerModel taggerModel;
      private TaggerController taggerController;

      public TaggerView(TaggerController taggerController)
      {
         this.InitializeComponent();

         this.taggerController = taggerController;
      }

      ////public event EventHandler LabelAdded;

      public string DisplayName
      {
         get
         {
            return this.taggerController.DisplayName;
         }
      }

      public bool Active
      {
         get
         {
            return this.taggerController.Active;
         }
      }

      public void UpdateLabelList()
      {
         this.labelsListView.Items.Clear();
         this.ClearImageList();

         int imageIndex = 0;

         foreach (string label in this.taggerController.Labels)
         {
            ////Color color = this.taggerController.LabelColors[label];
            Color color = Color.AliceBlue;
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
               Name = label
            };
            this.labelsListView.Items.Add(listViewItem);

            imageIndex++;
         }

         ////if (this.taggerModel.SelectedLabel != null)
         ////{
         ////   this.labelsListView.Items[this.taggerModel.SelectedLabel].Selected = true;
         ////}

         this.UpdateRemoveButtonState();
      }

      public void Close()
      {
         this.ClearImageList();
      }

      private void AddButton_Click(object sender, EventArgs e)
      {
         this.taggerController.AddLabel(this.labelTextBox.Text);

         this.UpdateLabelList();
      }

      private void RemoveButton_Click(object sender, EventArgs e)
      {
         List<string> labels = new List<string>();

         foreach (ListViewItem listViewItem in this.labelsListView.SelectedItems)
         {
            labels.Add(listViewItem.Text);
         }

         this.taggerController.RemoveLabels(labels);

         this.UpdateLabelList();
      }

      private void ClearImageList()
      {
         foreach (Image image in this.imageList.Images)
         {
            image.Dispose();
         }

         this.imageList.Images.Clear();
      }

      private void LabelTextBox_TextChanged(object sender, EventArgs e)
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

      private void LabelsListView_SelectedIndexChanged(object sender, EventArgs e)
      {
         if (this.labelsListView.SelectedItems.Count > 0)
         {
            ////this.taggerModel.SelectedLabel = this.labelsListView.SelectedItems[0].Text;
         }
         else
         {
            ////this.taggerModel.SelectedLabel = null;
         }

         this.UpdateRemoveButtonState();
      }

      private void UpdateRemoveButtonState()
      {
         if (this.labelsListView.SelectedItems.Count > 0)
         {
            this.removeButton.Enabled = true;
            ////this.taggerModel.SelectedLabel = this.labelsListView.SelectedItems[0].Text;
         }
         else
         {
            this.removeButton.Enabled = false;
            ////this.taggerModel.SelectedLabel = null;
         }
      }
   }
}
