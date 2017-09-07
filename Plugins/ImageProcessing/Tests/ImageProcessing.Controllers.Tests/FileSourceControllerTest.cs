// <copyright file="FileSourceControllerTest.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Controllers.Tests
{
   using Xunit;

   public class FileSourceControllerTest : ControllersBaseTest
      {
      [Fact]
      public void Constructor()
         {
         ////IFileSourceController fileSourceController = this.ServiceLocator.GetInstance<IFileSourceController>();

         ////fileSourceController.Initialize();
         }

      [Fact]
      public void Active()
         {
         ////IFileSourceController fileSourceController = this.ServiceLocator.GetInstance<IFileSourceController>();

         ////Assert.IsFalse(fileSourceController.Active);
         }

      [Fact]
      public void Filename()
         {
         ////IFileSourceController fileSourceController = this.ServiceLocator.GetInstance<IFileSourceController>();

         ////Assert.IsNull(fileSourceController.Filename);

         ////fileSourceController.Filename = "Dummy";

         ////Assert.AreEqual("Dummy", fileSourceController.Filename);
         }

      [Fact]
      public void IsDynamic()
         {
         ////IFileSourceController fileSourceController = this.ServiceLocator.GetInstance<IFileSourceController>();

         ////Assert.False(fileSourceController.IsDynamic(null));
         ////Assert.False(fileSourceController.IsDynamic(fileSourceController.RawPluginModel));
         }

      [Fact]
      public void Close()
         {
         ////IFileSourceController fileSourceController = this.ServiceLocator.GetInstance<IFileSourceController>();

         ////bool closingCalled = false;
         ////bool closedCalled = false;

         ////fileSourceController.Closing += (sender, eventArgs) => { closingCalled = true; };
         ////fileSourceController.Closed += (sender, eventArgs) => { closedCalled = true; };

         ////fileSourceController.Close();

         ////Assert.IsTrue(closingCalled);
         ////Assert.IsTrue(closedCalled);
         }

      [Fact]
      public void NextImageData()
         {
         ////IFileSourceController fileSourceController = this.ServiceLocator.GetInstance<IFileSourceController>();

         ////byte[, ,] resultImage = fileSourceController.NextImageData(fileSourceController.RawPluginModel);

         ////Assert.IsNull(resultImage);

         ////string tempImageFilename = string.Empty;
         ////string tempFileName = string.Empty;

         ////try
         ////   {
         ////   tempFileName = Path.GetTempFileName();

         ////   fileSourceController.Filename = tempFileName;

         ////   resultImage = fileSourceController.NextImageData(fileSourceController.RawPluginModel);

         ////   Assert.IsNull(resultImage);

         ////   tempImageFilename = Path.GetRandomFileName() + ".png";

         ////   using (UMat image = new UMat(1, 1, DepthType.Cv8U, 1))
         ////      {
         ////      image.Save(tempImageFilename);

         ////      fileSourceController.Filename = tempImageFilename;

         ////      resultImage = fileSourceController.NextImageData(fileSourceController.RawPluginModel);

         ////      Assert.IsNotNull(resultImage);
         ////      Assert.AreEqual(3, resultImage.Length);
         ////      }
         ////   }
         ////finally
         ////   {
         ////   if (!string.IsNullOrEmpty(tempImageFilename))
         ////      {
         ////      File.Delete(tempImageFilename);
         ////      }

         ////   if (!string.IsNullOrEmpty(tempFileName))
         ////      {
         ////      File.Delete(tempFileName);
         ////      }
         ////   }
         }

      [Fact]
      public void Disconnected()
         {
         ////IFileSourceController fileSourceController = this.ServiceLocator.GetInstance<IFileSourceController>();

         ////fileSourceController.Disconnected();
         }
      }
   }
