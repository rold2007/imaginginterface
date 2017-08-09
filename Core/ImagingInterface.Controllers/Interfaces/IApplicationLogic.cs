// <copyright file="IApplicationLogic.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.Interfaces
{
   using System.Collections.Generic;
   using ImagingInterface.Controllers.Views;
   using ImagingInterface.Plugins;

   public interface IApplicationLogic
   {
      void ManageNewImageSources(IEnumerable<IImageSource> imageSources);

      void RemoveImage(IImageView imageView);

      void ManageNewPlugin(string pluginName);

      void RemovePlugin(IPluginView pluginView);
   }
}
