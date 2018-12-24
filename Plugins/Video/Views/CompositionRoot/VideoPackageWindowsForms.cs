// <copyright file="VideoPackageWindowsForms.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace Video.Views.CompositionRoot
{
   using SimpleInjector;
   using SimpleInjector.Diagnostics;
   using SimpleInjector.Packaging;
   using Video.Controllers;
   using Video.Views;

   public class VideoPackageWindowsForms : IPackage
   {
      public void RegisterServices(Container container)
      {
         // Controllers
         container.Register<CaptureController>();
         container.Register<CaptureWrapper>();

         // Views
         container.Register<CaptureView>();

         InstanceProducer instanceProducer = container.GetRegistration(typeof(CaptureWrapper));

         instanceProducer.Registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Managed by the application.");
      }
   }
}
