namespace ImagingInterface.Tests.Common.Views
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading;
   using System.Threading.Tasks;
   using ImagingInterface.Models;
   using ImagingInterface.Views;

   public class ImageView : IImageView
      {
      private double updateFrequency = 0.0f;

      public IImageModel AssignedImageModel
         {
         get;
         private set;
         }

      public double UpdateFrequency
         {
         get
            {
            return this.updateFrequency;
            }

         set
            {
            this.updateFrequency = value;
            }
         }

      public void UpdateDisplay()
         {
         }

      public void AssignImageModel(IImageModel imageModel)
         {
         this.AssignedImageModel = imageModel;
         }

      public void Hide()
         {
         }

      public void Close()
         {
         }
      }
   }
