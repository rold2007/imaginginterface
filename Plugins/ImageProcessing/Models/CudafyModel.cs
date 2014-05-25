namespace ImageProcessing.Models
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;

   public class CudafyModel : ICudafyModel
      {
      public string DisplayName
         {
         get;
         set;
         }

      public string GPUName
         {
         get;
         set;
         }

      public int Add
         {
         get;
         set;
         }

      public int[] GridSize
         {
         get;
         set;
         }

      public int[] BlockSize
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
