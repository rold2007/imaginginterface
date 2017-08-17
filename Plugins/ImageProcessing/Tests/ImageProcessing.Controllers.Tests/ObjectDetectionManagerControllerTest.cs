// <copyright file="ObjectDetectionManagerControllerTest.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

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
   using ImageProcessing.Models;
   using ImagingInterface.Controllers;
   using ImagingInterface.Plugins;
   using NUnit.Framework;

   [TestFixture]
   public class ObjectDetectionManagerControllerTest : ControllersBaseTest
      {
      [Test]
      public void Constructor()
         {
         ////ObjectDetectionManagerController objectDetectionManagerController = this.ServiceLocator.GetInstance<ObjectDetectionManagerController>();
         }

      [Test]
      public void DisplayName()
         {
         ////this.Container.RegisterSingleton<IObjectDetectionManagerModel, ObjectDetectionManagerModel>();

         ////ObjectDetectionManagerController objectDetectionManagerController = this.ServiceLocator.GetInstance<ObjectDetectionManagerController>();
         ////IObjectDetectionManagerModel objectDetectionManagerModel = this.ServiceLocator.GetInstance<IObjectDetectionManagerModel>();

         ////Assert.AreEqual("Object detection", objectDetectionManagerModel.DisplayName);
         }

      [Test]
      public void Active()
         {
         ////ObjectDetectionManagerController objectDetectionManagerController = this.ServiceLocator.GetInstance<ObjectDetectionManagerController>();

         ////Assert.IsTrue(objectDetectionManagerController.Active);
         }

      [Test]
      public void Initialize()
         {
         ////ObjectDetectionManagerController objectDetectionManagerController = this.ServiceLocator.GetInstance<ObjectDetectionManagerController>();

         ////objectDetectionManagerController.Initialize();
         }

      [Test]
      public void Close()
         {
         ////this.Container.RegisterSingleton<IObjectDetectionManagerView, ObjectDetectionManagerView>();
         ////this.Container.RegisterSingleton<TaggerController>();
         ////this.Container.RegisterSingleton<ObjectDetectionController>();

         ////////ObjectDetectionManagerView objectDetectionManagerView = this.ServiceLocator.GetInstance<IObjectDetectionManagerView>() as ObjectDetectionManagerView;
         ////TaggerController taggerController = this.ServiceLocator.GetInstance<TaggerController>();
         ////ObjectDetectionController objectDetectionController = this.ServiceLocator.GetInstance<ObjectDetectionController>();
         ////ObjectDetectionManagerController objectDetectionManagerController = this.ServiceLocator.GetInstance<ObjectDetectionManagerController>();
         ////bool objectDetectionManagerControllerClosingCalled = false;
         ////bool objectDetectionManagerControllerClosedCalled = false;
         ////bool taggerControllerClosingCalled = false;
         ////bool taggerControllerClosedCalled = false;
         ////bool objectDetectionControllerClosingCalled = false;
         ////bool objectDetectionControllerClosedCalled = false;

         ////objectDetectionManagerController.Closing += (sender, eventArgs) => { objectDetectionManagerControllerClosingCalled = true; };
         ////objectDetectionManagerController.Closed += (sender, eventArgs) => { objectDetectionManagerControllerClosedCalled = true; };
         ////taggerController.Closing += (sender, eventArgs) => { taggerControllerClosingCalled = true; };
         ////taggerController.Closed += (sender, eventArgs) => { taggerControllerClosedCalled = true; };
         ////objectDetectionController.Closing += (sender, eventArgs) => { objectDetectionControllerClosingCalled = true; };
         ////objectDetectionController.Closed += (sender, eventArgs) => { objectDetectionControllerClosedCalled = true; };

         ////objectDetectionManagerController.Close();

         ////Assert.IsTrue(objectDetectionManagerControllerClosingCalled);
         ////Assert.IsTrue(objectDetectionManagerControllerClosedCalled);
         ////Assert.IsTrue(taggerControllerClosingCalled);
         ////Assert.IsTrue(taggerControllerClosedCalled);
         ////Assert.IsTrue(objectDetectionControllerClosingCalled);
         ////Assert.IsTrue(objectDetectionControllerClosedCalled);
         ////Assert.IsTrue(objectDetectionManagerView.CloseCalled);
         }
      }
   }
