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

                  this.mainImageBoxToolTip.SetToolTip(this.mainImageBox, fileName);
                  }
               catch
                  {
                  this.mainImageBoxToolTip.SetToolTip(this.mainImageBox, "Invalid file format: " + fileName);
                  }

               if (this.mainImageBox.Image != null)
                  {
                  this.mainImageBox.Image.Dispose();
                  }

               this.mainImageBox.Image = image;
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
      }
   }
