namespace Video.Views
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

   public partial class CaptureView : UserControl, ICaptureView
      {
      public CaptureView()
         {
         this.InitializeComponent();
         }

      public event EventHandler Start;

      public event EventHandler Stop;

      public event EventHandler SnapShot;

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
         if (this.Start != null)
            {
            this.Start(this, EventArgs.Empty);
            }
         }

      private void StopCaptureButton_Click(object sender, EventArgs e)
         {
         if (this.Stop != null)
            {
            this.Stop(this, EventArgs.Empty);
            }
         }

      private void SnapshotButton_Click(object sender, EventArgs e)
         {
         if (this.SnapShot != null)
            {
            this.SnapShot(this, EventArgs.Empty);
            }
         }
      }
   }
