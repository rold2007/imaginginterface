namespace ImageProcessing.Controllers.Tests
   {
   using System;
   using System.Collections.Generic;
   using System.Diagnostics;
   using System.Drawing;
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
   using ImagingInterface.Views;
   using Microsoft.Practices.ServiceLocation;
   using NUnit.Framework;

   [TestFixture]
   public class ObjectDetectionManagerControllerTest : ControllersBaseTest
      {
      [Test]
      public void Constructor()
         {
         IObjectDetectionManagerController objectDetectionManagerController = this.ServiceLocator.GetInstance<IObjectDetectionManagerController>();
         }

      [Test]
      public void RawPluginModel()
         {
         IObjectDetectionManagerController objectDetectionManagerController = this.ServiceLocator.GetInstance<IObjectDetectionManagerController>();
         IRawPluginModel rawPluginModel = objectDetectionManagerController.RawPluginModel;

         Assert.IsNotNull(rawPluginModel);
         }

      [Test]
      public void RawPluginView()
         {
         IObjectDetectionManagerController objectDetectionManagerController = this.ServiceLocator.GetInstance<IObjectDetectionManagerController>();
         IRawPluginView rawPluginView = objectDetectionManagerController.RawPluginView;

         Assert.IsNotNull(rawPluginView);
         }

      [Test]
      public void Active()
         {
         IObjectDetectionManagerController objectDetectionManagerController = this.ServiceLocator.GetInstance<IObjectDetectionManagerController>();

         Assert.IsTrue(objectDetectionManagerController.Active);
         }

      [Test]
      public void Initialize()
         {
         IObjectDetectionManagerController objectDetectionManagerController = this.ServiceLocator.GetInstance<IObjectDetectionManagerController>();

         objectDetectionManagerController.Initialize();
         }

      [Test]
      public void Close()
         {
         this.Container.RegisterSingle<IObjectDetectionManagerView, ObjectDetectionManagerView>();
         this.Container.RegisterSingle<ITaggerController, TaggerController>();
         this.Container.RegisterSingle<IObjectDetectionController, ObjectDetectionController>();

         ObjectDetectionManagerView objectDetectionManagerView = this.ServiceLocator.GetInstance<IObjectDetectionManagerView>() as ObjectDetectionManagerView;
         ITaggerController taggerController = this.ServiceLocator.GetInstance<ITaggerController>();
         IObjectDetectionController objectDetectionController = this.ServiceLocator.GetInstance<IObjectDetectionController>();
         IObjectDetectionManagerController objectDetectionManagerController = this.ServiceLocator.GetInstance<IObjectDetectionManagerController>();
         bool objectDetectionManagerControllerClosingCalled = false;
         bool objectDetectionManagerControllerClosedCalled = false;
         bool taggerControllerClosingCalled = false;
         bool taggerControllerClosedCalled = false;
         bool objectDetectionControllerClosingCalled = false;
         bool objectDetectionControllerClosedCalled = false;

         objectDetectionManagerController.Closing += (sender, eventArgs) => { objectDetectionManagerControllerClosingCalled = true; };
         objectDetectionManagerController.Closed += (sender, eventArgs) => { objectDetectionManagerControllerClosedCalled = true; };
         taggerController.Closing += (sender, eventArgs) => { taggerControllerClosingCalled = true; };
         taggerController.Closed += (sender, eventArgs) => { taggerControllerClosedCalled = true; };
         objectDetectionController.Closing += (sender, eventArgs) => { objectDetectionControllerClosingCalled = true; };
         objectDetectionController.Closed += (sender, eventArgs) => { objectDetectionControllerClosedCalled = true; };

         objectDetectionManagerController.Close();

         Assert.IsTrue(objectDetectionManagerControllerClosingCalled);
         Assert.IsTrue(objectDetectionManagerControllerClosedCalled);
         Assert.IsTrue(taggerControllerClosingCalled);
         Assert.IsTrue(taggerControllerClosedCalled);
         Assert.IsTrue(objectDetectionControllerClosingCalled);
         Assert.IsTrue(objectDetectionControllerClosedCalled);
         Assert.IsTrue(objectDetectionManagerView.CloseCalled);
         }
      }
   }
