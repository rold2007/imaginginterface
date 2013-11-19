namespace ImageProcessing.Controllers.Tests.Views
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImageProcessing.Views;

   public class RotateView : IRotateView
      {
      public event EventHandler Rotate;

      public void TriggerRotate()
         {
         if (this.Rotate != null)
            {
            this.Rotate(this, EventArgs.Empty);
            }
         }
      }
   }
