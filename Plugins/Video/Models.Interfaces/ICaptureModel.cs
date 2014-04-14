namespace Video.Models
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Plugins;

   public interface ICaptureModel : IRawPluginModel, ICloneable
      {
      byte[, ,] LastImageData
         {
         get;
         set;
         }

      bool LiveGrabRunning
         {
         get;
         set;
         }
      }
   }
