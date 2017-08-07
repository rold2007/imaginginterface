// <copyright file="IPackageWindowsForms.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.BootStrapper
   {
   using SimpleInjector;
   using SimpleInjector.Packaging;

   public interface IPackageWindowsForms : IPackage
      {
      void SuppressDiagnosticWarning(Container container);
      }
   }
