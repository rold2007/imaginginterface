namespace ImageProcessing.Views.EventArguments
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;

   public class CudafyAddEventArgs : EventArgs
      {
      public CudafyAddEventArgs(int add, int[] gridSize, int[] blockSize)
         {
         this.Add = add;
         this.GridSize = gridSize;
         this.BlockSize = blockSize;
         }

      public int Add
         {
         get;
         private set;
         }

      public int[] GridSize
         {
         get;
         private set;
         }

      public int[] BlockSize
         {
         get;
         private set;
         }
      }
   }
