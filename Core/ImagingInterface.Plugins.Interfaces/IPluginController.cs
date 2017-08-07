// <copyright file="IPluginController.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Plugins
{
   using System;
   using System.ComponentModel;

   public interface IPluginController
      {
      event CancelEventHandler Closing;

      event EventHandler Closed;

      ////IRawPluginView RawPluginView
      ////   {
      ////   get;
      ////   }

      IRawPluginModel RawPluginModel
         {
         get;
         }

      bool Active
         {
         get;
         }

      void Initialize();

      void Close();
      }
   }
