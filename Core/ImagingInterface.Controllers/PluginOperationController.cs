// <copyright file="PluginOperationController.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers
{
   using System;
   using System.Collections.Generic;
   using ImagingInterface.Controllers.Views;
   using ImagingInterface.Plugins;

   public class PluginOperationController
   {
      private SortedDictionary<string, Type> plugins;
      private PluginManagerController pluginManagerController;
      private PluginViewFactory pluginViewFactory;

      public PluginOperationController(IEnumerable<IPluginController> pluginsOld, IEnumerable<IPluginView> plugins, PluginManagerController pluginManagerController, PluginViewFactory pluginViewFactory)
      {
         this.plugins = new SortedDictionary<string, Type>();
         this.pluginManagerController = pluginManagerController;
         this.pluginViewFactory = pluginViewFactory;

         foreach (IPluginView plugin in plugins)
         {
            if (plugin.Active)
            {
               this.plugins.Add(plugin.DisplayName, plugin.GetType());
               ////pluginOperationView.AddPlugin(plugin.RawPluginModel.DisplayName);
            }
         }

         ////pluginOperationView.AddPlugin(PluginOperationController.closePluginName);

         ////pluginOperationView.PluginCreate += this.PluginCreate;
      }

      public IEnumerable<string> PluginNames
      {
         get
         {
            return this.plugins.Keys;
         }
      }

      public IPluginView CreatePlugin(string pluginName)
      {
         IPluginView pluginView = this.pluginViewFactory.CreateNew(pluginName);

         return pluginView;
      }

      public void ClosePlugin()
      {
         this.pluginManagerController.CloseActivePlugin();
      }
   }
}
