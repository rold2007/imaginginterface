namespace ImagingInterface.Plugins
   {
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;

   public interface IPluginController
      {
      event CancelEventHandler Closing;

      event EventHandler Closed;

      IRawPluginView RawPluginView
         {
         get;
         }

      IRawPluginModel RawPluginModel
         {
         get;
         }

      bool Active
         {
         get;
         }

      void Initialize();

      void Close();
      }
   }
