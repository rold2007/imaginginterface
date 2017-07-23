namespace ImageProcessing.Views
{
   using System;
   using System.Windows.Forms;
   using ImageProcessing.Controllers;
   using ImageProcessing.Controllers.EventArguments;
   using ImagingInterface.Plugins;

   public partial class RotateView : UserControl, IPluginView
   {
      private RotateController rotateController;

      public RotateView(RotateController rotateController)
      {
         this.InitializeComponent();

         this.rotateController = rotateController;
      }

      public event EventHandler<RotateEventArgs> Rotate;

      public string DisplayName
      {
         get
         {
            return this.rotateController.RawPluginModel.DisplayName;
         }
      }

      public bool Active
      {
         get
         {
            return this.rotateController.Active;
         }
      }

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
