namespace Video.Bootstrapper
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.BootStrapper;
   using SimpleInjector;
   using SimpleInjector.Diagnostics;
   using Video.Controllers;
   using Video.Models;
   using Video.Views;

   public class VideoPackageWindowsForms : IPackageWindowsForms
      {
      public void RegisterServices(Container container)
         {
         // Models
         container.Register<ICaptureModel, CaptureModel>();

         // Controllers
         container.Register<ICaptureController, CaptureController>();
         container.Register<ICaptureWrapper, CaptureWrapper>();

         // Views
         container.Register<ICaptureView, CaptureView>();
         }

      public void SuppressDiagnosticWarning(Container container)
         {
         container.GetRegistration(typeof(ICaptureController)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
         container.GetRegistration(typeof(CaptureController)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
         container.GetRegistration(typeof(ICaptureView)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
         container.GetRegistration(typeof(ICaptureWrapper)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
         }
      }
   }
