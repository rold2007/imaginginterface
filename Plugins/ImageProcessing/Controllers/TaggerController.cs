namespace ImageProcessing.Controllers
   {
   using System;
   using System.Collections;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Drawing;
   using System.IO;
   using ImageProcessing.Models;
   using ImageProcessing.Views;
   using ImagingInterface.Plugins;

   public class TaggerController : ITaggerController
      {
      private static readonly string TaggerDisplayName = "Tagger"; // ncrunch: no coverage
      private ITaggerView taggerView;
      private ITaggerModel taggerModel;
      private IImageManagerController imageManagerController;
      private IImageController registeredImageController;
      private string tempFilename;
      private bool dataPointsModified;
      private Dictionary<string, List<Point>> dataPoints;

      public TaggerController(ITaggerView taggerView, ITaggerModel taggerModel, IImageManagerController imageManagerController)
         {
         this.taggerView = taggerView;
         this.taggerModel = taggerModel;
         this.imageManagerController = imageManagerController;

         this.taggerModel.DisplayName = TaggerDisplayName;
         this.dataPoints = new Dictionary<string, List<Point>>();
         }

      public event CancelEventHandler Closing;

      public event EventHandler Closed;

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

      public Dictionary<string, List<Point>> DataPoints
         {
         get
            {
            return new Dictionary<string, List<Point>>(this.dataPoints);
            }
         }

      public void Initialize()
         {
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

      public byte[, ,] ProcessImageData(byte[, ,] imageData, IRawPluginModel rawPluginModel)
         {
         byte[, ,] image = imageData.Clone() as byte[, ,];

         if (imageData.GetLength(2) == 1)
            {
            foreach (string tag in this.dataPoints.Keys)
               {
               foreach (Point point in this.dataPoints[tag])
                  {
                  image[point.Y, point.X, 0] = 255;
                  }
               }
            }
         else
            {
            foreach (string tag in this.dataPoints.Keys)
               {
               foreach (Point point in this.dataPoints[tag])
                  {
                  image[point.Y, point.X, 0] = 0;
                  image[point.Y, point.X, 1] = 0;
                  image[point.Y, point.X, 2] = 255;
                  }
               }
            }

         return image;
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

            string filename = Path.GetFileNameWithoutExtension(this.registeredImageController.FullPath);

            this.tempFilename = Path.GetTempPath() + @"\Tagger\" + filename + ".imagedata";

            this.LoadPoints();
            }
         }

      private void UnregisterActiveImage()
         {
         if (this.registeredImageController != null)
            {
            this.registeredImageController.SelectionChanged -= this.RegisteredImageController_SelectionChanged;

            this.SavePoints();

            this.registeredImageController = null;
            this.tempFilename = null;
            }
         }

      private void RegisteredImageController_SelectionChanged(object sender, ImagingInterface.Plugins.EventArguments.SelectionChangedEventArgs e)
         {
         if (e.Select)
            {
            if (this.AddPoint("Label", e.PixelPosition))
               {
               this.registeredImageController.AddImageProcessingController(this, this.taggerModel);
               }
            }
         else
            {
            if (this.RemovePoint("Label", e.PixelPosition))
               {
               this.registeredImageController.AddImageProcessingController(this, this.taggerModel);
               }
            }
         }

      private void SavePoints()
         {
         if (this.dataPointsModified)
            {
            string directory = Path.GetDirectoryName(this.tempFilename);

            if (!Directory.Exists(directory))
               {
               Directory.CreateDirectory(directory);
               }

            using (StreamWriter streamWriter = new StreamWriter(this.tempFilename, false))
               {
               foreach (string tag in this.dataPoints.Keys)
                  {
                  foreach (Point point in this.dataPoints[tag])
                     {
                     streamWriter.WriteLine(string.Format("{0};{1};{2}", tag, point.X, point.Y));
                     }
                  }
               }

            this.dataPointsModified = false;
            }
         }

      private void LoadPoints()
         {
         this.dataPoints.Clear();

         if (File.Exists(this.tempFilename))
            {
            using (StreamReader streamReader = new StreamReader(this.tempFilename))
               {
               while (!streamReader.EndOfStream)
                  {
                  string line = streamReader.ReadLine();
                  string[] lineSplits = line.Split(';');
                  string tag = lineSplits[0];
                  Point readPoint = new Point(Convert.ToInt32(lineSplits[1]), Convert.ToInt32(lineSplits[2]));

                  this.AddPoint(tag, readPoint);
                  }
               }

            this.registeredImageController.AddImageProcessingController(this, this.taggerModel);
            }

         this.dataPointsModified = false;
         }

      private bool AddPoint(string tag, Point newPoint)
         {
         List<Point> points;

         if (this.dataPoints.TryGetValue(tag, out points))
            {
            if (!points.Contains(newPoint))
               {
               points.Add(newPoint);

               this.dataPointsModified = true;

               return true;
               }
            else
               {
               return false;
               }
            }
         else
            {
            points = new List<Point>();

            this.dataPoints.Add(tag, points);

            points.Add(newPoint);

            this.dataPointsModified = true;

            return true;
            }
         }

      private bool RemovePoint(string tag, Point newPoint)
         {
         List<Point> points;

         if (this.dataPoints.TryGetValue(tag, out points))
            {
            if (points.Contains(newPoint))
               {
               points.Remove(newPoint);

               this.dataPointsModified = true;

               return true;
               }
            }

         return false;
         }
      }
   }
