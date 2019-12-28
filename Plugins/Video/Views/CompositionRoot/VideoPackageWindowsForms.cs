// <copyright file="VideoPackageWindowsForms.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace Video.Views.CompositionRoot
{
   using SimpleInjector;
   using SimpleInjector.Packaging;
   using Video.Controllers;
   using Video.Views;

   public class VideoPackageWindowsForms : IPackage
   {
      public void RegisterServices(Container container)
      {
         // Controllers
         container.Register<CaptureController>();

         //// Need to enable the use of CaptureWrapper without implementing IDisposable in the Controller
         ////container.Register<CaptureWrapper>();

         // Views
         container.Register<CaptureView>();
      }
   }
}
