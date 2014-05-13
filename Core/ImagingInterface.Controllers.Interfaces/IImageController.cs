namespace ImagingInterface.Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Controllers.EventArguments;
   using ImagingInterface.Models;
   using ImagingInterface.Plugins;
   using ImagingInterface.Views;

   public interface IImageController
      {
      event CancelEventHandler Closing;

      event EventHandler Closed;

      event EventHandler<DisplayUpdateEventArgs> DisplayUpdated;

      IRawImageView RawImageView
         {
         get;
         }

      IRawImageModel RawImageModel
         {
         get;
         }

      void SetDisplayName(string displayName);

      void InitializeImageSourceController(IImageSourceController imageSourceController, IRawPluginModel rawPluginModel);

      void Close();

      void AddImageProcessingController(IPluginController pluginController, IImageProcessingController imageProcessingController, IRawPluginModel rawPluginModel);

      void RemoveImageProcessingController(IPluginController pluginController, IImageProcessingController imageProcessingController, IRawPluginModel rawPluginModel);
      }
   }
