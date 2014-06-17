namespace ImageProcessing.Controllers.Tests.Views
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImageProcessing.Models;
   using ImageProcessing.Views;
   using ImageProcessing.Views.EventArguments;

   public class TaggerView : ITaggerView
      {
      private ITaggerModel taggerModel;

      public event EventHandler LabelAdded;

      public bool CloseCalled
         {
         get;
         private set;
         }

      public void SetTaggerModel(ITaggerModel taggerModel)
         {
         this.taggerModel = taggerModel;
         }

      public void UpdateLabelList()
         {
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
