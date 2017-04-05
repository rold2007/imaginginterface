namespace ImagingInterface.Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Controllers.EventArguments;
   using ImagingInterface.Plugins;

   public class PluginOperationController
      {
      private static string closePluginName = "Close plugin"; // ncrunch: no coverage
      private SortedDictionary<string, Type> plugins;
      private PluginManagerController pluginManagerController;

      public PluginOperationController(IEnumerable<IPluginController> plugins, PluginManagerController pluginManagerController)
         {
         this.plugins = new SortedDictionary<string, Type>();
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
                throw new NotImplementedException("Need to replace obsolete use of Service Locator.");
                //See https://simpleinjector.readthedocs.io/en/latest/howto.html#resolve-instances-by-key
                //Or https://simpleinjector.readthedocs.io/en/latest/howto.html#package-registrations
            //IPluginController pluginController = this.serviceLocator.GetInstance(this.plugins[e.Name]) as IPluginController;

            //pluginController.Initialize();

            //this.pluginManagerController.AddPlugin(pluginController);
            }
         }
      }
   }
