// <copyright file="PluginOperationController.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers
{
   using System;
   using System.Collections.Generic;
   using ImagingInterface.Controllers.Interfaces;
   using ImagingInterface.Plugins;

   public class PluginOperationController
   {
      private SortedDictionary<string, Type> plugins;
      private IApplicationLogic applicationLogic;

      public PluginOperationController(IEnumerable<IPluginView> plugins, IApplicationLogic applicationLogic)
      {
         this.plugins = new SortedDictionary<string, Type>();
         this.applicationLogic = applicationLogic;

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

      public void CreatePlugin(string pluginName)
      {
         this.applicationLogic.ManageNewPlugin(pluginName);
      }

      public void ClosePlugin(IPluginView pluginView)
      {
         if (pluginView != null)
         {
            this.applicationLogic.RemovePlugin(pluginView);
         }
      }
   }
}
