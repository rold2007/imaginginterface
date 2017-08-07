// <copyright file="CudafyGPUChangedEventArgs.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Controllers.EventArguments
{
   using System;

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
