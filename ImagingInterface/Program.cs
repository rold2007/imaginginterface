namespace ImagingInterface.Views
   {
   using System;
   using System.Collections.Generic;
   using System.Diagnostics;
   using System.Linq;
   using System.Threading.Tasks;
   using System.Windows.Forms;
   using CommonServiceLocator.SimpleInjectorAdapter;
   using SimpleInjector;
   using SimpleInjector.Extensions;
   using ImagingInterface.Controllers;
   using ImagingInterface.Models;
   using Microsoft.Practices.ServiceLocation;

   static class Program
      {
      private static SimpleInjectorServiceLocatorAdapter simpleInjectorServiceLocatorAdapter;

      /// <summary>
      /// The main entry point for the application.
      /// </summary>
      [STAThread]
      static void Main()
         {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);

         Program.Bootstrap();
         
         Application.Run(Program.GetServiceLocator().GetInstance<MainWindow>());
         }

      private static void Bootstrap()
         {
         Container container = new Container();
         Program.simpleInjectorServiceLocatorAdapter = new SimpleInjectorServiceLocatorAdapter(container);
         ServiceLocator.SetLocatorProvider(Program.GetServiceLocator);
         MainWindow mainWindow = new MainWindow();
         FileController fileController = new FileController(mainWindow);

         // Need to register singleton instances for all things pertaining MainWindow
         container.RegisterSingle<MainWindow>(mainWindow);
         container.RegisterSingle<IFileView>(mainWindow);
         container.RegisterSingle<IImageViewManager>(mainWindow);
         container.RegisterSingle<IFileController>(fileController);

         // Views
         container.Register<IImageView, ImageView>();

         // Controllers
         container.Register<IImageController, ImageController>();

         // Models
         container.Register<IImageModel, ImageModel>();

         container.Verify();
         }

      private static IServiceLocator GetServiceLocator()
         {
         return Program.simpleInjectorServiceLocatorAdapter;
         }
      }
   }
