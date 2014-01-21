namespace ImagingInterface.Controllers
   {
   using System;
   using System.ComponentModel;
   using System.Diagnostics;
   using System.Drawing;
   using System.Threading;
   using System.Threading.Tasks;
   using ImageMagick;
   using ImagingInterface.Models;
   using ImagingInterface.Views;
   using Microsoft.Practices.ServiceLocation;

   public class ImageController : IImageController
      {
      private IImageView imageView;
      private IImageModel imageModel;
      private IServiceLocator serviceLocator;
      private bool? cancelLiveGrab;
      private bool closing;

      public ImageController(IImageView imageView, IImageModel imageModel, IServiceLocator serviceLocator)
         {
         this.imageView = imageView;
         this.imageModel = imageModel;
         this.serviceLocator = serviceLocator;
         this.cancelLiveGrab = null;
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

      public byte[,,] ImageData
         {
         get
            {
            return this.imageModel.ImageData;
            }
         }

      public bool CanLiveUpdate
         {
         get
            {
            return !this.closing;
            }
         }

      public void LoadImage(byte[,,] imageData, string displayName)
         {
         // Clone the input image so that the internal image memory management stays inside this class
         this.imageModel.ImageData = (byte[,,])imageData.Clone();

         this.imageModel.DisplayName = displayName;
         this.imageView.AssignImageModel(this.imageModel);
         }

      public bool LoadImage(string filename)
         {
         try
            {
            byte[] imageData;
            Size imageSize;

            using (MagickImage magickImage = new MagickImage(filename))
               {
               magickImage.Format = MagickFormat.Rgb;

               imageData = magickImage.ToByteArray();
               imageSize = new Size(magickImage.Width, magickImage.Height);
               }

            this.imageModel.ImageData = new byte[imageSize.Height, imageSize.Width, 3];

            int imageDataIndex = 0;

            for (int y = 0; y < this.imageModel.Size.Height; y++)
               {
               for (int x = 0; x < this.imageModel.Size.Width; x++)
                  {
                  for (int channel = 0; channel < 3; channel++)
                     {
                     this.imageModel.ImageData[y, x, channel] = imageData[imageDataIndex];

                     imageDataIndex++;
                     }
                  }
               }
            }
         catch (ArgumentException)
            {
            return false;
            }
         catch (MagickMissingDelegateErrorException)
            {
            return false;
            }

         this.imageModel.DisplayName = filename;
         this.imageView.AssignImageModel(this.imageModel);

         return true;
         }

      public void UpdateImageData(byte[,,] imageData)
         {
         this.imageModel.ImageData = (byte[,,])imageData.Clone();
         this.imageView.AssignImageModel(this.imageModel);
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
            Debug.Assert(!this.cancelLiveGrab.HasValue, "Each controller is responsible to stop the live update from the closing event.");

            this.closing = true;

            this.imageView.Close();
            this.imageModel.ImageData = null;

            if (this.Closed != null)
               {
               this.Closed(this, EventArgs.Empty);
               }

            this.imageView = null;
            }
         }

      public void StartLiveUpdate(IImageSourceController imageSourceController)
         {
         if (!this.cancelLiveGrab.HasValue && !this.closing)
            {
            this.cancelLiveGrab = false;

            // The ContinueWith task needs to be run on the main thread because it interacts with the UI
            // For some reason FromCurrentSynchronizationContext cannot be called from another thread
            // when debugging with NCrunch so it needs to be used outside and call TaskScheduler.Current
            // inside the ContinueWith task.
            this.CreateLiveGrabTask(imageSourceController, TaskScheduler.FromCurrentSynchronizationContext());
            }
         }

      public void StopLiveUpdate()
         {
         if (this.cancelLiveGrab.HasValue)
            {
            this.cancelLiveGrab = true;
            }
         }

      private void CreateLiveGrabTask(IImageSourceController imageSourceController, TaskScheduler taskScheduler)
         {
         Task<byte[,,]> task = new Task<byte[,,]>(this.FetchNextImageFromSource, imageSourceController);

         task.ContinueWith(this.DisplayNextImage, imageSourceController, taskScheduler);

         // Need to use default task scheduler need when the task is restarted from the ContinueWith action
         task.Start(TaskScheduler.Default);
         }

      private byte[,,] FetchNextImageFromSource(object data)
         {
         IImageSourceController imageSourceController = data as IImageSourceController;

         return imageSourceController.NextImageData();
         }

      private void DisplayNextImage(Task<byte[,,]> parentTask, object data)
         {
         // This method and the closing event should run on the main thread so there is no potential concurrency issue
         if (!this.closing)
            {
            IImageSourceController imageSourceController = data as IImageSourceController;

            this.UpdateImageData(parentTask.Result);

            if (this.cancelLiveGrab.HasValue && this.cancelLiveGrab == false)
               {
               this.CreateLiveGrabTask(imageSourceController, TaskScheduler.Current);
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
      }
   }
