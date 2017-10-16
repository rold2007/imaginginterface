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
   using CsvHelper;

   public class Tagger
   {
      ////private string tempFilename;
      ////private bool dataPointsModified;
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

      public bool AddPoint(string label, Point newPoint)
      {
         List<Point> points;

         if (this.dataPoints.TryGetValue(label, out points))
         {
            if (!points.Contains(newPoint))
            {
               points.Add(newPoint);

               ////this.dataPointsModified = true;

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

            this.dataPoints.Add(label, points);

            points.Add(newPoint);

            ////this.dataPointsModified = true;

            return true;
         }
      }

      public bool RemovePoint(string label, Point point)
      {
         List<Point> points;

         if (this.dataPoints.TryGetValue(label, out points))
         {
            if (points.Contains(point))
            {
               points.Remove(point);

               ////this.dataPointsModified = true;

               return true;
            }
         }

         return false;
      }

      public void RemoveAllPoints()
      {
         foreach (List<Point> points in this.dataPoints.Values)
         {
            points.Clear();
         }
      }

      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "Too complicated to solve.")]
      public string SavePoints()
      {
         using (MemoryStream stream = new MemoryStream())
         using (StreamReader reader = new StreamReader(stream))
         using (StreamWriter writer = new StreamWriter(stream))
         using (CsvWriter csv = new CsvWriter(writer))
         {
            foreach (string label in this.dataPoints.Keys)
            {
               foreach (Point point in this.dataPoints[label])
               {
                  csv.WriteField(label);
                  csv.WriteField(point.X);
                  csv.WriteField(point.Y);
                  csv.NextRecordAsync();
               }
            }

            writer.Flush();
            stream.Position = 0;

            string text = reader.ReadToEnd();

            return text;
         }

         /*
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
               foreach (string label in this.dataPoints.Keys)
               {
                  foreach (Point point in this.dataPoints[label])
                  {
                     streamWriter.WriteLine(string.Format("{0};{1};{2}", label, point.X, point.Y));
                  }
               }
            }

            this.dataPointsModified = false;
         }
         */
      }

      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "Too complicated to solve.")]
      public void LoadPoints(string dataPoints)
      {
         ////string filename = Path.GetFileNameWithoutExtension(imagePath);

         ////this.tempFilename = this.SavePath + @"\Tagger\" + filename + ".imagedata";

         this.dataPoints.Clear();

         using (MemoryStream stream = new MemoryStream())
         using (StringReader reader = new StringReader(dataPoints))
         using (CsvReader csv = new CsvReader(reader))
         {
            csv.Read();

            string label = csv.GetField<string>(0);
            Point readPoint = new Point(csv.GetField<int>(1), csv.GetField<int>(2));

            this.AddLabel(label);

            this.AddPoint(label, readPoint);
         }

         /*
         if (File.Exists(this.tempFilename))
         {
            using (StreamReader streamReader = new StreamReader(this.tempFilename))
            {
               while (!streamReader.EndOfStream)
               {
                  string line = streamReader.ReadLine();
                  string[] lineSplits = line.Split(';');
                  string label = lineSplits[0];
                  Point readPoint = new Point(Convert.ToInt32(lineSplits[1]), Convert.ToInt32(lineSplits[2]));

                  this.AddLabel(label);

                  this.AddPoint(label, readPoint);
               }
            }
         }
         */

         ////this.dataPointsModified = false;
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
