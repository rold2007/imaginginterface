namespace ImagingInterface.Controllers.Tests.Views
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Views;
   using ImagingInterface.Views.EventArguments;

   public class PluginOperationsView : IPluginOperationView
      {
      public event EventHandler<PluginCreateEventArgs> PluginCreate;

      public void AddPlugin(string name)
         {
         }

      public void TriggerPluginCreate(string pluginName)
         {
         this.PluginCreate(this, new PluginCreateEventArgs(pluginName));
         }
      }
   }
