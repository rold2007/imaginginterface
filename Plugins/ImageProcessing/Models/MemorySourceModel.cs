namespace ImageProcessing.Models
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Plugins;

   public class MemorySourceModel : IMemorySourceModel
      {
      public string DisplayName
         {
         get; // ncrunch: no coverage
         set; // ncrunch: no coverage
         }

      public byte[, ,] ImageData
         {
         get;
         set;
         }
      }
   }
