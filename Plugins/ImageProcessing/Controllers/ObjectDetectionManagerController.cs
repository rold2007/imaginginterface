namespace ImageProcessing.Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImageProcessing.Models;
   using ImageProcessing.Views;
   using ImagingInterface.Plugins;

   public class ObjectDetectionManagerController : IObjectDetectionManagerController
      {
      private static readonly string ObjectDetectionDisplayName = "Object detection"; // ncrunch: no coverage
      private IObjectDetectionManagerView objectDetectionManagerView;
      private IObjectDetectionManagerModel objectDetectionManagerModel;
      private ITaggerController taggerController;
      private IObjectDetectionController objectDetectionController;

      public ObjectDetectionManagerController(IObjectDetectionManagerView objectDetectionManagerView, IObjectDetectionManagerModel objectDetectionManagerModel, ITaggerController taggerController, IObjectDetectionController objectDetectionController)
         {
         this.objectDetectionManagerView = objectDetectionManagerView;
         this.objectDetectionManagerModel = objectDetectionManagerModel;
         this.taggerController = taggerController;
         this.objectDetectionController = objectDetectionController;

         this.objectDetectionManagerModel.DisplayName = ObjectDetectionDisplayName;
         }

      public event CancelEventHandler Closing;

      public event EventHandler Closed;

      public IRawPluginView RawPluginView
         {
         get
            {
            return this.objectDetectionManagerView;
            }
         }

      public IRawPluginModel RawPluginModel
         {
         get
            {
            return this.objectDetectionManagerModel;
            }
         }

      public bool Active
         {
         get
            {
            return true;
            }
         }

      public void Initialize()
         {
         this.objectDetectionController.Initialize();

         this.objectDetectionManagerView.AddView(this.taggerController.RawPluginView);
         this.objectDetectionManagerView.AddView(this.objectDetectionController.RawPluginView);

         this.objectDetectionController.SetTagger(this.taggerController);

         // Must initialize the object detection controller first so that all points
         // loaded by the tagger are sent to the object detection controller
         this.taggerController.Initialize();
         }

      public void Close()
         {
         CancelEventArgs cancelEventArgs = new CancelEventArgs();

         if (this.Closing != null)
            {
            this.Closing(this, cancelEventArgs);
            }

         if (!cancelEventArgs.Cancel)
            {
            this.objectDetectionManagerView.Hide();

            this.objectDetectionManagerView.Close();

            this.taggerController.Close();
            this.objectDetectionController.Close();

            if (this.Closed != null)
               {
               this.Closed(this, EventArgs.Empty);
               }
            }
         }
      }
   }
