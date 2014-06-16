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

   public class ObjectDetectionController : IObjectDetectionController
      {
      private IObjectDetectionView objectDetectionView;
      private IObjectDetectionModel objectDetectionModel;
      private ITaggerController taggerController;

      public ObjectDetectionController(IObjectDetectionView objectDetectionView, IObjectDetectionModel objectDetectionModel)
         {
         this.objectDetectionView = objectDetectionView;
         this.objectDetectionModel = objectDetectionModel;
         }

      public event CancelEventHandler Closing;

      public event EventHandler Closed;

      public IRawPluginView RawPluginView
         {
         get
            {
            return this.objectDetectionView;
            }
         }

      public IRawPluginModel RawPluginModel
         {
         get
            {
            return this.objectDetectionModel;
            }
         }

      public bool Active
         {
         get
            {
            return false;
            }
         }

      public void Initialize()
         {
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
            this.objectDetectionView.Hide();

            this.objectDetectionView.Close();

            if (this.Closed != null)
               {
               this.Closed(this, EventArgs.Empty);
               }
            }
         }

      public void SetTagger(ITaggerController taggerController)
         {
         this.taggerController = taggerController;

         this.taggerController.TagPointChanged += this.TaggerController_TagPointChanged;
         }

      public byte[, ,] ProcessImageData(byte[, ,] imageData, IRawPluginModel rawPluginModel)
         {
         byte[, ,] outputImageData = imageData.Clone() as byte[, ,];

         return outputImageData;
         }

      private void TaggerController_TagPointChanged(object sender, EventArguments.TagPointChangedEventArgs e)
         {
         }
      }
   }
