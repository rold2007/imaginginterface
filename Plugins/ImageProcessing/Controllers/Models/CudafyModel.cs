// <copyright file="CudafyModel.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Models
{
   using System.Diagnostics.CodeAnalysis;

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

      [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Too much work for now.")]
      public int[] GridSize
      {
         get;
         set;
      }

      [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Too much work for now.")]
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
