// <copyright file="PluginController1.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.Tests.Mocks
{
   using System;
   using System.ComponentModel;
   using System.Diagnostics.CodeAnalysis;
   using ImagingInterface.Plugins;

   public class PluginController1 : IImageProcessingService
   {
      private PluginModel1 pluginModel = new PluginModel1();

      public PluginController1()
      {
         this.pluginModel.DisplayName = "Plugin1";
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

      public bool IsClosed
      {
         get;
         private set;
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

            this.IsClosed = true;
         }
      }

      [SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", Justification = "Too much work for now.")]
      public void ProcessImageData(byte[,,] imageData, byte[] overlayData)
      {
      }
   }
}
