namespace ImagingInterface.BootStrapper
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using SimpleInjector;
   using SimpleInjector.Packaging;

   public interface IPackageWindowsForms : IPackage
      {
      void SuppressDiagnosticWarning(Container container);
      }
   }
