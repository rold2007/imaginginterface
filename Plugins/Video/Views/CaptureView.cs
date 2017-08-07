namespace Video.Views
{
   using System;
   using System.Windows.Forms;
   using Video.Controllers;

   public partial class CaptureView : UserControl, ICaptureView
   {
      private CaptureController captureController;

      public CaptureView(CaptureController captureController)
      {
         this.InitializeComponent();

         this.captureController = captureController;
      }

      public event EventHandler Start;

      public event EventHandler Stop;

      public event EventHandler SnapShot;

      public string DisplayName
      {
         get
         {
            return this.captureController.RawPluginModel.DisplayName;
         }
      }

      public bool Active
      {
         get
         {
            return this.captureController.Active;
         }
      }

      public void UpdateLiveGrabStatus(bool allowGrab, bool liveGrabRunning)
      {
         this.startCaptureButton.Enabled = allowGrab;
         this.stopCaptureButton.Enabled = liveGrabRunning;
         this.snapshotButton.Enabled = allowGrab;
      }

      public void Close()
      {
      }

      private void StartCaptureButton_Click(object sender, EventArgs e)
      {
         this.Start?.Invoke(this, EventArgs.Empty);
      }

      private void StopCaptureButton_Click(object sender, EventArgs e)
      {
         this.Stop?.Invoke(this, EventArgs.Empty);
      }

      private void SnapshotButton_Click(object sender, EventArgs e)
      {
         this.SnapShot?.Invoke(this, EventArgs.Empty);
      }
   }
}
