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
   using ImageProcessing.Views;

   public partial class RotateView : UserControl, IRotateView
      {
      public RotateView()
         {
         this.InitializeComponent();
         }

      public event EventHandler Rotate;

      private void RotateRightButton_Click(object sender, EventArgs e)
         {
         if (this.Rotate != null)
            {
            this.Rotate(this, EventArgs.Empty);
            }
         }
      }
   }
