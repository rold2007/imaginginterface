namespace ImagingInterface.Views
   {
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;

   public interface IMainView
      {
      event CancelEventHandler ApplicationClosing;

      void AddImageManagerView(IImageManagerView imageManagerView);

      void AddPluginManagerView(IPluginManagerView pluginManagerView);

      void Close();
      }
   }
