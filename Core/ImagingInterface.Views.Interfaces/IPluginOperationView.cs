namespace ImagingInterface.Views
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Controllers.EventArguments;

   public interface IPluginOperationView
      {
      event EventHandler<PluginCreateEventArgs> PluginCreate;

      void AddPlugin(string name);
      }
   }
