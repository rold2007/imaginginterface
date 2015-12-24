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
   using ImagingInterface.Tests.Common.Views;
   using ImagingInterface.Views;
   using Microsoft.Practices.ServiceLocation;
   using NUnit.Framework;

   [TestFixture]
   public class InvertControllerTest : ControllersBaseTest
      {
      [Test]
      public void Constructor()
         {
         IInvertController invertController = this.ServiceLocator.GetInstance<IInvertController>();
         }

      [Test]
      public void RawPluginView()
         {
         IInvertController invertController = this.ServiceLocator.GetInstance<IInvertController>();
         IRawPluginView invertView = invertController.RawPluginView;

         Assert.IsNotNull(invertView);
         }

      [Test]
      public void RawPluginModel()
         {
         IInvertController invertController = this.ServiceLocator.GetInstance<IInvertController>();
         IRawPluginModel rawPluginModel = invertController.RawPluginModel;

         Assert.IsNotNull(rawPluginModel);
         }

      [Test]
      public void Active()
         {
         IInvertController invertController = this.ServiceLocator.GetInstance<IInvertController>();

         Assert.IsTrue(invertController.Active);
         }

      [Test]
      public void Close()
         {
         this.Container.RegisterSingleton<IInvertView, InvertView>();

         InvertView invertView = this.ServiceLocator.GetInstance<IInvertView>() as InvertView;
         IInvertController invertController = this.ServiceLocator.GetInstance<IInvertController>();
         bool closingCalled = false;
         bool closedCalled = false;

         invertController.Closing += (sender, eventArgs) => { closingCalled = true; };
         invertController.Closed += (sender, eventArgs) => { closedCalled = true; };

         invertController.Close();

         Assert.IsTrue(closingCalled);
         Assert.IsTrue(closedCalled);
         Assert.IsTrue(invertView.CloseCalled);
         }

      [Test]
      public void InvertView_Invert()
         {
         IInvertController invertController = this.ServiceLocator.GetInstance<IInvertController>();
         InvertView invertView = invertController.RawPluginView as InvertView;
         IImageController imageController = this.ServiceLocator.GetInstance<IImageController>();
         IImageManagerView imageManagerView = this.ServiceLocator.GetInstance<IImageManagerView>();
         IImageManagerController imageManagerController = this.ServiceLocator.GetInstance<IImageManagerController>();
         ImageSourceController imageSourceController = this.Container.GetInstance<IImageSourceController>() as ImageSourceController;

         Assert.IsNotNull(invertView);

         invertController.Initialize();

         using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
            {
            imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

            imageManagerController.AddImage(imageController);

            ImageView imageView = imageManagerView.GetActiveImageView() as ImageView;

            imageControllerWrapper.WaitForDisplayUpdate();
            }

         using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
            {
            invertView.TriggerInvert(true);

            imageControllerWrapper.WaitForDisplayUpdate();
            }

         using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
            {
            // Test the 3 channels code
            imageSourceController.ImageData = new byte[1, 1, 3];

            invertView.TriggerInvert(true);

            imageControllerWrapper.WaitForDisplayUpdate();
            }

         // Test removing the invert processing
         using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
            {
            invertView.TriggerInvert(false);

            imageControllerWrapper.WaitForDisplayUpdate();
            }
         }
      }
   }
