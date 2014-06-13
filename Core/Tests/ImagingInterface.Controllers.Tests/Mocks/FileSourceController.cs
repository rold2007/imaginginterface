namespace ImagingInterface.Controllers.Tests.Mocks
   {
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Plugins;

   public class FileSourceController : IFileSourceController
      {
      private IFileSourceModel fileSourceModel;

      public FileSourceController(IFileSourceModel fileSourceModel)
         {
         this.fileSourceModel = fileSourceModel;
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
            return this.fileSourceModel;
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
         get
            {
            return this.fileSourceModel.DisplayName;
            }

         set
            {
            this.fileSourceModel.DisplayName = value;
            }
         }

      public void Initialize()
         {
         }

      public void Close()
         {
         }

      public bool IsDynamic(IRawPluginModel rawPluginModel)
         {
         return false;
         }

      public byte[,,] NextImageData(IRawPluginModel rawPluginModel)
         {
         if (this.fileSourceModel.ImageData == null)
            {
            this.fileSourceModel.ImageData = new byte[1, 1, 1];
            }

         return this.fileSourceModel.ImageData;
         }

      public void Disconnected()
         {
         }
      }
   }
