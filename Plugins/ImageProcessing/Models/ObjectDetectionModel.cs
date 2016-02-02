namespace ImageProcessing.Models
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;

   public class ObjectDetectionModel : IObjectDetectionModel
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
