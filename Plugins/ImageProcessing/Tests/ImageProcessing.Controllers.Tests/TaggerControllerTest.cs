namespace ImageProcessing.Controllers.Tests
   {
   using System;
   using System.Collections.Generic;
   using System.Drawing;
   using System.IO;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImageProcessing.Controllers;
   using ImageProcessing.Controllers.Tests.Views;
   using ImageProcessing.Views;
   using ImagingInterface.Controllers;
   using ImagingInterface.Plugins;
   using ImagingInterface.Tests.Common;
   using ImagingInterface.Tests.Common.Mocks;
   using ImagingInterface.Tests.Common.Views;
   using ImagingInterface.Views;
   using Microsoft.Practices.ServiceLocation;
   using NUnit.Framework;

   [TestFixture]
   public class TaggerControllerTest : ControllersBaseTest
      {
      [Test]
      public void Constructor()
         {
         ITaggerController taggerController = this.ServiceLocator.GetInstance<ITaggerController>();
         }

      [Test]
      public void RawPluginView()
         {
         ITaggerController taggerController = this.ServiceLocator.GetInstance<ITaggerController>();
         IRawPluginView rotateView = taggerController.RawPluginView;

         Assert.IsNotNull(rotateView);
         }

      [Test]
      public void RawPluginModel()
         {
         ITaggerController taggerController = this.ServiceLocator.GetInstance<ITaggerController>();
         IRawPluginModel rawPluginModel = taggerController.RawPluginModel;

         Assert.IsNotNull(rawPluginModel);
         }

      [Test]
      public void Active()
         {
         ITaggerController taggerController = this.ServiceLocator.GetInstance<ITaggerController>();

         Assert.IsTrue(taggerController.Active);
         }

      [Test]
      public void Close()
         {
         this.Container.RegisterSingle<ITaggerView, TaggerView>();

         TaggerView taggerView = this.ServiceLocator.GetInstance<ITaggerView>() as TaggerView;
         ITaggerController taggerController = this.ServiceLocator.GetInstance<ITaggerController>();
         bool closingCalled = false;
         bool closedCalled = false;

         taggerController.Closing += (sender, eventArgs) => { closingCalled = true; };
         taggerController.Closed += (sender, eventArgs) => { closedCalled = true; };

         taggerController.Close();

         Assert.IsTrue(closingCalled);
         Assert.IsTrue(closedCalled);
         Assert.IsTrue(taggerView.CloseCalled);
         }

      [Test]
      public void ProcessImageData()
         {
         string displayName = "temp";
         string directory = Path.GetTempPath() + @"\Tagger\";
         string extension = ".imagedata";
         string tempDataFilename = directory + displayName + extension;

         if (Directory.Exists(directory))
            {
            Directory.Delete(directory, true);
            }

         if (File.Exists(tempDataFilename))
            {
            File.Delete(tempDataFilename);
            }

         try
            {
            this.Container.RegisterSingle<IImageView, ImageView>();

            ITaggerController taggerController = this.ServiceLocator.GetInstance<ITaggerController>();
            TaggerView taggerView = this.ServiceLocator.GetInstance<ITaggerView>() as TaggerView;
            IImageController imageController = this.ServiceLocator.GetInstance<IImageController>();
            IImageManagerController imageManagerController = this.ServiceLocator.GetInstance<IImageManagerController>();
            ImageSourceController imageSourceController = this.Container.GetInstance<ImageSourceController>();
            ImageView imageView = this.Container.GetInstance<IImageView>() as ImageView;

            Assert.IsNotNull(taggerView);

            imageSourceController.ImageData = new byte[10, 10, 1];

            taggerController.Initialize();

            using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
               {
               imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

               imageController.SetDisplayName(displayName);

               imageManagerController.AddImage(imageController);

               imageControllerWrapper.WaitForDisplayUpdate();
               }

            // Tag a point
            using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
               {
               imageView.TriggerSelectionChanged(new Point(1, 1), true);

               imageControllerWrapper.WaitForDisplayUpdate();
               }

            // Add an already tagged point
            imageView.TriggerSelectionChanged(new Point(1, 1), true);

            // Try to untag an unexisting point
            imageView.TriggerSelectionChanged(new Point(2, 2), false);

            imageSourceController.ImageData = new byte[10, 10, 3];

            // Tag a point in a 3-channels image
            using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
               {
               imageView.TriggerSelectionChanged(new Point(2, 2), true);

               imageControllerWrapper.WaitForDisplayUpdate();
               }

            // Untag a point
            using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
               {
               imageView.TriggerSelectionChanged(new Point(2, 2), false);

               imageControllerWrapper.WaitForDisplayUpdate();
               }

            // Save points
            imageController.Close();

            imageController = this.ServiceLocator.GetInstance<IImageController>();

            // Load points
            using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
               {
               imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

               imageController.SetDisplayName(displayName);

               imageManagerController.AddImage(imageController);

               imageControllerWrapper.WaitForDisplayUpdate();
               }

            Dictionary<string, List<Point>> dataPoints = taggerController.DataPoints;

            Assert.AreEqual(1, dataPoints.Count);
            Assert.AreEqual(new Point(1, 1), dataPoints["Label"][0]);
            }
         finally
            {
            if (File.Exists(tempDataFilename))
               {
               File.Delete(tempDataFilename);
               }
            }
         }
      }
   }
