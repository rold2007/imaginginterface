namespace ImagingInterface.Models
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;

   public class AboutBoxModel : IAboutBoxModel
      {
      public string ProductName
         {
         get;
         set;
         }

      public string Version
         {
         get;
         set;
         }

      public string Copyright
         {
         get;
         set;
         }

      public string ProductDescription
         {
         get;
         set;
         }
      }
   }
