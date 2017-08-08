﻿// <copyright file="ObjectDetectionManagerController.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Controllers
{
   using System;
   using System.ComponentModel;
   using ImageProcessing.Models;
   using ImagingInterface.Plugins;

   public class ObjectDetectionManagerController
      {
      private static readonly string ObjectDetectionDisplayName = "Object detection"; // ncrunch: no coverage
      ////private IObjectDetectionManagerView objectDetectionManagerView;
      private IObjectDetectionManagerModel objectDetectionManagerModel;
      private TaggerController taggerController;
      private ObjectDetectionController objectDetectionController;

      public ObjectDetectionManagerController(ObjectDetectionManagerModel objectDetectionManagerModel, TaggerController taggerController, ObjectDetectionController objectDetectionController)
         {
         ////this.objectDetectionManagerView = objectDetectionManagerView;
         this.objectDetectionManagerModel = objectDetectionManagerModel;
         this.taggerController = taggerController;
         this.objectDetectionController = objectDetectionController;

         this.objectDetectionManagerModel.DisplayName = ObjectDetectionDisplayName;
         }

      public event CancelEventHandler Closing;

      public event EventHandler Closed;

      ////public IRawPluginView RawPluginView
      ////   {
      ////   get
      ////      {
      ////      return this.objectDetectionManagerView;
      ////      }
      ////   }

      public IRawPluginModel RawPluginModel
         {
         get
            {
            return this.objectDetectionManagerModel;
            }
         }

      public bool Active
         {
         get
            {
            return true;
            }
         }

      public void Initialize()
         {
         this.objectDetectionController.Initialize();

         ////this.objectDetectionManagerView.AddView(this.taggerController.RawPluginView);
         ////this.objectDetectionManagerView.AddView(this.objectDetectionController.RawPluginView);

         this.objectDetectionController.SetTagger(this.taggerController);

         // Must initialize the object detection controller first so that all points
         // loaded by the tagger are sent to the object detection controller
         this.taggerController.Initialize();
         }

      public void Close()
         {
         CancelEventArgs cancelEventArgs = new CancelEventArgs();

         this.Closing?.Invoke(this, cancelEventArgs);

         if (!cancelEventArgs.Cancel)
            {
            ////this.objectDetectionManagerView.Hide();

            ////this.objectDetectionManagerView.Close();

            this.taggerController.Close();
            this.objectDetectionController.Close();

            this.Closed?.Invoke(this, EventArgs.Empty);
         }
         }
      }
   }
