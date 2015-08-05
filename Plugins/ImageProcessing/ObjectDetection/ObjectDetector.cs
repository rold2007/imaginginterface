namespace ImageProcessing.ObjectDetection
   {
   using System;
   using System.Collections.Generic;
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
   using ImageProcessing.ObjectDetection;

   public class ObjectDetector : IObjectDetector
      {
      private Dictionary<string, List<Point>> tagPoints;

      public ObjectDetector()
         {
         this.tagPoints = new Dictionary<string, List<Point>>();
         this.Models = new Dictionary<string, Boost>();
         }

      ~ObjectDetector()
         { // ncrunch: no coverage
         this.Dispose(false); // ncrunch: no coverage
         } // ncrunch: no coverage

      private Dictionary<string, Boost> Models
         {
         get;
         set;
         }

      public void Dispose()
         {
         this.Dispose(true);
         GC.SuppressFinalize(this);
         }

      public void Add(string label, Point tagPoint)
         {
         List<Point> tagPoints;

         if (this.tagPoints.ContainsKey(label))
            {
            tagPoints = this.tagPoints[label];
            }
         else
            {
            tagPoints = new List<Point>();

            this.tagPoints.Add(label, tagPoints);
            }

         tagPoints.Add(tagPoint);
         }

      public void Remove(string label, Point tagPoint)
         {
         if (this.tagPoints.ContainsKey(label))
            {
            List<Point> tagPoints = this.tagPoints[label];

            tagPoints.Remove(tagPoint);
            }
         }

      public void Train(byte[, ,] imageData)
         {
         int trainSamples = 0;
         FeatureComputer featureComputer = new FeatureComputer(imageData);

         foreach (List<Point> currentTagPoints in this.tagPoints.Values)
            {
            trainSamples += currentTagPoints.Count;
            }

         if (trainSamples > 0)
            {
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

            using (Matrix<float> trainMatrix = new Matrix<float>(FeatureComputer.NumberOfFeatures, trainSamples), trainResponses = new Matrix<float>(1, trainSamples))
               {
               // Fill the train matrix
               foreach (string label in this.tagPoints.Keys)
                  {
                  foreach (Point tagPoint in this.tagPoints[label])
                     {
                     float[] trainingData = featureComputer.ComputeFeatures(tagPoint);

                     using (Matrix<float> source = new Matrix<float>(trainingData), destination = trainMatrix.GetCol(trueResponses.Count))
                        {
                        source.CopyTo(destination);
                        }

                     trueResponses.Add(label);
                     }
                  }

               // Train the models
               foreach (string label in this.tagPoints.Keys)
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

                  this.Models.Add(label, boost);
                  }
               }
            }
         }

      public Dictionary<string, List<Point>> Test(byte[, ,] imageData)
         {
         Dictionary<string, List<Point>> predictions = new Dictionary<string, List<Point>>();

         if (this.Models.Count() > 0)
            {
            MCvSlice mcvSlice = MCvSlice.WholeSeq;

            FeatureComputer featureComputer = new FeatureComputer(imageData);
            int imageWidth = imageData.GetLength(1);
            int imageHeight = imageData.GetLength(0);
            int imageSize = imageWidth * imageHeight;

            foreach (string model in this.Models.Keys)
               {
               predictions.Add(model, new List<Point>());
               }

            for (int y = 0; y < imageHeight; y++)
               {
               for (int x = 0; x < imageWidth; x++)
                  {
                  float[] featuresData = featureComputer.ComputeFeatures(new Point(x, y));

                  using (Matrix<float> features = new Matrix<float>(featuresData))
                     {
                     foreach (string model in this.Models.Keys)
                        {
                        float prediction = this.Models[model].Predict(features, null, null, mcvSlice, false);

                        if (prediction == 1.0f)
                           {
                           predictions[model].Add(new Point(x, y));
                           }
                        }
                     }
                  }
               }
            }

         return predictions;
         }

      protected virtual void Dispose(bool disposing)
         {
         if (disposing)
            {
            this.ClearModels();
            }
         }

      private void ClearModels()
         {
         foreach (string label in this.Models.Keys)
            {
            Boost boost = null;

            if (this.Models.TryGetValue(label, out boost))
               {
               boost.Dispose();
               }
            }

         this.Models.Clear();
         }
      }
   }
