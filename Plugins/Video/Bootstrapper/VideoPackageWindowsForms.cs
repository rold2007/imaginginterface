﻿namespace Video.Bootstrapper
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
         container.Register<CaptureModel>();

         // Controllers
         container.Register<CaptureController>();
         container.Register<CaptureWrapper>();

         // Views
         container.Register<CaptureView>();
         }

      public void SuppressDiagnosticWarning(Container container)
         {
         ////container.GetRegistration(typeof(ICaptureController)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
         container.GetRegistration(typeof(CaptureController)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
         container.GetRegistration(typeof(CaptureView)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
         container.GetRegistration(typeof(CaptureWrapper)).Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
      }
   }
   }
