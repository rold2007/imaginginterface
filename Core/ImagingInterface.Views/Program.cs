namespace ImagingInterface.Views
   {
   using System;
   using System.Collections.Generic;
   using System.Diagnostics;
   using System.Linq;
   using System.Threading.Tasks;
   using System.Windows.Forms;
   using SimpleInjector;
   using SimpleInjector.Extensions;
   using ImagingInterface.Controllers;

   static class Program
      {
      //private static Container container;

      //[DebuggerStepThrough]
      //public static TService GetInstance<TService>() where TService : class
      //   {
      //   return container.GetInstance<TService>();
      //   }

      /// <summary>
      /// The main entry point for the application.
      /// </summary>
      [STAThread]
      static void Main()
         {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(Bootstrap());
         }

      private static MainWindow Bootstrap()
         {
         System.Reflection.Assembly assembly = System.Reflection.Assembly.GetAssembly(typeof(Bootstrapper));

         System.Reflection.Assembly.LoadWithPartialName("ImagingInterface.Controllers.dll");
         //Bootstrapper abc = new Bootstrapper();
         Container container = new Container();

         container.RegisterPackages(new[] { assembly });
         //Container container = Bootstrapper.Initialize();

         //Program.container = new Container();
         //MainWindow mainWindow = new MainWindow();

         container.RegisterSingle<MainWindow>();
         //container.RegisterSingle<IFileView>(mainWindow);
         container.RegisterSingle<IFileView>(() => container.GetInstance<MainWindow>());
         //container.RegisterSingle<IFileView, FileView>();
         container.Register<IImageView, ImageView>();

         container.Verify();

         Extraire Program.cs vers un autre projet ?

         return container.GetInstance<MainWindow>();
         //return mainWindow;
         }
      }
   }
