namespace Video.Models
{
   using System;
   using System.Collections.Generic;
   using System.Diagnostics;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Plugins;

   public class CaptureModel : IRawPluginModel, ICloneable ////: ICaptureModel
   {
      public string DisplayName
      {
         get;
         set;
      }

      public byte[,,] LastImageData
      {
         get;
         set;
      }

      public bool LiveGrabRunning
      {
         get;
         set;
      }

      public Stopwatch TimeSinceLastGrab
      {
         get;
         set;
      }

      public object Clone()
      {
         return this.MemberwiseClone();
      }
   }
}
