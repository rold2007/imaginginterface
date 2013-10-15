namespace ImagingInterface
   {
   using System;
   using System.Windows.Forms;
   using CommonServiceLocator.SimpleInjectorAdapter;
   using ImagingInterface.Controllers;
   using ImagingInterface.Models;
   using ImagingInterface.Views;
   using Microsoft.Practices.ServiceLocation;
   using SimpleInjector;

   public static class Program
      {
      private static SimpleInjectorServiceLocatorAdapter simpleInjectorServiceLocatorAdapter;

      /// <summary>
      /// The main entry point for the application.
      /// </summary>
      [STAThread]
      public static void Main()
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
         ImageViewManagerController imageViewManagerController = new ImageViewManagerController(mainWindow);

         // Need to register singleton instances for all things pertaining MainWindow
         container.RegisterSingle<MainWindow>(mainWindow);
         container.RegisterSingle<IFileView>(mainWindow);
         container.RegisterSingle<IImageViewManager>(mainWindow);
         container.RegisterSingle<IFileController>(fileController);
         container.RegisterSingle<IImageViewManagerController>(imageViewManagerController);

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
