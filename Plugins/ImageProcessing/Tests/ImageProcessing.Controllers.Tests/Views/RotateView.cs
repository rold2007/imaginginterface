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

      public void TriggerRotate()
         {
         if (this.Rotate != null)
            {
            this.Rotate(this, new RotateEventArgs(42.54));
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
