namespace ImagingInterface.Controllers.Tests.Mocks
   {
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Diagnostics;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Plugins;

   public class PluginController1 : IPluginController, IImageProcessingService
      {
      public PluginController1(PluginModel1 pluginModel)
         {
         ////this.RawPluginView = pluginView;
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

      public bool Active
         {
         get
            {
            return true;
            }
         }

      public bool IsClosed
         {
         get;
         private set;
         }

      public void Initialize()
         {
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

            this.IsClosed = true;
            }
         }

      public byte[, ,] ProcessImageData(byte[, ,] imageData, byte[] overlayData, IRawPluginModel rawPluginModel)
         {
         return imageData.Clone() as byte[, ,];
         }
      }
   }
