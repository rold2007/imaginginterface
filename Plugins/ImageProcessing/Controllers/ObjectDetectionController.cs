namespace ImageProcessing.Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Diagnostics;
   using System.Drawing;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImageProcessing.Controllers.EventArguments;
   using ImageProcessing.Models;
   using ImageProcessing.Views;
   using ImagingInterface.Plugins;
   using ImageProcessing.ObjectDetection;

   public class ObjectDetectionController : IObjectDetectionController
      {
      private IObjectDetectionView objectDetectionView;
      private IObjectDetectionModel objectDetectionModel;
      private IImageManagerController imageManagerController;
      private ITaggerController taggerController;
      private IObjectDetector objectDetector;

      public ObjectDetectionController(IObjectDetectionView objectDetectionView, IObjectDetectionModel objectDetectionModel, IObjectDetector objectDetection, IImageManagerController imageManagerController)
         {
         this.objectDetectionView = objectDetectionView;
         this.objectDetectionModel = objectDetectionModel;
         this.objectDetector = objectDetection;
         this.imageManagerController = imageManagerController;
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
         this.objectDetectionView.Train += this.ObjectDetectionView_Train;

         this.objectDetectionView.Test += this.ObjectDetectionView_Test;
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
            this.objectDetectionView.Train -= this.ObjectDetectionView_Train;
            this.objectDetectionView.Test -= this.ObjectDetectionView_Test;

            this.objectDetector.Dispose();
            this.objectDetector = null;

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

      public byte[, ,] ProcessImageData(byte[, ,] imageData, byte[] overlayData, IRawPluginModel rawPluginModel)
         {
         Dictionary<string, List<Point>> predictions = this.objectDetector.Test(imageData);

         int imageWidth = imageData.GetLength(1);

         foreach (string prediction in predictions.Keys)
            {
            List<Point> predictedPoints = predictions[prediction];

            Color tagColor = this.taggerController.TagColor(prediction);

            foreach (Point predictedPoint in predictedPoints)
               {
               int pixelOffset = (predictedPoint.Y * imageWidth * 4) + predictedPoint.X * 4;

               // Red
               overlayData[pixelOffset] = tagColor.R;

               // Green
               overlayData[pixelOffset + 1] = tagColor.G;

               // Blue
               overlayData[pixelOffset + 2] = tagColor.B;

               // Alpha
               overlayData[pixelOffset + 3] = 255;
               }
            }

         return imageData;
         }

      private void TaggerController_TagPointChanged(object sender, TagPointChangedEventArgs e)
         {
         if (e.Added)
            {
            this.objectDetector.Add(e.Label, e.TagPoint);
            }
         else
            {
            this.objectDetector.Remove(e.Label, e.TagPoint);
            }
         }

      private void ObjectDetectionView_Train(object sender, EventArgs e)
         {
         IImageController imageController = this.imageManagerController.GetActiveImage();

         if (imageController != null)
            {
            this.objectDetector.Train(imageController.LastDisplayedImage);
            }
         }

      private void ObjectDetectionView_Test(object sender, EventArgs e)
         {
         IImageController imageController = this.imageManagerController.GetActiveImage();

         if (imageController != null)
            {
            imageController.AddImageProcessingController(this, this.objectDetectionModel.Clone() as IRawPluginModel);
            }
         }
      }
   }
