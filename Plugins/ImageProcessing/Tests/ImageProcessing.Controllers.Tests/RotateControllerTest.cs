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
         IImageManagerView imageManagerView = this.ServiceLocator.GetInstance<IImageManagerView>();
         IImageManagerController imageManagerController = this.ServiceLocator.GetInstance<IImageManagerController>();
         IImageSourceController imageSourceController = this.Container.GetInstance<IImageSourceController>();

         Assert.IsNotNull(rotateView);

         using (Image<Rgb, byte> image = new Image<Rgb, byte>(1, 1))
            {
            imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

            imageManagerController.AddImage(imageController);

            ImageView imageView = imageManagerView.GetActiveImageView() as ImageView;

            imageView.WaitForDisplayUpdate();

            rotateView.TriggerRotate();

            imageView.WaitForDisplayUpdate();
            }
         }
      }
   }
