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

   public class MemorySourceController : IMemorySourceController
      {
      private IMemorySourceModel memorySourceModel;

      public MemorySourceController(IMemorySourceModel memorySourceModel)
         {
         this.memorySourceModel = memorySourceModel;
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
         get
            {
            return this.memorySourceModel;
            }
         }

      public bool Active
         {
         get
            {
            return false;
            }
         }

      public byte[, ,] ImageData
         {
         get
            {
            return this.memorySourceModel.ImageData;
            }

         set
            {
            Debug.Assert(value != null, "ImageData cannot be null.");

            this.memorySourceModel.ImageData = value;
            }
         }

      public bool IsDynamic(IRawPluginModel rawPluginModel)
         {
         return false;
         }

      public byte[, ,] NextImageData(IRawPluginModel rawPluginModel)
         {
         IMemorySourceModel memorySourceModel = rawPluginModel as IMemorySourceModel;

         Debug.Assert(memorySourceModel.ImageData != null, "The image data should never be null.");

         return memorySourceModel.ImageData;
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
