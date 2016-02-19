namespace ImagingInterface.Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Controllers.EventArguments;
   using ImagingInterface.Plugins;
   using Microsoft.Practices.ServiceLocation;

   public class PluginOperationController
      {
      private static string closePluginName = "Close plugin"; // ncrunch: no coverage
      private IServiceLocator serviceLocator;
      private SortedDictionary<string, Type> plugins;
      private PluginManagerController pluginManagerController;

      public PluginOperationController(IEnumerable<IPluginController> plugins, IServiceLocator serviceLocator, PluginManagerController pluginManagerController)
         {
         this.plugins = new SortedDictionary<string, Type>();
         this.serviceLocator = serviceLocator;
         this.pluginManagerController = pluginManagerController;

         foreach (IPluginController plugin in plugins)
            {
            if (plugin.Active)
               {
               this.plugins.Add(plugin.RawPluginModel.DisplayName, plugin.GetType());
               ////pluginOperationView.AddPlugin(plugin.RawPluginModel.DisplayName);
               }
            }

         ////pluginOperationView.AddPlugin(PluginOperationController.closePluginName);

         ////pluginOperationView.PluginCreate += this.PluginCreate;
         }

      private void PluginCreate(object sender, PluginCreateEventArgs e)
         {
         if (e.Name == PluginOperationController.closePluginName)
            {
            this.pluginManagerController.CloseActivePlugin();
            }
         else
            {
            IPluginController pluginController = this.serviceLocator.GetInstance(this.plugins[e.Name]) as IPluginController;

            pluginController.Initialize();

            this.pluginManagerController.AddPlugin(pluginController);
            }
         }
      }
   }
