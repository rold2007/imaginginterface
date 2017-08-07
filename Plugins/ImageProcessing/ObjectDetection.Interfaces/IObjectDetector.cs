// <copyright file="IObjectDetector.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.ObjectDetection
{
   using System;
   using System.Collections.Generic;
   using System.Drawing;

   public interface IObjectDetector : IDisposable
      {
      void Add(string label, Point tagPoint);

      void Remove(string label, Point tagPoint);

      void Train(byte[,,] imageData);

      Dictionary<string, List<Point>> Test(byte[,,] imageData);
      }
   }
