// <copyright file="CudafyAddEventArgs.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Controllers.EventArguments
{
   using System;

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
