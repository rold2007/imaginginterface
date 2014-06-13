namespace ImageProcessing.Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.Drawing;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Plugins;

   public interface ITaggerController : IImageProcessingController
      {
      Dictionary<string, List<Point>> DataPoints
         {
         get;
         }
      }
   }
