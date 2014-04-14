namespace ImagingInterface.Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Models;
   using ImagingInterface.Plugins;
   using ImagingInterface.Views;

   public interface IImageController
      {
      event CancelEventHandler Closing;

      event EventHandler Closed;

      event EventHandler LiveUpdateStopped;

      IRawImageView RawImageView
         {
         get;
         }

      IRawImageModel RawImageModel
         {
         get;
         }

      bool CanLiveUpdate
         {
         get;
         }

      void InitializeImageSourceController(IImageSourceController imageSourceController, IRawPluginModel rawPluginModel);

      void Close();

      void StartLiveUpdate();

      void StopLiveUpdate();

      void AddImageProcessingController(IPluginController pluginController, IImageProcessingController imageProcessingController, IRawPluginModel rawPluginModel);
      }
   }
