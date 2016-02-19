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
         container.Register<RotateModel>();
         container.Register<InvertModel>();
         container.Register<FileSourceModel>();
         container.Register<CudafyModel>();
         container.Register<MemorySourceModel>();
         container.Register<TaggerModel>();
         container.Register<ObjectDetectionManagerModel>();
         container.Register<ObjectDetectionModel>();

         // Controllers
         container.Register<RotateController>();
         container.Register<InvertController>();
         container.Register<FileSourceController>();
         container.Register<CudafyController>();
         container.Register<MemorySourceController>();
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
         container.Register<IObjectDetector, ObjectDetector>();
         container.Register<ITagger, Tagger>();
         }

      public void SuppressDiagnosticWarning(Container container)
         {
         ////container.GetRegistration(typeof(ICudafyController)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
         ////container.GetRegistration(typeof(CudafyController)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
         ////container.GetRegistration(typeof(ICudafyView)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
         ////container.GetRegistration(typeof(IInvertView)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
         ////container.GetRegistration(typeof(IObjectDetectionView)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
         ////container.GetRegistration(typeof(IObjectDetector)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
         ////container.GetRegistration(typeof(IObjectDetectionManagerView)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
         ////container.GetRegistration(typeof(ITaggerView)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
         ////container.GetRegistration(typeof(IRotateView)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
         }
      }
   }
