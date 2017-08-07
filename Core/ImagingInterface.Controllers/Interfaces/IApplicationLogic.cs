// <copyright file="IApplicationLogic.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.Interfaces
{
   using System.Collections.Generic;
   using ImagingInterface.Plugins;

   public interface IApplicationLogic
   {
      void ManageNewImageSources(IEnumerable<IImageSource> imageSources);
   }
}
