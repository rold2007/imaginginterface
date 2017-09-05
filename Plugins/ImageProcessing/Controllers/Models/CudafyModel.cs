// <copyright file="CudafyModel.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Models
{
   public class CudafyModel
   {
      public string DisplayName
      {
         get;
         set;
      }

      public string GPUName
      {
         get;
         set;
      }

      public int Add
      {
         get;
         set;
      }

      public int[] GridSize
      {
         get;
         set;
      }

      public int[] BlockSize
      {
         get;
         set;
      }

      public object Clone()
      {
         return this.MemberwiseClone();
      }
   }
}
