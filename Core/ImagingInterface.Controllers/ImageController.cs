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
   using ImagingInterface.Views;
   using Microsoft.Practices.ServiceLocation;

   public class ImageController : IImageController
      {
      private IImageView imageView;
      private IImageModel imageModel;
      private IServiceLocator serviceLocator;
      private bool? cancelLiveGrab;
      private bool closing;
      private Dictionary<IImageProcessingController, IRawPluginModel> imageProcessingControllers;
      private Task<byte[, ,]> lastFetchNextImageFromSourceTask;
      private Task lastDisplayNextImageTask;
      private IImageSourceController imageSourceController;
      private IRawPluginModel imageSourceRawPluginModel;

      public ImageController(IImageView imageView, IImageModel imageModel, IServiceLocator serviceLocator)
         {
         this.imageView = imageView;
         this.imageModel = imageModel;
         this.serviceLocator = serviceLocator;
         this.cancelLiveGrab = null;
         this.imageProcessingControllers = new Dictionary<IImageProcessingController, IRawPluginModel>();
         this.lastFetchNextImageFromSourceTask = null;
         this.lastDisplayNextImageTask = null;

         this.imageModel.DisplayImageData = new byte[1, 1, 1];
         this.imageView.AssignImageModel(this.imageModel);
         }

      public event CancelEventHandler Closing;

      public event EventHandler Closed;

      public event EventHandler LiveUpdateStopped;

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

      public bool CanLiveUpdate
         {
         get
            {
            return !this.closing;
            }
         }

      public void InitializeImageSourceController(IImageSourceController imageSourceController, IRawPluginModel rawPluginModel)
         {
         this.imageSourceController = imageSourceController;
         this.imageSourceRawPluginModel = rawPluginModel;
         this.imageModel.DisplayName = imageSourceController.DisplayName(rawPluginModel);

         // If an assert pops here while running a unit test with NCrunch, don't forget to
         // call SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
         // at the beginning of the test
         this.CreateLiveGrabTask(this.imageSourceController, this.imageSourceRawPluginModel, TaskScheduler.FromCurrentSynchronizationContext(), false);
         }

      public void Close()
         {
         CancelEventArgs cancelEventArgs = new CancelEventArgs();

         if (this.Closing != null)
            {
            this.Closing(this, cancelEventArgs);
            }

         this.imageView.Hide();

         if (!cancelEventArgs.Cancel)
            {

            // This is certainly not true anymore. Threads are launched simply by calling InitializeImageSourceController
            //  So we need to make sure to wait for the last DisplayThread to finish AND make sure no new thread is started after that


            Debug.Assert(!this.cancelLiveGrab.HasValue, "Each controller is responsible to stop the live update from the closing event.");

            this.closing = true;

            this.imageView.Close();
            this.imageModel.DisplayImageData = null;

            if (this.Closed != null)
               {
               this.Closed(this, EventArgs.Empty);
               }

            this.imageView = null;
            }
         }

      public void StartLiveUpdate()
         {
         if (!this.cancelLiveGrab.HasValue && !this.closing)
            {
            this.cancelLiveGrab = false;

            // If an assert pops here while running a unit test with NCrunch, don't forget to
            // call SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
            // at the beginning of the test
            this.CreateLiveGrabTask(this.imageSourceController, this.imageSourceRawPluginModel, TaskScheduler.FromCurrentSynchronizationContext(), true);
            }
         }

      public void StopLiveUpdate()
         {
         if (this.cancelLiveGrab.HasValue)
            {
            this.cancelLiveGrab = true;
            }
         }

      public void AddImageProcessingController(IPluginController pluginController, IImageProcessingController imageProcessingController, IRawPluginModel rawPluginModel)
         {
         if (!this.closing)
            {
            //Si il ny a pas de dynamic imagesource, il faut faire un update du display avec processing
            //    Il faut faire un overwrite du processing de meme type (Rotate) pour eviter den accumuler

            //Attention, si on ferme le rotatecontroller on pointe sur un objet disposé... Il faut sabonner au closing du plugincontroller
            // puis faire le cancel et gerer un close dans le display (quand on revient sur la main thread)
            // Il faut donc maintenir une list des tasks en cours pour s'assurer qu'aucune task ne pointe sur cet item
            // Ajouter temporairement des boutons "Close" dans rotate et invert pour tester le close en attendant d'avoir des tabs avec close


            // For now, only one processing controller is supported. In the future, it should be possible to "pin" some
            // image processing controllers and overwrite any unpinned duplicate.
            this.imageProcessingControllers.Clear();

            this.imageProcessingControllers.Add(imageProcessingController, rawPluginModel);

            // If an assert pops here while running a unit test with NCrunch, don't forget to
            // call SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
            // at the beginning of the test
            this.CreateLiveGrabTask(this.imageSourceController, this.imageSourceRawPluginModel, TaskScheduler.FromCurrentSynchronizationContext(), false);
            }
         }

      private void CreateLiveGrabTask(IImageSourceController imageSourceController, IRawPluginModel rawPluginModel, TaskScheduler taskScheduler, bool launchHeartBeat)
         {
         Task<byte[, ,]> fetchNextImageFromSourceTask;
         Task<byte[, ,]> imageSourceTask;
         Tuple<IImageSourceController, IRawPluginModel> tuple = Tuple.Create<IImageSourceController, IRawPluginModel>(imageSourceController, rawPluginModel);
         Task waitForFetchNextImageSourceTask = new Task(this.WaitForNextImageSource, this.lastFetchNextImageFromSourceTask);

         if (launchHeartBeat)
            {
            Tuple<IImageSourceController, IRawPluginModel> startNewHeartBeatTuple = Tuple.Create<IImageSourceController, IRawPluginModel>(imageSourceController, rawPluginModel);

            waitForFetchNextImageSourceTask.ContinueWith(this.StartNewHeartBeat, startNewHeartBeatTuple, taskScheduler);
            }

         // Task needed to synchronize all the FetchNextImageFromSource and make sure only one is ran at any time and that they run in-order
         waitForFetchNextImageSourceTask.Start(TaskScheduler.Default);

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

         Tuple<Task<byte[, ,]>, Task> waitForDisplayNextImageTuple = new Tuple<Task<byte[, ,]>, Task>(imageSourceTask, this.lastDisplayNextImageTask);
         Task<byte[, ,]> waitForDisplayNextImageTask = new Task<byte[, ,]>(this.WaitForDisplayNextImage, waitForDisplayNextImageTuple);
         Task displayNextImageTask = waitForDisplayNextImageTask.ContinueWith(this.DisplayNextImage, null, taskScheduler);

         waitForDisplayNextImageTask.Start(TaskScheduler.Default);

         this.lastDisplayNextImageTask = displayNextImageTask;
         }

      private Task<byte[, ,]> CreateProcessingTasks(Task<byte[, ,]> taskInput)
         {
         Debug.Assert(this.imageProcessingControllers.Count > 0, "CreateProcessingTasks should only be called when there is some image processing to do.");

         Task<byte[, ,]> previousTask = taskInput;
         Tuple<Task<byte[, ,]>, IImageProcessingController, IRawPluginModel> tuple = null;

         foreach (KeyValuePair<IImageProcessingController, IRawPluginModel> keyValuePair in this.imageProcessingControllers)
            {
            tuple = Tuple.Create<Task<byte[, ,]>, IImageProcessingController, IRawPluginModel>(previousTask, keyValuePair.Key, keyValuePair.Value);

            previousTask = new Task<byte[, ,]>(this.ProcessImage, tuple);

            previousTask.Start(TaskScheduler.Default);
            }

         return previousTask;
         }

      //private void CreateLiveHeartBeat(Task<byte[, ,]> task, IImageSourceController imageSourceController, IRawPluginModel rawPluginModel, TaskScheduler taskScheduler)
      //   {
      //   Task heartBeatTask = new Task(this.WaitForNextImageSource, task);

      //   Tuple<IImageSourceController, IRawPluginModel> startNewHeartBeatTuple = Tuple.Create<IImageSourceController, IRawPluginModel>(imageSourceController, rawPluginModel);

      //   heartBeatTask.ContinueWith(this.StartNewHeartBeat, startNewHeartBeatTuple, taskScheduler);

      //   heartBeatTask.Start(TaskScheduler.Default);
      //   }

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
         Tuple<Task<byte[, ,]>, Task> waitForDisplayNextImageTuple = state as Tuple<Task<byte[, ,]>, Task>;
         Task<byte[, ,]> fetchNextImageSourceOrProcessImageTask = waitForDisplayNextImageTuple.Item1;
         Task displayNextImageTask = waitForDisplayNextImageTuple.Item2;

         // Make sure the previous display task is completed to run them in-order
         if (displayNextImageTask != null)
            {
            displayNextImageTask.Wait();
            }

         // Wait for the image to be ready to be displayed
         fetchNextImageSourceOrProcessImageTask.Wait();

         return fetchNextImageSourceOrProcessImageTask.Result;
         }

      private void StartNewHeartBeat(Task parentTask, object state)
         {
         if (!this.closing)
            {
            if (this.cancelLiveGrab.HasValue && this.cancelLiveGrab == false)
               {
               Tuple<IImageSourceController, IRawPluginModel> tuple = state as Tuple<IImageSourceController, IRawPluginModel>;
               IImageSourceController imageSourceController = tuple.Item1;
               IRawPluginModel rawPluginModel = tuple.Item2;

               // TaskScheduler.Current should be the MainThread
               this.CreateLiveGrabTask(imageSourceController, rawPluginModel, TaskScheduler.Current, true);
               }
            else
               {
               this.cancelLiveGrab = null;

               if (this.LiveUpdateStopped != null)
                  {
                  this.LiveUpdateStopped(this, EventArgs.Empty);
                  }
               }
            }
         }

      private byte[, ,] FetchNextImageFromSource(object state)
         {
         Tuple<IImageSourceController, IRawPluginModel> tuple = state as Tuple<IImageSourceController, IRawPluginModel>;
         IImageSourceController imageSourceController = tuple.Item1;
         IRawPluginModel rawPluginModel = tuple.Item2;

         //    Il faudra peut-etre utiliser un canceltoken pour empecher dappeler le NextImageData

         byte[, ,] resultImage = imageSourceController.NextImageData(rawPluginModel);
         byte[, ,] clonedResultImage = null;

         if (resultImage != null)
            {
            clonedResultImage = (byte[, ,])resultImage.Clone();
            }

         return clonedResultImage;
         }

      private byte[, ,] ProcessImage(object state)
         {
         Tuple<Task<byte[, ,]>, IImageProcessingController, IRawPluginModel> tuple = state as Tuple<Task<byte[, ,]>, IImageProcessingController, IRawPluginModel>;
         Task<byte[, ,]> previousTask = tuple.Item1;
         IImageProcessingController imageProcessingController = tuple.Item2;
         IRawPluginModel rawPluginModel = tuple.Item3;

         Debug.Assert(previousTask != null, "This task should always exist as it gives the next image source.");
         previousTask.Wait();

         byte[, ,] resultImage = imageProcessingController.ProcessImageData(previousTask.Result, rawPluginModel);

         return resultImage;
         }

      private void DisplayNextImage(Task<byte[, ,]> parentTask, object state)
         {
         // This method and the closing event should run on the main thread so there is no potential concurrency issue
         if (!this.closing)
            {
            this.UpdateDisplayImageData(parentTask.Result);

            if (Task.CurrentId == this.lastDisplayNextImageTask.Id)
               {
               this.lastDisplayNextImageTask = null;
               this.lastFetchNextImageFromSourceTask = null;
               }
            }
         }

      private void UpdateDisplayImageData(byte[, ,] imageData)
         {
         this.imageModel.DisplayImageData = imageData;
         this.imageView.UpdateDisplay();
         }
      }
   }
