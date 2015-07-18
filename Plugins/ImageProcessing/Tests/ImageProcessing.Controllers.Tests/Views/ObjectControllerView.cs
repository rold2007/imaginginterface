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

   public class ObjectControllerView : IObjectDetectionView
      {
      public event EventHandler Train;

      public event EventHandler Test;

      public void Hide()
         {
         }

      public void Close()
         {
         }
      }
   }
