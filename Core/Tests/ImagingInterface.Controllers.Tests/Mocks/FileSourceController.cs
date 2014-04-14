namespace ImagingInterface.Controllers.Tests.Mocks
   {
   using System;
   using System.Collections.Generic;
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

      public IRawPluginModel RawPluginModel
         {
         get
            {
            return this.fileSourceModel;
            }
         }

      public string Filename
         {
         get;
         set;
         }

      public string DisplayName(IRawPluginModel rawPluginModel)
         {
         return "DisplayName";
         }

      public byte[,,] NextImageData(IRawPluginModel rawPluginModel)
         {
         return this.fileSourceModel.ImageData;
         }
      }
   }
