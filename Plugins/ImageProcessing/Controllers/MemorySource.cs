namespace ImageProcessing.Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Diagnostics;
   using System.Drawing;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Plugins;

   public class MemorySource : IMemorySource
      {
      ////public string DisplayName
      ////   {
      ////   get; // ncrunch: no coverage
      ////   set; // ncrunch: no coverage
      ////   }

      ////public byte[,,] ImageData
      ////   {
      ////   get;
      ////   set;
      ////   }

      public MemorySource()
         {
         }

      public event CancelEventHandler Closing;

      public event EventHandler Closed;

      public IRawPluginView RawPluginView
         {
         get; // ncrunch: no coverage
         private set; // ncrunch: no coverage
         }

      ////public IRawPluginModel RawPluginModel
      ////   {
      ////   get
      ////      {
      ////      return this.memorySourceModel;
      ////      }
      ////   }

      public bool Active
         {
         get
            {
            return false;
            }
         }

      public byte[, ,] ImageData
         {
         get;
         private set;
         }

      public bool IsDynamic(IRawPluginModel rawPluginModel)
         {
         return false;
         }

      public byte[, ,] NextImageData(IRawPluginModel rawPluginModel)
         {
         ////IMemorySourceModel memorySourceModel = rawPluginModel as IMemorySourceModel;

         ////Debug.Assert(memorySourceModel.ImageData != null, "The image data should never be null.");

         ////return memorySourceModel.ImageData;
         return null;
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
