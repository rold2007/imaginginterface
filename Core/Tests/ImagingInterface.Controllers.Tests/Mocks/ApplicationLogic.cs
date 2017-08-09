// <copyright file="ApplicationLogic.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.Tests.Mocks
{
   using System;
   using System.Collections.Generic;
   using ImagingInterface.Controllers.Interfaces;
   using ImagingInterface.Controllers.Views;
   using ImagingInterface.Plugins;

   public class ApplicationLogic : IApplicationLogic
   {
      public void ManageNewImageSources(IEnumerable<IImageSource> imageSources)
      {
      }

      public void ManageNewPlugin(string pluginName)
      {
      }

      public void RemoveImage(IImageView imageView)
      {
      }

      public void RemovePlugin(IPluginView pluginView)
      {
      }
   }
}
