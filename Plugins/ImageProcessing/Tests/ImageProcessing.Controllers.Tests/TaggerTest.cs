// <copyright file="TaggerTest.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Controllers.Tests
{
   using System.Collections.Generic;
   using System.Drawing;
   using ImageProcessing.ObjectDetection;
   using Shouldly;
   using Xunit;

   public class TaggerTest : ControllersBaseTest
   {
      private readonly List<string> labels = new List<string> { "DummyLabel1", "DummyLabel2", "DummyLabel3", "DummyLabel4", "DummyLabel5" };
      private readonly List<Point> points = new List<Point>
      {
            new Point(3, 4),
            new Point(5, 6),
            new Point(7, 8)
      };

      public TaggerTest()
      {
         Container.Register<Tagger>();
      }

      [Fact]
      public void Constructor()
      {
         Tagger tagger = this.Container.GetInstance<Tagger>();
      }

      [Fact]
      public void DataPoints()
      {
         Tagger tagger = this.Container.GetInstance<Tagger>();

         IReadOnlyDictionary<string, List<Point>> dataPoints = tagger.DataPoints;

         dataPoints.ShouldBeEmpty();

         tagger.AddPoint(labels[0], points[0]);

         dataPoints.ShouldNotBeEmpty();
         dataPoints[labels[0]].ShouldContain(points[0]);

         tagger.AddPoint(labels[1], points[1]);

         dataPoints[labels[0]].ShouldContain(points[0]);
         dataPoints[labels[1]].ShouldContain(points[1]);

         tagger.AddPoint(labels[0], points[2]);

         dataPoints[labels[0]].ShouldContain(points[0]);
         dataPoints[labels[0]].ShouldContain(points[2]);
         dataPoints[labels[1]].ShouldContain(points[1]);

         tagger.RemovePoint(labels[0], points[0]);

         dataPoints[labels[0]].ShouldContain(points[2]);
         dataPoints[labels[1]].ShouldContain(points[1]);

         tagger.RemoveAllPoints();

         dataPoints[labels[0]].ShouldBeEmpty();
         dataPoints[labels[1]].ShouldBeEmpty();
      }

      [Fact]
      public void SavePoints()
      {
         Tagger tagger = this.Container.GetInstance<Tagger>();

         string savedPoints = tagger.SavePoints();

         savedPoints.ShouldBeEmpty();

         tagger.AddPoint(labels[0], points[0]);

         savedPoints = tagger.SavePoints();

         savedPoints.ShouldBe("DummyLabel1,3,4\r\n");
      }

      [Fact]
      public void LoadPoints()
      {
         Tagger tagger = this.Container.GetInstance<Tagger>();

         tagger.DataPoints.ShouldBeEmpty();

         tagger.LoadPoints(labels[0] + "," + points[0].X.ToString() + "," + points[0].Y.ToString() + "\r\n");

         tagger.DataPoints.Keys.ShouldContain(labels[0]);
         tagger.DataPoints[labels[0]].ShouldContain(points[0]);
      }

      [Fact]
      public void AddRemoveLabels()
      {
         Tagger tagger = this.Container.GetInstance<Tagger>();

         tagger.DataPoints.ShouldBeEmpty();

         tagger.AddLabel(labels[0]);
         tagger.AddLabel(labels[1]);

         tagger.RemoveLabel(labels[1]);
         tagger.Labels.ShouldContain(labels[0]);
         tagger.RemoveLabel(labels[0]);
         tagger.Labels.ShouldBeEmpty();
         tagger.DataPoints.ShouldBeEmpty();

         tagger.AddLabels(labels);
         tagger.Labels.ShouldContain(labels[0]);
         tagger.Labels.ShouldContain(labels[1]);
         tagger.Labels.ShouldContain(labels[2]);
         tagger.DataPoints[labels[0]].ShouldBeEmpty();
         tagger.DataPoints[labels[1]].ShouldBeEmpty();
         tagger.DataPoints[labels[2]].ShouldBeEmpty();

         tagger.RemoveLabels(labels);
         tagger.Labels.ShouldBeEmpty();
         tagger.DataPoints.ShouldBeEmpty();
      }
   }
}
