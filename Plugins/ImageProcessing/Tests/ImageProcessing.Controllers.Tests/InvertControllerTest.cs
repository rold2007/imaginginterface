// <copyright file="InvertControllerTest.cs" company="David Rolland">
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
   using Xunit;

   public class InvertControllerTest : ControllersBaseTest
      {
      [Fact]
      public void Constructor()
         {
         ////InvertController invertController = this.ServiceLocator.GetInstance<InvertController>();
         }

      [Fact]
      public void RawPluginView()
         {
         ////InvertController invertController = this.ServiceLocator.GetInstance<InvertController>();
         ////IRawPluginView invertView = invertController.RawPluginView;

         ////Assert.IsNotNull(invertView);
         }

      [Fact]
      public void RawPluginModel()
         {
         ////InvertController invertController = this.ServiceLocator.GetInstance<InvertController>();
         ////IRawPluginModel rawPluginModel = invertController.RawPluginModel;

         ////Assert.IsNotNull(rawPluginModel);
         }

      [Fact]
      public void DisplayName()
         {
         ////this.Container.RegisterSingleton<IInvertModel, InvertModel>();

         ////InvertController invertController = this.ServiceLocator.GetInstance<InvertController>();
         ////IInvertModel invertModel = this.ServiceLocator.GetInstance<IInvertModel>();

         ////Assert.AreEqual("Invert", invertModel.DisplayName);
         }

      [Fact]
      public void Active()
         {
         ////InvertController invertController = this.ServiceLocator.GetInstance<InvertController>();

         ////Assert.IsTrue(invertController.Active);
         }

      [Fact]
      public void Close()
         {
         ////this.Container.RegisterSingleton<IInvertView, InvertView>();

         ////InvertView invertView = this.ServiceLocator.GetInstance<IInvertView>() as InvertView;
         ////InvertController invertController = this.ServiceLocator.GetInstance<InvertController>();
         ////bool closingCalled = false;
         ////bool closedCalled = false;

         ////invertController.Closing += (sender, eventArgs) => { closingCalled = true; };
         ////invertController.Closed += (sender, eventArgs) => { closedCalled = true; };

         ////invertController.Close();

         ////Assert.IsTrue(closingCalled);
         ////Assert.IsTrue(closedCalled);
         ////Assert.IsTrue(invertView.CloseCalled);
         }

      [Fact]
      public void InvertView_Invert()
         {
         ////InvertController invertController = this.ServiceLocator.GetInstance<InvertController>();
         ////InvertView invertView = invertController.RawPluginView as InvertView;
         ////ImageController imageController = this.ServiceLocator.GetInstance<ImageController>();
         ////IImageManagerView imageManagerView = this.ServiceLocator.GetInstance<IImageManagerView>();
         ////ImageManagerController imageManagerController = this.ServiceLocator.GetInstance<ImageManagerController>();
         ////ImageSourceController imageSourceController = this.Container.GetInstance<IImageSourceController>() as ImageSourceController;

         ////Assert.IsNotNull(invertView);

         ////invertController.Initialize();

         ////using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
         ////   {
         ////   imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

         ////   imageManagerController.AddImage(imageController);

            ////ImageView imageView = imageManagerView.GetActiveImageView() as ImageView;

            ////imageControllerWrapper.WaitForDisplayUpdate();
            ////}

         ////using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
         ////   {
            ////invertView.TriggerInvert(true);

            ////imageControllerWrapper.WaitForDisplayUpdate();
            ////}

         ////using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
         ////   {
            // Test the 3 channels code
            ////imageSourceController.ImageData = new byte[1, 1, 3];

            ////invertView.TriggerInvert(true);

            ////imageControllerWrapper.WaitForDisplayUpdate();
            ////}

         // Test removing the invert processing
         ////using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
         ////   {
            ////invertView.TriggerInvert(false);

            ////imageControllerWrapper.WaitForDisplayUpdate();
            ////}
         }
      }
   }
