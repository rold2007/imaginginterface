namespace ImageProcessing.Models
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Plugins;

   public interface ICudafyModel : IPluginModel, ICloneable
      {
      string GPUName
         {
         get;
         set;
         }

      int Add
         {
         get;
         set;
         }

      int[] GridSize
         {
         get;
         set;
         }

      int[] BlockSize
         {
         get;
         set;
         }
      }
   }
