namespace ImageProcessing.Views
   {
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Data;
   using System.Drawing;
   using System.Drawing.Imaging;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using System.Windows.Forms;
   using ImageProcessing.Models;
   using ImageProcessing.Views;

   public partial class TaggerView : UserControl, ITaggerView
      {
      private ITaggerModel taggerModel;
      
      public TaggerView()
         {
         this.InitializeComponent();
         }

      public event EventHandler LabelAdded;

      public void SetTaggerModel(ITaggerModel taggerModel)
         {
         this.taggerModel = taggerModel;
         }

      public void UpdateLabelList()
         {
         this.labelsListView.Items.Clear();
         this.ClearImageList();

         int imageIndex = 0;

         foreach (KeyValuePair<string, double[]> label in this.taggerModel.Labels)
            {
            Bitmap bitmap = new Bitmap(16, 16, PixelFormat.Format24bppRgb);

            using (Graphics graphics = Graphics.FromImage(bitmap))
               {
               Color color = Color.FromArgb(Convert.ToInt32(label.Value[0]), Convert.ToInt32(label.Value[1]), Convert.ToInt32(label.Value[2]));

               using (SolidBrush solidBrush = new SolidBrush(color))
                  {
                  graphics.FillRectangle(solidBrush, 0, 0, 15, 15);
                  }
               }

            this.imageList.Images.Add(bitmap);

            ListViewItem listViewItem = new ListViewItem(label.Key, imageIndex);

            listViewItem.Name = label.Key;

            this.labelsListView.Items.Add(listViewItem);

            imageIndex++;
            }

         if (this.taggerModel.SelectedLabel != null)
            {
            this.labelsListView.Items[this.taggerModel.SelectedLabel].Selected = true;
            }
         }

      public void Close()
         {
         this.ClearImageList();
         }

      private void AddButton_Click(object sender, EventArgs e)
         {
         this.taggerModel.AddedLabel = this.labelTextBox.Text;

         if (this.LabelAdded != null)
            {
            this.LabelAdded(this, EventArgs.Empty);
            }

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
            this.taggerModel.SelectedLabel = this.labelsListView.SelectedItems[0].Text;
            }
         else
            {
            this.taggerModel.SelectedLabel = null;
            }
         }
      }
   }
