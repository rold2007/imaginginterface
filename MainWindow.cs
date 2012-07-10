namespace ImagingInterface
   {
   using System;
   using System.Collections.Generic;
   using System.Drawing;
   using System.Linq;
   using System.Windows.Forms;
   using AForge.Imaging;
   using AForge.Imaging.Filters;
   using Emgu.CV;
   using Emgu.CV.Structure;

   public partial class mainWindow : Form
      {
      public mainWindow()
         {
         InitializeComponent();
         }

      private void mainWindow_DragDrop(object sender, DragEventArgs e)
         {
         if (this.DragEventValid(e))
            {
            string[] data = e.Data.GetData(DataFormats.FileDrop) as string[];

            if (data != null)
               {
               string fileName = data[0];
               Image<Rgb, UInt16> image = null;

               try
                  {
                  image = new Image<Rgb, UInt16>(fileName);

                  this.imagingInterfaceToolTip.SetToolTip(this.mainImageBox, fileName);
                  }
               catch
                  {
                  this.imagingInterfaceToolTip.SetToolTip(this.mainImageBox, "Invalid file format: " + fileName);
                  }

               if (this.mainImageBox.Image != null)
                  {
                  this.mainImageBox.Image.Dispose();
                  }

               this.mainImageBox.Image = image;

               UpdateBlobAreaFilterRange();
               }
            }
         }

      private void mainWindow_DragEnter(object sender, DragEventArgs e)
         {
         if (DragEventValid(e))
            {
            e.Effect = DragDropEffects.Copy;
            }
         else
            {
            e.Effect = DragDropEffects.None;
            }
         }

      private bool DragEventValid(DragEventArgs e)
         {
         if ((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
            {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
               {
               string[] data = e.Data.GetData(DataFormats.FileDrop) as string[];

               if (data.Length == 1)
                  {
                  return true;
                  }
               }
            }

         return false;
         }

      private void backgroundColorTrackBar_Scroll(object sender, EventArgs e)
         {
         this.backgroundColorLabel.Text = "Background color: " + this.backgroundColorTrackBar.Value.ToString();
         }

      private void minAreaThresholdTrackBar_ValueChanged(object sender, EventArgs e)
         {
         UpdateMinAreaLabel();

         if (this.minAreaThresholdTrackBar.Value > this.maxAreaThresholdTrackBar.Value)
            {
            this.maxAreaThresholdTrackBar.Value = this.minAreaThresholdTrackBar.Value;
            }
         }

      private void maxAreaThresholdTrackBar_ValueChanged(object sender, EventArgs e)
         {
         UpdateMaxAreaLabel();

         if (this.maxAreaThresholdTrackBar.Value < this.minAreaThresholdTrackBar.Value)
            {
            this.minAreaThresholdTrackBar.Value = this.maxAreaThresholdTrackBar.Value;
            }
         }

      private void UpdateBlobAreaFilterRange()
         {
         this.minAreaThresholdLabel.Enabled = true;
         this.minAreaThresholdTrackBar.Enabled = true;
         this.maxAreaThresholdLabel.Enabled = true;
         this.maxAreaThresholdTrackBar.Enabled = true;
         this.blobAnalysisButton.Enabled = true;
         this.maxAreaThresholdTrackBar.Minimum = 0;

         int imageSize = this.mainImageBox.Image.Size.Height * this.mainImageBox.Image.Size.Width;

         this.minAreaThresholdTrackBar.Maximum = imageSize;
         this.maxAreaThresholdTrackBar.Maximum = imageSize;

         this.minAreaThresholdTrackBar.Value = 0;
         this.maxAreaThresholdTrackBar.Value = imageSize;

         this.minAreaThresholdTrackBar.LargeChange = (imageSize / 100) > 0 ? (imageSize / 100) : 1;
         this.maxAreaThresholdTrackBar.LargeChange = (imageSize / 100) > 0 ? (imageSize / 100) : 1;
         this.minAreaThresholdTrackBar.TickFrequency = (imageSize / 20) > 0 ? (imageSize / 20) : 1;
         this.maxAreaThresholdTrackBar.TickFrequency = (imageSize / 20) > 0 ? (imageSize / 20) : 1;

         UpdateMinAreaLabel();
         UpdateMaxAreaLabel();
         }

      private void UpdateMinAreaLabel()
         {
         this.minAreaThresholdLabel.Text = "Min area threshold: " + this.minAreaThresholdTrackBar.Value.ToString();
         }

      private void UpdateMaxAreaLabel()
         {
         this.maxAreaThresholdLabel.Text = "Max area threshold: " + this.maxAreaThresholdTrackBar.Value.ToString();
         }

      private void blobAnalysisButton_Click(object sender, EventArgs e)
         {
         BlobCounter blobCounter = new BlobCounter();

         blobCounter.BackgroundThreshold = Color.FromArgb(this.backgroundColorTrackBar.Value, this.backgroundColorTrackBar.Value, this.backgroundColorTrackBar.Value);
         blobCounter.BlobsFilter = new BlobFilter(this.minAreaThresholdTrackBar.Value, this.maxAreaThresholdTrackBar.Value);
         blobCounter.FilterBlobs = true;
         blobCounter.ObjectsOrder = ObjectsOrder.YX;

         if (this.whiteBackgroundCheckBox.Checked)
            {
            Invert invert = new Invert();

            using (System.Drawing.Bitmap processImage = invert.Apply(this.mainImageBox.Image.Bitmap))
               {
               blobCounter.ProcessImage(processImage);
               }
            }
         else
            {
            blobCounter.ProcessImage(this.mainImageBox.Image.Bitmap);
            }

         this.blobRectangleImageList.Images.Clear();
         this.blobImageList.Images.Clear();
         this.blobListView.Groups.Clear();

         Crop crop = new Crop(new Rectangle());
         Blob[] blobs = blobCounter.GetObjects(this.mainImageBox.Image.Bitmap, false);
         int line = 1;
         List<float> yPositions = new List<float>();
         float maxHeightDifference = this.mainImageBox.Image.Size.Height / 10;

         foreach (Blob blob in blobs)
            {
            crop.Rectangle = blob.Rectangle;

            using (Bitmap blobImage = crop.Apply(this.mainImageBox.Image.Bitmap))
               {
               this.blobRectangleImageList.Images.Add(blobImage);
               
               }
            this.blobImageList.Images.Add(blob.Image.ToManagedImage());

            if (yPositions.Count == 0)
               {
               ListViewGroup listViewGroup = new ListViewGroup("Line " + line.ToString());

               this.blobListView.Groups.Add(listViewGroup);
               line++;

               yPositions.Clear();
               yPositions.Add(blob.CenterOfGravity.Y);
               }
            else
               {
               float average = yPositions.Average();

               if (Math.Abs(blob.CenterOfGravity.Y - average) > maxHeightDifference)
                  {
                  ListViewGroup listViewGroup = new ListViewGroup("Line " + line.ToString());

                  this.blobListView.Groups.Add(listViewGroup);
                  line++;

                  yPositions.Clear();
                  yPositions.Add(blob.CenterOfGravity.Y);
                  }
               else
                  {
                  yPositions.Add(blob.CenterOfGravity.Y);
                  }
               }

            ListViewItem listViewItem = new ListViewItem();

            listViewItem.Group = this.blobListView.Groups[this.blobListView.Groups.Count - 1];
            listViewItem.ImageIndex = this.blobRectangleImageList.Images.Count - 1;

            this.blobListView.Items.Add(listViewItem);
            }
         }

      private void viewBlobCheckBox_CheckedChanged(object sender, EventArgs e)
         {
         if(this.viewBlobCheckBox.Checked)
            {
            this.blobListView.LargeImageList = this.blobImageList;
            }
         else
            {
            this.blobListView.LargeImageList = this.blobRectangleImageList;
            }
         }
      }

   public class BlobFilter : IBlobsFilter
      {
      public int MinAreaThreshold
         {
         get;
         private set;
         }
      public int MaxAreaThreshold
         {
         get;
         private set;
         }

      private BlobFilter()
         {
         }

      public BlobFilter(int minAreaThreshold, int maxAreaThreshold)
         : this()
         {
         this.MinAreaThreshold = minAreaThreshold;
         this.MaxAreaThreshold = maxAreaThreshold;
         }

      public bool Check(Blob blob)
         {
         if (blob.Area < this.MinAreaThreshold)
            {
            return false;
            }
         else if (blob.Area > this.MaxAreaThreshold)
            {
            return false;
            }

         return true;
         }
      }
   }
