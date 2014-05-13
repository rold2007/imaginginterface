namespace ImageProcessing.Controllers.Tests.Views
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImageProcessing.Views;
   using ImageProcessing.Views.EventArguments;

   public class InvertView : IInvertView
      {
      public event EventHandler<InvertEventArgs> Invert;

      public bool CloseCalled
         {
         get;
         private set;
         }

      public void TriggerInvert(bool invert)
         {
         if (this.Invert != null)
            {
            this.Invert(this, new InvertEventArgs(invert));
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
