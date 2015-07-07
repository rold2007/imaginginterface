namespace ImageProcessing.Models
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;

   public class TaggerModel : ITaggerModel
      {
      public TaggerModel()
         {
         }

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

      public SortedSet<string> Labels
         {
         get;
         set;
         }

      public SortedList<string, double[]> LabelColors
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
