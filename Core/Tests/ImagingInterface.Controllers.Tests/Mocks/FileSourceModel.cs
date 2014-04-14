namespace ImagingInterface.Controllers.Tests.Mocks
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
         get;
         set;
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
