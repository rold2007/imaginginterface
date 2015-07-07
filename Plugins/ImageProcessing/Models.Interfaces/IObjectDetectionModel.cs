namespace ImageProcessing.Models
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using Emgu.CV;
   using Emgu.CV.ML;
   using Emgu.CV.ML.MlEnum;
   using Emgu.CV.ML.Structure;
   using ImagingInterface.Plugins;

   public interface IObjectDetectionModel : IRawPluginModel, ICloneable
      {
      }
   }
