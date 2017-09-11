// <copyright file="Tagger.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.ObjectDetection
{
   using System;
   using System.Collections.Generic;
   using System.Collections.ObjectModel;
   using System.Drawing;
   using System.IO;

   public class Tagger
      {
      private string tempFilename;
      private bool dataPointsModified;
      private Dictionary<string, List<Point>> dataPoints;

      public Tagger()
         {
         this.dataPoints = new Dictionary<string, List<Point>>();
         this.SavePath = Path.GetTempPath();
         }

      public IReadOnlyDictionary<string, List<Point>> DataPoints
         {
         get
            {
            return new ReadOnlyDictionary<string, List<Point>>(this.dataPoints);
            }
         }

      private string SavePath
         {
         get;
         set;
         }

      public bool AddPoint(string tag, Point newPoint)
         {
         List<Point> points;

         if (this.dataPoints.TryGetValue(tag, out points))
            {
            if (!points.Contains(newPoint))
               {
               points.Add(newPoint);

               this.dataPointsModified = true;

               return true;
               }
            else
               {
               return false;
               }
            }
         else
            {
            points = new List<Point>();

            this.dataPoints.Add(tag, points);

            points.Add(newPoint);

            this.dataPointsModified = true;

            return true;
            }
         }

      public bool RemovePoint(string tag, Point newPoint)
         {
         List<Point> points;

         if (this.dataPoints.TryGetValue(tag, out points))
            {
            if (points.Contains(newPoint))
               {
               points.Remove(newPoint);

               this.dataPointsModified = true;

               return true;
               }
            }

         return false;
         }

      public void SavePoints()
         {
         if (this.dataPointsModified)
            {
            string directory = Path.GetDirectoryName(this.tempFilename);

            // ncrunch: no coverage start
            if (!Directory.Exists(directory))
               {
               Directory.CreateDirectory(directory);
               }

            //// ncrunch: no coverage end

            using (StreamWriter streamWriter = new StreamWriter(this.tempFilename, false))
               {
               foreach (string tag in this.dataPoints.Keys)
                  {
                  foreach (Point point in this.dataPoints[tag])
                     {
                     streamWriter.WriteLine(string.Format("{0};{1};{2}", tag, point.X, point.Y));
                     }
                  }
               }

            this.dataPointsModified = false;
            }
         }

      public void LoadPoints(string imagePath)
         {
         string filename = Path.GetFileNameWithoutExtension(imagePath);

         this.tempFilename = this.SavePath + @"\Tagger\" + filename + ".imagedata";

         this.dataPoints.Clear();

         if (File.Exists(this.tempFilename))
            {
            using (StreamReader streamReader = new StreamReader(this.tempFilename))
               {
               while (!streamReader.EndOfStream)
                  {
                  string line = streamReader.ReadLine();
                  string[] lineSplits = line.Split(';');
                  string tag = lineSplits[0];
                  Point readPoint = new Point(Convert.ToInt32(lineSplits[1]), Convert.ToInt32(lineSplits[2]));

                  this.AddLabel(tag);

                  this.AddPoint(tag, readPoint);
                  }
               }
            }

         this.dataPointsModified = false;
         }

      public void AddLabel(string label)
         {
         if (!this.dataPoints.ContainsKey(label))
            {
            this.dataPoints.Add(label, new List<Point>());
            }
         }

      public void AddLabels(IEnumerable<string> labels)
      {
         foreach (string label in labels)
         {
            this.AddLabel(label);
         }
      }

      public void RemoveLabel(string label)
      {
         if (this.dataPoints.ContainsKey(label))
         {
            this.dataPoints.Remove(label);
         }
      }

      public void RemoveLabels(IEnumerable<string> labels)
      {
         foreach (string label in labels)
         {
            this.RemoveLabel(label);
         }
      }
   }
   }
