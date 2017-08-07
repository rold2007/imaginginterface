// <copyright file="FeatureComputerTest.Test.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.ObjectDetection.Tests
{
   using System.Drawing;
   using ImageProcessing.ObjectDetection;
   using ImagingInterface.Tests.Common;
   using NUnit.Framework;

   [TestFixture]
   public class FeatureComputerTest : BaseTest
      {
      [Test]
      public void Constructor()
         {
         byte[,,] imageData = new byte[1, 1, 1];
         FeatureComputer featureComputer = new FeatureComputer(null);

         featureComputer = new FeatureComputer(imageData);

         imageData = new byte[1, 1, 2];

         featureComputer = new FeatureComputer(imageData);

         imageData = new byte[1, 1, 3];

         featureComputer = new FeatureComputer(imageData);

         imageData = new byte[1, 1, 4];

         featureComputer = new FeatureComputer(imageData);
         }

      [Test]
      public void ComputeFeatures()
         {
         byte[,,] imageData = new byte[1, 1, 1];
         FeatureComputer featureComputer = new FeatureComputer(imageData);
         float[] features;

         features = featureComputer.ComputeFeatures(new Point(0, 0));

         imageData = new byte[1, 1, 3];
         featureComputer = new FeatureComputer(imageData);

         features = featureComputer.ComputeFeatures(new Point(0, 0));

         imageData = new byte[50, 50, 3];
         featureComputer = new FeatureComputer(imageData);

         features = featureComputer.ComputeFeatures(new Point(25, 25));
         }

      [Test]
      public void ComputeFeaturesWrongChannels()
         {
         byte[,,] imageData = new byte[1, 1, 2];
         FeatureComputer featureComputer = new FeatureComputer(imageData);
         float[] features;

         features = featureComputer.ComputeFeatures(new Point(0, 0));

         Assert.AreEqual(-1, features[0]);
         }
      }
   }
