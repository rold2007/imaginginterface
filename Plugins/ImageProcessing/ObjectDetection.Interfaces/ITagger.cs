namespace ImageProcessing.ObjectDetection
   {
   using System;
   using System.Collections.Generic;
   using System.Drawing;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;

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
