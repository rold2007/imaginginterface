namespace ImagingInterface.Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Plugins;
   using ImagingInterface.Views;

   public class PluginManagerController : IPluginManagerController
      {
      private IPluginManagerView pluginManagerView;

      public PluginManagerController(IPluginManagerView pluginManagerView, IMainController mainController)
         {
         this.pluginManagerView = pluginManagerView;

         mainController.AddPluginManagerView(this.pluginManagerView);
         }

      public void Add(IPluginController pluginController)
         {
         this.pluginManagerView.AddPluginView(pluginController.RawPluginView, pluginController.RawPluginModel);
         }
      }
   }
