// <copyright file="ITaggerModel.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Models
{
   using System;
   using System.Collections.Generic;
   using System.Drawing;
   using ImagingInterface.Plugins;

   public interface ITaggerModel : ICloneable
      {
      string AddedLabel
         {
         get;
         set;
         }

      string SelectedLabel
         {
         get;
         set;
         }

      SortedSet<string> Labels
         {
         get;
         set;
         }

      SortedList<string, Color> LabelColors
         {
         get;
         set;
         }
      }
   }
