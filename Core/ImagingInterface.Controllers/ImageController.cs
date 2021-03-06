﻿// <copyright file="ImageController.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers
{
   using System;
   using System.Diagnostics;
   using System.Diagnostics.CodeAnalysis;
   using System.Drawing;
   using ImageProcessor.Imaging.Colors;
   using ImagingInterface.Controllers.Services;
   using ImagingInterface.Plugins;

   public class ImageController
   {
      private ImageService imageService;

      // private bool closing;
      // private bool closed;
      ////private List<Tuple<IImageProcessingService, IRawPluginModel>> imageProcessingControllers;

      // private Task<byte[,,]> lastFetchNextImageFromSourceTask;
      // private Task lastDisplayNextImageTask;
      // private IImageSource imageSourceController;
      // private IRawPluginModel imageSourceRawPluginModel;
      private Stopwatch lastDisplayUpdate;

      // private double updatePeriod;
      ////private Dictionary<IPluginController, int> asyncPluginControllers;
      ////private HashSet<IPluginController> closingPluginControllers;

      public ImageController(ImageService imageService)
      {
         this.imageService = imageService;

         ////this.imageProcessingControllers = new List<Tuple<IImageProcessingService, IRawPluginModel>>();

         // this.lastFetchNextImageFromSourceTask = null;
         // this.lastDisplayNextImageTask = null;
         ////this.asyncPluginControllers = new Dictionary<IPluginController, int>();
         ////this.closingPluginControllers = new HashSet<IPluginController>();

         this.lastDisplayUpdate = Stopwatch.StartNew();

         ////double updateFrequency = this.imageView.UpdateFrequency;

         ////if (updateFrequency != 0.0)
         ////   {
         ////   this.updatePeriod = 1000 / this.imageView.UpdateFrequency;
         ////   }
         ////else
         ////   {
         ////   this.updatePeriod = -1.0;
         ////   }

         // this.closed = false;

         ////this.imageView.ZoomLevelIncreased += this.ImageView_ZoomLevelIncreased;
         ////this.imageView.ZoomLevelDecreased += this.ImageView_ZoomLevelDecreased;
         ////this.imageView.PixelViewChanged += this.ImageView_PixelViewChanged;
         ////this.imageView.SelectionChanged += this.ImageView_SelectionChanged;
      }

      ////public event CancelEventHandler Closing;

      ////public event EventHandler Closed;

      ////public event EventHandler<DisplayUpdateEventArgs> DisplayUpdated;

      ////public event EventHandler<Plugins.EventArguments.SelectionChangedEventArgs> SelectionChanged;

      public event EventHandler UpdateDisplay;

      ////public IRawImageView RawImageView
      ////   {
      ////   get
      ////      {
      ////      return this.imageView;
      ////      }
      ////   }

      ////public IRawImageModel RawImageModel
      ////   {
      ////   get
      ////      {
      ////      return this.imageModel;
      ////      }
      ////   }

      ////public byte[, ,] LastDisplayedImage
      ////   {
      ////   get
      ////      {
      ////      return this.imageModel.DisplayImageData;
      ////      }
      ////   }

      public string DisplayName
      {
         get
         {
            return this.imageService.DisplayName;
         }
      }

      public double ZoomLevel
      {
         get
         {
            return this.imageService.ZoomLevel;
         }
      }

      public IImageSource ImageSource
      {
         get
         {
            return this.imageService.ImageSource;
         }

         set
         {
            this.imageService.ImageSource = value;

            this.imageService.ImageSource.ImageDataUpdated += this.ImageSource_ImageDataUpdated;

            this.TriggerUpdateDisplay();
         }
      }

      [SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", Justification = "Too much work for now.")]
      [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Too much work for now.")]
      public byte[,,] DisplayImageData
      {
         get
         {
            return this.imageService.DisplayImageData;
         }
      }

      [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Too much work for now.")]
      public byte[] OverlayImageData
      {
         get
         {
            return this.imageService.OverlayImageData;
         }
      }

      public Size Size
      {
         get
         {
            return this.imageService.Size;
         }
      }

      public bool IsGrayscale
      {
         get
         {
            return this.imageService.IsGrayscale;
         }
      }

      public void AssignToImageManager()
      {
         this.imageService.AssignToImageManager();
      }

      public void UnAssignFromImageManager()
      {
         this.imageService.UnAssignFromImageManager();
      }

      public RgbaColor GetRgbaPixel(Point pixelPosition)
      {
         return this.imageService.GetRgbaPixel(pixelPosition);
      }

      public void UpdateZoomLevel(double zoomLevel)
      {
         this.imageService.ZoomLevel = zoomLevel;
      }

      public void SelectPixel(Point mouseClickPixel)
      {
         this.imageService.SelectPixel(mouseClickPixel);
      }

      [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Will be fixed when done refactoring.")]
      public void Close()
      {
         /*
         if (!this.closed)
            {
            CancelEventArgs cancelEventArgs = new CancelEventArgs();

            // The close was prevented by the ImageController, do not send a closing again
            if (!this.closing)
               {
               if (this.Closing != null)
                  {
                  this.Closing(this, cancelEventArgs);
                  }
               }

            if (!cancelEventArgs.Cancel)
               {
               this.closing = true;

               // Make sure the display thread is finished before really closing
               if (this.lastDisplayNextImageTask == null)
                  {
                  ////this.imageView.ZoomLevelIncreased -= this.ImageView_ZoomLevelIncreased;
                  ////this.imageView.ZoomLevelDecreased -= this.ImageView_ZoomLevelDecreased;
                  ////this.imageView.PixelViewChanged -= this.ImageView_PixelViewChanged;
                  ////this.imageView.SelectionChanged -= this.ImageView_SelectionChanged;

                  // Prevent calling the Closed event more than once
                  this.closed = true;

                  ////this.imageView.Hide();
                  ////this.imageView.Close();
                  this.imageModel.DisplayImageData = null;

                  if (this.Closed != null)
                     {
                     this.Closed(this, EventArgs.Empty);
                     }

                  // The image view is used by some Closed events
                  ////this.imageView = null;
                  }
               }
            }*/
      }

      /*
      public void AddImageProcessingController(IImageProcessingController imageProcessingController, IRawPluginModel rawPluginModel)
         {
         if (!this.closing)
            {
            if (!this.closingPluginControllers.Contains(imageProcessingController))
               {
               // For now, only one processing controller is supported. In the future, it should be possible to "pin" some
               // image processing controllers and overwrite any unpinned duplicate.
               this.imageProcessingControllers.Clear();

               this.imageProcessingControllers.Add(Tuple.Create<IImageProcessingController, IRawPluginModel>(imageProcessingController, rawPluginModel));

               // If an assert pops here while running a unit test with NCrunch, you need to use
               // the STASynchronizationContext class to initialize a proper SynchronizationContext
               this.CreateLiveUpdateTask(TaskScheduler.FromCurrentSynchronizationContext(), false);
               }
            }
         }

      public void RemoveImageProcessingController(IImageProcessingController imageProcessingController, IRawPluginModel rawPluginModel)
         {
         if (!this.closing)
            {
            // For now, only one processing controller is supported. In the future, it should be possible to "pin" some
            // image processing controllers and overwrite any unpinned duplicate.
            this.imageProcessingControllers.Clear();

            // If an assert pops here while running a unit test with NCrunch, you need to use
            // the STASynchronizationContext class to initialize a proper SynchronizationContext
            this.CreateLiveUpdateTask(TaskScheduler.FromCurrentSynchronizationContext(), false);
            }
         }
      */

      private void TriggerUpdateDisplay()
      {
         this.UpdateDisplay?.Invoke(this, EventArgs.Empty);
      }

      private void ImageSource_ImageDataUpdated(object sender, EventArgs e)
      {
         this.TriggerUpdateDisplay();
      }

      /*
      private void CreateDynamicUpdateTasks(TaskScheduler taskScheduler)
         {
         bool dynamic = this.imageSourceController.IsDynamic(this.imageSourceRawPluginModel);

         this.CreateLiveUpdateTask(taskScheduler, dynamic);
         }

      private void CreateLiveUpdateTask(TaskScheduler taskScheduler, bool launchHeartBeat)
         {
         Task<byte[, ,]> fetchNextImageFromSourceTask;
         Task<byte[, ,]> imageSourceTask;
         Task waitForFetchNextImageSourceTask = new Task(this.WaitForNextImageSource, this.lastFetchNextImageFromSourceTask);

         if (launchHeartBeat)
            {
            waitForFetchNextImageSourceTask.ContinueWith(this.StartNewHeartBeat, null, taskScheduler);
            }

         // Task needed to synchronize all the FetchNextImageFromSource and make sure only one is ran at any time and that they run in-order
         waitForFetchNextImageSourceTask.Start(TaskScheduler.Default);

         ImageProcessingAsyncData imageProcessingAsyncData = new ImageProcessingAsyncData(this.imageSourceController, this.imageSourceRawPluginModel, null, waitForFetchNextImageSourceTask, this.lastDisplayNextImageTask);

         fetchNextImageFromSourceTask = new Task<byte[, ,]>(this.FetchNextImageFromSource, imageProcessingAsyncData);

         fetchNextImageFromSourceTask.Start(TaskScheduler.Default);

         this.RegisterClosingEvent(this.imageSourceController);

         this.lastFetchNextImageFromSourceTask = fetchNextImageFromSourceTask;

         if (this.imageProcessingControllers.Count > 0)
            {
            List<Tuple<IImageProcessingController, IRawPluginModel>> imageProcessingControllers = new List<Tuple<IImageProcessingController, IRawPluginModel>>();
            List<byte[]> overlays = new List<byte[]>();

            imageProcessingAsyncData.ImageProcessingControllers = imageProcessingControllers;
            imageProcessingAsyncData.Overlays = overlays;

            imageSourceTask = this.CreateProcessingTasks(fetchNextImageFromSourceTask, imageProcessingControllers, overlays);
            }
         else
            {
            imageSourceTask = fetchNextImageFromSourceTask;
            }

         Debug.Assert(this.imageProcessingControllers.Count <= 1, "Need to support a list of image processing controllers in the ImageProcessingAsyncData");

         imageProcessingAsyncData.TaskByte = imageSourceTask;
         imageProcessingAsyncData.LastDisplayNextImageTask = this.lastDisplayNextImageTask;

         Task<byte[, ,]> waitForDisplayNextImageTask = new Task<byte[, ,]>(this.WaitForDisplayNextImage, imageProcessingAsyncData);
         Task displayNextImageTask = waitForDisplayNextImageTask.ContinueWith(this.DisplayNextImage, imageProcessingAsyncData, taskScheduler);

         waitForDisplayNextImageTask.Start(TaskScheduler.Default);

         this.lastDisplayNextImageTask = displayNextImageTask;
         }

      private void RegisterClosingEvent(IPluginController pluginController)
         {
         if (this.asyncPluginControllers.ContainsKey(pluginController))
            {
            this.asyncPluginControllers[pluginController]++;
            }
         else
            {
            this.asyncPluginControllers.Add(pluginController, 1);

            pluginController.Closing += this.PluginController_Closing;
            }
         }

      private Task<byte[, ,]> CreateProcessingTasks(Task<byte[, ,]> imageSourceTask, List<Tuple<IImageProcessingController, IRawPluginModel>> imageProcessingControllers, List<byte[]> overlays)
         {
         Debug.Assert(this.imageProcessingControllers.Count > 0, "CreateProcessingTasks should only be called when there is some image processing to do.");

         Task<byte[, ,]> previousTask = null;

         foreach (Tuple<IImageProcessingController, IRawPluginModel> imageProcessingControllerTuple in this.imageProcessingControllers)
            {
            if (!this.closingPluginControllers.Contains(imageProcessingControllerTuple.Item1))
               {
               ImageProcessingAsyncData imageProcessingAsyncData = new ImageProcessingAsyncData(null, null, imageSourceTask, null, null);

               imageProcessingAsyncData.ImageProcessingControllers = imageProcessingControllers;
               imageProcessingAsyncData.Overlays = overlays;

               imageProcessingControllers.Add(imageProcessingControllerTuple);

               previousTask = new Task<byte[, ,]>(this.ProcessImage, imageProcessingAsyncData);

               previousTask.Start(TaskScheduler.Default);

               this.RegisterClosingEvent(imageProcessingControllerTuple.Item1);
               }
            }

         return previousTask;
         }

      private void WaitForNextImageSource(object state)
         {
         if (state != null)
            {
            Task<byte[, ,]> task = state as Task<byte[, ,]>;

            task.Wait();
            }
         }

      private byte[, ,] WaitForDisplayNextImage(object state)
         {
         ImageProcessingAsyncData imageProcessingAsyncData = state as ImageProcessingAsyncData;
         Task<byte[, ,]> fetchNextImageSourceOrProcessImageTask = imageProcessingAsyncData.TaskByte;
         Task displayNextImageTask = imageProcessingAsyncData.LastDisplayNextImageTask;

         // Make sure the previous display task is completed to run them in-order
         if (displayNextImageTask != null)
            {
            displayNextImageTask.Wait();
            displayNextImageTask.Dispose();
            }

         // Wait for the image to be ready to be displayed
         fetchNextImageSourceOrProcessImageTask.Wait();

         byte[, ,] imageData = fetchNextImageSourceOrProcessImageTask.Result;

         return imageData;
         }

      private void StartNewHeartBeat(Task parentTask, object state)
         {
         parentTask.Dispose();

         if (!this.closing)
            {
            // TaskScheduler.Current should be the MainThread
            this.CreateDynamicUpdateTasks(TaskScheduler.Current);
            }
         }

      private byte[, ,] FetchNextImageFromSource(object state)
         {
         ImageProcessingAsyncData imageProcessingAsyncData = state as ImageProcessingAsyncData;
         IImageSourceController imageSourceController = imageProcessingAsyncData.ImageSourceController;
         IRawPluginModel rawPluginModel = imageProcessingAsyncData.RawPluginModel;
         Task previousFetchNextImageFromSourceTask = imageProcessingAsyncData.WaitForFetchNextImageSourceTask;

         previousFetchNextImageFromSourceTask.Wait();

         // Do not clone the result here. It is the responsibility of the IImageSourceController to return the same (unmodified) image
         // or a new image
         byte[, ,] resultImage = imageSourceController.NextImageData(rawPluginModel);

         Debug.Assert(resultImage != null, "The image source controller should always return a valid array.");

         return resultImage;
         }

      private byte[, ,] ProcessImage(object state)
         {
         ImageProcessingAsyncData imageProcessingAsyncData = state as ImageProcessingAsyncData;
         Task<byte[, ,]> previousTask = imageProcessingAsyncData.TaskByte;
         List<Tuple<IImageProcessingController, IRawPluginModel>> imageProcessingController = imageProcessingAsyncData.ImageProcessingControllers;
         List<byte[]> overlays = imageProcessingAsyncData.Overlays;

         Debug.Assert(imageProcessingController.Count == 1, "For now this list should only contain one item.");

         Debug.Assert(previousTask != null, "This task should always exist as it gives the next image source.");
         previousTask.Wait();

         byte[, ,] previousResult = previousTask.Result;

         previousTask.Dispose();

         // Prepare overlay for next processing
         int width = previousResult.GetLength(1);
         int height = previousResult.GetLength(0);

         // Allocated enough for RGBA format
         byte[] overlay = new byte[width * height * 4];

         overlays.Add(overlay);

         Debug.Assert(overlays.Count == 1, "For now this list should only contain one item.");

         byte[, ,] resultImage = imageProcessingController[0].Item1.ProcessImageData(previousResult, overlay, imageProcessingController[0].Item2);

         imageProcessingAsyncData.Clear();

         return resultImage;
         }

      private void DisplayNextImage(Task<byte[, ,]> parentTask, object state)
         {
         parentTask.Dispose();

         Debug.Assert(Task.CurrentId != null, "There's a problem with the current Task.");
         Debug.Assert(this != null, "Protect the next assert.");
         Debug.Assert(this.lastDisplayNextImageTask != null, "The last display task should not be set to null if another task is still running.");

         bool lastUpdateQueued;

         if (Task.CurrentId == this.lastDisplayNextImageTask.Id)
            {
            this.lastDisplayNextImageTask = null;
            this.lastFetchNextImageFromSourceTask = null;

            lastUpdateQueued = true;
            }
         else
            {
            lastUpdateQueued = false;
            }

         ImageProcessingAsyncData imageProcessingAsyncData = state as ImageProcessingAsyncData;

         // Keep a copy of the last source image data in case the image source controller gets closed
         this.imageModel.SourceImageData = imageProcessingAsyncData.TaskByte.Result;

         imageProcessingAsyncData.TaskByte.Dispose();

         // This method and the closing event should run on the main thread so there is no potential concurrency issue
         if (!this.closing)
            {
            Debug.Assert(parentTask.Status == TaskStatus.RanToCompletion, string.Format("The parent task should be completed. TaskStatus: {0}", parentTask.Status.ToString()));

            byte[] overlay = null;

            if (imageProcessingAsyncData.Overlays != null)
               {
               Debug.Assert(imageProcessingAsyncData.Overlays.Count == 1, "For now only one overlay is supported.");

               overlay = imageProcessingAsyncData.Overlays[0];
               }

            this.UpdateDisplayImageData(parentTask.Result, overlay, lastUpdateQueued);

            if (this.DisplayUpdated != null)
               {
               this.DisplayUpdated(this, new DisplayUpdateEventArgs(imageProcessingAsyncData.RawPluginModel));
               }

            // This must be done after calling the DisplayUpdated event
            this.ClearPluginControllers(imageProcessingAsyncData);
            }
         else
            {
            // The close is the responsibility of the ImageController
            if (this.lastDisplayNextImageTask == null)
               {
               this.imageSourceController.Disconnected();

               this.ClearPluginControllers(imageProcessingAsyncData);

               this.Close();
               }
            else
               {
               this.ClearPluginControllers(imageProcessingAsyncData);
               }
            }

         imageProcessingAsyncData.Clear();
         }

      private void ClearPluginControllers(ImageProcessingAsyncData imageProcessingAsyncData)
         {
         // Manage closing image source controller, this must be done after calling the DisplayUpdated event
         this.ManageClosingPluginController(imageProcessingAsyncData.ImageSourceController);

         if (imageProcessingAsyncData.ImageProcessingControllers != null)
            {
            // Manage any closing image processing controller, this must be done after calling the DisplayUpdated event
            foreach (Tuple<IImageProcessingController, IRawPluginModel> tuple in imageProcessingAsyncData.ImageProcessingControllers)
               {
               this.ManageClosingPluginController(tuple.Item1);
               }
            }
         }

      private void ManageClosingPluginController(IPluginController pluginController)
         {
         this.asyncPluginControllers[pluginController]--;

         if (this.asyncPluginControllers[pluginController] == 0)
            {
            pluginController.Closing -= this.PluginController_Closing;

            if (this.closingPluginControllers.Contains(pluginController))
               {
               if (this.imageSourceController == pluginController)
                  {
                  IImageSourceController previousImageSourceController = pluginController as IImageSourceController;
                  IMemorySourceController memorySourceController = this.serviceLocator.GetInstance<IMemorySourceController>();

                  memorySourceController.ImageData = this.imageModel.SourceImageData;

                  // Replace currently closing image source controller by a memory controller holding the last know source image
                  this.InitializeImageSourceController(memorySourceController, memorySourceController.RawPluginModel);

                  previousImageSourceController.Disconnected();
                  }

               pluginController.Close();

               this.closingPluginControllers.Remove(pluginController);
               }

            this.asyncPluginControllers.Remove(pluginController);
            }
         }

      private void UpdateDisplayImageData(byte[, ,] imageData, byte[] overlayData, bool forced)
         {
         // Do not clone the result here. It is the responsibility of the IImageSourceController to return the same (unmodified) image
         // or a new image
         if (this.imageModel.DisplayImageData != imageData || this.imageModel.OverlayImageData != overlayData)
            {
            long lastDisplayUpdateMilliseconds = this.lastDisplayUpdate.ElapsedMilliseconds;

            if (lastDisplayUpdateMilliseconds > this.updatePeriod || forced)
               {
               this.imageModel.DisplayImageData = imageData;
               this.imageModel.OverlayImageData = overlayData;
               ////this.imageView.UpdateDisplay();

               this.lastDisplayUpdate = Stopwatch.StartNew();
               }
            else
               {
               lastDisplayUpdateMilliseconds = 0;
               }
            }
         }
      */
      /*
      private void ImageView_ZoomLevelIncreased(object sender, EventArgs e)
         {
         this.imageModel.ZoomLevel *= 2.0;

         ////this.imageView.UpdateZoomLevel();
         }

      private void ImageView_ZoomLevelDecreased(object sender, EventArgs e)
         {
         this.imageModel.ZoomLevel *= 0.5;

         ////this.imageView.UpdateZoomLevel();
         }

      private void ImageView_PixelViewChanged(object sender, PixelViewChangedEventArgs e)
         {
         int gray = 0;
         int[] rgb = null;
         double[] hsv = null;

         if (this.imageModel.IsGrayscale)
            {
            gray = this.imageModel.DisplayImageData[e.PixelPosition.Y, e.PixelPosition.X, 0];
            }
         else
            {
            rgb = new int[3];
            hsv = new double[3];

            rgb[0] = this.imageModel.DisplayImageData[e.PixelPosition.Y, e.PixelPosition.X, 0];
            rgb[1] = this.imageModel.DisplayImageData[e.PixelPosition.Y, e.PixelPosition.X, 1];
            rgb[2] = this.imageModel.DisplayImageData[e.PixelPosition.Y, e.PixelPosition.X, 2];

            int maximumColorValues = Math.Max(Math.Max(rgb[0], rgb[1]), rgb[2]);

            hsv[2] = maximumColorValues;

            if (hsv[2] == 0)
               {
               hsv[0] = 0;
               hsv[1] = 0;
               }
            else
               {
               int minimumColorValues = Math.Min(Math.Min(rgb[0], rgb[1]), rgb[2]);
               int chroma = maximumColorValues - minimumColorValues;

               hsv[1] = chroma / hsv[2];

               if (chroma == 0)
                  {
                  hsv[0] = 0;
                  }
               else
                  {
                  double huePrime;

                  if (maximumColorValues == rgb[0])
                     {
                     huePrime = ((double)(rgb[1] - rgb[2]) / chroma) % 6;
                     }
                  else if (maximumColorValues == rgb[1])
                     {
                     huePrime = ((double)(rgb[2] - rgb[0]) / chroma) + 2;
                     }
                  else
                     {
                     Debug.Assert(maximumColorValues == rgb[2], "There should be no other cases. Helps skip huePrime initialization.");

                     huePrime = ((double)(rgb[0] - rgb[1]) / chroma) + 4;
                     }

                  hsv[0] = Convert.ToInt32(60 * huePrime);

                  if (hsv[0] < 0.0)
                     {
                     hsv[0] += 360;
                     }
                  }
               }
            }

         ////this.imageView.UpdatePixelView(e.PixelPosition, gray, rgb, hsv);
         }

      private void ImageView_SelectionChanged(object sender, ImagingInterface.Controllers.EventArguments.SelectionChangedEventArgs e)
         {
         if (this.SelectionChanged != null)
            {
            this.SelectionChanged(this, new Plugins.EventArguments.SelectionChangedEventArgs(e.PixelPosition, e.Select));
            }
         }
      */
      /*
      private void PluginController_Closing(object sender, CancelEventArgs e)
         {
         IPluginController pluginController = sender as IPluginController;

         this.closingPluginControllers.Add(pluginController);

         e.Cancel = true;

         if (this.imageProcessingControllers.Count > 0)
            {
            IImageProcessingController imageProcessingController = pluginController as IImageProcessingController;

            if (this.imageProcessingControllers[0].Item1 == imageProcessingController)
               {
               this.RemoveImageProcessingController(imageProcessingController, null);
               }
            }
         }
      */
      /*
      private class ImageProcessingAsyncData
         {
         public ImageProcessingAsyncData(IImageSourceController imageSourceController, IRawPluginModel rawPluginModel, Task<byte[, ,]> taskByte, Task waitForFetchNextImageSourceTask, Task lastDisplayNextImageTask)
            {
            this.ImageSourceController = imageSourceController;
            this.RawPluginModel = rawPluginModel;
            this.TaskByte = taskByte;
            this.WaitForFetchNextImageSourceTask = waitForFetchNextImageSourceTask;
            this.LastDisplayNextImageTask = lastDisplayNextImageTask;
            }

         public IImageSourceController ImageSourceController
            {
            get;
            private set;
            }

         public IRawPluginModel RawPluginModel
            {
            get;
            private set;
            }

         public Task<byte[, ,]> TaskByte
            {
            get;
            set;
            }

         public Task WaitForFetchNextImageSourceTask
            {
            get;
            private set;
            }

         public Task LastDisplayNextImageTask
            {
            get;
            set;
            }

         public List<Tuple<IImageProcessingController, IRawPluginModel>> ImageProcessingControllers
            {
            get;
            set;
            }

         public List<byte[]> Overlays
            {
            get;
            set;
            }

         public void Clear()
            {
            this.ImageSourceController = null;
            this.RawPluginModel = null;
            this.TaskByte = null;
            this.WaitForFetchNextImageSourceTask = null;
            this.LastDisplayNextImageTask = null;
            this.ImageProcessingControllers = null;
            this.Overlays = null;
            }
         }*/
   }
}
