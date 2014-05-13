namespace ImageProcessing.Controllers.Tests
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using Emgu.CV;
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
   public class RotateControllerTest : ControllersBaseTest
      {
      [Test]
      public void Constructor()
         {
         IRotateController rotateController = this.ServiceLocator.GetInstance<IRotateController>();
         }

      [Test]
      public void RawPluginView()
         {
         IRotateController rotateController = this.ServiceLocator.GetInstance<IRotateController>();
         IRawPluginView rotateView = rotateController.RawPluginView;

         Assert.IsNotNull(rotateView);
         }

      [Test]
      public void RawPluginModel()
         {
         IRotateController rotateController = this.ServiceLocator.GetInstance<IRotateController>();
         IRawPluginModel rawPluginModel = rotateController.RawPluginModel;

         Assert.IsNotNull(rawPluginModel);
         }

      [Test]
      public void Close()
         {
         this.Container.RegisterSingle<IRotateView, RotateView>();

         RotateView rotateView = this.ServiceLocator.GetInstance<IRotateView>() as RotateView;
         IRotateController rotateController = this.ServiceLocator.GetInstance<IRotateController>();
         bool closingCalled = false;
         bool closedCalled = false;

         rotateController.Closing += (sender, eventArgs) => { closingCalled = true; };
         rotateController.Closed += (sender, eventArgs) => { closedCalled = true; };

         rotateController.Close();

         Assert.IsTrue(closingCalled);
         Assert.IsTrue(closedCalled);
         Assert.IsTrue(rotateView.CloseCalled);
         }

      [Test]
      public void RotateView_Rotate()
         {
         IRotateController rotateController = this.ServiceLocator.GetInstance<IRotateController>();
         RotateView rotateView = rotateController.RawPluginView as RotateView;
         IImageController imageController = this.ServiceLocator.GetInstance<IImageController>();
         IImageManagerController imageManagerController = this.ServiceLocator.GetInstance<IImageManagerController>();
         ImageSourceController imageSourceController = this.Container.GetInstance<ImageSourceController>();

         Assert.IsNotNull(rotateView);

         using (Image<Rgb, byte> image = new Image<Rgb, byte>(1, 1))
            {
            using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
               {
               imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

               imageManagerController.AddImage(imageController);

               imageControllerWrapper.WaitForDisplayUpdate();
               }

            using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
               {
               rotateView.TriggerRotate(42.54);

               imageControllerWrapper.WaitForDisplayUpdate();
               }

            using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
               {
               imageSourceController.ImageData = new byte[1, 1, 3];

               // Change the angle to make sur the rotate executes itself
               rotateView.TriggerRotate(90);

               imageControllerWrapper.WaitForDisplayUpdate();
               }
            }
         }
      }
   }
