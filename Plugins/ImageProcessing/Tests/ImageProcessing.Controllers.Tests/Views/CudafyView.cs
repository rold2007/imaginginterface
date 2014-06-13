namespace ImageProcessing.Controllers.Tests.Views
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImageProcessing.Views;
   using ImageProcessing.Views.EventArguments;

   public class CudafyView : ICudafyView
      {
      public CudafyView()
         {
         this.GridSizeX = 1;
         this.GridSizeY = 1;
         this.GridSizeZ = 1;
         this.BlockSizeX = 1;
         this.BlockSizeY = 1;
         this.BlockSizeZ = 1;
         }

      public event EventHandler<CudafyAddEventArgs> Add;

      public event EventHandler<CudafyGPUChangedEventArgs> GPUChanged;

      public event EventHandler<CudafyGridSizeChangedEventArgs> GridSizeChanged;

      public event EventHandler BlockSizeXChanged;

      public event EventHandler BlockSizeYChanged;

      public bool CloseCalled
         {
         get;
         private set;
         }

      public int GridSizeX
         {
         get;
         set;
         }

      public int GridSizeY
         {
         get;
         set;
         }

      public int GridSizeZ
         {
         get;
         set;
         }

      public int MaxGridSizeX
         {
         get;
         set;
         }

      public int MaxGridSizeY
         {
         get;
         set;
         }

      public int MaxGridSizeZ
         {
         get;
         set;
         }

      public int BlockSizeX
         {
         get;
         set;
         }

      public int BlockSizeY
         {
         get;
         set;
         }

      public int BlockSizeZ
         {
         get;
         set;
         }

      public int MaxBlockSizeX
         {
         get;
         set;
         }

      public int MaxBlockSizeY
         {
         get;
         set;
         }

      public int MaxBlockSizeZ
         {
         get;
         set;
         }

      public List<string> GPUS
         {
         get;
         set;
         }

      public void SetGPUs(List<string> gpus)
         {
         this.GPUS = gpus;
         }

      public void SetBenchmarkAddCudafy(long benchmark)
         {
         }

      public void SetBenchmarkAddOpenCV(long benchmark)
         {
         }

      public void Hide()
         {
         }

      public void Close()
         {
         this.CloseCalled = true;
         }

      public void TriggerAdd()
         {
         this.Add(this, new CudafyAddEventArgs(1, new int[3] { this.GridSizeX, this.GridSizeY, this.GridSizeZ }, new int[3] { this.BlockSizeX, this.BlockSizeY, this.BlockSizeZ }));
         }

      public void TriggerGPUChanged(string gpuName)
         {
         this.GPUChanged(this, new CudafyGPUChangedEventArgs(gpuName));
         }

      public void TriggerGridSizeChanged(int x, int y, int z)
         {
         this.GridSizeChanged(this, new CudafyGridSizeChangedEventArgs(x, y, z));
         }

      public void TriggerBlockSizeXChanged()
         {
         this.BlockSizeXChanged(this, EventArgs.Empty);
         }

      public void TriggerBlockSizeYChanged()
         {
         this.BlockSizeYChanged(this, EventArgs.Empty);
         }
      }
   }
