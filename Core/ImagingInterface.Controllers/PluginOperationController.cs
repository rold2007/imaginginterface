namespace ImagingInterface.Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Plugins;
   using ImagingInterface.Views;
   using ImagingInterface.Views.EventArguments;
   using Microsoft.Practices.ServiceLocation;

   public class PluginOperationController : IPluginOperationController
      {
      private IServiceLocator serviceLocator;
      private SortedDictionary<string, Type> plugins;

      public PluginOperationController(IPluginOperationView pluginOperationView, IEnumerable<IPluginController> plugins, IServiceLocator serviceLocator)
         {
         this.plugins = new SortedDictionary<string, Type>();
         this.serviceLocator = serviceLocator;

         foreach (IPluginController plugin in plugins)
            {
            this.plugins.Add(plugin.RawPluginModel.DisplayName, plugin.GetType());
            pluginOperationView.AddPlugin(plugin.RawPluginModel.DisplayName);
            }

         pluginOperationView.PluginCreate += this.PluginCreate;
         }

      private void PluginCreate(object sender, PluginCreateEventArgs e)
         {
         IPluginController pluginController = this.serviceLocator.GetInstance(this.plugins[e.Name]) as IPluginController;
         IPluginManagerController pluginManagerController = this.serviceLocator.GetInstance<IPluginManagerController>();

         pluginManagerController.AddPlugin(pluginController);
         }
      }
   }
