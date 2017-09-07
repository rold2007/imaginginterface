// <copyright file="RotateControllerTest.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Controllers.Tests
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImageProcessing.Controllers;
   using ImageProcessing.Models;
   using ImagingInterface.Plugins;
   using NUnit.Framework;

   [TestFixture]
   public class RotateControllerTest : ControllersBaseTest
      {
      [Test]
      public void Constructor()
         {
         ////RotateController rotateController = this.ServiceLocator.GetInstance<RotateController>();
         }

      [Test]
      public void RawPluginView()
         {
         ////RotateController rotateController = this.ServiceLocator.GetInstance<RotateController>();
         ////IRawPluginView rotateView = rotateController.RawPluginView;

         ////Assert.IsNotNull(rotateView);
         }

      [Test]
      public void RawPluginModel()
         {
         ////RotateController rotateController = this.ServiceLocator.GetInstance<RotateController>();
         ////IRawPluginModel rawPluginModel = rotateController.RawPluginModel;

         ////Assert.IsNotNull(rawPluginModel);
         }

      [Test]
      public void DisplayName()
         {
         ////this.Container.RegisterSingleton<IRotateModel, RotateModel>();

         ////RotateController rotateController = this.ServiceLocator.GetInstance<RotateController>();
         ////IRotateModel rotateModel = this.ServiceLocator.GetInstance<IRotateModel>();

         ////Assert.AreEqual("Rotate", rotateModel.DisplayName);
         }

      [Test]
      public void Active()
         {
         ////RotateController rotateController = this.ServiceLocator.GetInstance<RotateController>();

         ////Assert.IsTrue(rotateController.Active);
         }

      [Test]
      public void Close()
         {
         ////this.Container.RegisterSingleton<IRotateView, RotateView>();

         ////RotateView rotateView = this.ServiceLocator.GetInstance<IRotateView>() as RotateView;
         ////RotateController rotateController = this.ServiceLocator.GetInstance<RotateController>();
         ////bool closingCalled = false;
         ////bool closedCalled = false;

         ////rotateController.Closing += (sender, eventArgs) => { closingCalled = true; };
         ////rotateController.Closed += (sender, eventArgs) => { closedCalled = true; };

         ////rotateController.Close();

         ////Assert.IsTrue(closingCalled);
         ////Assert.IsTrue(closedCalled);
         ////Assert.IsTrue(rotateView.CloseCalled);
         }

      [Test]
      public void RotateView_Rotate()
         {
         ////RotateController rotateController = this.ServiceLocator.GetInstance<RotateController>();
         ////RotateView rotateView = rotateController.RawPluginView as RotateView;
         ////ImageController imageController = this.ServiceLocator.GetInstance<ImageController>();
         ////ImageManagerController imageManagerController = this.ServiceLocator.GetInstance<ImageManagerController>();
         ////ImageSourceController imageSourceController = this.Container.GetInstance<ImageSourceController>();

         ////Assert.IsNotNull(rotateView);

         ////rotateController.Initialize();

         ////using (UMat image = new UMat(1, 1, DepthType.Cv8U, 3))
            {
            ////using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
            ////   {
            ////   imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

            ////   imageManagerController.AddImage(imageController);

            ////   imageControllerWrapper.WaitForDisplayUpdate();
            ////   }

            ////using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
               {
               ////rotateView.TriggerRotate(42.54);

               ////imageControllerWrapper.WaitForDisplayUpdate();
               }

            ////using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
               {
               ////imageSourceController.ImageData = new byte[1, 1, 3];

               // Change the angle to make sur the rotate executes itself
               ////rotateView.TriggerRotate(90);

               ////imageControllerWrapper.WaitForDisplayUpdate();
               }
            }
         }
      }
   }
