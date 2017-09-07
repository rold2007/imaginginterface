﻿// <copyright file="IRotateModel.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Models
{
   using System;

   public interface IRotateModel : ICloneable
      {
      double Angle
         {
         get;
         set;
         }
      }
   }