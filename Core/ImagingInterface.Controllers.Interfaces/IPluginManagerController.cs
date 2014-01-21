namespace ImagingInterface.Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Plugins;

   public interface IPluginManagerController
      {
      void AddPlugin(IPluginController pluginController);

      IPluginController GetActivePlugin();

      IList<IPluginController> GetAllPlugins();
      }
   }
