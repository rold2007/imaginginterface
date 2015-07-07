namespace ImageProcessing.Controllers
   {
   using System;
   using System.Collections;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Drawing;
   using ImageProcessing.Controllers.EventArguments;
   using ImageProcessing.Models;
   using ImageProcessing.Views;
   using ImagingInterface.Plugins;
   using ImagingInterface.Plugins.EventArguments;
   using ImageProcessing.ObjectDetection;

   public class TaggerController : ITaggerController
      {
      private static readonly string TaggerDisplayName = "Tagger"; // ncrunch: no coverage
      private ITaggerView taggerView;
      private ITaggerModel taggerModel;
      private IImageManagerController imageManagerController;
      private IImageController registeredImageController;
      private ITagger tagger;

      public TaggerController(ITaggerView taggerView, ITaggerModel taggerModel, ITagger tagger, IImageManagerController imageManagerController)
         {
         this.taggerView = taggerView;
         this.taggerModel = taggerModel;
         this.tagger = tagger;
         this.imageManagerController = imageManagerController;

         this.taggerModel.DisplayName = TaggerDisplayName;
         this.taggerModel.Labels = new SortedSet<string>();
         this.taggerModel.LabelColors = new SortedList<string, double[]>();
         }

      public event CancelEventHandler Closing;

      public event EventHandler Closed;

      public event EventHandler<TagPointChangedEventArgs> TagPointChanged;

      public IRawPluginView RawPluginView
         {
         get
            {
            return this.taggerView;
            }
         }

      public IRawPluginModel RawPluginModel
         {
         get
            {
            return this.taggerModel;
            }
         }

      public bool Active
         {
         get
            {
            return true;
            }
         }

      public void Initialize()
         {
         this.taggerView.SetTaggerModel(this.taggerModel);

         this.taggerView.LabelAdded += this.TaggerView_LabelAdded;

         this.imageManagerController.ActiveImageChanged += this.ImageManagerController_ActiveImageChanged;

         this.RegisterActiveImage();
         }

      public void Close()
         {
         CancelEventArgs cancelEventArgs = new CancelEventArgs();

         if (this.Closing != null)
            {
            this.Closing(this, cancelEventArgs);
            }

         if (!cancelEventArgs.Cancel)
            {
            this.taggerView.LabelAdded -= this.TaggerView_LabelAdded;

            this.imageManagerController.ActiveImageChanged -= this.ImageManagerController_ActiveImageChanged;

            this.UnregisterActiveImage();

            this.taggerView.Hide();

            this.taggerView.Close();

            if (this.Closed != null)
               {
               this.Closed(this, EventArgs.Empty);
               }
            }
         }

      public byte[, ,] ProcessImageData(byte[, ,] imageData, byte[] overlayData, IRawPluginModel rawPluginModel)
         {
         int imageWidth = imageData.GetLength(1);
         int imageHeight = imageData.GetLength(0);
         int imageSize = imageWidth * imageHeight;

         foreach (string tag in this.tagger.DataPoints.Keys)
            {
            double[] rgb = this.taggerModel.LabelColors[tag];
            byte red = Convert.ToByte(rgb[0]);
            byte green = Convert.ToByte(rgb[1]);
            byte blue = Convert.ToByte(rgb[2]);

            foreach (Point point in this.tagger.DataPoints[tag])
               {
               int pixelOffset = (point.Y * imageWidth * 4) + point.X * 4;

               overlayData[pixelOffset] = red;
               overlayData[pixelOffset + 1] = green;
               overlayData[pixelOffset + 2] = blue;
               overlayData[pixelOffset + 3] = 255;
               }
            }

         return imageData;
         }

      public bool AddPoint(string tag, Point newPoint)
         {
         if (this.tagger.AddPoint(tag, newPoint))
            {
            this.TriggerTagPointChanged(tag, newPoint, true);

            return true;
            }

         return false;
         }

      public bool RemovePoint(string tag, Point newPoint)
         {
         if (this.tagger.RemovePoint(tag, newPoint))
            {
            this.TriggerTagPointChanged(tag, newPoint, false);

            return true;
            }

         return false;
         }

      private void ImageManagerController_ActiveImageChanged(object sender, EventArgs e)
         {
         this.UnregisterActiveImage();
         this.RegisterActiveImage();
         }

      private void RegisterActiveImage()
         {
         this.registeredImageController = this.imageManagerController.GetActiveImage();

         if (this.registeredImageController != null)
            {
            this.registeredImageController.SelectionChanged += this.RegisteredImageController_SelectionChanged;

            if (this.registeredImageController.LastDisplayedImage == null)
               {
               // Need to wait for the first display update
               this.registeredImageController.DisplayUpdated += this.RegisteredImageController_DisplayUpdated;
               }
            else
               {
               this.ExtractPoints();
               }
            }
         }

      private void UnregisterActiveImage()
         {
         if (this.registeredImageController != null)
            {
            this.registeredImageController.SelectionChanged -= this.RegisteredImageController_SelectionChanged;
            this.registeredImageController.DisplayUpdated -= this.RegisteredImageController_DisplayUpdated;

            this.tagger.SavePoints();

            this.registeredImageController = null;
            }
         }

      private void ExtractPoints()
         {
         this.tagger.LoadPoints(this.registeredImageController.FullPath);

         this.AssignColors();

         this.registeredImageController.AddImageProcessingController(this, this.taggerModel.Clone() as IRawPluginModel);

         this.taggerView.UpdateLabelList();
         }

      private void RegisteredImageController_DisplayUpdated(object sender, DisplayUpdateEventArgs e)
         {
         this.registeredImageController.DisplayUpdated -= this.RegisteredImageController_DisplayUpdated;

         this.ExtractPoints();
         }

      private void RegisteredImageController_SelectionChanged(object sender, SelectionChangedEventArgs e)
         {
         if (this.taggerModel.SelectedLabel != null)
            {
            if (e.Select)
               {
               if (this.AddPoint(this.taggerModel.SelectedLabel, e.PixelPosition))
                  {
                  this.registeredImageController.AddImageProcessingController(this, this.taggerModel.Clone() as IRawPluginModel);
                  }
               }
            else
               {
               if (this.RemovePoint(this.taggerModel.SelectedLabel, e.PixelPosition))
                  {
                  this.registeredImageController.AddImageProcessingController(this, this.taggerModel.Clone() as IRawPluginModel);
                  }
               }
            }
         }

      private void TriggerTagPointChanged(string label, Point tagPoint, bool added)
         {
         Dictionary<string, List<Point>> tagPoints = new Dictionary<string, List<Point>>();
         List<Point> points = new List<Point>();

         points.Add(tagPoint);

         tagPoints.Add(label, points);

         if (this.TagPointChanged != null)
            {
            this.TagPointChanged(this, new TagPointChangedEventArgs(this.registeredImageController, label, tagPoint, added));
            }
         }
       
      private void TaggerView_LabelAdded(object sender, EventArgs e)
         {
         this.tagger.AddLabel(this.taggerModel.AddedLabel);

         this.taggerModel.Labels.UnionWith(this.tagger.DataPoints.Keys);

         this.AssignColors();
         }

      private void AssignColors()
         {
         int labelIndex = 0;

         foreach (string label in this.taggerModel.Labels)
            {
            double hue = 360 * labelIndex / this.taggerModel.Labels.Count;
            double[] hsv = new double[3] { hue, 1.0, 255.0 };
            double[] rgb = ImagingInterface.Plugins.Utilities.Color.HSVToRGB(hsv);

            this.taggerModel.LabelColors[label] = rgb;

            labelIndex++;
            }
         }
      }
   }
