namespace ImageProcessing.Controllers.Tests
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImageProcessing.Controllers;
   using ImageProcessing.Controllers.Tests.Views;
   using ImageProcessing.Models;
   using ImageProcessing.Views;
   using ImagingInterface.Controllers;
   using ImagingInterface.Plugins;
   using ImagingInterface.Tests.Common;
   using ImagingInterface.Tests.Common.Mocks;
   using ImagingInterface.Tests.Common.Views;
   using ImagingInterface.Views;
   using Microsoft.Practices.ServiceLocation;
   using NUnit.Framework;

   [TestFixture]
   public class CudafyControllerTest : ControllersBaseTest
      {
      [Test]
      public void Constructor()
         {
         try
            {
            ICudafyController cudafyController = this.ServiceLocator.GetInstance<ICudafyController>();
            }
         catch (ActivationException)
            {
            Assert.Fail("For unit tests the Cudafy.NET.dll needs to be registered in the GAC using: gacutil -i Cudafy.NET.dll");
            }
         }

      [Test]
      public void Active()
         {
         try
            {
            ICudafyController cudafyController = this.ServiceLocator.GetInstance<ICudafyController>();

            Assert.IsTrue(cudafyController.Active);
            }
         catch (ActivationException)
            {
            Assert.Fail("For unit tests the Cudafy.NET.dll needs to be registered in the GAC using: gacutil -i Cudafy.NET.dll");
            }
         }

      [Test]
      public void ProcessImageData()
         {
         CudafyController cudafyController = null;

         this.Container.RegisterSingleton<ICudafyView, CudafyView>();
         this.Container.RegisterSingleton<IImageManagerController, ImageManagerController>();

         try
            {
            cudafyController = this.ServiceLocator.GetInstance<ICudafyController>() as CudafyController;
            CudafyView cudafyView = cudafyController.RawPluginView as CudafyView;
            IImageManagerController imageManagerController = this.ServiceLocator.GetInstance<IImageManagerController>();
            ImageController imageController = this.ServiceLocator.GetInstance<ImageController>();
            ImageSourceController imageSourceController = this.Container.GetInstance<IImageSourceController>() as ImageSourceController;
            byte[, ,] imageData = new byte[10, 10, 1];

            cudafyController.Initialize();

            imageManagerController.AddImage(imageController);

            using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
               {
               imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

               imageControllerWrapper.WaitForDisplayUpdate();
               }

            cudafyController.ProcessImageData(imageData, null, cudafyController.RawPluginModel);

            cudafyView.GridSizeX = 2;
            cudafyView.GridSizeY = 2;
            cudafyView.GridSizeZ = 1;
            cudafyView.BlockSizeX = 3;
            cudafyView.BlockSizeY = 3;
            cudafyView.BlockSizeZ = 1;

            using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
               {
               cudafyView.TriggerAdd(1);

               // Make sure there one of the GPU is already selected upon initialization
               cudafyController.ProcessImageData(imageData, null, cudafyController.RawPluginModel);
               
               imageControllerWrapper.WaitForDisplayUpdate();
               }

            using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
               {
               cudafyView.TriggerGPUChanged(cudafyView.GPUS[0]);

               imageControllerWrapper.WaitForDisplayUpdate();
               }

            using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
               {
               cudafyView.TriggerAdd(2);

               imageControllerWrapper.WaitForDisplayUpdate();
               }

            imageData = new byte[1, 1, 3];

            cudafyController.ProcessImageData(imageData, null, cudafyController.RawPluginModel);
            }
         catch (ActivationException)
            {
            Assert.Fail("For unit tests the Cudafy.NET.dll needs to be registered in the GAC using: gacutil -i Cudafy.NET.dll");
            }
         finally
            {
            if (cudafyController != null)
               {
               cudafyController.Dispose();
               }
            }
         }

      [Test]
      public void Close()
         {
         try
            {
            this.Container.RegisterSingleton<ICudafyView, CudafyView>();

            ICudafyController cudafyController = this.ServiceLocator.GetInstance<ICudafyController>();
            CudafyView cudafyView = cudafyController.RawPluginView as CudafyView;

            bool closingCalled = false;
            bool closedCalled = false;

            cudafyController.Closing += (sender, eventArgs) => { closingCalled = true; };
            cudafyController.Closed += (sender, eventArgs) => { closedCalled = true; };

            cudafyController.Close();

            Assert.IsTrue(closingCalled);
            Assert.IsTrue(closedCalled);
            Assert.IsTrue(cudafyView.CloseCalled);
            }
         catch (ActivationException)
            {
            Assert.Fail("For unit tests the Cudafy.NET.dll needs to be registered in the GAC using: gacutil -i Cudafy.NET.dll");
            }
         }

      [Test]
      public void GPUChanged()
         {
         CudafyController cudafyController = null;

         this.Container.RegisterSingleton<ICudafyView, CudafyView>();
         this.Container.RegisterSingleton<ICudafyModel, CudafyModel>();

         try
            {
            cudafyController = this.ServiceLocator.GetInstance<CudafyController>();

            CudafyView cudafyView = cudafyController.RawPluginView as CudafyView;
            ICudafyModel cudafyModel = this.ServiceLocator.GetInstance<ICudafyModel>();
            IImageManagerController imageManagerController = this.ServiceLocator.GetInstance<IImageManagerController>();
            ImageController imageController = this.ServiceLocator.GetInstance<ImageController>();
            ImageSourceController imageSourceController = this.Container.GetInstance<IImageSourceController>() as ImageSourceController;
            byte[, ,] imageData = new byte[1, 1, 1];

            cudafyController.Initialize();

            imageManagerController.AddImage(imageController);

            using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
               {
               imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

               imageControllerWrapper.WaitForDisplayUpdate();
               }

            foreach (string gpu in cudafyView.GPUS)
               {
               using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
                  {
                  cudafyView.TriggerGPUChanged(gpu);

                  imageControllerWrapper.WaitForDisplayUpdate();
                  }
               }

            Assert.AreEqual(1, cudafyView.BlockSizeY);

            using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
               {
               cudafyView.TriggerGPUChanged(cudafyView.GPUS[0]);

               imageControllerWrapper.WaitForDisplayUpdate();
               }

            cudafyView.BlockSizeX = cudafyView.MaxBlockSizeX;
            cudafyView.BlockSizeY = cudafyView.MaxBlockSizeY;
            cudafyView.BlockSizeZ = cudafyView.MaxBlockSizeZ;

            using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
               {
               cudafyView.TriggerGPUChanged(cudafyView.GPUS[0]);

               imageControllerWrapper.WaitForDisplayUpdate();
               }

            Assert.AreEqual(1, cudafyView.BlockSizeY);
            }
         catch (ActivationException)
            {
            Assert.Fail("For unit tests the Cudafy.NET.dll needs to be registered in the GAC using: gacutil -i Cudafy.NET.dll");
            }
         finally
            {
            if (cudafyController != null)
               {
               cudafyController.Dispose();
               }
            }
         }

      [Test]
      public void GridSizeChanged()
         {
         CudafyController cudafyController = null;

         this.Container.RegisterSingleton<ICudafyView, CudafyView>();
         this.Container.RegisterSingleton<ICudafyModel, CudafyModel>();

         try
            {
            cudafyController = this.ServiceLocator.GetInstance<CudafyController>();

            CudafyView cudafyView = cudafyController.RawPluginView as CudafyView;
            ICudafyModel cudafyModel = this.ServiceLocator.GetInstance<ICudafyModel>();
            IImageManagerController imageManagerController = this.ServiceLocator.GetInstance<IImageManagerController>();
            ImageController imageController = this.ServiceLocator.GetInstance<ImageController>();
            ImageSourceController imageSourceController = this.Container.GetInstance<IImageSourceController>() as ImageSourceController;
            byte[, ,] imageData = new byte[1, 1, 1];

            cudafyController.Initialize();

            imageManagerController.AddImage(imageController);

            using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
               {
               imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

               imageControllerWrapper.WaitForDisplayUpdate();
               }

            using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
               {
               cudafyView.TriggerGridSizeChanged(3, 4, 5);

               imageControllerWrapper.WaitForDisplayUpdate();
               }

            Assert.AreEqual(3, cudafyModel.GridSize[0]);
            Assert.AreEqual(4, cudafyModel.GridSize[1]);
            Assert.AreEqual(5, cudafyModel.GridSize[2]);
            }
         catch (ActivationException)
            {
            Assert.Fail("For unit tests the Cudafy.NET.dll needs to be registered in the GAC using: gacutil -i Cudafy.NET.dll");
            }
         finally
            {
            if (cudafyController != null)
               {
               cudafyController.Dispose();
               }
            }
         }

      [Test]
      public void BlockSizeXChanged()
         {
         CudafyController cudafyController = null;

         this.Container.RegisterSingleton<ICudafyView, CudafyView>();
         this.Container.RegisterSingleton<ICudafyModel, CudafyModel>();

         try
            {
            cudafyController = this.ServiceLocator.GetInstance<CudafyController>();

            CudafyView cudafyView = cudafyController.RawPluginView as CudafyView;
            ICudafyModel cudafyModel = this.ServiceLocator.GetInstance<ICudafyModel>();
            IImageManagerController imageManagerController = this.ServiceLocator.GetInstance<IImageManagerController>();
            ImageController imageController = this.ServiceLocator.GetInstance<ImageController>();
            ImageSourceController imageSourceController = this.Container.GetInstance<IImageSourceController>() as ImageSourceController;
            byte[, ,] imageData = new byte[1, 1, 1];

            cudafyController.Initialize();

            imageManagerController.AddImage(imageController);

            using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
               {
               imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

               imageControllerWrapper.WaitForDisplayUpdate();
               }

            using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
               {
               cudafyView.TriggerGPUChanged(cudafyView.GPUS[0]);

               imageControllerWrapper.WaitForDisplayUpdate();
               }

            using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
               {
               cudafyView.BlockSizeX = 3;
               cudafyView.TriggerBlockSizeXChanged();

               imageControllerWrapper.WaitForDisplayUpdate();
               }

            Assert.AreEqual(3, cudafyModel.BlockSize[0]);

            using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
               {
               cudafyView.BlockSizeY = cudafyView.MaxBlockSizeY;

               cudafyView.TriggerBlockSizeYChanged();

               imageControllerWrapper.WaitForDisplayUpdate();
               }

            using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
               {
               cudafyView.BlockSizeX = cudafyView.MaxBlockSizeX;

               cudafyView.TriggerBlockSizeXChanged();

               imageControllerWrapper.WaitForDisplayUpdate();
               }

            Assert.AreEqual(cudafyView.MaxBlockSizeX, cudafyModel.BlockSize[0]);
            Assert.AreEqual(1, cudafyModel.BlockSize[1]);
            }
         catch (ActivationException)
            {
            Assert.Fail("For unit tests the Cudafy.NET.dll needs to be registered in the GAC using: gacutil -i Cudafy.NET.dll");
            }
         finally
            {
            if (cudafyController != null)
               {
               cudafyController.Dispose();
               }
            }
         }

      [Test]
      public void BlockSizeYChanged()
         {
         CudafyController cudafyController = null;

         this.Container.RegisterSingleton<ICudafyView, CudafyView>();
         this.Container.RegisterSingleton<ICudafyModel, CudafyModel>();

         try
            {
            cudafyController = this.ServiceLocator.GetInstance<CudafyController>();

            CudafyView cudafyView = cudafyController.RawPluginView as CudafyView;
            ICudafyModel cudafyModel = this.ServiceLocator.GetInstance<ICudafyModel>();
            IImageManagerController imageManagerController = this.ServiceLocator.GetInstance<IImageManagerController>();
            ImageController imageController = this.ServiceLocator.GetInstance<ImageController>();
            ImageSourceController imageSourceController = this.Container.GetInstance<IImageSourceController>() as ImageSourceController;
            byte[, ,] imageData = new byte[1, 1, 1];

            cudafyController.Initialize();

            imageManagerController.AddImage(imageController);

            using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
               {
               imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

               imageControllerWrapper.WaitForDisplayUpdate();
               }

            using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
               {
               cudafyView.TriggerGPUChanged(cudafyView.GPUS[0]);

               imageControllerWrapper.WaitForDisplayUpdate();
               }

            using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
               {
               cudafyView.BlockSizeY = 4;
               cudafyView.TriggerBlockSizeYChanged();

               imageControllerWrapper.WaitForDisplayUpdate();
               }

            Assert.AreEqual(4, cudafyModel.BlockSize[1]);

            using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
               {
               cudafyView.BlockSizeX = cudafyView.MaxBlockSizeX;

               cudafyView.TriggerBlockSizeXChanged();

               imageControllerWrapper.WaitForDisplayUpdate();
               }

            using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
               {
               cudafyView.BlockSizeY = cudafyView.MaxBlockSizeY;

               cudafyView.TriggerBlockSizeYChanged();

               imageControllerWrapper.WaitForDisplayUpdate();
               }

            Assert.AreEqual(1, cudafyModel.BlockSize[0]);
            Assert.AreEqual(cudafyView.MaxBlockSizeY, cudafyModel.BlockSize[1]);
            }
         catch (ActivationException)
            {
            Assert.Fail("For unit tests the Cudafy.NET.dll needs to be registered in the GAC using: gacutil -i Cudafy.NET.dll");
            }
         finally
            {
            if (cudafyController != null)
               {
               cudafyController.Dispose();
               }
            }
         }
      }
   }
