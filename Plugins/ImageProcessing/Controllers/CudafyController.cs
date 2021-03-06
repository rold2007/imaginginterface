﻿// <copyright file="CudafyController.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Controllers
{
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Diagnostics;
   using System.Diagnostics.CodeAnalysis;
   using System.IO;
   using System.Runtime.InteropServices;
   using Cudafy;
   using Cudafy.Host;
   using Cudafy.Translator;
   using ImageProcessing.Controllers.EventArguments;
   using ImageProcessing.Cudafied;
   using ImageProcessing.Models;
   using ImagingInterface.Plugins;

   public class CudafyController : IImageProcessingService
   {
      private const string CudafyDisplayName = "Cudafy"; // ncrunch: no coverage
      private CudafyModel cudafyModel = new CudafyModel();

      //// The controller shouldn't manage GPGPU directly, or even reference Cudafy. In the same way, it shouldn't implement IDisposable.
      //// IDisposable should not spread from the bottom layers up to the controller/view.
      ////private Dictionary<string, GPGPU> gpgpus;
      ////private Dictionary<string, GPGPUProperties> gpgpuProperties;
      ////private Dictionary<string, eGPUType> gpuTypes;

      public CudafyController()
      {
         this.cudafyModel.DisplayName = CudafyController.CudafyDisplayName;

         ////this.gpgpus = new Dictionary<string, GPGPU>();
         ////this.gpgpuProperties = new Dictionary<string, GPGPUProperties>();
         ////this.gpuTypes = new Dictionary<string, eGPUType>();
      }

      ////~CudafyController()
      ////{ // ncrunch: no coverage
      ////   this.Dispose(false); // ncrunch: no coverage
      ////} // ncrunch: no coverage

      public event CancelEventHandler Closing;

      public event EventHandler Closed;

      ////public IRawPluginView RawPluginView
      ////   {
      ////   get
      ////      {
      ////      return this.cudafyView;
      ////      }
      ////   }

      public string DisplayName
      {
         get
         {
            return this.cudafyModel.DisplayName;
         }
      }

      ////public void Dispose()
      ////{
      ////   this.Dispose(true);
      ////   GC.SuppressFinalize(this);
      ////}

      public void Initialize()
      {
         this.InitializeGPUs();

         ////this.cudafyView.Add += this.CudafyView_Add;
         ////this.cudafyView.GPUChanged += this.CudafyView_GPUChanged;
         ////this.cudafyView.GridSizeChanged += this.CudafyView_GridSizeChanged;
         ////this.cudafyView.BlockSizeXChanged += this.CudafyView_BlockSizeXChanged;
         ////this.cudafyView.BlockSizeYChanged += this.CudafyView_BlockSizeYChanged;

         ////this.cudafyView.SetGPUs(this.gpgpus.Keys.ToList());
      }

      public void Close()
      {
         CancelEventArgs cancelEventArgs = new CancelEventArgs();

         this.Closing?.Invoke(this, cancelEventArgs);

         if (!cancelEventArgs.Cancel)
         {
            ////this.cudafyView.Hide();

            ////this.cudafyView.Close();

            this.Closed?.Invoke(this, EventArgs.Empty);

            ////this.Dispose();
         }
      }

      [SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", Justification = "Too much work for now.")]
      public void ProcessImageData(byte[,,] imageData, byte[] overlayData)
      {
         /*
         CudafyModel cudafyModel = this.cudafyModel;

         if (cudafyModel.Add != 0 && !string.IsNullOrEmpty(cudafyModel.GPUName))
         {
            Stopwatch totalTime = Stopwatch.StartNew();
            int allocatedX = imageData.GetLength(1);
            int allocatedY = imageData.GetLength(0);
            int allocatedZ = imageData.GetLength(2);

            byte[,,] gpuSourceData;
            byte[,,] gpuDestinationData;

            lock (this)
            {
               dim3 gpuDataSize = new dim3(allocatedX, allocatedY, allocatedZ);

               GPGPU gpgpu = this.gpgpus[cudafyModel.GPUName];
               GPGPUProperties gpgpuProperties = this.gpgpuProperties[cudafyModel.GPUName];

               gpuSourceData = gpgpu.Allocate<byte>(gpuDataSize.y, gpuDataSize.x, gpuDataSize.z);
               gpuDestinationData = gpgpu.Allocate<byte>(gpuDataSize.y, gpuDataSize.x, gpuDataSize.z);

               IntPtr hostImageData = gpgpu.HostAllocate<byte>(imageData.Length);
               byte[] tempImageData = new byte[imageData.Length];

               Buffer.BlockCopy(imageData, 0, tempImageData, 0, imageData.Length);
               Marshal.Copy(tempImageData, 0, hostImageData, imageData.Length);

               gpgpu.CopyToDeviceAsync(hostImageData, 0, gpuSourceData, 0, allocatedX * allocatedY * allocatedZ, 0);

               dim3 gridSize = new dim3(cudafyModel.GridSize[0], cudafyModel.GridSize[1], cudafyModel.GridSize[2]);
               dim3 blockSize = new dim3(cudafyModel.BlockSize[0], cudafyModel.BlockSize[1], cudafyModel.BlockSize[2]);

               for (int channel = 0; channel < allocatedZ; channel++)
               {
                  gpgpu.LaunchAsync(gridSize, blockSize, 0, Primitives.Add, gpuSourceData, allocatedX, allocatedY, cudafyModel.Add, channel, gpuDestinationData);
               }

               gpgpu.CopyFromDeviceAsync(gpuDestinationData, 0, hostImageData, 0, allocatedX * allocatedY * allocatedZ);

               Marshal.Copy(hostImageData, tempImageData, 0, imageData.Length);
               Buffer.BlockCopy(tempImageData, 0, imageData, 0, imageData.Length);

               gpgpu.Synchronize();

               gpgpu.Free(gpuSourceData);
               gpgpu.Free(gpuDestinationData);
               gpgpu.HostFree(hostImageData);

               totalTime.Stop();
            }

            Stopwatch openCVBenchmark = Stopwatch.StartNew();

            if (allocatedZ == 1)
            {
               using (Image<Gray, byte> sourceImage = new Image<Gray, byte>(imageData), destinationImage = sourceImage.Add(new Gray(cudafyModel.Add)))
               {
               }
            }
            else
            {
               using (Image<Bgr, byte> sourceImage = new Image<Bgr, byte>(imageData), destinationImage = sourceImage.Add(new Bgr(cudafyModel.Add, cudafyModel.Add, cudafyModel.Add)))
               {
               }
            }

            openCVBenchmark.Stop();

            ////this.cudafyView.SetBenchmarkAddCudafy(totalTime.ElapsedMilliseconds);
            ////this.cudafyView.SetBenchmarkAddOpenCV(openCVBenchmark.ElapsedMilliseconds);
         }
         //*/
      }

      protected virtual void Dispose(bool disposing)
      {
         if (disposing)
         {
            ////this.cudafyView.Add -= this.CudafyView_Add;
            ////this.cudafyView.GPUChanged -= this.CudafyView_GPUChanged;
            ////this.cudafyView.GridSizeChanged -= this.CudafyView_GridSizeChanged;
            ////this.cudafyView.BlockSizeXChanged -= this.CudafyView_BlockSizeXChanged;
            ////this.cudafyView.BlockSizeYChanged -= this.CudafyView_BlockSizeYChanged;

            ////foreach (KeyValuePair<string, GPGPU> gpgpu in this.gpgpus)
            ////{
            ////   GPGPU currentGPGPU = gpgpu.Value;

            ////   currentGPGPU.DisableMultithreading();
            ////   currentGPGPU.Dispose();
            ////}

            ////this.gpgpus.Clear();
         }

         ////Debug.Assert(this.gpgpus.Count == 0, "The GPUs were not disposed of properly.");
      }

      private void CudafyView_Add(object sender, CudafyAddEventArgs e)
      {
         if (this.cudafyModel.Add != e.Add)
         {
            this.cudafyModel.Add = e.Add;

            this.TriggerAddProcessing();
         }
      }

      [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Will be fixed when done refactoring.")]
      private void TriggerAddProcessing()
      {
         ////ImageController imageController = this.imageManagerController.GetActiveImage();

         ////if (imageController != null)
         {
            ////imageController.AddImageProcessingController(this, this.cudafyModel.Clone() as IRawPluginModel);
         }
      }

      private void CudafyView_GPUChanged(object sender, CudafyGPUChangedEventArgs e)
      {
         this.SelectGPU(e.GPUName);
      }

      private void SelectGPU(string gpuName)
      {
         ////GPGPU gpgpu = this.gpgpus[gpuName];
         ////GPGPUProperties gpgpuProperties = gpgpu.GetDeviceProperties(true);
         ////dim3 maxGridSize;
         ////int maxThreadsPerBlock;

         this.cudafyModel.GPUName = gpuName;

         /*
         if (this.gpuTypes[gpuName] == eGPUType.Emulator)
         {
            int numberOfProcessorsToUse = Math.Max(1, Environment.ProcessorCount - 1);

            maxGridSize = gpgpuProperties.MaxGridSize;
            maxThreadsPerBlock = numberOfProcessorsToUse;
         }
         else
         {
            maxGridSize = gpgpuProperties.MaxGridSize;
            maxThreadsPerBlock = gpgpuProperties.MaxThreadsPerBlock;
         }
         //*/

         ////this.cudafyView.MaxGridSizeX = maxGridSize.x;
         ////this.cudafyView.MaxGridSizeY = maxGridSize.y;
         ////this.cudafyView.MaxGridSizeZ = maxGridSize.z;

         ////this.cudafyView.MaxBlockSizeX = maxThreadsPerBlock;
         ////this.cudafyView.MaxBlockSizeY = maxThreadsPerBlock;
         ////this.cudafyView.MaxBlockSizeZ = maxThreadsPerBlock;

         ////this.cudafyModel.GridSize = new int[] { this.cudafyView.GridSizeX, this.cudafyView.GridSizeY, this.cudafyView.GridSizeZ };
         ////this.cudafyModel.BlockSize = new int[] { this.cudafyView.BlockSizeX, this.cudafyView.BlockSizeY, this.cudafyView.BlockSizeZ };

         long totalBlockThreads = this.cudafyModel.BlockSize[0] * this.cudafyModel.BlockSize[1] * this.cudafyModel.BlockSize[2];

         ////if (totalBlockThreads > gpgpuProperties.MaxThreadsPerBlock)
         {
            ////this.cudafyView.BlockSizeY = Convert.ToInt32(Math.Floor((double)gpgpuProperties.MaxThreadsPerBlock / this.cudafyModel.BlockSize[0]));
         }

         this.TriggerAddProcessing();
      }

      private void CudafyView_GridSizeChanged(object sender, CudafyGridSizeChangedEventArgs e)
      {
         this.cudafyModel.GridSize = new int[] { e.X, e.Y, e.Z };

         this.TriggerAddProcessing();
      }

      private void CudafyView_BlockSizeXChanged(object sender, EventArgs e)
      {
         ////if (this.cudafyView.BlockSizeX * this.cudafyModel.BlockSize[1] * this.cudafyModel.BlockSize[2] > this.gpgpuProperties[this.cudafyModel.GPUName].MaxThreadsPerBlock)
         ////   {
         ////   this.cudafyView.BlockSizeY = Convert.ToInt32(Math.Floor((double)this.gpgpuProperties[this.cudafyModel.GPUName].MaxThreadsPerBlock / this.cudafyView.BlockSizeX));
         ////   }

         ////this.cudafyModel.BlockSize = new int[] { this.cudafyView.BlockSizeX, this.cudafyView.BlockSizeY, this.cudafyView.BlockSizeZ };

         this.TriggerAddProcessing();
      }

      private void CudafyView_BlockSizeYChanged(object sender, EventArgs e)
      {
         ////if (this.cudafyView.BlockSizeY * this.cudafyModel.BlockSize[0] * this.cudafyModel.BlockSize[2] > this.gpgpuProperties[this.cudafyModel.GPUName].MaxThreadsPerBlock)
         ////   {
         ////   this.cudafyView.BlockSizeX = Convert.ToInt32(Math.Floor((double)this.gpgpuProperties[this.cudafyModel.GPUName].MaxThreadsPerBlock / this.cudafyView.BlockSizeY));
         ////   }

         ////this.cudafyModel.BlockSize = new int[] { this.cudafyView.BlockSizeX, this.cudafyView.BlockSizeY, this.cudafyView.BlockSizeZ };

         this.TriggerAddProcessing();
      }

      [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Will be fixed when done refactoring.")]
      private void InitializeGPUs()
      {
         eGPUType[] gpuTypes = new eGPUType[] { eGPUType.Cuda, eGPUType.OpenCL, eGPUType.Emulator };
         eLanguage[] languages = new eLanguage[] { eLanguage.Cuda, eLanguage.OpenCL };

         foreach (eGPUType gpuType in gpuTypes)
         {
            try
            {
               int numberOfAvailableDevices = CudafyHost.GetDeviceCount(gpuType);

               for (int deviceNumber = 0; deviceNumber < numberOfAvailableDevices; deviceNumber++)
               {
                  GPGPU gpgpu = CudafyHost.GetDevice(gpuType, deviceNumber);
                  GPGPUProperties gpgpuProperties = gpgpu.GetDeviceProperties(true);
                  CudafyModes.Target = gpuType;

                  foreach (eLanguage language in languages)
                  {
                     string cudaRandomFilename = Path.GetRandomFileName();

                     try
                     {
                        CudafyTranslator.Language = language;

                        CompileProperties compileProperties = CompilerHelper.Create(ePlatform.Auto, eArchitecture.Unknown, eCudafyCompileMode.Default, CudafyTranslator.WorkingDirectory, CudafyTranslator.GenerateDebug);

                        // Use a random filename to prevent conflict on default temp file when multithreading (unit tests)
                        compileProperties.InputFile = cudaRandomFilename;

                        // If this line fails with NCrunch/Unit tests, there probably is a new version of Cudafy.NET
                        // and it needs to be registered in the GAC like this: gacutil -i Cudafy.NET.dll
                        CudafyModule cudafyModule = CudafyTranslator.Cudafy(compileProperties, typeof(Primitives));

                        if (!gpgpu.IsModuleLoaded(cudafyModule.Name))
                        {
                           gpgpu.LoadModule(cudafyModule);
                        }

                        gpgpu.EnableMultithreading();

                        string gpuName = gpgpuProperties.Name.Trim() + " - " + gpuType.ToString() + " - " + language.ToString();

                        ////this.gpgpus.Add(gpuName, gpgpu);
                        ////this.gpgpuProperties.Add(gpuName, gpgpuProperties);
                        ////this.gpuTypes.Add(gpuName, gpuType);
                     }
                     catch (CudafyCompileException)
                     {
                        // Language not supported
                     }
                     finally
                     {
                        File.Delete(cudaRandomFilename);

                        // ncrunch: no coverage start
                     }
                  }
               }
            }
            catch (DllNotFoundException)
            {
            }
            catch (InvalidOperationException)
            {
               // Language not supported
            }
            catch (Cloo.ComputeException)
            {
               // Language not supported
            } // ncrunch: no coverage end
         }
      }
   }
}
