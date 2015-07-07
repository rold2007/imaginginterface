namespace ImageProcessing.Models
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using Emgu.CV;
   using Emgu.CV.ML;
   using Emgu.CV.ML.MlEnum;
   using Emgu.CV.ML.Structure;

   public class ObjectDetectionModel : IObjectDetectionModel
      {
      public string DisplayName
         {
         get;
         set;
         }

      public object Clone()
         {
         return this.MemberwiseClone();
         }
      }
   }
