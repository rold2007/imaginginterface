// <copyright file="ArrayUtils.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Plugins.Utilities
{
   using System.Diagnostics.CodeAnalysis;
   using Shouldly;

   public static class ArrayUtils
   {
      [SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", Justification = "Too much work for now.")]
      public static void ArrayCopy(byte[,,] source, byte[,,] destination)
      {
         source.ShouldNotBeNull();
         destination.ShouldNotBeNull();
         destination.GetLength(0).ShouldBe(source.GetLength(0));
         destination.GetLength(1).ShouldBeLessThanOrEqualTo(source.GetLength(1));
         destination.GetLength(2).ShouldBe(source.GetLength(2));

         int destinationBlocks = destination.GetLength(0);
         int sourceBlocksSize = source.GetLength(1) * source.GetLength(2);
         int destinationBlocksSize = destination.GetLength(1) * destination.GetLength(2);

         for (int y = 0; y < destinationBlocks; y++)
         {
            System.Array.Copy(source, y * sourceBlocksSize, destination, y * destinationBlocksSize, destinationBlocksSize);
         }
      }
   }
}
