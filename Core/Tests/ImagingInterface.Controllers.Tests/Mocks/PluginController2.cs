// <copyright file="PluginController2.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.Tests.Mocks
{
   using System;
   using System.ComponentModel;
   using System.Diagnostics.CodeAnalysis;

   public class PluginController2
   {
      private PluginModel2 pluginModel = new PluginModel2();

      public PluginController2()
      {
         this.pluginModel.DisplayName = "Plugin2";
      }

      public event CancelEventHandler Closing;

      public event EventHandler Closed;

      [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Will be fixed when done refactoring.")]
      public bool Active
      {
         get
         {
            return true;
         }
      }

      [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Will be fixed when done refactoring.")]
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
