namespace ImageProcessing.Views
   {
   using System;
   using System.Collections.Generic;
   using ImageProcessing.Views.EventArguments;
   using ImagingInterface.Plugins;

   public interface ICudafyView : IPluginView
      {
      event EventHandler<CudafyAddEventArgs> Add;

      event EventHandler<CudafyGPUChangedEventArgs> GPUChanged;

      event EventHandler<CudafyGridSizeChangedEventArgs> GridSizeChanged;

      event EventHandler<CudafyBlockSizeChangedEventArgs> BlockSizeXChanged;

      event EventHandler<CudafyBlockSizeChangedEventArgs> BlockSizeYChanged;

      event EventHandler<CudafyBlockSizeChangedEventArgs> BlockSizeZChanged;

      int GridSizeX
         {
         get;
         set;
         }

      int GridSizeY
         {
         get;
         set;
         }

      int GridSizeZ
         {
         get;
         set;
         }

      int MaxGridSizeX
         {
         get;
         set;
         }

      int MaxGridSizeY
         {
         get;
         set;
         }

      int MaxGridSizeZ
         {
         get;
         set;
         }

      int BlockSizeX
         {
         get;
         set;
         }

      int BlockSizeY
         {
         get;
         set;
         }

      int BlockSizeZ
         {
         get;
         set;
         }

      int MaxBlockSizeX
         {
         get;
         set;
         }

      int MaxBlockSizeY
         {
         get;
         set;
         }

      int MaxBlockSizeZ
         {
         get;
         set;
         }

      void SetGPUs(List<string> gpus);

      void SetBenchmarkAddCudafy(long benchmark);

      void SetBenchmarkAddOpenCV(long benchmark);
      }
   }
