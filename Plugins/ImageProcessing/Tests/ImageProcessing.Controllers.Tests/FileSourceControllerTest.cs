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
   public class FileSourceControllerTest : ControllersBaseTest
      {
      [Test]
      public void Constructor()
         {
         IFileSourceController fileSourceController = this.ServiceLocator.GetInstance<IFileSourceController>();
         }

      [Test]
      public void Filename()
         {
         IFileSourceController fileSourceController = this.ServiceLocator.GetInstance<IFileSourceController>();

         Assert.IsNull(fileSourceController.Filename);

         fileSourceController.Filename = "Dummy";

         Assert.AreEqual("Dummy", fileSourceController.Filename);
         }

      [Test]
      public void IsDynamic()
         {
         IFileSourceController fileSourceController = this.ServiceLocator.GetInstance<IFileSourceController>();

         Assert.False(fileSourceController.IsDynamic(null));
         Assert.False(fileSourceController.IsDynamic(fileSourceController.RawPluginModel));
         }

      [Test]
      public void NextImageData()
         {
         IFileSourceController fileSourceController = this.ServiceLocator.GetInstance<IFileSourceController>();

         byte[, ,] resultImage = fileSourceController.NextImageData(fileSourceController.RawPluginModel);

         Assert.IsNull(resultImage);

         string tempImageFilename = string.Empty;
         string tempFileName = string.Empty;

         try
            {
            tempFileName = Path.GetTempFileName();

            fileSourceController.Filename = tempFileName;

            resultImage = fileSourceController.NextImageData(fileSourceController.RawPluginModel);

            Assert.IsNull(resultImage);

            tempImageFilename = Path.GetRandomFileName() + ".png";

            using (Image<Gray, byte> image = new Image<Gray, byte>(1, 1))
               {
               image.Save(tempImageFilename);

               fileSourceController.Filename = tempImageFilename;

               resultImage = fileSourceController.NextImageData(fileSourceController.RawPluginModel);

               Assert.IsNotNull(resultImage);
               }
            }
         finally
            {
            if (!string.IsNullOrEmpty(tempImageFilename))
               {
               File.Delete(tempImageFilename);
               }

            if (!string.IsNullOrEmpty(tempFileName))
               {
               File.Delete(tempFileName);
               }
            }
         }
      }
   }
