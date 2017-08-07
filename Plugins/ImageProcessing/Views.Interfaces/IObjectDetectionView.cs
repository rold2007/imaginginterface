// <copyright file="IObjectDetectionView.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Views
{
   using System;
   using ImagingInterface.Plugins;

   public interface IObjectDetectionView : IPluginView
      {
      event EventHandler Train;

      event EventHandler Test;
      }
   }
