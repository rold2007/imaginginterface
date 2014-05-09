namespace ImageProcessing.Controllers.Tests.Views
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
      private AutoResetEvent displayUpdated = new AutoResetEvent(false);

      public IImageModel AssignedImageModel
         {
         get;
         private set;
         }

      public double UpdateFrequency
         {
         get
            {
            return 0.0;
            }
         }

      public void UpdateDisplay()
         {
         this.displayUpdated.Set();
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

      public void WaitForDisplayUpdate()
         {
         this.displayUpdated.WaitOne();
         }
      }
   }
