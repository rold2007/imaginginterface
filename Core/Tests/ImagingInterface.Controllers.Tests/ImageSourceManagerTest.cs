// <copyright file="ImageSourceManagerTest.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.Tests
{
   using System;
   using System.Collections.Generic;
   using ImagingInterface.Controllers.Services;
   using ImagingInterface.Controllers.Tests.Mocks;
   using ImagingInterface.Plugins;
   using NUnit.Framework;

   [TestFixture]
   public class ImageSourceManagerTest : ControllersBaseTest
   {
      [Test]
      public void Constructor()
      {
         ImageSourceService imageSourceManager = new ImageSourceService(new FileSourceFactory());
      }

      [Test]
      public void AddImageFiles()
      {
         ImageSourceService imageSourceManager = new ImageSourceService(new FileSourceFactory());

         List<string> files = new List<string>();

         imageSourceManager.AddImageFiles(files);

         files.Add("ValidFile");

         imageSourceManager.AddImageFiles(files);

         // Invalid file
         files.Add("dummy");

         imageSourceManager.AddImageFiles(files);
      }

      [Test]
      public void AddImageFilesNullArgument()
      {
         ImageSourceService imageSourceManager = new ImageSourceService(new FileSourceFactory());

         Assert.Throws<ArgumentNullException>(() => imageSourceManager.AddImageFiles(null));
      }

      [Test]
      public void RemoveImageSource()
      {
         ImageSourceService imageSourceManager = new ImageSourceService(new FileSourceFactory());

         List<string> files = new List<string>
            {
                "ValidFile"
            };

         IEnumerable<IImageSource> imageSources = imageSourceManager.AddImageFiles(files);

         foreach (IImageSource imageSource in imageSources)
         {
            imageSourceManager.RemoveImageSource(imageSource);
         }
      }

      [Test]
      public void RemoveImageSourceNullArgument()
      {
         ImageSourceService imageSourceManager = new ImageSourceService(new FileSourceFactory());

         Assert.Throws<ArgumentNullException>(() => imageSourceManager.RemoveImageSource(null));
      }
   }
}
