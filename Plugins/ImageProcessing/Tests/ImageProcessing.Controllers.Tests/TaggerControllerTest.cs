// <copyright file="TaggerControllerTest.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Controllers.Tests
{
   using System;
   using System.Collections.Generic;
   using System.Drawing;
   using System.Linq;
   using ImageProcessing.Controllers;
   using ImageProcessing.Controllers.Services;
   using ImageProcessing.ObjectDetection;
   using ImagingInterface.Plugins;
   using Moq;
   using Shouldly;
   using Xunit;

   public class TaggerControllerTest : ControllersBaseTest
   {
      private const string LabelName1 = "DummyLabel1";
      private const string LabelName2 = "DummyLabel2";
      private const string DuplicateLabelName = "DummyLabel1";

      private readonly List<string> labels = new List<string> { "DummyLabel1", "DummyLabel2", "DummyLabel3", "DummyLabel4", "DummyLabel5" };
      private readonly List<Point> points = new List<Point>
      {
            new Point(3, 4),
            new Point(5, 6),
            new Point(7, 8)
      };

      public TaggerControllerTest()
      {
         Container.Register<Tagger>();
         Container.Register<TaggerService>();
         Container.Register<TaggerController>();

         Container.Register(() => { return Mock.Of<IImageService>(x => x.ImageSource == Mock.Of<IImageSource>()); });
         Container.RegisterSingleton(() => { return Mock.Of<IImageProcessingManagerService>(); });
      }

      [Fact]
      public void Constructor()
      {
         TaggerController taggerController = this.Container.GetInstance<TaggerController>();
      }

      [Fact]
      public void DisplayName()
      {
         TaggerController taggerController = this.Container.GetInstance<TaggerController>();

         taggerController.DisplayName.ShouldNotBeNullOrEmpty();
         taggerController.DisplayName.ShouldNotBeNullOrWhiteSpace();
         taggerController.DisplayName.ShouldBe("Tagger");
      }

      [Fact]
      public void AddLabel()
      {
         TaggerController taggerController = this.Container.GetInstance<TaggerController>();

         taggerController.Labels.Count().ShouldBe(0);

         taggerController.AddLabel(LabelName1);

         taggerController.Labels.Count().ShouldBe(1);
         taggerController.Labels.First().ShouldBe(LabelName1);

         taggerController.AddLabel(LabelName2);

         taggerController.Labels.Count().ShouldBe(2);
         taggerController.Labels.First().ShouldBe(LabelName1);
         taggerController.Labels.Last().ShouldBe(LabelName2);
      }

      [Fact]
      public void AddDuplicateLabel()
      {
         TaggerController taggerController = this.Container.GetInstance<TaggerController>();

         taggerController.Labels.Count().ShouldBe(0);

         taggerController.AddLabel(LabelName1);

         taggerController.Labels.Count().ShouldBe(1);
         taggerController.Labels.First().ShouldBe(LabelName1);

         taggerController.AddLabel(DuplicateLabelName);

         taggerController.Labels.Count().ShouldBe(1);
         taggerController.Labels.First().ShouldBe(LabelName1);
      }

      [Fact]
      public void RemoveLabel()
      {
         TaggerController taggerController = this.Container.GetInstance<TaggerController>();

         taggerController.Labels.Count().ShouldBe(0);

         taggerController.RemoveLabel(LabelName1);

         taggerController.AddLabel(LabelName1);

         taggerController.Labels.Count().ShouldBe(1);
         taggerController.Labels.First().ShouldBe(LabelName1);

         taggerController.RemoveLabel(LabelName1);

         taggerController.Labels.Count().ShouldBe(0);
      }

      [Fact]
      public void AddPoint()
      {
         TaggerController taggerController = this.Container.GetInstance<TaggerController>();
         IImageProcessingManagerService imageProcessingManagerService = this.Container.GetInstance<IImageProcessingManagerService>();
         IImageService imageService = this.Container.GetInstance<IImageService>();

         taggerController.GetPoints(LabelName1).Count.ShouldBe(0);

         imageProcessingManagerService.ActiveImageService = imageService;

         taggerController.AddPoint(LabelName1, new Point(42, 54));

         taggerController.GetPoints(LabelName1).Count.ShouldBe(1);

         taggerController.GetPoints(LabelName1)[0].X.ShouldBe(42);
         taggerController.GetPoints(LabelName1)[0].Y.ShouldBe(54);

         imageProcessingManagerService.ActiveImageService = Container.GetInstance<IImageService>();

         taggerController.GetPoints(LabelName1).Count().ShouldBe(0);

         taggerController.AddPoint(LabelName1, new Point(6, 9));

         taggerController.GetPoints(LabelName1).Count().ShouldBe(1);

         taggerController.GetPoints(LabelName1)[0].X.ShouldBe(6);
         taggerController.GetPoints(LabelName1)[0].Y.ShouldBe(9);

         imageProcessingManagerService.ActiveImageService = imageService;

         Assert.True(taggerController.AddPoint(LabelName1, new Point(99, 100)));

         taggerController.GetPoints(LabelName1).Count().ShouldBe(2);

         taggerController.GetPoints(LabelName1)[1].X.ShouldBe(99);
         taggerController.GetPoints(LabelName1)[1].Y.ShouldBe(100);

         Assert.False(taggerController.AddPoint(LabelName1, new Point(99, 100)));

         taggerController.GetPoints(LabelName1).Count().ShouldBe(2);
      }

      [Fact]
      public void AddPointNoImage()
      {
         TaggerController taggerController = this.Container.GetInstance<TaggerController>();

         taggerController.GetPoints(LabelName1).Count().ShouldBe(0);

         Assert.Throws<NullReferenceException>(() => { taggerController.AddPoint(string.Empty, Point.Empty); });
      }

      [Fact]
      public void RemovePoint()
      {
         TaggerController taggerController = this.Container.GetInstance<TaggerController>();
         IImageProcessingManagerService imageProcessingManagerService = this.Container.GetInstance<IImageProcessingManagerService>();

         Assert.Throws<NullReferenceException>(() => { taggerController.RemovePoint(string.Empty, new Point(1, 1)); });

         taggerController.AddLabel(LabelName1);

         taggerController.GetPoints(LabelName1).Count.ShouldBe(0);

         Assert.Throws<NullReferenceException>(() => { taggerController.RemovePoint(LabelName1, new Point(1, 1)); });

         imageProcessingManagerService.ActiveImageService = Container.GetInstance<IImageService>();

         taggerController.AddPoint(LabelName1, new Point(42, 54));

         taggerController.GetPoints(LabelName1).Count.ShouldBe(1);

         Assert.False(taggerController.RemovePoint(LabelName1, new Point(1, 1)));

         taggerController.GetPoints(LabelName1).Count.ShouldBe(1);

         Assert.True(taggerController.RemovePoint(LabelName1, new Point(42, 54)));

         taggerController.GetPoints(LabelName1).Count.ShouldBe(0);
      }

      [Fact]
      public void RemoveAllPoint()
      {
         TaggerController taggerController = this.Container.GetInstance<TaggerController>();
         IImageProcessingManagerService imageProcessingManagerService = this.Container.GetInstance<IImageProcessingManagerService>();

         imageProcessingManagerService.ActiveImageService = Container.GetInstance<IImageService>();

         taggerController.AddPoint(LabelName1, new Point(42, 54));

         taggerController.GetPoints(LabelName1).Count.ShouldBe(1);

         taggerController.RemoveAllPoints();

         taggerController.GetPoints(LabelName1).Count.ShouldBe(0);
      }

      [Fact]
      public void Activate()
      {
         TaggerController taggerController = this.Container.GetInstance<TaggerController>();

         taggerController.Activate();
      }

      [Fact]
      public void Close()
      {
         TaggerController taggerController = this.Container.GetInstance<TaggerController>();

         taggerController.Close();
      }

      [Fact]
      public void SelectLabel()
      {
         TaggerController taggerController = this.Container.GetInstance<TaggerController>();

         ////taggerController.SelectedLabel.ShouldBeNull();

         // Cannot select label which has not been added yet
         Assert.Throws<Shouldly.ShouldAssertException>(() => { taggerController.SelectLabel(LabelName1); });

         taggerController.AddLabel(LabelName1);

         taggerController.SelectLabel(LabelName1);

         ////taggerController.SelectedLabel.ShouldBe(LabelName);

         taggerController.SelectLabel(null);

         ////taggerController.SelectedLabel.ShouldBeNull();
      }

      [Fact]
      public void TagColor()
      {
         TaggerController taggerController = this.Container.GetInstance<TaggerController>();

         taggerController.AddLabels(labels);

         HashSet<Color> labelColors = new HashSet<Color>();

         foreach (string label in labels)
         {
            Color tagColor = taggerController.TagColor(label);

            tagColor.ShouldNotBeNull();

            labelColors.Add(tagColor);
         }

         labelColors.Count().ShouldBe(labels.Count());
      }
   }
}
