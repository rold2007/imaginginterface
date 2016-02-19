namespace ImageProcessing.Controllers.EventArguments
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;

   public class CudafyAddEventArgs : EventArgs
      {
      public CudafyAddEventArgs(int add)
         {
         this.Add = add;
         }

      public int Add
         {
         get;
         private set;
         }
      }
   }
