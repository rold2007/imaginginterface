namespace ImagingInterface.Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Models;
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

      byte[,,] ImageData
         {
         get;
         }

      bool CanLiveUpdate
         {
         get;
         }

      void LoadImage(byte[,,] imageData, string displayName);

      bool LoadImage(string file);

      void UpdateImageData(byte[,,] imageData);

      void Close();

      void StartLiveUpdate(IImageSourceController imageSourceController);

      void StopLiveUpdate();
      }
   }
