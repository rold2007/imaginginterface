// <copyright file="TagPointChangedEventArgs.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Controllers.EventArguments
{
   using System;
   using System.Drawing;

   public class TagPointChangedEventArgs : EventArgs
      {
      public TagPointChangedEventArgs(/*ImageController imageController, */string label, Point tagPoint, bool added)
         {
         ////this.ImageController = imageController;
         this.Label = label;
         this.TagPoint = tagPoint;
         this.Added = added;
         }

      ////public ImageController ImageController
      ////   {
      ////   get; // ncrunch: no coverage
      ////   private set;
      ////   }

      public string Label
         {
         get;
         private set;
         }

      public Point TagPoint
         {
         get;
         private set;
         }

      public bool Added
         {
         get;
         private set;
         }
      }
   }
