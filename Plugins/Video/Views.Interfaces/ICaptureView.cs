// <copyright file="ICaptureView.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace Video.Views
{
   using System;
   using ImagingInterface.Plugins;

   public interface ICaptureView : IPluginView
      {
      event EventHandler Start;

      event EventHandler Stop;

      event EventHandler SnapShot;

      void UpdateLiveGrabStatus(bool allowGrab, bool liveGrabRunning);
      }
   }
