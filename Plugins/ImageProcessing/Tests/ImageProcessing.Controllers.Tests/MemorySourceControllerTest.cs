// <copyright file="MemorySourceControllerTest.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Controllers.Tests
   {
   using System;
   using System.Collections.Generic;
   using System.IO;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImageProcessing.Controllers;
   using ImagingInterface.Plugins;
   using NUnit.Framework;

   [TestFixture]
   public class MemorySourceControllerTest : ControllersBaseTest
      {
      [Test]
      public void Constructor()
         {
         ////IMemorySourceController memorySourceController = this.ServiceLocator.GetInstance<IMemorySourceController>();

         ////memorySourceController.Initialize();
         }

      [Test]
      public void Active()
         {
         ////IMemorySourceController memorySourceController = this.ServiceLocator.GetInstance<IMemorySourceController>();

         ////Assert.IsFalse(memorySourceController.Active);
         }

      [Test]
      public void IsDynamic()
         {
         ////IMemorySourceController memorySourceController = this.ServiceLocator.GetInstance<IMemorySourceController>();

         ////Assert.False(memorySourceController.IsDynamic(null));
         ////Assert.False(memorySourceController.IsDynamic(memorySourceController.RawPluginModel));
         }

      [Test]
      public void Close()
         {
         ////IMemorySourceController memorySourceController = this.ServiceLocator.GetInstance<IMemorySourceController>();

         ////bool closingCalled = false;
         ////bool closedCalled = false;

         ////memorySourceController.Closing += (sender, eventArgs) => { closingCalled = true; };
         ////memorySourceController.Closed += (sender, eventArgs) => { closedCalled = true; };

         ////memorySourceController.Close();

         ////Assert.IsTrue(closingCalled);
         ////Assert.IsTrue(closedCalled);
         }

      [Test]
      public void NextImageData()
         {
         ////IMemorySourceController memorySourceController = this.ServiceLocator.GetInstance<IMemorySourceController>();

         ////Assert.IsNull(memorySourceController.ImageData);

         ////using (UMat image = new UMat(1, 1, DepthType.Cv8U, 1))
         ////using (Image<Gray, byte> imageData = image.ToImage<Gray, byte>())
         ////   {
         ////   memorySourceController.ImageData = imageData.Data;

         ////   byte[, ,] resultImage = memorySourceController.NextImageData(memorySourceController.RawPluginModel);

         ////   Assert.IsNotNull(resultImage);
         ////   }
         }

      [Test]
      public void Disconnected()
         {
         ////IMemorySourceController memorySourceController = this.ServiceLocator.GetInstance<IMemorySourceController>();

         ////memorySourceController.Disconnected();
         }
      }
   }
