﻿// <copyright file="InvertController.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Controllers
{
   using System;
   using System.ComponentModel;
   using System.Diagnostics.CodeAnalysis;
   using ImageProcessing.Controllers.Services;

   public class InvertController
   {
      private InvertService invertService;

      public InvertController(InvertService invertService)
      {
         this.invertService = invertService;
      }

      public event CancelEventHandler Closing;

      public event EventHandler Closed;

      public string DisplayName
      {
         get
         {
            return this.invertService.DisplayName;
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
            ////this.invertView.Invert -= this.InvertView_Invert;

            ////this.invertView.Hide();

            ////this.invertView.Close();

            this.Closed?.Invoke(this, EventArgs.Empty);
         }
      }

      public void Invert(bool invert)
      {
         this.invertService.ApplyInvert = invert;
         this.invertService.Invert();

         ////ImageController imageController = this.imageManagerController.GetActiveImage();

         ////if (imageController != null)
         {
            ////if (e.Invert)
            {
               ////imageController.AddImageProcessingController(this, null);
            }

            ////else
            {
               ////imageController.RemoveImageProcessingController(this, null);
            }
         }
      }
   }
}
