namespace ImagingInterface.Tests.Common.Views
   {
   using System;
   using System.Collections.Generic;
   using System.Drawing;
   using System.Linq;
   using System.Text;
   using System.Threading;
   using System.Threading.Tasks;
   using ImagingInterface.Models;
   using ImagingInterface.Views;
   using ImagingInterface.Views.EventArguments;

   public class ImageView : IImageView
      {
      private double updateFrequency = 0.0f;

      public event EventHandler ZoomLevelIncreased;

      public event EventHandler ZoomLevelDecreased;

      public event EventHandler<PixelViewChangedEventArgs> PixelViewChanged;

      public event EventHandler<SelectionChangedEventArgs> SelectionChanged;

      public IImageModel AssignedImageModel
         {
         get;
         private set;
         }

      public double UpdateFrequency
         {
         get
            {
            return this.updateFrequency;
            }

         set
            {
            this.updateFrequency = value;
            }
         }

      public Point PixelPosition
         {
         get;
         private set;
         }

      public int Gray
         {
         get;
         private set;
         }

      public int[] RGB
         {
         get;
         private set;
         }

      public double[] HSV
         {
         get;
         private set;
         }

      public bool IsClosed
         {
         get;
         private set;
         }

      public void UpdateDisplay()
         {
         }

      public void UpdateZoomLevel()
         {
         }

      public void AssignImageModel(IImageModel imageModel)
         {
         this.AssignedImageModel = imageModel;
         }

      public void UpdatePixelView(Point pixelPosition, int gray, int[] rgb, double[] hsv)
         {
         this.PixelPosition = pixelPosition;
         this.Gray = gray;
         this.RGB = rgb;
         this.HSV = hsv;
         }

      public void Hide()
         {
         }

      public void Close()
         {
         this.IsClosed = true;
         }

      public void TriggerZoomLevelIncreased()
         {
         this.ZoomLevelIncreased(this, EventArgs.Empty);
         }

      public void TriggerZoomLevelDecreased()
         {
         this.ZoomLevelDecreased(this, EventArgs.Empty);
         }

      public void TriggerPixelViewChanged(Point pixelPosition)
         {
         this.PixelViewChanged(this, new PixelViewChangedEventArgs(pixelPosition));
         }
      }
   }
