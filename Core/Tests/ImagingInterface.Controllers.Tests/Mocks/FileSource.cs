namespace ImagingInterface.Controllers.Tests.Mocks
   {
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Plugins;

   public class FileSourceController : IFileSource
      {
      public FileSourceController()
         {
         }

      public event CancelEventHandler Closing;

      public event EventHandler Closed;

      ////public IRawPluginModel RawPluginModel
      ////   {
      ////   get
      ////      {
      ////      return this.fileSourceModel;
      ////      }
      ////   }

      public byte[,,] ImageData
         {
         get;
         }

      public string ImageName
         {
         get
            {
            return "Mock";
            }
         }

      public bool Active
         {
         get
            {
            return false;
            }
         }

      public string Filename
         {
         get;
         private set;
         }

      public void Initialize()
         {
         }

      public bool LoadFile(string file)
         {
         return false;
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

      public bool IsDynamic(IRawPluginModel rawPluginModel)
         {
         return false;
         }

      public byte[,,] NextImageData(IRawPluginModel rawPluginModel)
         {
         ////if (this.fileSourceModel.ImageData == null)
         ////   {
         ////   this.fileSourceModel.ImageData = new byte[1, 1, 1];
         ////   }

         ////return this.fileSourceModel.ImageData;
         return null;
         }

      public void Disconnected()
         {
         }
      }
   }
