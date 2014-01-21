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
      public void Close()
         {
         this.Container.RegisterSingle<IInvertView, InvertView>();

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

         Assert.IsNotNull(invertView);

         using (Image<Rgb, byte> image = new Image<Rgb, byte>(1, 1))
            {
            imageController.LoadImage(image.Data, string.Empty);

            imageManagerController.AddImage(imageController);

            ImageView imageView = imageManagerView.GetActiveImageView() as ImageView;

            imageView.AssignedImageModel = null;

            invertView.TriggerInvert();

            Assert.IsNotNull(imageView.AssignedImageModel);
            }
         }
      }
   }
