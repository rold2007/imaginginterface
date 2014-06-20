namespace ImageProcessing.Controllers.Tests
   {
   using System;
   using System.Collections.Generic;
   using System.Diagnostics;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using Emgu.CV;
   using Emgu.CV.ML;
   using Emgu.CV.ML.MlEnum;
   using Emgu.CV.ML.Structure;
   using Emgu.CV.Structure;
   using ImageProcessing.Controllers;
   using ImageProcessing.Controllers.Tests.Views;
   using ImageProcessing.Views;
   using ImagingInterface.Controllers;
   using ImagingInterface.Plugins;
   using ImagingInterface.Tests.Common;
   using ImagingInterface.Tests.Common.Mocks;
   using ImagingInterface.Views;
   using Microsoft.Practices.ServiceLocation;
   using NUnit.Framework;

   [TestFixture]
   public class ObjectDetectionControllerTest : ControllersBaseTest
      {
      [Test]
      public void Constructor()
         {
         IObjectDetectionController objectDetectionController = this.ServiceLocator.GetInstance<IObjectDetectionController>();
         }
      }
   }
