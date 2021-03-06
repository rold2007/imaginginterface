﻿// <copyright file="TaggerControllerTest.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Controllers.Tests
{
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
      private const string LabelName = "DummyLabel";

      public TaggerControllerTest()
      {
         this.Container.Register<Tagger>();
         this.Container.Register<TaggerService>();
         this.Container.Register<TaggerController>();
         this.Container.Register(() => { return new Mock<IImageProcessingManagerService>().Object; });
         this.Container.Register(() => { return new Mock<IImageSource>().Object; });
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

         TaggerController.DisplayName.ShouldNotBeNullOrEmpty();
         TaggerController.DisplayName.ShouldNotBeNullOrWhiteSpace();
         TaggerController.DisplayName.ShouldBe("Tagger");
      }

      [Fact]
      public void AddLabel()
      {
         TaggerController taggerController = this.Container.GetInstance<TaggerController>();

         taggerController.Labels.Count().ShouldBe(0);

         taggerController.AddLabel(LabelName);

         taggerController.Labels.Count().ShouldBe(1);
         taggerController.Labels.First().ShouldBe(LabelName);
      }

      [Fact]
      public void RemoveLabel()
      {
         TaggerController taggerController = this.Container.GetInstance<TaggerController>();

         taggerController.Labels.Count().ShouldBe(0);

         taggerController.RemoveLabel(LabelName);

         taggerController.AddLabel(LabelName);

         taggerController.Labels.Count().ShouldBe(1);
         taggerController.Labels.First().ShouldBe(LabelName);

         taggerController.RemoveLabel(LabelName);

         taggerController.Labels.Count().ShouldBe(0);
      }

      [Fact]
      public void AddPoint()
      {
         TaggerController taggerController = this.Container.GetInstance<TaggerController>();

         taggerController.GetPoints(LabelName).Count.ShouldBe(0);

         taggerController.AddPoint(LabelName, new Point(42, 54));

         taggerController.GetPoints(LabelName).Count.ShouldBe(1);

         taggerController.GetPoints(LabelName)[0].X.ShouldBe(42);
         taggerController.GetPoints(LabelName)[0].Y.ShouldBe(54);
      }

      [Fact]
      public void RemovePoint()
      {
         TaggerController taggerController = this.Container.GetInstance<TaggerController>();

         taggerController.RemovePoint(string.Empty, new Point(1, 1));

         taggerController.AddLabel(LabelName);

         taggerController.GetPoints(LabelName).Count.ShouldBe(0);

         taggerController.RemovePoint(LabelName, new Point(1, 1));

         taggerController.AddPoint(LabelName, new Point(42, 54));

         taggerController.GetPoints(LabelName).Count.ShouldBe(1);

         taggerController.GetPoints(LabelName)[0].X.ShouldBe(42);
         taggerController.GetPoints(LabelName)[0].Y.ShouldBe(54);

         taggerController.RemovePoint(LabelName, new Point(1, 1));

         taggerController.GetPoints(LabelName).Count.ShouldBe(1);

         taggerController.RemovePoint(LabelName, new Point(42, 54));

         taggerController.GetPoints(LabelName).Count.ShouldBe(0);
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

         taggerController.SelectedLabel.ShouldBeNull();

         // Cannot select label which has not been added yet
         Assert.Throws<Shouldly.ShouldAssertException>(() => { taggerController.SelectLabel(LabelName); });

         taggerController.AddLabel(LabelName);

         taggerController.SelectLabel(LabelName);

         taggerController.SelectedLabel.ShouldBe(LabelName);

         taggerController.SelectLabel(null);

         taggerController.SelectedLabel.ShouldBeNull();
      }

      [Fact]
      public void SelectPixel()
      {
         IImageSource imageSource = this.Container.GetInstance<IImageSource>();
         TaggerController taggerController = this.Container.GetInstance<TaggerController>();

         taggerController.GetPoints(LabelName).Count.ShouldBe(0);

         taggerController.SelectPixel(null, Point.Empty);

         taggerController.GetPoints(LabelName).Count.ShouldBe(0);

         taggerController.AddLabel(LabelName);

         taggerController.SelectPixel(null, Point.Empty);

         taggerController.GetPoints(LabelName).Count.ShouldBe(0);

         taggerController.SelectLabel(LabelName);

         taggerController.SelectPixel(imageSource, new Point(42, 54));

         taggerController.GetPoints(LabelName).Count.ShouldBe(1);

         taggerController.GetPoints(LabelName)[0].X.ShouldBe(42);
         taggerController.GetPoints(LabelName)[0].Y.ShouldBe(54);
      }
   }
}
