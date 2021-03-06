﻿// <copyright file="IPluginView.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Plugins
{
   using System.Drawing;

   public interface IPluginView
   {
      string DisplayName
      {
         get;
      }

      void Hide();

      void Close();

      void SelectPixel(IImageSource imageSource, Point pixelPosition);
   }
}
