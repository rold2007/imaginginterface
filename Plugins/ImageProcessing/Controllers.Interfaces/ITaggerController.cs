namespace ImageProcessing.Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.Drawing;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImageProcessing.Controllers.EventArguments;
   using ImagingInterface.Plugins;

   public interface ITaggerController : IImageProcessingController
      {
      event EventHandler<TagPointChangedEventArgs> TagPointChanged;

      Dictionary<string, List<Point>> DataPoints
         {
         get;
         }

      string SavePath
         {
         get;
         set;
         }
      }
   }
