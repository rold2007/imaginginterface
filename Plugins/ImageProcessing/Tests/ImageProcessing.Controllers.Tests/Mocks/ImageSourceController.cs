namespace ImageProcessing.Controllers.Tests.Mocks
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Plugins;

   public class ImageSourceController : IImageSourceController
      {
      public ImageSourceController()
         {
         this.ImageData = new byte[1, 1, 1];
         }

      public byte[, ,] ImageData
         {
         get;
         set;
         }

      public IRawPluginModel RawPluginModel
         {
         get;
         set;
         }

      public string DisplayName(IRawPluginModel rawPluginModel)
         {
         return "DisplayName";
         }

      public byte[, ,] NextImageData(IRawPluginModel rawPluginModel)
         {
         return this.ImageData;
         }
      }
   }
