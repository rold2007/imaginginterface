namespace ImageProcessing.Controllers.Tests.Views
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImageProcessing.Views;
   using ImageProcessing.Views.EventArguments;

   public class TaggerView : ITaggerView
      {
      public bool CloseCalled
         {
         get;
         private set;
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
