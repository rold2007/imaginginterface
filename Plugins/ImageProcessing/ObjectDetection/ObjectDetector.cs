namespace ImageProcessing.ObjectDetection
   {
   using System;
   using System.Collections.Generic;
   using System.Diagnostics;
   using System.Drawing;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using Accord.MachineLearning.DecisionTrees;
   using Accord.MachineLearning.DecisionTrees.Learning;
   using Accord.Math;
   using ImageProcessing.ObjectDetection;

   public class ObjectDetector : IObjectDetector
      {
      private Dictionary<string, List<Point>> tagPoints;

      public ObjectDetector()
         {
         this.tagPoints = new Dictionary<string, List<Point>>();
         this.Models = new Dictionary<string, DecisionTree>();
         }

      ~ObjectDetector()
         { // ncrunch: no coverage
         this.Dispose(false); // ncrunch: no coverage
         } // ncrunch: no coverage

      private Dictionary<string, DecisionTree> Models
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
            double[][] inputs = new double[trainSamples][];
            int[] outputs = new int[trainSamples];

            this.ClearModels();

            List<string> trueResponses = new List<string>();
            List<DecisionVariable> attributes = new List<DecisionVariable>(FeatureComputer.NumberOfFeatures);

            for (int i = 0; i < FeatureComputer.NumberOfFeatures; i++)
               {
               string columnName = "Feature" + i.ToString();

               attributes.Add(new DecisionVariable(columnName, new AForge.DoubleRange(double.MinValue, double.MaxValue)));
               }

            int trainSampleIndex = 0;

            // Fill the train matrix
            foreach (string label in this.tagPoints.Keys)
               {
               foreach (Point tagPoint in this.tagPoints[label])
                  {
                  float[] trainingData = featureComputer.ComputeFeatures(tagPoint);

                  inputs[trainSampleIndex] = new double[FeatureComputer.NumberOfFeatures];
                  Array.Copy(trainingData, inputs[trainSampleIndex], FeatureComputer.NumberOfFeatures);

                  trueResponses.Add(label);

                  trainSampleIndex++;
                  }
               }

            // Train the models
            foreach (string label in this.tagPoints.Keys)
               {
               int responseIndex = 0;

               // Prepare the responses matrix
               foreach (string responseLabel in trueResponses)
                  {
                  outputs[responseIndex] = (responseLabel == label) ? 1 : 0;

                  responseIndex++;
                  }

               DecisionTree decisionTree = new DecisionTree(attributes, this.tagPoints.Count());
               C45Learning c45Learning = new C45Learning(decisionTree);

               c45Learning.Run(inputs, outputs);

               this.Models.Add(label, decisionTree);
               }
            }
         }

      public Dictionary<string, List<Point>> Test(byte[, ,] imageData)
         {
         Dictionary<string, List<Point>> predictions = new Dictionary<string, List<Point>>();

         if (this.Models.Count() > 0)
            {
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
                  double[] input = new double[FeatureComputer.NumberOfFeatures];

                  Array.Copy(featuresData, input, FeatureComputer.NumberOfFeatures);

                  foreach (string model in this.Models.Keys)
                     {
                     int prediction = this.Models[model].Compute(input);

                     if (prediction == 1)
                        {
                        predictions[model].Add(new Point(x, y));
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
            ////Boost boost = null;

            ////if (this.Models.TryGetValue(label, out boost))
            ////   {
            ////   boost.Dispose();
            ////   }
            }

         this.Models.Clear();
         }
      }
   }
