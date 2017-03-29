﻿namespace ImagingInterface.Controllers.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ImagingInterface.Plugins;
    using NUnit.Framework;

    [TestFixture]
    public class ImageSourceManagerTest : ControllersBaseTest
    {
        [Test]
        public void Constructor()
        {
            ImageSourceManager imageSourceManager = this.ServiceLocator.GetInstance<ImageSourceManager>();
        }

        [Test]
        public void AddImageFiles()
        {
            int imageAddedCount = 0;
            ImageSourceManager imageSourceManager = this.ServiceLocator.GetInstance<ImageSourceManager>();

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
            ImageSourceManager imageSourceManager = this.ServiceLocator.GetInstance<ImageSourceManager>();

            Assert.Throws<ArgumentNullException>(() => imageSourceManager.AddImageFiles(null));
        }

        [Test]
        public void RemoveImageSource()
        {
            IImageSource imageSource = null;
            int imageSourceRemovedCount = 0;
            ImageSourceManager imageSourceManager = this.ServiceLocator.GetInstance<ImageSourceManager>();

            imageSourceManager.ImageSourceAdded += (sender, eventArgs) =>
            {
                Assert.IsNull(imageSource);
                imageSource = eventArgs.ImageSource;
            };

            imageSourceManager.ImageSourceRemoved += (sender, eventArgs) => { imageSourceRemovedCount++; };

            List<string> files = new List<string>
            {
                "ValidFile"
            };

            imageSourceManager.AddImageFiles(files);

            Assert.IsNotNull(imageSource);
            Assert.AreEqual(0, imageSourceRemovedCount);

            imageSourceManager.RemoveImageSource(imageSource);

            Assert.AreEqual(1, imageSourceRemovedCount);
        }

        [Test]
        public void RemoveImageSourceNullArgument()
        {
            ImageSourceManager imageSourceManager = this.ServiceLocator.GetInstance<ImageSourceManager>();

            Assert.Throws<ArgumentNullException>(() => imageSourceManager.RemoveImageSource(null));
        }
    }
}
