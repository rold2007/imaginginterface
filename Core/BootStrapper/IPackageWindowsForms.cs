namespace ImagingInterface.BootStrapper
   {
   using SimpleInjector;
   using SimpleInjector.Packaging;

   public interface IPackageWindowsForms : IPackage
      {
      void SuppressDiagnosticWarning(Container container);
      }
   }
