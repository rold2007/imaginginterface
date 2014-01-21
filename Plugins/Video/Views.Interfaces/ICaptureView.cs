namespace Video.Views
   {
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Plugins;

   public interface ICaptureView : IPluginView
      {
      event EventHandler Start;

      event EventHandler Stop;

      event EventHandler SnapShot;

      void UpdateLiveGrabStatus(bool allowGrab, bool liveGrabRunning);
      }
   }
