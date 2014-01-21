namespace ImagingInterface.Controllers.Tests.Mocks
   {
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Plugins;

   public class PluginController1 : IPluginController
      {
      public PluginController1(IPluginView pluginView, PluginModel1 pluginModel)
         {
         this.RawPluginView = pluginView;
         this.RawPluginModel = pluginModel;

         this.RawPluginModel.DisplayName = "Plugin1";
         }

      public event CancelEventHandler Closing;

      public event EventHandler Closed;

      public IRawPluginView RawPluginView
         {
         get;
         private set;
         }

      public IRawPluginModel RawPluginModel
         {
         get;
         private set;
         }

      public void Close()
         {
         CancelEventArgs cancelEventArgs = new CancelEventArgs();

         if (this.Closing != null)
            {
            this.Closing(this, cancelEventArgs);
            }

         if (!cancelEventArgs.Cancel)
            {
            if (this.Closed != null)
               {
               this.Closed(this, EventArgs.Empty);
               }
            }
         }
      }
   }
