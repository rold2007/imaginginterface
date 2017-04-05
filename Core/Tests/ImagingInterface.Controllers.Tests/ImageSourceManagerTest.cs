namespace ImagingInterface.Controllers.Tests
{
   using System;
   using System.Collections.Generic;
   using ImagingInterface.Controllers.Tests.Mocks;
   using ImagingInterface.Plugins;
   using NUnit.Framework;

   [TestFixture]
   public class ImageSourceManagerTest : ControllersBaseTest
   {
      [Test]
      public void Constructor()
      {
         ImageSourceManager imageSourceManager = new ImageSourceManager(new FileSourceFactory());
      }

      [Test]
      public void AddImageFiles()
      {
         int imageAddedCount = 0;
         ImageSourceManager imageSourceManager = new ImageSourceManager(new FileSourceFactory());

         imageSourceManager.ImageSourceAdded += (sender, eventArgs) => { imageAddedCount++; };

         List<string> files = new List<string>();

         imageSourceManager.AddImageFiles(files);

         Assert.AreEqual(0, imageAddedCount);

         files.Add("ValidFile");

         imageSourceManager.AddImageFiles(files);

         Assert.AreEqual(1, imageAddedCount);

         // Invalid file
         files.Add("dummy");

         imageSourceManager.AddImageFiles(files);

         Assert.AreEqual(2, imageAddedCount);
      }

      [Test]
      public void AddImageFilesNullArgument()
      {
         ImageSourceManager imageSourceManager = new ImageSourceManager(new FileSourceFactory());

         Assert.Throws<ArgumentNullException>(() => imageSourceManager.AddImageFiles(null));
      }

      [Test]
      public void RemoveImageSource()
      {
         IImageSource imageSourceAdded = null;
         IImageSource imageSourceRemoved = null;
         int imageSourceRemovedCount = 0;
         ImageSourceManager imageSourceManager = new ImageSourceManager(new FileSourceFactory());

         imageSourceManager.ImageSourceAdded += (sender, eventArgs) =>
         {
            Assert.IsNull(imageSourceAdded);
            imageSourceAdded = eventArgs.ImageSource;
         };

         imageSourceManager.ImageSourceRemoved += (sender, eventArgs) => 
         {
            Assert.IsNull(imageSourceRemoved);
            imageSourceRemoved = eventArgs.ImageSource;

            imageSourceRemovedCount++;
         };

         List<string> files = new List<string>
            {
                "ValidFile"
            };

         imageSourceManager.AddImageFiles(files);

         Assert.IsNotNull(imageSourceAdded);
         Assert.AreEqual(0, imageSourceRemovedCount);

         imageSourceManager.RemoveImageSource(imageSourceAdded);

         Assert.IsNotNull(imageSourceRemoved);
         Assert.AreSame(imageSourceAdded, imageSourceRemoved);
         Assert.AreEqual(1, imageSourceRemovedCount);
      }

      [Test]
      public void RemoveImageSourceNullArgument()
      {
         ImageSourceManager imageSourceManager = new ImageSourceManager(new FileSourceFactory());

         Assert.Throws<ArgumentNullException>(() => imageSourceManager.RemoveImageSource(null));
      }
   }
}
