namespace ImagingInterface.Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Plugins;

   public class PluginManagerController
      {
      ////private IPluginManagerView pluginManagerView;
      private Dictionary<IRawPluginView, IPluginController> pluginControllers;

      public PluginManagerController(/*MainController mainController*/)
         {
         ////this.pluginManagerView = pluginManagerView;
         this.pluginControllers = new Dictionary<IRawPluginView, IPluginController>();

         ////mainController.AddPluginManager(this);
         }

      public void AddPlugin(IPluginController pluginController)
         {
         pluginController.Closed += this.PluginController_Closed;

         ////this.pluginManagerView.AddPlugin(pluginController.RawPluginView, pluginController.RawPluginModel);
         ////this.pluginControllers.Add(pluginController.RawPluginView, pluginController);
         }

      public IPluginController GetActivePlugin()
         {
         ////IRawPluginView activeRawPluginView = this.pluginManagerView.GetActivePlugin();

         ////if (activeRawPluginView != null)
         ////   {
         ////   return this.pluginControllers[activeRawPluginView];
         ////   }
         ////else
            {
            return null;
            }
         }

      public IList<IPluginController> GetAllPlugins()
         {
         return this.pluginControllers.Values.ToList();
         }

      public void CloseActivePlugin()
         {
         IPluginController activePlugin = this.GetActivePlugin();

         if (activePlugin != null)
            {
            activePlugin.Close();
            }
         }

      private void RemovePlugin(IPluginController pluginController)
         {
         pluginController.Closed -= this.PluginController_Closed;

         ////this.pluginManagerView.RemovePlugin(pluginController.RawPluginView);
         ////this.pluginControllers.Remove(pluginController.RawPluginView);
         }

      private void PluginController_Closed(object sender, EventArgs e)
         {
         this.RemovePlugin(sender as IPluginController);
         }
      }
   }
