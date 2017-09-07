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
   using Xunit;

   public class MemorySourceControllerTest : ControllersBaseTest
      {
      [Fact]
      public void Constructor()
         {
         ////IMemorySourceController memorySourceController = this.ServiceLocator.GetInstance<IMemorySourceController>();

         ////memorySourceController.Initialize();
         }

      [Fact]
      public void Active()
         {
         ////IMemorySourceController memorySourceController = this.ServiceLocator.GetInstance<IMemorySourceController>();

         ////Assert.IsFalse(memorySourceController.Active);
         }

      [Fact]
      public void IsDynamic()
         {
         ////IMemorySourceController memorySourceController = this.ServiceLocator.GetInstance<IMemorySourceController>();

         ////Assert.False(memorySourceController.IsDynamic(null));
         ////Assert.False(memorySourceController.IsDynamic(memorySourceController.RawPluginModel));
         }

      [Fact]
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

      [Fact]
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

      [Fact]
      public void Disconnected()
         {
         ////IMemorySourceController memorySourceController = this.ServiceLocator.GetInstance<IMemorySourceController>();

         ////memorySourceController.Disconnected();
         }
      }
   }
