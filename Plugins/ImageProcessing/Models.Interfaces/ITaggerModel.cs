namespace ImageProcessing.Models
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Plugins;

   public interface ITaggerModel : IRawPluginModel, ICloneable
      {
      string AddedLabel
         {
         get;
         set;
         }

      string SelectedLabel
         {
         get;
         set;
         }

      SortedSet<string> Labels
         {
         get;
         set;
         }

      SortedList<string, double[]> LabelColors
         {
         get;
         set;
         }
      }
   }
