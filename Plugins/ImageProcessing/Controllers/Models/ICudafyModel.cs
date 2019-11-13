// <copyright file="ICudafyModel.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Models
{
   using System;
   using System.Diagnostics.CodeAnalysis;

   public interface ICudafyModel : ICloneable
   {
      string GPUName
      {
         get;
         set;
      }

      int Add
      {
         get;
         set;
      }

      [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Too much work for now.")]
      int[] GridSize
      {
         get;
         set;
      }

      [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Too much work for now.")]
      int[] BlockSize
      {
         get;
         set;
      }
   }
}
