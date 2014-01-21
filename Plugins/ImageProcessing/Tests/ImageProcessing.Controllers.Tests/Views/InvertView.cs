namespace ImageProcessing.Controllers.Tests.Views
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImageProcessing.Views;

   public class InvertView : IInvertView
      {
      public event EventHandler Invert;

      public bool CloseCalled
         {
         get;
         private set;
         }

      public void TriggerInvert()
         {
         if (this.Invert != null)
            {
            this.Invert(this, EventArgs.Empty);
            }
         }

      public void Close()
         {
         this.CloseCalled = true;
         }
      }
   }
