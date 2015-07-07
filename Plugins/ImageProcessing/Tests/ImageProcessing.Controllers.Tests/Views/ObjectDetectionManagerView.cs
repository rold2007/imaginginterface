namespace ImageProcessing.Controllers.Tests.Views
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImageProcessing.Views;
   using ImageProcessing.Views.EventArguments;
   using ImagingInterface.Plugins;

   public class ObjectDetectionManagerView : IObjectDetectionManagerView
      {
      public ObjectDetectionManagerView()
         {
         }

      public bool CloseCalled
         {
         get;
         private set;
         }

      public void AddView(IRawPluginView rawPluginView)
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
