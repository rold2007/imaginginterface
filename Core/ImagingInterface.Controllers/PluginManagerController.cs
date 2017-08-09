// <copyright file="PluginManagerController.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using ImagingInterface.Plugins;

   public class PluginManagerController
      {
      private Dictionary<IRawPluginView, IPluginController> pluginControllers;

      public PluginManagerController()
         {
         this.pluginControllers = new Dictionary<IRawPluginView, IPluginController>();

         ////mainController.AddPluginManager(this);
         }

      public void AddPlugin(IPluginController pluginController)
         {
         pluginController.Closed += this.PluginController_Closed;

         ////this.pluginManagerView.AddPlugin(pluginController.RawPluginView, pluginController.RawPluginModel);
         ////this.pluginControllers.Add(pluginController.RawPluginView, pluginController);
         }

      public IList<IPluginController> GetAllPlugins()
         {
         return this.pluginControllers.Values.ToList();
         }

      private void RemovePlugin(IPluginController pluginController)
         {
         pluginController.Closed -= this.PluginController_Closed;

         ////this.pluginManagerView.RemovePlugin(pluginController.RawPluginView);
         ////this.pluginControllers.Remove(pluginController.RawPluginView);
         }

      private void PluginController_Closed(object sender, EventArgs e)
         {
         this.RemovePlugin(sender as IPluginController);
         }
      }
   }
