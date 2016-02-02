namespace ImageProcessing.Models
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Plugins;

   public class FileSourceModel : IFileSourceModel
      {
      public string DisplayName
         {
         get; // ncrunch: no coverage
         set; // ncrunch: no coverage
         }

      public string Filename
         {
         get;
         set;
         }

      public byte[, ,] ImageData
         {
         get;
         set;
         }
      }
   }
