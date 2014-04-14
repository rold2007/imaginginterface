namespace ImagingInterface.Plugins
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;

   public interface IFileSourceModel : IRawPluginModel
      {
      string Filename
         {
         get;
         set;
         }

      byte[, ,] ImageData
         {
         get;
         set;
         }
      }
   }
