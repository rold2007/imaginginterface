namespace ImagingInterface.Plugins
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;

   public interface IMemorySource : IImageSource
      {
      byte[, ,] ImageData
         {
         get;
         }
      }
   }
