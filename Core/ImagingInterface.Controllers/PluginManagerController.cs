// <copyright file="PluginManagerController.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers
{
   using System;
   using ImagingInterface.Controllers.EventArguments;
   using ImagingInterface.Controllers.Services;

   public class PluginManagerController
   {
      private PluginManagerService pluginManagerService;

      public PluginManagerController(PluginManagerService pluginManagerService)
      {
         this.pluginManagerService = pluginManagerService;
      }

      public event EventHandler<PixelSelectionEventArgs> ActiveImagePixelSelected;

      public event EventHandler<ImageSourceChangedEventArgs> ActiveImageSourceChanged;

      public int ActivePluginIndex
      {
         get
         {
            return this.pluginManagerService.ActivePluginIndex;
         }
      }

      public void Initialize()
      {
         // PluginManagerService is a singleton, so it is better not to register this event in the constructor
         // otherwise it is registered twice because of SimpleInjector's Verify().
         this.pluginManagerService.ActiveImagePixelSelected += this.PluginManagerService_ActiveImagePixelSelected;
         this.pluginManagerService.ActiveImageSourceChanged += this.PluginManagerService_ActiveImageSourceChanged;
      }

      public void AddPlugin()
      {
         this.pluginManagerService.AddPlugin();
      }

      public void RemoveActivePlugin()
      {
      }

      private void PluginManagerService_ActiveImagePixelSelected(object sender, PixelSelectionEventArgs e)
      {
         this.ActiveImagePixelSelected?.Invoke(sender, e);
      }

      private void PluginManagerService_ActiveImageSourceChanged(object sender, ImageSourceChangedEventArgs e)
      {
         this.ActiveImageSourceChanged.Invoke(sender, e);
      }
   }
}
