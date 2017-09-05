// <copyright file="ImageProcessingPackageWindowsForms.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Views.CompositionRoot
{
   using ImageProcessing.Controllers;
   using ImageProcessing.Controllers.Services;
   using ImageProcessing.ObjectDetection;
   using ImageProcessing.Views;
   using ImagingInterface.Plugins;
   using SimpleInjector;
   using SimpleInjector.Packaging;

   public class ImageProcessingPackageWindowsForms : IPackage
   {
      public void RegisterServices(Container container)
      {
         // Factories
         container.RegisterSingleton<IFileSourceFactory, FileSourceFactory>();

         // Models
         container.Register<RotateService>();

         // Controllers
         container.Register<RotateController>();
         container.Register<InvertController>();
         container.Register<IFileSource, ImageProcessorFileSource>();
         container.Register<CudafyController>();
         container.Register<IMemorySource, MemorySource>();
         container.Register<TaggerController>();
         container.Register<ObjectDetectionManagerController>();
         container.Register<ObjectDetectionController>();

         // Views
         container.Register<RotateView>();
         container.Register<InvertView>();
         container.Register<CudafyView>();
         container.Register<TaggerView>();
         container.Register<ObjectDetectionManagerView>();
         container.Register<ObjectDetectionView>();

         // ObjectDetection
         container.Register<ObjectDetector>();
         container.Register<Tagger>();
      }
   }
}
