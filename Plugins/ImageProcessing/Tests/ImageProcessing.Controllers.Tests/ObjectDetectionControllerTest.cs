// <copyright file="ObjectDetectionControllerTest.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Controllers.Tests
{
   using Xunit;

   public class ObjectDetectionControllerTest : ControllersBaseTest
      {
      [Fact]
      public void Constructor()
         {
         ////ObjectDetectionController objectDetectionController = this.ServiceLocator.GetInstance<ObjectDetectionController>();
         }

      [Fact]
      public void Active()
         {
         ////ObjectDetectionController objectDetectionController = this.ServiceLocator.GetInstance<ObjectDetectionController>();

         ////Assert.IsFalse(objectDetectionController.Active);
         }

      [Fact]
      public void Initialize()
         {
         ////this.Container.RegisterSingleton<IObjectDetectionView, ObjectDetectionView>();

         ////ObjectDetectionView objectDetectionView = this.ServiceLocator.GetInstance<IObjectDetectionView>() as ObjectDetectionView;
         ////ObjectDetectionController objectDetectionController = this.ServiceLocator.GetInstance<ObjectDetectionController>();

         ////Assert.IsFalse(objectDetectionView.CanTrain());
         ////Assert.IsFalse(objectDetectionView.CanTest());

         ////objectDetectionController.Initialize();

         ////Assert.IsTrue(objectDetectionView.CanTrain());
         ////Assert.IsTrue(objectDetectionView.CanTest());
         }

      [Fact]
      public void Close()
         {
         ////this.Container.RegisterSingleton<IObjectDetectionView, ObjectDetectionView>();

         ////ObjectDetectionView objectDetectionView = this.ServiceLocator.GetInstance<IObjectDetectionView>() as ObjectDetectionView;
         ////ObjectDetectionController objectDetectionController = this.ServiceLocator.GetInstance<ObjectDetectionController>();
         ////bool closingCalled = false;
         ////bool closedCalled = false;

         ////objectDetectionController.Closing += (sender, eventArgs) => { closingCalled = true; };
         ////objectDetectionController.Closed += (sender, eventArgs) => { closedCalled = true; };

         ////objectDetectionController.Close();

         ////Assert.IsTrue(closingCalled);
         ////Assert.IsTrue(closedCalled);
         ////Assert.IsTrue(objectDetectionView.CloseCalled);
         }

      [Fact]
      public void ProcessImageData()
         {
         ////this.Container.RegisterSingleton<IObjectDetectionView, ObjectDetectionView>();
         ////this.Container.RegisterSingleton<ITaggerView, TaggerView>();
         ////this.Container.RegisterSingleton<ITaggerModel, TaggerModel>();

         ////string displayName = "temp";
         ////////ObjectDetectionView objectDetectionView = this.ServiceLocator.GetInstance<IObjectDetectionView>() as ObjectDetectionView;
         ////ObjectDetectionController objectDetectionController = this.ServiceLocator.GetInstance<ObjectDetectionController>();
         ////TaggerController taggerController = this.ServiceLocator.GetInstance<TaggerController>();
         ////////TaggerView taggerView = this.ServiceLocator.GetInstance<ITaggerView>() as TaggerView;
         ////ITaggerModel taggerModel = this.ServiceLocator.GetInstance<ITaggerModel>();
         ////ImageController imageController = this.ServiceLocator.GetInstance<ImageController>();
         ////ImageManagerController imageManagerController = this.ServiceLocator.GetInstance<ImageManagerController>();
         ////ImageSourceController imageSourceController = this.Container.GetInstance<ImageSourceController>();
         ////byte[, ,] imageData = new byte[100, 100, 1];
         ////byte[] overlayData = new byte[100 * 100 * 4];

         ////for (int imageIndex = 0; imageIndex < 100; imageIndex++)
         ////   {
         ////   imageData[imageIndex, imageIndex, 0] = (byte)(imageIndex + 1);
         ////   }

         ////objectDetectionController.Initialize();

         ////// Make sure all goes well without any model
         ////objectDetectionController.ProcessImageData(imageData, overlayData, objectDetectionController.RawPluginModel);

         ////// Trying to train without any data
         ////////objectDetectionView.TriggerTrain();

         ////// Add some data
         ////objectDetectionController.SetTagger(taggerController);

         ////taggerController.Initialize();

         ////taggerModel.AddedLabel = "a";
         ////////taggerView.TriggerLabelAdded();
         ////taggerModel.AddedLabel = "b";
         ////////taggerView.TriggerLabelAdded();

         ////taggerController.AddPoint("a", new Point(49, 49));
         ////taggerController.AddPoint("b", new Point(50, 50));

         ////imageSourceController.ImageData = new byte[100, 100, 1];

         ////for (int imageIndex = 0; imageIndex < 100; imageIndex++)
         ////   {
         ////   imageSourceController.ImageData[imageIndex, imageIndex, 0] = (byte)(imageIndex + 1);
         ////   }

         ////////using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
         ////////   {
         ////////   imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

         ////////   imageController.SetDisplayName(displayName);

         ////////   imageManagerController.AddImage(imageController);

         ////////   imageControllerWrapper.WaitForDisplayUpdate();
         ////////   }

         ////////objectDetectionView.TriggerTrain();

         ////Assert.AreEqual(0, overlayData[0]);
         ////Assert.AreEqual(0, overlayData[1]);
         ////Assert.AreEqual(0, overlayData[2]);
         ////Assert.AreEqual(0, overlayData[3]);

         ////objectDetectionController.ProcessImageData(imageData, overlayData, objectDetectionController.RawPluginModel);

         ////int pixelOffset = (49 * 100 * 4) + (49 * 4);
         ////Assert.AreEqual(taggerModel.LabelColors["a"].R, overlayData[pixelOffset]);
         ////Assert.AreEqual(taggerModel.LabelColors["a"].G, overlayData[pixelOffset + 1]);
         ////Assert.AreEqual(taggerModel.LabelColors["a"].B, overlayData[pixelOffset + 2]);
         ////Assert.AreEqual(255, overlayData[pixelOffset + 3]);

         ////pixelOffset = (50 * 100 * 4) + (50 * 4);
         ////Assert.AreEqual(taggerModel.LabelColors["b"].R, overlayData[pixelOffset]);
         ////Assert.AreEqual(taggerModel.LabelColors["b"].G, overlayData[pixelOffset + 1]);
         ////Assert.AreEqual(taggerModel.LabelColors["b"].B, overlayData[pixelOffset + 2]);
         ////Assert.AreEqual(255, overlayData[pixelOffset + 3]);
         }

      [Fact]
      public void Test()
         {
         ////this.Container.RegisterSingleton<IObjectDetectionView, ObjectDetectionView>();

         ////string displayName = "temp";
         ////ObjectDetectionView objectDetectionView = this.ServiceLocator.GetInstance<IObjectDetectionView>() as ObjectDetectionView;
         ////ObjectDetectionController objectDetectionController = this.ServiceLocator.GetInstance<ObjectDetectionController>();

         ////objectDetectionController.Initialize();

         // Make sure there is no issue when there is no image controller
         ////objectDetectionView.TriggerTest();

         ////ImageController imageController = this.ServiceLocator.GetInstance<ImageController>();
         ////ImageManagerController imageManagerController = this.ServiceLocator.GetInstance<ImageManagerController>();
         ////ImageSourceController imageSourceController = this.Container.GetInstance<ImageSourceController>();

         ////using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
         ////   {
         ////   imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

         ////   imageController.SetDisplayName(displayName);

         ////   imageManagerController.AddImage(imageController);

            ////objectDetectionView.TriggerTest();

            ////imageControllerWrapper.WaitForDisplayUpdate();
            ////}
         }

      [Fact]
      public void SetTagger()
         {
         ////ObjectDetectionController objectDetectionController = this.ServiceLocator.GetInstance<ObjectDetectionController>();
         ////TaggerController taggerController = this.ServiceLocator.GetInstance<TaggerController>();

         ////objectDetectionController.SetTagger(taggerController);
         }

      [Fact]
      public void TagPointChanged()
         {
         ////this.Container.RegisterSingleton<IObjectDetectionView, ObjectDetectionView>();

         ////ObjectDetectionView objectDetectionView = this.ServiceLocator.GetInstance<IObjectDetectionView>() as ObjectDetectionView;
         ////ObjectDetectionController objectDetectionController = this.ServiceLocator.GetInstance<ObjectDetectionController>();
         ////TaggerController taggerController = this.ServiceLocator.GetInstance<TaggerController>();
         ////ImageController imageController = this.ServiceLocator.GetInstance<ImageController>();
         ////ImageManagerController imageManagerController = this.ServiceLocator.GetInstance<ImageManagerController>();
         ////ImageSourceController imageSourceController = this.Container.GetInstance<ImageSourceController>();
         ////byte[, ,] imageData = new byte[1, 1, 1];
         ////byte[] overlayData = new byte[4];

         ////objectDetectionController.Initialize();

         ////// Make sure all goes well without any model
         ////objectDetectionController.ProcessImageData(imageData, overlayData, objectDetectionController.RawPluginModel);

         ////// Trying to train without any data
         ////////objectDetectionView.TriggerTrain();

         ////// Add some data
         ////objectDetectionController.SetTagger(taggerController);

         ////taggerController.Initialize();

         ////taggerController.AddPoint("a", new Point(0, 0));
         ////taggerController.RemovePoint("a", new Point(0, 0));
         }
      }
   }
