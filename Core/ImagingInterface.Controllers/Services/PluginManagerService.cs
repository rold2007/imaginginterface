// <copyright file="PluginManagerService.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.Services
{
   using System;
   using System.Diagnostics.CodeAnalysis;
   using System.Drawing;
   using ImagingInterface.Controllers.EventArguments;
   using ImagingInterface.Plugins;
   using Shouldly;

   public class PluginManagerService
   {
      private int activePluginIndex;

      public PluginManagerService()
      {
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
                  throw new ArgumentOutOfRangeException(nameof(value));
               }
            }
            else if (value < 0 || value >= this.PluginCount)
            {
               throw new ArgumentOutOfRangeException(nameof(value));
            }

            this.activePluginIndex = value;
         }
      }

      public int PluginCount
      {
         get;
         private set;
      }

      public int AddPlugin()
      {
         this.PluginCount++;

         this.ActivePluginIndex = this.PluginCount - 1;

         return this.ActivePluginIndex;
      }

      public void RemoveActivePlugin()
      {
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

      [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Too much work for now.")]
      public void SelectPixel(IImageSource imageSource, Point mouseClickPixel)
      {
         if (this.PluginCount > 0)
         {
            this.ActiveImagePixelSelected?.Invoke(this, new PixelSelectionEventArgs(imageSource, mouseClickPixel, true));
         }
      }
   }
}
