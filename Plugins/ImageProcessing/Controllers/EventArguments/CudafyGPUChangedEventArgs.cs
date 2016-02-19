namespace ImageProcessing.Controllers.EventArguments
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;

   public class CudafyGPUChangedEventArgs : EventArgs
      {
      public CudafyGPUChangedEventArgs(string gpuName)
         {
         this.GPUName = gpuName;
         }

      public string GPUName
         {
         get;
         private set;
         }
      }
   }
