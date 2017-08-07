namespace ImageProcessing.ObjectDetection
   {
   using System;
   using System.Collections.Generic;
   using System.Drawing;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;

   public interface IObjectDetector : IDisposable
      {
      void Add(string label, Point tagPoint);

      void Remove(string label, Point tagPoint);

      void Train(byte[,,] imageData);

      Dictionary<string, List<Point>> Test(byte[,,] imageData);
      }
   }
