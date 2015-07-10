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

      bool AddPoint(string tag, Point newPoint);

      bool RemovePoint(string tag, Point newPoint);

      Color TagColor(string tag);
      }
   }
