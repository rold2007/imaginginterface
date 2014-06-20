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
   using Emgu.CV;
   using Emgu.CV.ML;
   using Emgu.CV.ML.MlEnum;
   using Emgu.CV.ML.Structure;
   using Emgu.CV.Structure;
   using ImageProcessing.Models;
   using ImageProcessing.Views;
   using ImagingInterface.Plugins;

   public class ObjectDetectionController : IObjectDetectionController
      {
      private static readonly int WindowHalfHeight = 10;
      private static readonly int WindowHalfWidth = 10;
      private static readonly int NumberOfFeatures = Convert.ToInt32(Math.Pow(WindowHalfHeight + WindowHalfWidth + 1, 2));
      private IObjectDetectionView objectDetectionView;
      private IObjectDetectionModel objectDetectionModel;
      private IImageManagerController imageManagerController;
      private ITaggerController taggerController;
      private Dictionary<string, List<float[]>> trainingData;

      public ObjectDetectionController(IObjectDetectionView objectDetectionView, IObjectDetectionModel objectDetectionModel, IImageManagerController imageManagerController)
         {
         this.objectDetectionView = objectDetectionView;
         this.objectDetectionModel = objectDetectionModel;
         this.imageManagerController = imageManagerController;

         this.trainingData = new Dictionary<string, List<float[]>>();
         this.objectDetectionModel.Models = new Dictionary<string, Boost>();
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

            this.ClearModels();

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
         IObjectDetectionModel objectDetectionModel = rawPluginModel as IObjectDetectionModel;
         byte[, ,] outputImageData = imageData.Clone() as byte[, ,];
         int imageWidth = outputImageData.GetLength(1);
         int imageHeight = outputImageData.GetLength(0);

         if (objectDetectionModel.Models.Count != 0)
            {
            MCvSlice mcvSlice = MCvSlice.WholeSeq;

            for (int y = 0; y < imageHeight; y++)
               {
               for (int x = 0; x < imageWidth; x++)
                  {
                  float[] featuresData = this.ComputeFeatures(imageData, new Point(x, y));

                  using (Matrix<float> features = new Matrix<float>(featuresData))
                     {
                     float prediction = objectDetectionModel.Models["a"].Predict(features, null, null, mcvSlice, false);

                     if (prediction == 1.0f)
                        {
                        outputImageData[y, x, 0] = 255;
                        outputImageData[y, x, 1] = 0;
                        outputImageData[y, x, 2] = 0;
                        }
                     }
                  }
               }
            }

         return outputImageData;
         }

      private void TaggerController_TagPointChanged(object sender, EventArguments.TagPointChangedEventArgs e)
         {
         if (e.Added)
            {
            List<float[]> currentTrainingData;

            if (this.trainingData.ContainsKey(e.Label))
               {
               currentTrainingData = this.trainingData[e.Label];
               }
            else
               {
               currentTrainingData = new List<float[]>();

               this.trainingData.Add(e.Label, currentTrainingData);
               }

            float[] features = this.ComputeFeatures(e.ImageController.LastDisplayedImage, e.TagPoint);

            currentTrainingData.Add(features);
            }
         else
            {
            Debug.Fail("Removing points not supported for now.");
            }
         }

      private float[] ComputeFeatures(byte[, ,] imageData, Point pixelPosition)
         {
         float[] features = new float[NumberOfFeatures];
         int featureIndex = 0;
         int imageWidth = imageData.GetLength(1);
         int imageHeight = imageData.GetLength(0);
         int channels = imageData.GetLength(2);

         for (int y = pixelPosition.Y - WindowHalfHeight; y <= pixelPosition.Y + WindowHalfHeight; y++)
            {
            for (int x = pixelPosition.X - WindowHalfWidth; x <= pixelPosition.X + WindowHalfWidth; x++)
               {
               if ((x >= 0 && x < imageWidth) && (y >= 0 && y < imageHeight))
                  {
                  if (channels == 1)
                     {
                     features[featureIndex] = imageData[y, x, 0];
                     }
                  else
                     {
                     double[] rgb = new double[] { imageData[y, x, 0], imageData[y, x, 1], imageData[y, x, 2] };
                     double[] hsv = ImagingInterface.Plugins.Utilities.Color.RGBToHSV(rgb);

                     features[featureIndex] = Convert.ToSingle(hsv[2]);
                     }
                  }
               else
                  {
                  features[featureIndex] = -1.0f;
                  }

               featureIndex++;
               }
            }

         return features;
         }

      private void ObjectDetectionView_Train(object sender, EventArgs e)
         {
         int trainSamples = 0;

         foreach (List<float[]> currentTrainingData in this.trainingData.Values)
            {
            trainSamples += currentTrainingData.Count;
            }

         this.ClearModels();

         MCvBoostParams mcvBoostParams = MCvBoostParams.GetDefaultParameter();

         mcvBoostParams.boostType = BOOST_TYPE.GENTLE;
         mcvBoostParams.weakCount = 50; // Number of trees/stumps

         // Using 0.95 slowed down the training process when I tried it, but it should have sped it...
         mcvBoostParams.weightTrimRate = 0.0; // Default 0.95
         mcvBoostParams.cvFolds = 0;
         mcvBoostParams.maxDepth = 1; // Depth of trees
         mcvBoostParams.splitCriteria = (int)BOOST_SPLIT_CREITERIA.DEFAULT; // Error computation function
         mcvBoostParams.maxCategories = 2;

         // Make sure we work even with a low number of samples
         mcvBoostParams.minSampleCount = 1;

         List<string> trueResponses = new List<string>();

         using (Matrix<float> trainMatrix = new Matrix<float>(ObjectDetectionController.NumberOfFeatures, trainSamples), trainResponses = new Matrix<float>(1, trainSamples))
            {
            // Fill the train matrix
            foreach (string label in this.trainingData.Keys)
               {
               foreach (float[] currentTrainingData in this.trainingData[label])
                  {
                  using (Matrix<float> source = new Matrix<float>(currentTrainingData), destination = trainMatrix.GetCol(trueResponses.Count))
                     {
                     source.CopyTo(destination);
                     }

                  trueResponses.Add(label);
                  }
               }

            // Train the models
            foreach (string label in this.trainingData.Keys)
               {
               int responseIndex = 0;

               // Prepare the responses matrix
               foreach (string responseLabel in trueResponses)
                  {
                  trainResponses[0, responseIndex] = (responseLabel == label) ? 1.0f : 0.0f;

                  responseIndex++;
                  }

               Boost boost = new Boost();

               boost.Train(trainMatrix, DATA_LAYOUT_TYPE.COL_SAMPLE, trainResponses, null, null, null, null, mcvBoostParams, false);

               this.objectDetectionModel.Models.Add(label, boost);
               }
            }
         }

      private void ClearModels()
         {
         foreach (string label in this.trainingData.Keys)
            {
            Boost boost = null;

            if (this.objectDetectionModel.Models.TryGetValue(label, out boost))
               {
               boost.Dispose();
               }
            }

         this.objectDetectionModel.Models.Clear();
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
