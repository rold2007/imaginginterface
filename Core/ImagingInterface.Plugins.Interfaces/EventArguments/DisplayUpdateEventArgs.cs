namespace ImagingInterface.Plugins.EventArguments
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Plugins;

   public class DisplayUpdateEventArgs : EventArgs
      {
      public DisplayUpdateEventArgs(IRawPluginModel rawPluginModel)
         {
         this.RawPluginModel = rawPluginModel;
         }

      public IRawPluginModel RawPluginModel
         {
         get;
         set;
         }
      }
   }
