namespace ImageProcessing.Bootstrapper
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImageProcessing.Controllers;
   using ImageProcessing.Models;
   using ImageProcessing.Views;
   using ImagingInterface.BootStrapper;
   using SimpleInjector;

   public class ImageProcessingPackageWindowsForms : IPackageWindowsForms
      {
      public void RegisterServices(Container container)
         {
         // Models
         container.Register<IRotateModel, RotateModel>();
         container.Register<IInvertModel, InvertModel>();

         // Controllers
         container.Register<IRotateController, RotateController>();
         container.Register<IInvertController, InvertController>();

         // Views
         container.Register<IRotateView, RotateView>();
         container.Register<IInvertView, InvertView>();
         }
      }
   }
