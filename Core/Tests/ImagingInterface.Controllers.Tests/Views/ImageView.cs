namespace ImagingInterface.Controllers.Tests.Views
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
      public IImageModel AssignedImageModel
         {
         get;
         private set;
         }

      public bool DisplayUpdated
         {
         get;
         set;
         }

      public void UpdateDisplay()
         {
         this.DisplayUpdated = true;
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
         while (!this.DisplayUpdated)
            {
            Thread.Sleep(10);
            }

         this.DisplayUpdated = false;
         }
      }
   }
