namespace ImagingInterface.Models
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;

   public interface IAboutBoxModel
      {
      string ProductName
         {
         get;
         set;
         }

      string Version
         {
         get;
         set;
         }

      string Copyright
         {
         get;
         set;
         }

      string ProductDescription
         {
         get;
         set;
         }
      }
   }
