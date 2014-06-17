namespace ImageProcessing.Views
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImageProcessing.Models;
   using ImagingInterface.Plugins;

   public interface ITaggerView : IPluginView
      {
      event EventHandler LabelAdded;

      void SetTaggerModel(ITaggerModel taggerModel);

      void UpdateLabelList();
      }
   }
