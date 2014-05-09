﻿namespace ImagingInterface.Plugins
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;

   public interface IImageSourceController
      {
      IRawPluginModel RawPluginModel
         {
         get;
         }

      bool IsDynamic(IRawPluginModel rawPluginModel);

      byte[, ,] NextImageData(IRawPluginModel rawPluginModel);
      }
   }
