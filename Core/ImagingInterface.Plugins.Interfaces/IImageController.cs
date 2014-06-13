namespace ImagingInterface.Plugins
   {
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Plugins;
   using ImagingInterface.Plugins.EventArguments;

   public interface IImageController
      {
      event CancelEventHandler Closing;

      event EventHandler Closed;

      event EventHandler<DisplayUpdateEventArgs> DisplayUpdated;

      event EventHandler<SelectionChangedEventArgs> SelectionChanged;

      IRawImageView RawImageView
         {
         get;
         }

      IRawImageModel RawImageModel
         {
         get;
         }

      string FullPath
         {
         get;
         }

      void SetDisplayName(string displayName);

      void InitializeImageSourceController(IImageSourceController imageSourceController, IRawPluginModel rawPluginModel);

      void Close();

      void AddImageProcessingController(IImageProcessingController imageProcessingController, IRawPluginModel rawPluginModel);

      void RemoveImageProcessingController(IImageProcessingController imageProcessingController, IRawPluginModel rawPluginModel);
      }
   }
