namespace ImageProcessing.Models
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;

   public class TaggerModel : ITaggerModel
      {
      public string DisplayName
         {
         get;
         set;
         }

      public string AddedLabel
         {
         get;
         set;
         }

      public string SelectedLabel
         {
         get;
         set;
         }

      public SortedList<string, double[]> Labels
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
