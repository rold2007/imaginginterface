namespace ImagingInterface.Controllers.Tests.Mocks
{
   using System.Collections.Generic;
   using ImagingInterface.Controllers.Interfaces;
   using ImagingInterface.Plugins;

   public class ApplicationLogic : IApplicationLogic
   {
      public void ManageNewImageSources(IEnumerable<IImageSource> imageSources)
      {
      }
   }
}
