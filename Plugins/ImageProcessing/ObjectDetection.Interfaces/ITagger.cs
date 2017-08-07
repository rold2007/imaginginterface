// <copyright file="ITagger.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.ObjectDetection
{
   using System.Collections.Generic;
   using System.Drawing;

   public interface ITagger
      {
      IReadOnlyDictionary<string, List<Point>> DataPoints
         {
         get;
         }

      bool AddPoint(string tag, Point newPoint);

      bool RemovePoint(string tag, Point newPoint);

      void SavePoints();

      void LoadPoints(string imagePath);

      void AddLabel(string tag);
      }
   }
