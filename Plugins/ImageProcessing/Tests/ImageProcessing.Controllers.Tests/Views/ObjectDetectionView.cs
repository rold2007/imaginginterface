namespace ImageProcessing.Controllers.Tests.Views
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImageProcessing.Views;
   using ImageProcessing.Views.EventArguments;

   public class ObjectDetectionView : IObjectDetectionView
      {
      public ObjectDetectionView()
         {
         }

      public event EventHandler Train;

      public event EventHandler Test;

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

      public void TriggerTrain()
         {
         if (this.Train != null)
            {
            this.Train(this, EventArgs.Empty);
            }
         }

      public void TriggerTest()
         {
         if (this.Test != null)
            {
            this.Test(this, EventArgs.Empty);
            }
         }

      public bool CanTrain()
         {
         return this.Test != null;
         }

      public bool CanTest()
         {
         return this.Test != null;
         }
      }
   }
