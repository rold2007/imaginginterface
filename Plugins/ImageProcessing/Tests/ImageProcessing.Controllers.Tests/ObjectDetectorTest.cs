// <copyright file="ObjectDetectorTest.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.ObjectDetection.Tests
{
   using System.Drawing;
   using ImageProcessing.ObjectDetection;
   using NUnit.Framework;

   [TestFixture]
   public class ObjectDetectorTest
   {
      [Test]
      public void Constructor()
      {
         ObjectDetector objectDetector = new ObjectDetector();
      }

      [Test]
      public void Add()
      {
         ObjectDetector objectDetector = new ObjectDetector();

         objectDetector.Add("temp", new Point(0, 0));
         objectDetector.Add("temp", new Point(1, 1));
      }

      [Test]
      public void Train()
      {
         ////ObjectDetector objectDetector = new ObjectDetector();

         ////byte[,,] imageData = new byte[100, 100, 1];

         ////for (int imageIndex = 0; imageIndex < 100; imageIndex++)
         ////{
         ////   imageData[imageIndex, imageIndex, 0] = (byte)(imageIndex + 1);
         ////}

         ////// Training on C4.5 needs to have unique values for all features otherwise
         ////// it cannot find a threshold for each feature.
         ////objectDetector.Add("temp1", new Point(49, 49));
         ////objectDetector.Add("temp2", new Point(50, 50));

         ////objectDetector.Train(imageData);
      }
   }
}
