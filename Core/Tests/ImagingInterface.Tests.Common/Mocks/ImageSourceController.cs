namespace ImagingInterface.Tests.Common.Mocks
   {
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Plugins;

   public class ImageSourceController : IImageSourceController
      {
      public ImageSourceController()
         {
         this.ImageData = new byte[50, 50, 1];
         }

      public event CancelEventHandler Closing;

      public event EventHandler Closed;

      public byte[, ,] ImageData
         {
         get;
         set;
         }

      public IRawPluginView RawPluginView
         {
         get;
         private set;
         }

      public IRawPluginModel RawPluginModel
         {
         get;
         set;
         }

      public bool Active
         {
         get
            {
            return true;
            }
         }

      public bool IsDynamic(IRawPluginModel rawPluginModel)
         {
         return false;
         }

      public byte[, ,] NextImageData(IRawPluginModel rawPluginModel)
         {
         return this.ImageData;
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
            }
         }

      public void Disconnected()
         {
         }
      }
   }
