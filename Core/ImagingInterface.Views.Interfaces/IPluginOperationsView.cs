namespace ImagingInterface.Views
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Views.EventArguments;

   public interface IPluginOperationsView
      {
      event EventHandler<PluginCreateEventArgs> PluginCreate;

      void AddPlugin(string name);
      }
   }
