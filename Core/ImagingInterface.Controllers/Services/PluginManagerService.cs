// <copyright file="PluginManagerService.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.Services
{
   using System;
   using System.Collections.Generic;
   using System.Drawing;
   using ImagingInterface.Controllers.EventArguments;
   using ImagingInterface.Plugins;
   using Shouldly;

   public class PluginManagerService
   {
      private int activePluginIndex;
      ////private List<IImageProcessingService> imageProcessingPlugins;

      public PluginManagerService()
      {
         ////this.imageProcessingPlugins = new List<IImageProcessingService>();

         this.ActivePluginIndex = -1;
         this.PluginCount = 0;
      }

      public event EventHandler<PixelSelectionEventArgs> ActiveImagePixelSelected;

      public int ActivePluginIndex
      {
         get
         {
            return this.activePluginIndex;
         }

         set
         {
            if (this.PluginCount == 0)
            {
               if (value != -1)
               {
                  throw new ArgumentOutOfRangeException();
               }
            }
            else if (value < 0 || value >= this.PluginCount)
            {
               throw new ArgumentOutOfRangeException();
            }

            this.activePluginIndex = value;
         }
      }

      public int PluginCount
      {
         get;
         ////{
         ////   //return this.imageProcessingPlugins.Count;
         ////}

         private set;
      }

      public int AddPlugin(/*IImageProcessingService imageProcessingPlugin*/)
      {
         ////this.imageProcessingPlugins.Add(imageProcessingPlugin);

         this.PluginCount++;

         this.ActivePluginIndex = this.PluginCount - 1;

         return this.ActivePluginIndex;
      }

      public void RemoveActivePlugin()
      {
         ////this.imageProcessingPlugins.RemoveAt(this.ActivePluginIndex);
         this.PluginCount--;

         if (this.ActivePluginIndex > 0)
         {
            this.ActivePluginIndex--;
         }
         else if (this.PluginCount == 0)
         {
            this.ActivePluginIndex = -1;
         }

         this.PluginCount.ShouldBeLessThanOrEqualTo(0, "Invalid image count.");
      }

      public void SelectPixel(IImageSource imageSource, Point mouseClickPixel)
      {
         this.ActiveImagePixelSelected?.Invoke(this, new PixelSelectionEventArgs(mouseClickPixel, imageSource, true));
      }

      ////public IImageProcessingService GetPluginFromIndex(int activePluginIndex)
      ////{
      ////   if (activePluginIndex < 0 || activePluginIndex >= this.PluginCount)
      ////   {
      ////      throw new ArgumentOutOfRangeException("activePluginIndex");
      ////   }

      ////   return this.imageProcessingPlugins[activePluginIndex];
      ////}
   }
}
