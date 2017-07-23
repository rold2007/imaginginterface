﻿namespace ImagingInterface.Controllers
{
   using System.Collections.Generic;
   using ImagingInterface.Plugins;

   public class PluginViewFactory
   {
      private IEnumerable<IPluginView> pluginViews;

      public PluginViewFactory(IEnumerable<IPluginView> pluginViews)
      {
         this.pluginViews = pluginViews;
      }

      public IPluginView CreateNew(string pluginName)
      {
         foreach (IPluginView pluginView in this.pluginViews)
         {
            if (pluginView.DisplayName == pluginName)
            {
               return pluginView;
            }
         }

         return null;
      }
   }
}
