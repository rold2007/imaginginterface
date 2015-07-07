namespace ImageProcessing.ObjectDetection.Tests
   {
   using System;
   using System.Collections.Generic;
   using System.Drawing;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImageProcessing.ObjectDetection;
   using ImagingInterface.Tests.Common;
   using ImagingInterface.Tests.Common.Mocks;
   using Microsoft.Practices.ServiceLocation;
   using NUnit.Framework;

   [TestFixture]
   public class ObjectDetectorTest : BaseTest
      {
      [Test]
      public void Constructor()
         {
         using (ObjectDetector objectDetector = new ObjectDetector())
            {
            }
         }

      [Test]
      public void Add()
         {
         using (ObjectDetector objectDetector = new ObjectDetector())
            {
            objectDetector.Add("temp", new Point(0, 0));
            objectDetector.Add("temp", new Point(1, 1));
            }
         }

      [Test]
      public void Train()
         {
         using (ObjectDetector objectDetector = new ObjectDetector())
            {
            byte[, ,] imageData = new byte[10, 10, 1];

            objectDetector.Add("temp1", new Point(0, 0));
            objectDetector.Add("temp2", new Point(1, 1));

            objectDetector.Train(imageData);
            }
         }
      }
   }
