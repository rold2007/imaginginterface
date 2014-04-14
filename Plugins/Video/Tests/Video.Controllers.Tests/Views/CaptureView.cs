namespace Video.Controllers.Tests.Views
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using Video.Views;

   public class CaptureView : ICaptureView
      {
      public event EventHandler Start;

      public event EventHandler Stop;

      public event EventHandler SnapShot;

      public bool AllowGrab
         {
         get;
         private set;
         }

      public bool LiveGrabRunning
         {
         get;
         private set;
         }

      public bool CloseCalled
         {
         get;
         private set;
         }

      public void UpdateLiveGrabStatus(bool allowGrab, bool liveGrabRunning)
         {
         this.AllowGrab = allowGrab;
         this.LiveGrabRunning = liveGrabRunning;
         }

      public void Hide()
         {
         }

      public void Close()
         {
         this.CloseCalled = true;
         }

      public void TriggerStart()
         {
         this.Start(this, EventArgs.Empty);
         }

      public void TriggerStop()
         {
         this.Stop(this, EventArgs.Empty);
         }

      public void TriggerSnapShot()
         {
         this.SnapShot(this, EventArgs.Empty);
         }
      }
   }
