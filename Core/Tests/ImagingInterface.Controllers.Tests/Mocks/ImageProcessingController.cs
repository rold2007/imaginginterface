﻿// <copyright file="ImageProcessingController.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.Tests.Mocks
{
   using System;
   using System.ComponentModel;
   using ImagingInterface.Plugins;

   public class ImageProcessingController : IImageProcessingService
      {
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
            return false;
            }
         }

      public void Initialize()
         {
         }

      public void Close()
         {
         CancelEventArgs cancelEventArgs = new CancelEventArgs();

         if (this.Closing != null)
            {
            this.Closing(this, cancelEventArgs);
            }

         if (!cancelEventArgs.Cancel)
            {
            if (this.Closed != null)
               {
               this.Closed(this, EventArgs.Empty);
               }
            }
         }

      public byte[,,] ProcessImageData(byte[,,] imageData, byte[] overlayData, IRawPluginModel rawPluginModel)
         {
         return imageData.Clone() as byte[,,];
         }
      }
   }
