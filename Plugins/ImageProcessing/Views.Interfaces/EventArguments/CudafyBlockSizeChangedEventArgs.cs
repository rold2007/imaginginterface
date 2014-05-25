namespace ImageProcessing.Views.EventArguments
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;

   public class CudafyBlockSizeChangedEventArgs : EventArgs
      {
      public CudafyBlockSizeChangedEventArgs(int blockSize)
         {
         this.BlockSize = blockSize;
         }

      public int BlockSize
         {
         get;
         private set;
         }
      }
   }
