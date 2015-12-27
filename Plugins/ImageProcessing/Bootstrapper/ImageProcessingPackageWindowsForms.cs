namespace ImageProcessing.Bootstrapper
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImageProcessing.Controllers;
   using ImageProcessing.Models;
   using ImageProcessing.ObjectDetection;
   using ImageProcessing.Views;
   using ImagingInterface.BootStrapper;
   using ImagingInterface.Plugins;
   using SimpleInjector;
   using SimpleInjector.Diagnostics;

   public class ImageProcessingPackageWindowsForms : IPackageWindowsForms
      {
      public void RegisterServices(Container container)
         {
         // Models
         container.Register<IRotateModel, RotateModel>();
         container.Register<IInvertModel, InvertModel>();
         container.Register<IFileSourceModel, FileSourceModel>();
         container.Register<ICudafyModel, CudafyModel>();
         container.Register<IMemorySourceModel, MemorySourceModel>();
         container.Register<ITaggerModel, TaggerModel>();
         container.Register<IObjectDetectionManagerModel, ObjectDetectionManagerModel>();
         container.Register<IObjectDetectionModel, ObjectDetectionModel>();

         // Controllers
         container.Register<IRotateController, RotateController>();
         container.Register<IInvertController, InvertController>();
         container.Register<IFileSourceController, FileSourceController>();
         container.Register<ICudafyController, CudafyController>();
         container.Register<IMemorySourceController, MemorySourceController>();
         container.Register<ITaggerController, TaggerController>();
         container.Register<IObjectDetectionManagerController, ObjectDetectionManagerController>();
         container.Register<IObjectDetectionController, ObjectDetectionController>();

         // Views
         container.Register<IRotateView, RotateView>();
         container.Register<IInvertView, InvertView>();
         container.Register<ICudafyView, CudafyView>();
         container.Register<ITaggerView, TaggerView>();
         container.Register<IObjectDetectionManagerView, ObjectDetectionManagerView>();
         container.Register<IObjectDetectionView, ObjectDetectionView>();

         // ObjectDetection
         container.Register<IObjectDetector, ObjectDetector>();
         container.Register<ITagger, Tagger>();
         }

      public void SuppressDiagnosticWarning(Container container)
         {
         container.GetRegistration(typeof(ICudafyController)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
         container.GetRegistration(typeof(CudafyController)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
         container.GetRegistration(typeof(ICudafyView)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
         container.GetRegistration(typeof(IInvertView)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
         container.GetRegistration(typeof(IObjectDetectionView)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
         container.GetRegistration(typeof(IObjectDetector)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
         container.GetRegistration(typeof(IObjectDetectionManagerView)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
         container.GetRegistration(typeof(ITaggerView)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
         container.GetRegistration(typeof(IRotateView)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
         }
      }
   }
