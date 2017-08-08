// <copyright file="PluginController2.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.Tests.Mocks
{
   using System;
   using System.ComponentModel;
   using ImagingInterface.Plugins;

   public class PluginController2 : IPluginController
      {
      public PluginController2(PluginModel2 pluginModel)
         {
         ////this.RawPluginView = pluginView;
         this.RawPluginModel = pluginModel;

         this.RawPluginModel.DisplayName = "Plugin2";
         }

      public event CancelEventHandler Closing;

      public event EventHandler Closed;

      public IRawPluginView RawPluginView
         {
         get;
         private set;
         }

      public IRawPluginModel RawPluginModel
         {
         get;
         private set;
         }

      public bool Active
         {
         get
            {
            return true;
            }
         }

      public void Initialize()
         {
         }

      public void Close()
         {
         CancelEventArgs cancelEventArgs = new CancelEventArgs();

         this.Closing?.Invoke(this, cancelEventArgs);

         if (!cancelEventArgs.Cancel)
            {
            this.Closed?.Invoke(this, EventArgs.Empty);
         }
         }
      }
   }
