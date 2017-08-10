// <copyright file="IPluginController.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Plugins
{
   public interface IPluginController
      {
      bool Active
         {
         get;
         }

      void Initialize();

      void Close();
      }
   }
