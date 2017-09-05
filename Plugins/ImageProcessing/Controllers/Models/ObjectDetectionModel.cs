// <copyright file="ObjectDetectionModel.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Models
{
   public class ObjectDetectionModel
      {
      public string DisplayName
         {
         get; // ncrunch: no coverage
         set; // ncrunch: no coverage
         }

      public object Clone()
         {
         return this.MemberwiseClone();
         }
      }
   }
