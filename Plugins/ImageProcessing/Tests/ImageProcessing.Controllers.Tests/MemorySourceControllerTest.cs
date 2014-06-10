namespace ImageProcessing.Controllers.Tests
   {
   using System;
   using System.Collections.Generic;
   using System.IO;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using Emgu.CV;
   using Emgu.CV.Structure;
   using ImageProcessing.Controllers;
   using ImagingInterface.Plugins;
   using Microsoft.Practices.ServiceLocation;
   using NUnit.Framework;

   [TestFixture]
   public class MemorySourceControllerTest : ControllersBaseTest
      {
      [Test]
      public void Constructor()
         {
         IMemorySourceController memorySourceController = this.ServiceLocator.GetInstance<IMemorySourceController>();
         }

      [Test]
      public void Active()
         {
         IMemorySourceController memorySourceController = this.ServiceLocator.GetInstance<IMemorySourceController>();

         Assert.IsFalse(memorySourceController.Active);
         }

      [Test]
      public void IsDynamic()
         {
         IMemorySourceController memorySourceController = this.ServiceLocator.GetInstance<IMemorySourceController>();

         Assert.False(memorySourceController.IsDynamic(null));
         Assert.False(memorySourceController.IsDynamic(memorySourceController.RawPluginModel));
         }

      [Test]
      public void Close()
         {
         IMemorySourceController memorySourceController = this.ServiceLocator.GetInstance<IMemorySourceController>();

         memorySourceController.Close();
         }

      [Test]
      public void NextImageData()
         {
         IMemorySourceController memorySourceController = this.ServiceLocator.GetInstance<IMemorySourceController>();

         Assert.IsNull(memorySourceController.ImageData);

         using (Image<Gray, byte> image = new Image<Gray, byte>(1, 1))
            {
            memorySourceController.ImageData = image.Data;

            byte[, ,] resultImage = memorySourceController.NextImageData(memorySourceController.RawPluginModel);

            Assert.IsNotNull(resultImage);
            }
         }

      [Test]
      public void Disconnected()
         {
         IMemorySourceController memorySourceController = this.ServiceLocator.GetInstance<IMemorySourceController>();

         memorySourceController.Disconnected();
         }
      }
   }
