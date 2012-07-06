namespace ImagingInterface
   {
   using System;
   using System.Drawing;
   using System.Windows.Forms;
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
      }
   }
