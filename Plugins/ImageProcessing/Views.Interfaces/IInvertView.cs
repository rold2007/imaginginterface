// <copyright file="IInvertView.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Views
{
   using System;
   using ImageProcessing.Controllers.EventArguments;
   using ImagingInterface.Plugins;

   public interface IInvertView : IPluginView
      {
      event EventHandler<InvertEventArgs> Invert;
      }
   }
