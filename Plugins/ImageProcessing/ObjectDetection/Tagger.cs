// <copyright file="Tagger.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.ObjectDetection
{
   using System.Collections.Generic;
   using System.Collections.ObjectModel;
   using System.Drawing;
   using System.IO;
   using CsvHelper;

   public class Tagger
   {
      private Dictionary<string, List<Point>> dataPoints;

      public Tagger()
      {
         this.dataPoints = new Dictionary<string, List<Point>>();
      }

      public IReadOnlyDictionary<string, List<Point>> DataPoints
      {
         get
         {
            return new ReadOnlyDictionary<string, List<Point>>(this.dataPoints);
         }
      }

      public IReadOnlyCollection<string> Labels
      {
         get
         {
            return this.dataPoints.Keys;
         }
      }

      public bool AddPoint(string label, Point newPoint)
      {
         List<Point> points;

         if (this.dataPoints.TryGetValue(label, out points))
         {
            if (!points.Contains(newPoint))
            {
               points.Add(newPoint);

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
      }

      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "Too complicated to solve.")]
      public void LoadPoints(string dataPoints)
      {
         this.dataPoints.Clear();

         using (MemoryStream stream = new MemoryStream())
         using (StringReader reader = new StringReader(dataPoints))
         using (CsvReader csv = new CsvReader(reader))
         {
            if (csv.Read())
            {
               string label = csv.GetField<string>(0);
               Point readPoint = new Point(csv.GetField<int>(1), csv.GetField<int>(2));

               this.AddLabel(label);

               this.AddPoint(label, readPoint);
            }
         }
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
