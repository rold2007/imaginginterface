namespace ImagingInterface.Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Controllers;
   using ImagingInterface.Views;

   public interface IMainController
      {
      void AddImageManager(IImageManagerController imageManagerController, IImageManagerView imageManagerView);

      void AddPluginManager(IPluginManagerController pluginManagerController, IPluginManagerView pluginManagerView);
      }
   }
