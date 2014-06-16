namespace ImageProcessing.Models
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Plugins;

   public class IObjectDetectionManagerModel : IRawPluginModel
      {
      public string DisplayName
         {
         get;
         set;
         }
      }
   }
