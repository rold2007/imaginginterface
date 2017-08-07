// <copyright file="TaggerModel.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Models
{
   using System.Collections.Generic;
   using System.Drawing;

   public class TaggerModel : ITaggerModel
      {
      public TaggerModel()
         {
         }

      public string DisplayName
         {
         get;
         set;
         }

      public string AddedLabel
         {
         get;
         set;
         }

      public string SelectedLabel
         {
         get;
         set;
         }

      public SortedSet<string> Labels
         {
         get;
         set;
         }

      public SortedList<string, Color> LabelColors
         {
         get;
         set;
         }

      public object Clone()
         {
         return this.MemberwiseClone();
         }
      }
   }
