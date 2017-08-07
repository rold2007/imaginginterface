// <copyright file="ITaggerView.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Views
{
   using System;
   using ImageProcessing.Models;
   using ImagingInterface.Plugins;

   public interface ITaggerView : IPluginView
      {
      event EventHandler LabelAdded;

      void SetTaggerModel(ITaggerModel taggerModel);

      void UpdateLabelList();
      }
   }
