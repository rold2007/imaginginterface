// <copyright file="PluginController1.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.Tests.Mocks
{
   using System;
   using System.ComponentModel;
   using System.Drawing;
   using ImagingInterface.Plugins;

   public class PluginController1 : ImageProcessingServiceBase
   {
      private PluginModel1 pluginModel = new PluginModel1();

      public PluginController1()
      {
         this.pluginModel.DisplayName = "Plugin1";
      }

      public event CancelEventHandler Closing;

      public event EventHandler Closed;

      public bool Active
      {
         get
         {
            return true;
         }
      }

      public bool IsClosed
      {
         get;
         private set;
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

            this.IsClosed = true;
         }
      }
   }
}
