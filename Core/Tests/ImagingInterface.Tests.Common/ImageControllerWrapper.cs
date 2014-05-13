namespace ImagingInterface.Tests.Common
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading;
   using System.Threading.Tasks;
   using System.Windows.Forms;
   using ImagingInterface.Controllers;
   using ImagingInterface.Plugins;
   using ImagingInterface.Plugins.EventArguments;

   public class ImageControllerWrapper : IDisposable
      {
      private List<IImageController> imageControllers = new List<IImageController>();
      private CountdownEvent displayUpdateCountdownEvent;
      private CountdownEvent closedCountdownEvent;

      public ImageControllerWrapper(IImageController imageController)
         {
         this.imageControllers.Add(imageController);

         this.displayUpdateCountdownEvent = new CountdownEvent(1);
         this.closedCountdownEvent = new CountdownEvent(1);

         this.RegisterAllEvents();
         }

      public ImageControllerWrapper(IEnumerable<IImageController> imageControllers)
         {
         this.imageControllers.AddRange(imageControllers);

         this.displayUpdateCountdownEvent = new CountdownEvent(imageControllers.Count());
         this.closedCountdownEvent = new CountdownEvent(imageControllers.Count());

         this.RegisterAllEvents();
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
         foreach (IImageController imageController in this.imageControllers)
            {
            imageController.DisplayUpdated += this.ImageController_DisplayUpdated;
            imageController.Closed += this.ImageController_Closed;
            }
         }

      private void UnregisterAllEvents()
         {
         foreach (IImageController imageController in this.imageControllers)
            {
            this.UnregisterEvents(imageController);
            }
         }

      private void UnregisterEvents(IImageController imageController)
         {
         imageController.DisplayUpdated -= this.ImageController_DisplayUpdated;
         imageController.Closed -= this.ImageController_Closed;
         }

      private void ImageController_Closed(object sender, EventArgs e)
         {
         IImageController imageController = sender as IImageController;

         this.closedCountdownEvent.Signal();

         imageController.Closed -= this.ImageController_Closed;
         }

      private void ImageController_DisplayUpdated(object sender, DisplayUpdateEventArgs e)
         {
         IImageController imageController = sender as IImageController;

         this.displayUpdateCountdownEvent.Signal();

         imageController.DisplayUpdated -= this.ImageController_DisplayUpdated;
         }
      }
   }
