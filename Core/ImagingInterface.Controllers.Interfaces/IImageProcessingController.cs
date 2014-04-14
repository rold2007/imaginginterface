namespace ImagingInterface.Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Plugins;

   public interface IImageProcessingController
      {
      byte[, ,] ProcessImageData(byte[, ,] imageData, IRawPluginModel rawPluginModel);
      }
   }
