// <copyright file="ImageControllerWrapper.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Tests.Common
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Threading;
   using System.Windows.Forms;
   using ImagingInterface.Controllers;
   using ImagingInterface.Plugins.EventArguments;

   public class ImageControllerWrapper : IDisposable
      {
      private List<ImageController> imageControllers = new List<ImageController>();
      private CountdownEvent displayUpdateCountdownEvent;
      private CountdownEvent closedCountdownEvent;

      public ImageControllerWrapper(ImageController imageController)
         {
         this.imageControllers.Add(imageController);

         this.displayUpdateCountdownEvent = new CountdownEvent(1);
         this.closedCountdownEvent = new CountdownEvent(1);

         this.RegisterAllEvents();
         }

      public ImageControllerWrapper(IEnumerable<ImageController> imageControllers)
         {
         this.imageControllers.AddRange(imageControllers);

         this.displayUpdateCountdownEvent = new CountdownEvent(imageControllers.Count());
         this.closedCountdownEvent = new CountdownEvent(imageControllers.Count());

         this.RegisterAllEvents();
         }

      ~ImageControllerWrapper()
         {
         this.Dispose(false);
         }

      public void Dispose()
         {
         this.Dispose(true);
         GC.SuppressFinalize(this);
         }

      public void WaitForDisplayUpdate()
         {
         ImageControllerWrapper.WaitForEvent(this.displayUpdateCountdownEvent);
         }

      public void WaitForClosed()
         {
         ImageControllerWrapper.WaitForEvent(this.closedCountdownEvent);
         }

      protected virtual void Dispose(bool disposing)
         {
         if (disposing)
            {
            if (this.displayUpdateCountdownEvent != null)
               {
               this.displayUpdateCountdownEvent.Dispose();
               this.displayUpdateCountdownEvent = null;
               }

            if (this.closedCountdownEvent != null)
               {
               this.closedCountdownEvent.Dispose();
               this.closedCountdownEvent = null;
               }
            }

         this.UnregisterAllEvents();
         }

      private static void WaitForEvent(CountdownEvent countdownEvent)
         {
         Application.DoEvents();

         while (countdownEvent.Wait(10) == false)
            {
            Application.DoEvents();
            }
         }

      private void RegisterAllEvents()
         {
         foreach (ImageController imageController in this.imageControllers)
            {
            ////imageController.DisplayUpdated += this.ImageController_DisplayUpdated;
            ////imageController.Closed += this.ImageController_Closed;
            }
         }

      private void UnregisterAllEvents()
         {
         foreach (ImageController imageController in this.imageControllers)
            {
            this.UnregisterEvents(imageController);
            }
         }

      private void UnregisterEvents(ImageController imageController)
         {
         ////imageController.DisplayUpdated -= this.ImageController_DisplayUpdated;
         ////imageController.Closed -= this.ImageController_Closed;
         }

      private void ImageController_Closed(object sender, EventArgs e)
         {
         ImageController imageController = sender as ImageController;

         this.closedCountdownEvent.Signal();

         ////imageController.Closed -= this.ImageController_Closed;
         }

      ////private void ImageController_DisplayUpdated(object sender, DisplayUpdateEventArgs e)
      ////   {
      ////   ImageController imageController = sender as ImageController;

      ////   this.displayUpdateCountdownEvent.Signal();

      ////   ////imageController.DisplayUpdated -= this.ImageController_DisplayUpdated;
      ////   }
      }
   }
