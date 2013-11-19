namespace ImagingInterface.Views.EventArguments
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;

   public class PluginCreateEventArgs : EventArgs
      {
      public PluginCreateEventArgs(string name)
         {
         this.Name = name;
         }

      public string Name
         {
         get;
         set;
         }
      }
   }
