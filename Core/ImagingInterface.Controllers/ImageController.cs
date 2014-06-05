namespace ImagingInterface.Controllers
   {
   using System;
   using System.Collections;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Diagnostics;
   using System.Drawing;
   using System.Threading;
   using System.Threading.Tasks;
   using ImagingInterface.Models;
   using ImagingInterface.Plugins;
   using ImagingInterface.Plugins.EventArguments;
   using ImagingInterface.Views;
   using ImagingInterface.Views.EventArguments;
   using Microsoft.Practices.ServiceLocation;

   public class ImageController : IImageController
      {
      private IImageView imageView;
      private IImageModel imageModel;
      private IServiceLocator serviceLocator;
      private bool closing;
      private bool closed;
      private Dictionary<IImageProcessingController, IRawPluginModel> imageProcessingControllers;
      private Task<byte[, ,]> lastFetchNextImageFromSourceTask;
      private Task lastDisplayNextImageTask;
      private IImageSourceController imageSourceController;
      private IRawPluginModel imageSourceRawPluginModel;
      private Stopwatch lastDisplayUpdate;
      private double updatePeriod;

      public ImageController(IImageView imageView, IImageModel imageModel, IServiceLocator serviceLocator)
         {
         this.imageView = imageView;
         this.imageModel = imageModel;
         this.serviceLocator = serviceLocator;
         this.imageProcessingControllers = new Dictionary<IImageProcessingController, IRawPluginModel>();
         this.lastFetchNextImageFromSourceTask = null;
         this.lastDisplayNextImageTask = null;

         this.imageView.AssignImageModel(this.imageModel);

         this.lastDisplayUpdate = Stopwatch.StartNew();

         double updateFrequency = this.imageView.UpdateFrequency;

         if (updateFrequency != 0.0)
            {
            this.updatePeriod = 1000 / this.imageView.UpdateFrequency;
            }
         else
            {
            this.updatePeriod = -1.0;
            }

         this.closed = false;

         this.imageModel.DisplayImageData = new byte[1, 1, 1];
         this.imageModel.ZoomLevel = 1.0;

         this.imageView.ZoomLevelIncreased += this.ImageView_ZoomLevelIncreased;
         this.imageView.ZoomLevelDecreased += this.ImageView_ZoomLevelDecreased;
         this.imageView.PixelViewChanged += this.ImageView_PixelViewChanged;
         }

      ~ImageController()
         {
         }

      public event CancelEventHandler Closing;

      public event EventHandler Closed;

      public event EventHandler<DisplayUpdateEventArgs> DisplayUpdated;

      public IRawImageView RawImageView
         {
         get
            {
            return this.imageView;
            }
         }

      public IRawImageModel RawImageModel
         {
         get
            {
            return this.imageModel;
            }
         }

      public void SetDisplayName(string displayName)
         {
         this.imageModel.DisplayName = displayName;
         }

      public void InitializeImageSourceController(IImageSourceController imageSourceController, IRawPluginModel rawPluginModel)
         {
         this.imageSourceController = imageSourceController;
         this.imageSourceRawPluginModel = rawPluginModel;

         // If an assert pops here while running a unit test with NCrunch, you need to use
         // the AsynchronousTestRunner class to initialize a proper SynchronizationContext
         this.CreateDynamicUpdateTasks(TaskScheduler.FromCurrentSynchronizationContext());
         }

      public void Close()
         {
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
                  // Prevent calling the Closed event more than once
                  this.closed = true;

                  this.imageView.Hide();
                  this.imageView.Close();
                  this.imageModel.DisplayImageData = null;

                  if (this.Closed != null)
                     {
                     this.Closed(this, EventArgs.Empty);
                     }

                  // The image view is used by some Closed events
                  this.imageView = null;
                  }
               }
            }
         }

      public void AddImageProcessingController(IPluginController pluginController, IImageProcessingController imageProcessingController, IRawPluginModel rawPluginModel)
         {
         if (!this.closing)
            {
            // For now, only one processing controller is supported. In the future, it should be possible to "pin" some
            // image processing controllers and overwrite any unpinned duplicate.
            this.imageProcessingControllers.Clear();

            this.imageProcessingControllers.Add(imageProcessingController, rawPluginModel);

            // If an assert pops here while running a unit test with NCrunch, you need to use
            // the AsynchronousTestRunner class to initialize a proper SynchronizationContext
            this.CreateLiveUpdateTask(TaskScheduler.FromCurrentSynchronizationContext(), false);
            }
         }

      public void RemoveImageProcessingController(IPluginController pluginController, IImageProcessingController imageProcessingController, IRawPluginModel rawPluginModel)
         {
         if (!this.closing)
            {
            // For now, only one processing controller is supported. In the future, it should be possible to "pin" some
            // image processing controllers and overwrite any unpinned duplicate.
            this.imageProcessingControllers.Clear();

            // If an assert pops here while running a unit test with NCrunch, you need to use
            // the AsynchronousTestRunner class to initialize a proper SynchronizationContext
            this.CreateLiveUpdateTask(TaskScheduler.FromCurrentSynchronizationContext(), false);
            }
         }

      private void CreateDynamicUpdateTasks(TaskScheduler taskScheduler)
         {
         bool isDynamic = this.imageSourceController.IsDynamic(this.imageSourceRawPluginModel);

         this.CreateLiveUpdateTask(taskScheduler, isDynamic);
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

         MutableTuple tuple = new MutableTuple(this.imageSourceController, this.imageSourceRawPluginModel, null, waitForFetchNextImageSourceTask, null);

         fetchNextImageFromSourceTask = new Task<byte[, ,]>(this.FetchNextImageFromSource, tuple);

         fetchNextImageFromSourceTask.Start(TaskScheduler.Default);

         this.lastFetchNextImageFromSourceTask = fetchNextImageFromSourceTask;

         if (this.imageProcessingControllers.Count > 0)
            {
            imageSourceTask = this.CreateProcessingTasks(fetchNextImageFromSourceTask);
            }
         else
            {
            imageSourceTask = fetchNextImageFromSourceTask;
            }

         MutableTuple waitForDisplayNextImageTuple = new MutableTuple(null, null, imageSourceTask, this.lastDisplayNextImageTask, null);

         Task<byte[, ,]> waitForDisplayNextImageTask = new Task<byte[, ,]>(this.WaitForDisplayNextImage, waitForDisplayNextImageTuple);
         Task displayNextImageTask = waitForDisplayNextImageTask.ContinueWith(this.DisplayNextImage, this.imageSourceRawPluginModel, taskScheduler);

         waitForDisplayNextImageTask.Start(TaskScheduler.Default);

         this.lastDisplayNextImageTask = displayNextImageTask;
         }

      private Task<byte[, ,]> CreateProcessingTasks(Task<byte[, ,]> taskInput)
         {
         Debug.Assert(this.imageProcessingControllers.Count > 0, "CreateProcessingTasks should only be called when there is some image processing to do.");

         Task<byte[, ,]> previousTask = taskInput;
         MutableTuple tuple;

         foreach (KeyValuePair<IImageProcessingController, IRawPluginModel> keyValuePair in this.imageProcessingControllers)
            {
            tuple = new MutableTuple(null, keyValuePair.Value, previousTask, null, keyValuePair.Key);

            previousTask = new Task<byte[, ,]>(this.ProcessImage, tuple);

            previousTask.Start(TaskScheduler.Default);
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
         MutableTuple waitForDisplayNextImageTuple = state as MutableTuple;
         Task<byte[, ,]> fetchNextImageSourceOrProcessImageTask = waitForDisplayNextImageTuple.TaskByte;
         Task displayNextImageTask = waitForDisplayNextImageTuple.Task;

         // Make sure the previous display task is completed to run them in-order
         if (displayNextImageTask != null)
            {
            displayNextImageTask.Wait();
            displayNextImageTask.Dispose();
            }

         // Wait for the image to be ready to be displayed
         fetchNextImageSourceOrProcessImageTask.Wait();

         byte[, ,] imageData = fetchNextImageSourceOrProcessImageTask.Result;

         fetchNextImageSourceOrProcessImageTask.Dispose();

         waitForDisplayNextImageTuple.Clear();

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
         MutableTuple tuple = state as MutableTuple;
         IImageSourceController imageSourceController = tuple.ImageSourceController;
         IRawPluginModel rawPluginModel = tuple.RawPluginModel;
         Task previousFetchNextImageFromSourceTask = tuple.Task;

         previousFetchNextImageFromSourceTask.Wait();

         // Do not clone the result here. It is the responsibility of the IImageSourceController to return the same (unmodified) image
         // or a new image
         byte[, ,] resultImage = imageSourceController.NextImageData(rawPluginModel);

         Debug.Assert(resultImage != null, "The image source controller should always return a valid array.");

         tuple.Clear();

         return resultImage;
         }

      private byte[, ,] ProcessImage(object state)
         {
         MutableTuple tuple = state as MutableTuple;
         Task<byte[, ,]> previousTask = tuple.TaskByte;
         IImageProcessingController imageProcessingController = tuple.ImageProcessingController;
         IRawPluginModel rawPluginModel = tuple.RawPluginModel;

         Debug.Assert(previousTask != null, "This task should always exist as it gives the next image source.");
         previousTask.Wait();

         byte[, ,] previousResult = previousTask.Result;

         previousTask.Dispose();

         byte[, ,] resultImage = imageProcessingController.ProcessImageData(previousResult, rawPluginModel);

         tuple.Clear();

         return resultImage;
         }

      private void DisplayNextImage(Task<byte[, ,]> parentTask, object state)
         {
         parentTask.Dispose();

         Debug.Assert(Task.CurrentId != null, "There's a problem with the current Task.");
         Debug.Assert(this != null, "Protect the next assert.");
         Debug.Assert(this.lastDisplayNextImageTask != null, "The last display task should not be set to null if another task is still running.");

         bool isLastUpdateQueued;

         if (Task.CurrentId == this.lastDisplayNextImageTask.Id)
            {
            this.lastDisplayNextImageTask = null;
            this.lastFetchNextImageFromSourceTask = null;

            isLastUpdateQueued = true;
            }
         else
            {
            isLastUpdateQueued = false;
            }

         // This method and the closing event should run on the main thread so there is no potential concurrency issue
         if (!this.closing)
            {
            Debug.Assert(parentTask.Status == TaskStatus.RanToCompletion, string.Format("The parent task should be completed. TaskStatus: {0}", parentTask.Status.ToString()));

            this.UpdateDisplayImageData(parentTask.Result, isLastUpdateQueued);

            if (this.DisplayUpdated != null)
               {
               this.DisplayUpdated(this, new DisplayUpdateEventArgs((IRawPluginModel)state));
               }
            }
         else
            {
            // The close is the responsibility of the ImageController
            if (this.lastDisplayNextImageTask == null)
               {
               this.Close();
               }
            }
         }

      private void UpdateDisplayImageData(byte[, ,] imageData, bool forced)
         {
         // Do not clone the result here. It is the responsibility of the IImageSourceController to return the same (unmodified) image
         // or a new image
         if (this.imageModel.DisplayImageData != imageData)
            {
            long lastDisplayUpdateMilliseconds = this.lastDisplayUpdate.ElapsedMilliseconds;

            if (lastDisplayUpdateMilliseconds > this.updatePeriod || forced)
               {
               this.imageModel.DisplayImageData = imageData;
               this.imageView.UpdateDisplay();

               this.lastDisplayUpdate = Stopwatch.StartNew();
               }
            else
               {
               lastDisplayUpdateMilliseconds = 0;
               }
            }
         }

      private void ImageView_ZoomLevelIncreased(object sender, EventArgs e)
         {
         this.imageModel.ZoomLevel *= 2.0;

         this.imageView.UpdateZoomLevel();
         }

      private void ImageView_ZoomLevelDecreased(object sender, EventArgs e)
         {
         this.imageModel.ZoomLevel *= 0.5;

         this.imageView.UpdateZoomLevel();
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

         this.imageView.UpdatePixelView(e.PixelPosition, gray, rgb, hsv);
         }

      private class MutableTuple
         {
         public MutableTuple(IImageSourceController imageSourceController, IRawPluginModel rawPluginModel, Task<byte[, ,]> taskByte, Task task, IImageProcessingController imageProcessingController)
            {
            this.ImageSourceController = imageSourceController;
            this.RawPluginModel = rawPluginModel;
            this.TaskByte = taskByte;
            this.Task = task;
            this.ImageProcessingController = imageProcessingController;
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
            private set;
            }

         public Task Task
            {
            get;
            private set;
            }

         public IImageProcessingController ImageProcessingController
            {
            get;
            private set;
            }

         public void Clear()
            {
            this.ImageSourceController = null;
            this.RawPluginModel = null;
            this.TaskByte = null;
            this.Task = null;
            this.ImageProcessingController = null;
            }
         }
      }
   }
