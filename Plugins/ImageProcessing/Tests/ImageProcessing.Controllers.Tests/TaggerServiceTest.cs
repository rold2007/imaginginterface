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
      public void CloseImage()
      {
         TaggerService taggerService = this.Container.GetInstance<TaggerService>();

         taggerService.CloseImage(null);
      }
   }
}
