namespace ImagingInterface.Views
   {
   using System;
   using System.Collections.Generic;
   using System.Drawing;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Models;
   using ImagingInterface.Plugins;
   using ImagingInterface.Views.EventArguments;

   public interface IImageView : IRawImageView
      {
      event EventHandler ZoomLevelIncreased;

      event EventHandler ZoomLevelDecreased;

      event EventHandler<PixelViewChangedEventArgs> PixelViewChanged;

      double UpdateFrequency
         {
         get;
         }

      void AssignImageModel(IImageModel imageModel);

      void UpdateDisplay();

      void UpdateZoomLevel();

      void UpdatePixelView(Point pixelPosition, int gray, int[] rgb, double[] hsv);

      void Hide();

      void Close();
      }
   }
