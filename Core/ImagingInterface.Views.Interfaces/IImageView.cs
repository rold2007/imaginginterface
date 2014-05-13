namespace ImagingInterface.Views
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Models;
   using ImagingInterface.Plugins;

   public interface IImageView : IRawImageView
      {
      double UpdateFrequency
         {
         get;
         }

      void AssignImageModel(IImageModel imageModel);

      void UpdateDisplay();

      void Hide();

      void Close();
      }
   }
