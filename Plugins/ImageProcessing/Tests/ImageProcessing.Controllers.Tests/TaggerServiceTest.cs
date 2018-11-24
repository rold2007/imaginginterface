// <copyright file="TaggerServiceTest.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Controllers.Tests
{
   using System.Collections.Generic;
   using System.Drawing;
   using System.Linq;
   using ImageProcessing.Controllers.Services;
   using ImageProcessing.ObjectDetection;
   using ImagingInterface.Plugins;
   using Moq;
   using Shouldly;
   using Xunit;

   public class TaggerServiceTest : ControllersBaseTest
   {
      private readonly List<string> labels = new List<string> { "DummyLabel1", "DummyLabel2", "DummyLabel3", "DummyLabel4", "DummyLabel5" };
      private readonly List<Point> points = new List<Point>
      {
            new Point(3, 4),
            new Point(5, 6),
            new Point(7, 8)
      };

      public TaggerServiceTest()
      {
         Mock<IImageService> imageServiceMock = new Mock<IImageService>();
         Mock<IImageSource> imageSourceMock = new Mock<IImageSource>();

         imageServiceMock.SetupGet(x => x.ImageSource).Returns(imageSourceMock.Object);
         imageSourceMock.SetupGet(x => x.OriginalImageData).Returns(new byte[10, 10, 10]);

         Container.Register<Tagger>();
         Container.Register<TaggerService>();
         Container.Register(() => { return imageServiceMock.Object; });

         Container.RegisterSingleton(() => { return Mock.Of<IImageProcessingManagerService>(); });
      }

      [Fact]
      public void Constructor()
      {
         TaggerService taggerService = this.Container.GetInstance<TaggerService>();
      }

      [Fact]
      public void Activate()
      {
         TaggerService taggerService = this.Container.GetInstance<TaggerService>();
         IImageProcessingManagerService imageProcessingManagerService = this.Container.GetInstance<IImageProcessingManagerService>();

         imageProcessingManagerService.ActiveImageProcessingService.ShouldBeNull();

         taggerService.Activate();

         imageProcessingManagerService.ActiveImageProcessingService.ShouldBe(taggerService);
      }

      [Fact]
      public void AddRemoveLabels()
      {
         TaggerService taggerService = this.Container.GetInstance<TaggerService>();

         Should.Throw<System.NullReferenceException>(() => { taggerService.AddLabels(labels); });
         Should.Throw<System.NullReferenceException>(() => { taggerService.RemoveLabels(labels); });

         this.Container.GetInstance<IImageProcessingManagerService>().ActiveImageService = this.Container.GetInstance<IImageService>();

         taggerService.AddLabels(labels);

         taggerService.Labels.Count().ShouldBe(labels.Count);

         taggerService.AddLabels(new[] { labels[0] });

         taggerService.Labels.Count().ShouldBe(labels.Count);

         taggerService.RemoveLabels(new[] { labels[1] });

         taggerService.Labels.Count().ShouldBe(labels.Count - 1);
      }

      [Fact]
      public void AddRemovePoint()
      {
         TaggerService taggerService = this.Container.GetInstance<TaggerService>();

         bool added;
         bool removed;

         Should.Throw<System.NullReferenceException>(() => { added = taggerService.AddPoint(string.Empty, points[0]); });
         Should.Throw<System.NullReferenceException>(() => { taggerService.RemovePoint(string.Empty, points[0]); });

         this.Container.GetInstance<IImageProcessingManagerService>().ActiveImageService = this.Container.GetInstance<IImageService>();

         added = taggerService.AddPoint(string.Empty, points[0]);
         added.ShouldBe(true);

         taggerService.GetPoints(string.Empty).ShouldContain(points[0]);
         taggerService.GetPoints(string.Empty).Count.ShouldBe(1);

         removed = taggerService.RemovePoint(string.Empty, points[0]);
         removed.ShouldBe(true);
         taggerService.GetPoints(string.Empty).Count.ShouldBe(0);
      }

      [Fact]
      public void SelectLabel()
      {
         TaggerService taggerService = this.Container.GetInstance<TaggerService>();

         taggerService.SelectedLabel.ShouldBe(null);

         Should.Throw<System.NullReferenceException>(() => { taggerService.SelectLabel(labels[0]); });

         Should.Throw<System.NullReferenceException>(() => { taggerService.AddLabels(new[] { labels[0] }); });

         this.Container.GetInstance<IImageProcessingManagerService>().ActiveImageService = this.Container.GetInstance<IImageService>();

         taggerService.AddLabels(new[] { labels[0] });

         taggerService.SelectedLabel.ShouldBe(null);

         taggerService.SelectLabel(labels[0]);

         taggerService.SelectedLabel.ShouldBe(labels[0]);

         taggerService.SelectLabel(null);

         taggerService.SelectedLabel.ShouldBe(null);

         taggerService.AddLabels(new[] { labels[1] });

         taggerService.SelectLabel(labels[1]);

         taggerService.SelectedLabel.ShouldBe(labels[1]);

         taggerService.RemoveLabels(new[] { labels[1] });

         taggerService.SelectedLabel.ShouldBe(null);
      }

      [Fact]
      public void SelectPixel()
      {
         TaggerService taggerService = this.Container.GetInstance<TaggerService>();
         this.Container.GetInstance<IImageProcessingManagerService>().ActiveImageService = this.Container.GetInstance<IImageService>();

         taggerService.AddLabels(new[] { labels[1] });

         taggerService.SelectLabel(labels[1]);

         taggerService.GetPoints(labels[1]).ShouldBeEmpty();

         taggerService.SelectPixel(points[0]);

         taggerService.GetPoints(labels[1]).ShouldContain(points[0]);
      }

      [Fact]
      public void ProcessImageData()
      {
         TaggerService taggerService = this.Container.GetInstance<TaggerService>();
         IImageService imageService = this.Container.GetInstance<IImageService>();
         this.Container.GetInstance<IImageProcessingManagerService>().ActiveImageService = imageService;

         int imageWidth = imageService.ImageSource.OriginalImageData.GetLength(1);
         int imageHeight = imageService.ImageSource.OriginalImageData.GetLength(0);
         int imageSize = imageWidth * imageHeight;
         byte[] overlayData = new byte[imageSize * 4];

         taggerService.AddLabels(new[] { labels[2] });

         taggerService.SelectLabel(labels[2]);

         taggerService.SelectPixel(points[0]);

         int pixelOffset = (points[0].Y * imageWidth * 4) + (points[0].X * 4);

         overlayData[pixelOffset].ShouldBe<byte>(0);

         taggerService.ProcessImageData(imageService, overlayData);
      }

      [Fact]
      public void CloseImage()
      {
         TaggerService taggerService = this.Container.GetInstance<TaggerService>();
         IImageService imageService = this.Container.GetInstance<IImageService>();
         IImageProcessingManagerService imageProcessingManagerService = this.Container.GetInstance<IImageProcessingManagerService>();

         imageProcessingManagerService.ActiveImageService = imageService;

         taggerService.Labels.ShouldBeEmpty();
         taggerService.AddLabels(new[] { labels[2] });

         taggerService.CloseImage(imageService);

         taggerService.Labels.ShouldBeEmpty();
      }
   }
}
