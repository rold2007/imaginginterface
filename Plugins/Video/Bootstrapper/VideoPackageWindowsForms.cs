namespace Video.Bootstrapper
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.BootStrapper;
   using SimpleInjector;
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
      }
   }
