namespace ImagingInterface.Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
      using ImagingInterface.Views;

   public interface IMainController
      {
      void AddImageManagerView(IImageManagerView imageManagerView);

      void AddPluginManagerView(IPluginManagerView pluginManagerView);
      }
   }
