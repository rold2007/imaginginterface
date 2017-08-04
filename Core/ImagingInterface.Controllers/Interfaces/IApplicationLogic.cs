namespace ImagingInterface.Controllers.Interfaces
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Controllers;
   using ImagingInterface.Plugins;

   public interface IApplicationLogic
   {
      void ManageNewImageSources(IEnumerable<IImageSource> imageSources);
   }
}
