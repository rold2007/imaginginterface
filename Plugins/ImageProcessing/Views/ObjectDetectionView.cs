namespace ImageProcessing.Views
   {
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Data;
   using System.Drawing;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using System.Windows.Forms;

   public partial class ObjectDetectionView : UserControl, IObjectDetectionView
      {
      public ObjectDetectionView()
         {
         this.InitializeComponent();
         }

      public event EventHandler Train;

      public event EventHandler Test;

      public void Close()
         {
         }

      private void TrainButton_Click(object sender, EventArgs e)
         {
         if (this.Train != null)
            {
            this.Train(this, EventArgs.Empty);
            }
         }

      private void DetectButton_Click(object sender, EventArgs e)
         {
         if (this.Test != null)
            {
            this.Test(this, EventArgs.Empty);
            }
         }
      }
   }
