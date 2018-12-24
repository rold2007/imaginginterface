// <copyright file="TaggerController.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Controllers
{
   using System.Collections.Generic;
   using System.Drawing;
   using ImageProcessing.Controllers.Services;

   public class TaggerController
   {
      private TaggerService taggerService;

      public TaggerController(TaggerService taggerService)
      {
         this.taggerService = taggerService;
      }

      public IEnumerable<string> Labels
      {
         get
         {
            return taggerService.Labels;
         }
      }

      public string DisplayName
      {
         get
         {
            return taggerService.DisplayName;
         }
      }

      public string SelectedLabel
      {
         get
         {
            return taggerService.SelectedLabel;
         }
      }

      public void Close()
      {
      }

      public void Activate()
      {
         this.taggerService.Activate();
      }

      public void AddLabel(string label)
      {
         AddLabels(new[] { label });
      }

      public void AddLabels(IEnumerable<string> labels)
      {
         taggerService.AddLabels(labels);
      }

      public void RemoveLabel(string label)
      {
         this.RemoveLabels(new[] { label });
      }

      public void SelectLabel(string label)
      {
         this.taggerService.SelectLabel(label);
      }

      public void RemoveLabels(IEnumerable<string> labels)
      {
         this.taggerService.RemoveLabels(labels);
      }

      public bool AddPoint(string label, Point newPoint)
      {
         return taggerService.AddPoint(label, newPoint);
      }

      public bool RemovePoint(string label, Point point)
      {
         return taggerService.RemovePoint(label, point);
      }

      public void RemoveAllPoints()
      {
         taggerService.RemoveAllPoints();
      }

      public IList<Point> GetPoints(string label)
      {
         return this.taggerService.GetPoints(label);
      }

      public Color TagColor(string label)
      {
         return this.taggerService.LabelColors[label];
      }
   }
}
