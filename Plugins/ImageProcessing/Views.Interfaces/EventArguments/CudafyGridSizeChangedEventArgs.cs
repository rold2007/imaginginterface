namespace ImageProcessing.Views.EventArguments
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;

   public class CudafyGridSizeChangedEventArgs : EventArgs
      {
      public CudafyGridSizeChangedEventArgs(int x, int y, int z)
         {
         this.X = x;
         this.Y = y;
         this.Z = z;
         }

      public int X
         {
         get;
         private set;
         }

      public int Y
         {
         get;
         private set;
         }

      public int Z
         {
         get;
         private set;
         }
      }
   }
