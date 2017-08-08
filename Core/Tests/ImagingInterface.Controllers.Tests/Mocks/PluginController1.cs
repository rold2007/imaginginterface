// <copyright file="PluginController1.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.Tests.Mocks
{
   using System;
   using System.ComponentModel;
   using ImagingInterface.Plugins;

   public class PluginController1 : IPluginController, IImageProcessingService
      {
      public PluginController1(PluginModel1 pluginModel)
         {
         ////this.RawPluginView = pluginView;
         this.RawPluginModel = pluginModel;

         this.RawPluginModel.DisplayName = "Plugin1";
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

      public byte[,,] ProcessImageData(byte[,,] imageData, byte[] overlayData, IRawPluginModel rawPluginModel)
         {
         return imageData.Clone() as byte[,,];
         }
      }
   }
