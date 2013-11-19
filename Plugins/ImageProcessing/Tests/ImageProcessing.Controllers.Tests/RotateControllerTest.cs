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
      public void RotateView_Rotate()
         {
         IRotateController rotateController = this.ServiceLocator.GetInstance<IRotateController>();
         RotateView rotateView = rotateController.RawPluginView as RotateView;
         IImageController imageController = this.ServiceLocator.GetInstance<IImageController>();
         IImageManagerView imageManagerView = this.ServiceLocator.GetInstance<IImageManagerView>();
         IImageManagerController imageManagerController = this.ServiceLocator.GetInstance<IImageManagerController>();

         Assert.IsNotNull(rotateView);

         using (Image<Bgra, byte> image = new Image<Bgra, byte>(1, 1))
            {
            imageController.LoadImage(image, string.Empty);
            imageManagerController.AddImageController(imageController);

            ImageView imageView = imageManagerView.GetActiveImageView() as ImageView;

            imageView.AssignedImageModel = null;

            rotateView.TriggerRotate();

            Assert.IsNotNull(imageView.AssignedImageModel);
            }
         }
      }
   }
