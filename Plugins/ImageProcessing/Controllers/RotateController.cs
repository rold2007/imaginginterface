// <copyright file="RotateController.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Controllers
{
   using System;
   using System.ComponentModel;
   using System.Diagnostics.CodeAnalysis;
   using ImageProcessing.Controllers.Services;

   public class RotateController
   {
      private RotateService rotateService;

      public RotateController(RotateService rotateService)
      {
         this.rotateService = rotateService;
      }

      public event CancelEventHandler Closing;

      public event EventHandler Closed;

      public string DisplayName
      {
         get
         {
            return this.rotateService.DisplayName;
         }
      }

      [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Will be fixed when done refactoring.")]
      public void Initialize()
      {
      }

      public void Close()
      {
         CancelEventArgs cancelEventArgs = new CancelEventArgs();

         this.Closing?.Invoke(this, cancelEventArgs);

         if (!cancelEventArgs.Cancel)
         {
            ////this.rotateView.Rotate -= this.RotateView_Rotate;

            ////this.rotateView.Hide();

            ////this.rotateView.Close();

            this.Closed?.Invoke(this, EventArgs.Empty);
         }
      }

      public void SetRotationAngle(double angle)
      {
         if (this.rotateService.Angle != angle)
         {
            this.rotateService.Angle = angle;

            // Force to apply a rotation
            this.rotateService.Rotate();

            // ImageController imageController = this.imageManagerController.GetActiveImage();

            ////if (imageController != null)
            {
               ////imageController.AddImageProcessingController(this, this.rotateModel.Clone() as IRawPluginModel);
            }
         }
      }
   }
}
