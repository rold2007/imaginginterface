namespace ImageProcessing.Views
   {
   using System;
   using System.Windows.Forms;
   using ImageProcessing.Controllers.EventArguments;

   public partial class RotateView : UserControl, IRotateView
      {
      public RotateView()
         {
         this.InitializeComponent();
         }

      public event EventHandler<RotateEventArgs> Rotate;

      public void Close()
         {
         }

      private void SetRotationAngle(double angle)
         {
         this.rotationAngleTrackBar.Value = Convert.ToInt32(angle * 100);
         this.rotationAngleNumericUpDown.Value = Convert.ToDecimal(angle);

         if (this.Rotate != null)
            {
            this.Rotate(this, new RotateEventArgs(angle));
            }
         }

      private void RotationAngleTrackBar_Scroll(object sender, EventArgs e)
         {
         double angle = Convert.ToDouble(this.rotationAngleTrackBar.Value / 100);

         this.SetRotationAngle(angle);
         }

      private void RotationAngleNumericUpDown_ValueChanged(object sender, EventArgs e)
         {
         double angle = Convert.ToDouble(this.rotationAngleNumericUpDown.Value);

         this.SetRotationAngle(angle);
         }
      }
   }
