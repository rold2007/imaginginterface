// <copyright file="IPluginViewFactory.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Plugins
{
   public interface IPluginViewFactory
   {
      IPluginView CreateNew(string pluginName);
   }
}
