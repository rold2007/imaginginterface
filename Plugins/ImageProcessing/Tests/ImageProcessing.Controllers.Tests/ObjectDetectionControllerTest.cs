namespace ImageProcessing.Controllers.Tests
   {
   using System.Drawing;
   using ImageProcessing.Controllers;
   using ImageProcessing.Controllers.Tests.Views;
   using ImageProcessing.Models;
   using ImageProcessing.Views;
   using ImagingInterface.Plugins;
   using ImagingInterface.Tests.Common;
   using ImagingInterface.Tests.Common.Mocks;
   using NUnit.Framework;

   [TestFixture]
   public class ObjectDetectionControllerTest : ControllersBaseTest
      {
      [Test]
      public void Constructor()
         {
         IObjectDetectionController objectDetectionController = this.ServiceLocator.GetInstance<IObjectDetectionController>();
         }

      [Test]
      public void Active()
         {
         IObjectDetectionController objectDetectionController = this.ServiceLocator.GetInstance<IObjectDetectionController>();

         Assert.IsFalse(objectDetectionController.Active);
         }

      [Test]
      public void Initialize()
         {
         this.Container.RegisterSingle<IObjectDetectionView, ObjectDetectionView>();

         ObjectDetectionView objectDetectionView = this.ServiceLocator.GetInstance<IObjectDetectionView>() as ObjectDetectionView;
         IObjectDetectionController objectDetectionController = this.ServiceLocator.GetInstance<IObjectDetectionController>();

         Assert.IsFalse(objectDetectionView.CanTrain());
         Assert.IsFalse(objectDetectionView.CanTest());

         objectDetectionController.Initialize();

         Assert.IsTrue(objectDetectionView.CanTrain());
         Assert.IsTrue(objectDetectionView.CanTest());
         }

      [Test]
      public void Close()
         {
         this.Container.RegisterSingle<IObjectDetectionView, ObjectDetectionView>();

         ObjectDetectionView objectDetectionView = this.ServiceLocator.GetInstance<IObjectDetectionView>() as ObjectDetectionView;
         IObjectDetectionController objectDetectionController = this.ServiceLocator.GetInstance<IObjectDetectionController>();
         bool closingCalled = false;
         bool closedCalled = false;

         objectDetectionController.Closing += (sender, eventArgs) => { closingCalled = true; };
         objectDetectionController.Closed += (sender, eventArgs) => { closedCalled = true; };

         objectDetectionController.Close();

         Assert.IsTrue(closingCalled);
         Assert.IsTrue(closedCalled);
         Assert.IsTrue(objectDetectionView.CloseCalled);
         }

      [Test]
      public void ProcessImageData()
         {
         this.Container.RegisterSingle<IObjectDetectionView, ObjectDetectionView>();
         this.Container.RegisterSingle<ITaggerView, TaggerView>();
         this.Container.RegisterSingle<ITaggerModel, TaggerModel>();

         string displayName = "temp";
         ObjectDetectionView objectDetectionView = this.ServiceLocator.GetInstance<IObjectDetectionView>() as ObjectDetectionView;
         IObjectDetectionController objectDetectionController = this.ServiceLocator.GetInstance<IObjectDetectionController>();
         ITaggerController taggerController = this.ServiceLocator.GetInstance<ITaggerController>();
         TaggerView taggerView = this.ServiceLocator.GetInstance<ITaggerView>() as TaggerView;
         ITaggerModel taggerModel = this.ServiceLocator.GetInstance<ITaggerModel>();
         IImageController imageController = this.ServiceLocator.GetInstance<IImageController>();
         IImageManagerController imageManagerController = this.ServiceLocator.GetInstance<IImageManagerController>();
         ImageSourceController imageSourceController = this.Container.GetInstance<ImageSourceController>();
         byte[, ,] imageData = new byte[1, 1, 1];
         byte[] overlayData = new byte[4];

         objectDetectionController.Initialize();

         // Make sure all goes well without any model
         objectDetectionController.ProcessImageData(imageData, overlayData, objectDetectionController.RawPluginModel);

         // Trying to train without any data
         objectDetectionView.TriggerTrain();

         // Add some data
         objectDetectionController.SetTagger(taggerController);

         taggerController.Initialize();

         taggerModel.AddedLabel = "a";
         taggerView.TriggerLabelAdded();
         taggerModel.AddedLabel = "b";
         taggerView.TriggerLabelAdded();

         taggerController.AddPoint("a", new Point(0, 0));
         taggerController.AddPoint("b", new Point(1, 1));

         imageSourceController.ImageData = new byte[1, 1, 1];

         using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
            {
            imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

            imageController.SetDisplayName(displayName);

            imageManagerController.AddImage(imageController);

            imageControllerWrapper.WaitForDisplayUpdate();
            }

         objectDetectionView.TriggerTrain();

         Assert.AreEqual(0, overlayData[0]);
         Assert.AreEqual(0, overlayData[1]);
         Assert.AreEqual(0, overlayData[2]);
         Assert.AreEqual(0, overlayData[3]);

         objectDetectionController.ProcessImageData(imageData, overlayData, objectDetectionController.RawPluginModel);

         ////Assert.AreEqual(taggerModel.LabelColors["a"].R, overlayData[0]);
         ////Assert.AreEqual(taggerModel.LabelColors["a"].G, overlayData[1]);
         ////Assert.AreEqual(taggerModel.LabelColors["a"].B, overlayData[2]);
         ////Assert.AreEqual(255, overlayData[3]);
         }

      [Test]
      public void Test()
         {
         this.Container.RegisterSingle<IObjectDetectionView, ObjectDetectionView>();

         string displayName = "temp";
         ObjectDetectionView objectDetectionView = this.ServiceLocator.GetInstance<IObjectDetectionView>() as ObjectDetectionView;
         IObjectDetectionController objectDetectionController = this.ServiceLocator.GetInstance<IObjectDetectionController>();

         objectDetectionController.Initialize();

         // Make sure there is no issue when there is no image controller
         objectDetectionView.TriggerTest();

         IImageController imageController = this.ServiceLocator.GetInstance<IImageController>();
         IImageManagerController imageManagerController = this.ServiceLocator.GetInstance<IImageManagerController>();
         ImageSourceController imageSourceController = this.Container.GetInstance<ImageSourceController>();

         using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
            {
            imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

            imageController.SetDisplayName(displayName);

            imageManagerController.AddImage(imageController);

            objectDetectionView.TriggerTest();

            imageControllerWrapper.WaitForDisplayUpdate();
            }
         }

      [Test]
      public void RawPluginModel()
         {
         IObjectDetectionController objectDetectionController = this.ServiceLocator.GetInstance<IObjectDetectionController>();
         IRawPluginModel rawPluginModel = objectDetectionController.RawPluginModel;

         Assert.IsNotNull(rawPluginModel);
         }

      [Test]
      public void RawPluginView()
         {
         IObjectDetectionController objectDetectionController = this.ServiceLocator.GetInstance<IObjectDetectionController>();
         IRawPluginView rawPluginView = objectDetectionController.RawPluginView;

         Assert.IsNotNull(rawPluginView);
         }

      [Test]
      public void SetTagger()
         {
         IObjectDetectionController objectDetectionController = this.ServiceLocator.GetInstance<IObjectDetectionController>();
         ITaggerController taggerController = this.ServiceLocator.GetInstance<ITaggerController>();

         objectDetectionController.SetTagger(taggerController);
         }

      [Test]
      public void TagPointChanged()
         {
         this.Container.RegisterSingle<IObjectDetectionView, ObjectDetectionView>();

         ObjectDetectionView objectDetectionView = this.ServiceLocator.GetInstance<IObjectDetectionView>() as ObjectDetectionView;
         IObjectDetectionController objectDetectionController = this.ServiceLocator.GetInstance<IObjectDetectionController>();
         ITaggerController taggerController = this.ServiceLocator.GetInstance<ITaggerController>();
         IImageController imageController = this.ServiceLocator.GetInstance<IImageController>();
         IImageManagerController imageManagerController = this.ServiceLocator.GetInstance<IImageManagerController>();
         ImageSourceController imageSourceController = this.Container.GetInstance<ImageSourceController>();
         byte[, ,] imageData = new byte[1, 1, 1];
         byte[] overlayData = new byte[4];

         objectDetectionController.Initialize();

         // Make sure all goes well without any model
         objectDetectionController.ProcessImageData(imageData, overlayData, objectDetectionController.RawPluginModel);

         // Trying to train without any data
         objectDetectionView.TriggerTrain();

         // Add some data
         objectDetectionController.SetTagger(taggerController);

         taggerController.Initialize();

         taggerController.AddPoint("a", new Point(0, 0));
         taggerController.RemovePoint("a", new Point(0, 0));
         }
      }
   }
