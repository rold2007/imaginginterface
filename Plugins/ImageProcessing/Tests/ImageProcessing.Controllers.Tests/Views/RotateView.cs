namespace ImageProcessing.Controllers.Tests.Views
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImageProcessing.Views;
   using ImageProcessing.Views.EventArguments;

   public class RotateView : IRotateView
      {
      public event EventHandler<RotateEventArgs> Rotate;

      public bool CloseCalled
         {
         get;
         private set;
         }

      public void TriggerRotate(double angle)
         {
         if (this.Rotate != null)
            {
            this.Rotate(this, new RotateEventArgs(angle));
            }
         }

      public void Hide()
         {
         }

      public void Close()
         {
         this.CloseCalled = true;
         }
      }
   }
