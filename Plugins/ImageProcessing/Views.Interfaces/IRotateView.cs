namespace ImageProcessing.Views
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImageProcessing.Views.EventArguments;
   using ImagingInterface.Plugins;

   public interface IRotateView : IPluginView
      {
      event EventHandler<RotateEventArgs> Rotate;
      }
   }
