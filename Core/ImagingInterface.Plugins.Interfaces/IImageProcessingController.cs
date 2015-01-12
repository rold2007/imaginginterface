namespace ImagingInterface.Plugins
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Plugins;

   public interface IImageProcessingController : IPluginController
      {
      byte[, ,] ProcessImageData(byte[, ,] imageData, byte[] overlayData, IRawPluginModel rawPluginModel);
      }
   }
