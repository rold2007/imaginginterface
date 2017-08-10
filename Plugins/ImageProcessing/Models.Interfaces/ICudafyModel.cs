// <copyright file="ICudafyModel.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Models
{
   using System;
   using ImagingInterface.Plugins;

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

      int[] GridSize
         {
         get;
         set;
         }

      int[] BlockSize
         {
         get;
         set;
         }
      }
   }
