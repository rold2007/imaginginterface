namespace ImageProcessing.Models
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Plugins;

   public interface ITaggerModel : IRawPluginModel
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

      SortedList<string, double[]> Labels
         {
         get;
         set;
         }
      }
   }
