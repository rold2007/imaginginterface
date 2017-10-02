// <copyright file="ImageProcessingController.cs" company="David Rolland">
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

         this.Closing?.Invoke(this, cancelEventArgs);

         if (!cancelEventArgs.Cancel)
            {
            this.Closed?.Invoke(this, EventArgs.Empty);
         }
         }

      public void ProcessImageData(byte[,,] imageData, byte[] overlayData)
         {
         }
   }
}
