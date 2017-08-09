// <copyright file="IImageView.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Plugins
{
   public interface IImageView
   {
      IImageSource ImageSource
      {
         get;
      }
   }
}
