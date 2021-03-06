﻿// <copyright file="CudafyControllerTest.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Controllers.Tests
{
   using Xunit;

   public class CudafyControllerTest : ControllersBaseTest
      {
      [Fact]
      public void Constructor()
         {
         ////try
         ////   {
         ////   CudafyController cudafyController = this.ServiceLocator.GetInstance<CudafyController>();
         ////   }
         ////catch (ActivationException)
         ////   {
         ////   Assert.Fail("For unit tests the Cudafy.NET.dll needs to be registered in the GAC using: gacutil -i Cudafy.NET.dll");
         ////   }
         }

      [Fact]
      public void DisplayName()
         {
         ////this.Container.RegisterSingleton<ICudafyModel, CudafyModel>();

         ////CudafyController cudafyController = this.ServiceLocator.GetInstance<CudafyController>();
         ////ICudafyModel cudafyModel = this.ServiceLocator.GetInstance<ICudafyModel>();

         ////Assert.AreEqual("Cudafy", cudafyModel.DisplayName);
         }

      [Fact]
      public void Active()
         {
         ////try
         ////   {
         ////   CudafyController cudafyController = this.ServiceLocator.GetInstance<CudafyController>();

         ////   Assert.IsTrue(cudafyController.Active);
         ////   }
         ////catch (ActivationException)
         ////   {
         ////   Assert.Fail("For unit tests the Cudafy.NET.dll needs to be registered in the GAC using: gacutil -i Cudafy.NET.dll");
         ////   }
         }

      [Fact]
      public void ProcessImageData()
         {
         ////CudafyController cudafyController = null;

         ////this.Container.RegisterSingleton<ICudafyView, CudafyView>();
         ////this.Container.RegisterSingleton<ImageManagerController, ImageManagerController>();

         ////try
         ////   {
         ////   cudafyController = this.ServiceLocator.GetInstance<CudafyController>() as CudafyController;
         ////   ////CudafyView cudafyView = cudafyController.RawPluginView as CudafyView;
         ////   ImageManagerController imageManagerController = this.ServiceLocator.GetInstance<ImageManagerController>();
         ////   ImageController imageController = this.ServiceLocator.GetInstance<ImageController>();
         ////   ////ImageSourceController imageSourceController = this.Container.GetInstance<IImageSourceController>() as ImageSourceController;
         ////   byte[,,] imageData = new byte[10, 10, 1];

         ////   cudafyController.Initialize();

         ////   ////imageManagerController.AddImage(imageController);

         ////   ////using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
         ////   ////   {
         ////   ////   imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

         ////   ////   imageControllerWrapper.WaitForDisplayUpdate();
         ////   ////   }

         ////   cudafyController.ProcessImageData(imageData, null, cudafyController.RawPluginModel);

         ////   ////cudafyView.GridSizeX = 2;
         ////   ////cudafyView.GridSizeY = 2;
         ////   ////cudafyView.GridSizeZ = 1;
         ////   ////cudafyView.BlockSizeX = 3;
         ////   ////cudafyView.BlockSizeY = 3;
         ////   ////cudafyView.BlockSizeZ = 1;

         ////   using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
         ////      {
         ////      ////cudafyView.TriggerAdd(1);

         ////      // Make sure there one of the GPU is already selected upon initialization
         ////      cudafyController.ProcessImageData(imageData, null, cudafyController.RawPluginModel);

         ////      imageControllerWrapper.WaitForDisplayUpdate();
         ////      }

         ////   using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
         ////      {
         ////      ////cudafyView.TriggerGPUChanged(cudafyView.GPUS[0]);

         ////      imageControllerWrapper.WaitForDisplayUpdate();
         ////      }

         ////   using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
         ////      {
         ////      ////cudafyView.TriggerAdd(2);

         ////      imageControllerWrapper.WaitForDisplayUpdate();
         ////      }

         ////   imageData = new byte[1, 1, 3];

         ////   cudafyController.ProcessImageData(imageData, null, cudafyController.RawPluginModel);
         ////   }
         ////catch (ActivationException)
         ////   {
         ////   Assert.Fail("For unit tests the Cudafy.NET.dll needs to be registered in the GAC using: gacutil -i Cudafy.NET.dll");
         ////   }
         ////finally
         ////   {
         ////   if (cudafyController != null)
         ////      {
         ////      cudafyController.Dispose();
         ////      }
         ////   }
         }

      [Fact]
      public void Close()
         {
         ////try
         ////   {
         ////   ////this.Container.RegisterSingleton<ICudafyView, CudafyView>();

         ////   CudafyController cudafyController = this.ServiceLocator.GetInstance<CudafyController>();
         ////   ////CudafyView cudafyView = cudafyController.RawPluginView as CudafyView;

         ////   bool closingCalled = false;
         ////   bool closedCalled = false;

         ////   cudafyController.Closing += (sender, eventArgs) => { closingCalled = true; };
         ////   cudafyController.Closed += (sender, eventArgs) => { closedCalled = true; };

         ////   cudafyController.Close();

         ////   Assert.IsTrue(closingCalled);
         ////   Assert.IsTrue(closedCalled);
         ////   ////Assert.IsTrue(cudafyView.CloseCalled);
         ////   }
         ////catch (ActivationException)
         ////   {
         ////   Assert.Fail("For unit tests the Cudafy.NET.dll needs to be registered in the GAC using: gacutil -i Cudafy.NET.dll");
         ////   }
         }

      [Fact]
      public void GPUChanged()
         {
         ////CudafyController cudafyController = null;

         ////////this.Container.RegisterSingleton<ICudafyView, CudafyView>();
         ////this.Container.RegisterSingleton<ICudafyModel, CudafyModel>();

         ////try
         ////   {
         ////   cudafyController = this.ServiceLocator.GetInstance<CudafyController>();

         ////   ////CudafyView cudafyView = cudafyController.RawPluginView as CudafyView;
         ////   ICudafyModel cudafyModel = this.ServiceLocator.GetInstance<ICudafyModel>();
         ////   ImageManagerController imageManagerController = this.ServiceLocator.GetInstance<ImageManagerController>();
         ////   ImageController imageController = this.ServiceLocator.GetInstance<ImageController>();
         ////   ////ImageSourceController imageSourceController = this.Container.GetInstance<IImageSourceController>() as ImageSourceController;
         ////   byte[,,] imageData = new byte[1, 1, 1];

         ////   ////cudafyController.Initialize();

         ////   ////imageManagerController.AddImage(imageController);

         ////   ////using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
         ////   ////   {
         ////   ////   imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

         ////   ////   imageControllerWrapper.WaitForDisplayUpdate();
         ////   ////   }

         ////   ////foreach (string gpu in cudafyView.GPUS)
         ////   ////   {
         ////   ////   using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
         ////   ////      {
         ////   ////      cudafyView.TriggerGPUChanged(gpu);

         ////   ////      imageControllerWrapper.WaitForDisplayUpdate();
         ////   ////      }
         ////   ////   }

         ////   ////Assert.AreEqual(1, cudafyView.BlockSizeY);

         ////   using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
         ////      {
         ////      ////cudafyView.TriggerGPUChanged(cudafyView.GPUS[0]);

         ////      ////imageControllerWrapper.WaitForDisplayUpdate();
         ////      }

         ////   ////cudafyView.BlockSizeX = cudafyView.MaxBlockSizeX;
         ////   ////cudafyView.BlockSizeY = cudafyView.MaxBlockSizeY;
         ////   ////cudafyView.BlockSizeZ = cudafyView.MaxBlockSizeZ;

         ////   ////using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
         ////   ////   {
         ////   ////   cudafyView.TriggerGPUChanged(cudafyView.GPUS[0]);

         ////   ////   imageControllerWrapper.WaitForDisplayUpdate();
         ////   ////   }

            ////Assert.AreEqual(1, cudafyView.BlockSizeY);
         ////   }
         ////catch (ActivationException)
         ////   {
         ////   Assert.Fail("For unit tests the Cudafy.NET.dll needs to be registered in the GAC using: gacutil -i Cudafy.NET.dll");
         ////   }
         ////finally
         ////   {
         ////   if (cudafyController != null)
         ////      {
         ////      cudafyController.Dispose();
         ////      }
         ////   }
         }

      [Fact]
      public void GridSizeChanged()
         {
         ////CudafyController cudafyController = null;

         ////////this.Container.RegisterSingleton<ICudafyView, CudafyView>();
         ////this.Container.RegisterSingleton<ICudafyModel, CudafyModel>();

         ////try
         ////   {
         ////   cudafyController = this.ServiceLocator.GetInstance<CudafyController>();

         ////   ////CudafyView cudafyView = cudafyController.RawPluginView as CudafyView;
         ////   ICudafyModel cudafyModel = this.ServiceLocator.GetInstance<ICudafyModel>();
         ////   ImageManagerController imageManagerController = this.ServiceLocator.GetInstance<ImageManagerController>();
         ////   ImageController imageController = this.ServiceLocator.GetInstance<ImageController>();
         ////   ////ImageSourceController imageSourceController = this.Container.GetInstance<IImageSourceController>() as ImageSourceController;
         ////   byte[,,] imageData = new byte[1, 1, 1];

         ////   ////cudafyController.Initialize();

         ////   ////imageManagerController.AddImage(imageController);

         ////   ////using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
         ////   ////   {
         ////   ////   imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

         ////   ////   imageControllerWrapper.WaitForDisplayUpdate();
         ////   ////   }

         ////   using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
         ////      {
         ////      ////cudafyView.TriggerGridSizeChanged(3, 4, 5);

         ////      ////imageControllerWrapper.WaitForDisplayUpdate();
         ////      }

         ////   Assert.AreEqual(3, cudafyModel.GridSize[0]);
         ////   Assert.AreEqual(4, cudafyModel.GridSize[1]);
         ////   Assert.AreEqual(5, cudafyModel.GridSize[2]);
         ////   }
         ////catch (ActivationException)
         ////   {
         ////   Assert.Fail("For unit tests the Cudafy.NET.dll needs to be registered in the GAC using: gacutil -i Cudafy.NET.dll");
         ////   }
         ////finally
         ////   {
         ////   if (cudafyController != null)
         ////      {
         ////      cudafyController.Dispose();
         ////      }
         ////   }
         }

      [Fact]
      public void BlockSizeXChanged()
         {
         ////CudafyController cudafyController = null;

         ////////this.Container.RegisterSingleton<ICudafyView, CudafyView>();
         ////this.Container.RegisterSingleton<ICudafyModel, CudafyModel>();

         ////try
         ////   {
         ////   cudafyController = this.ServiceLocator.GetInstance<CudafyController>();

         ////   ////CudafyView cudafyView = cudafyController.RawPluginView as CudafyView;
         ////   ICudafyModel cudafyModel = this.ServiceLocator.GetInstance<ICudafyModel>();
         ////   ImageManagerController imageManagerController = this.ServiceLocator.GetInstance<ImageManagerController>();
         ////   ImageController imageController = this.ServiceLocator.GetInstance<ImageController>();
         ////   ////ImageSourceController imageSourceController = this.Container.GetInstance<IImageSourceController>() as ImageSourceController;
         ////   byte[,,] imageData = new byte[1, 1, 1];

         ////   ////cudafyController.Initialize();

         ////   ////imageManagerController.AddImage(imageController);

         ////   ////using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
         ////   ////   {
         ////   ////   imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

         ////   ////   imageControllerWrapper.WaitForDisplayUpdate();
         ////   ////   }

         ////   using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
         ////      {
         ////      ////cudafyView.TriggerGPUChanged(cudafyView.GPUS[0]);

         ////      ////imageControllerWrapper.WaitForDisplayUpdate();
         ////      }

         ////   using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
         ////      {
         ////      ////cudafyView.BlockSizeX = 3;
         ////      ////cudafyView.TriggerBlockSizeXChanged();

         ////      ////imageControllerWrapper.WaitForDisplayUpdate();
         ////      }

         ////   Assert.AreEqual(3, cudafyModel.BlockSize[0]);

         ////   using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
         ////      {
         ////      ////cudafyView.BlockSizeY = cudafyView.MaxBlockSizeY;

         ////      ////cudafyView.TriggerBlockSizeYChanged();

         ////      ////imageControllerWrapper.WaitForDisplayUpdate();
         ////      }

         ////   using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
         ////      {
         ////      ////cudafyView.BlockSizeX = cudafyView.MaxBlockSizeX;

         ////      ////cudafyView.TriggerBlockSizeXChanged();

         ////      ////imageControllerWrapper.WaitForDisplayUpdate();
         ////      }

         ////   ////Assert.AreEqual(cudafyView.MaxBlockSizeX, cudafyModel.BlockSize[0]);
         ////   Assert.AreEqual(1, cudafyModel.BlockSize[1]);
         ////   }
         ////catch (ActivationException)
         ////   {
         ////   Assert.Fail("For unit tests the Cudafy.NET.dll needs to be registered in the GAC using: gacutil -i Cudafy.NET.dll");
         ////   }
         ////finally
         ////   {
         ////   if (cudafyController != null)
         ////      {
         ////      cudafyController.Dispose();
         ////      }
         ////   }
         }

      [Fact]
      public void BlockSizeYChanged()
         {
         ////CudafyController cudafyController = null;

         ////////this.Container.RegisterSingleton<ICudafyView, CudafyView>();
         ////this.Container.RegisterSingleton<ICudafyModel, CudafyModel>();

         ////try
         ////   {
         ////   cudafyController = this.ServiceLocator.GetInstance<CudafyController>();

         ////   ////CudafyView cudafyView = cudafyController.RawPluginView as CudafyView;
         ////   ICudafyModel cudafyModel = this.ServiceLocator.GetInstance<ICudafyModel>();
         ////   ImageManagerController imageManagerController = this.ServiceLocator.GetInstance<ImageManagerController>();
         ////   ImageController imageController = this.ServiceLocator.GetInstance<ImageController>();
         ////   ////ImageSourceController imageSourceController = this.Container.GetInstance<IImageSourceController>() as ImageSourceController;
         ////   byte[,,] imageData = new byte[1, 1, 1];

         ////   ////cudafyController.Initialize();

         ////   ////imageManagerController.AddImage(imageController);

         ////   ////using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
         ////   ////   {
         ////   ////   imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

         ////   ////   imageControllerWrapper.WaitForDisplayUpdate();
         ////   ////   }

         ////   using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
         ////      {
         ////      ////cudafyView.TriggerGPUChanged(cudafyView.GPUS[0]);

         ////      ////imageControllerWrapper.WaitForDisplayUpdate();
         ////      }

         ////   using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
         ////      {
         ////      ////cudafyView.BlockSizeY = 4;
         ////      ////cudafyView.TriggerBlockSizeYChanged();

         ////      ////imageControllerWrapper.WaitForDisplayUpdate();
         ////      }

         ////   Assert.AreEqual(4, cudafyModel.BlockSize[1]);

         ////   using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
         ////      {
         ////      ////cudafyView.BlockSizeX = cudafyView.MaxBlockSizeX;

         ////      ////cudafyView.TriggerBlockSizeXChanged();

         ////      ////imageControllerWrapper.WaitForDisplayUpdate();
         ////      }

         ////   using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
         ////      {
         ////      ////cudafyView.BlockSizeY = cudafyView.MaxBlockSizeY;

         ////      ////cudafyView.TriggerBlockSizeYChanged();

         ////      ////imageControllerWrapper.WaitForDisplayUpdate();
         ////      }

         ////   Assert.AreEqual(1, cudafyModel.BlockSize[0]);
         ////   ////Assert.AreEqual(cudafyView.MaxBlockSizeY, cudafyModel.BlockSize[1]);
         ////   }
         ////catch (ActivationException)
         ////   {
         ////   Assert.Fail("For unit tests the Cudafy.NET.dll needs to be registered in the GAC using: gacutil -i Cudafy.NET.dll");
         ////   }
         ////finally
         ////   {
         ////   if (cudafyController != null)
         ////      {
         ////      cudafyController.Dispose();
         ////      }
         ////   }
         }
      }
   }
