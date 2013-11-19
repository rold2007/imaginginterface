namespace ImagingInterface.Views
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Plugins;

   public interface IPluginManagerView
      {
      void AddPluginView(IRawPluginView rawPluginView, IRawPluginModel rawPluginModel);

      IRawPluginView GetActivePluginView();

      void RemovePluginView(IRawPluginView rawPluginView);
      }
   }
