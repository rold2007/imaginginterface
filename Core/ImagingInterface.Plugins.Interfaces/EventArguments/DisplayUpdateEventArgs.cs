// <copyright file="DisplayUpdateEventArgs.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Plugins.EventArguments
{
   using System;
   using ImagingInterface.Plugins;

   public class DisplayUpdateEventArgs : EventArgs
      {
      public DisplayUpdateEventArgs(IRawPluginModel rawPluginModel)
         {
         this.RawPluginModel = rawPluginModel;
         }

      public IRawPluginModel RawPluginModel
         {
         get;
         private set;
         }
      }
   }
