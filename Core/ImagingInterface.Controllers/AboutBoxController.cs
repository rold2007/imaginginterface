namespace ImagingInterface.Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Reflection;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Models;
   using ImagingInterface.Views;

   public class AboutBoxController : IAboutBoxController
      {
      private IAboutBoxView aboutBoxView;
      private IAboutBoxModel aboutBoxModel;

      public AboutBoxController(IAboutBoxView aboutBoxView, IAboutBoxModel aboutBoxModel)
         {
         this.InitializeModel(aboutBoxModel);

         this.aboutBoxView = aboutBoxView;
         this.aboutBoxModel = aboutBoxModel;
         }

      public void Show()
         {
         this.aboutBoxView.Display(this.aboutBoxModel);
         }

      private void InitializeModel(IAboutBoxModel aboutBoxModel)
         {
         Assembly executable = Assembly.GetEntryAssembly();

         if (executable == null)
            {
            executable = Assembly.GetExecutingAssembly();
            }

         object[] attributes = executable.GetCustomAttributes(typeof(AssemblyProductAttribute), false);

         aboutBoxModel.ProductName = ((AssemblyProductAttribute)attributes[0]).Product;

         aboutBoxModel.Version = executable.GetName().Version.ToString();

         attributes = executable.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);

         aboutBoxModel.Copyright = ((AssemblyCopyrightAttribute)attributes[0]).Copyright;

         attributes = executable.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);

         aboutBoxModel.ProductDescription = ((AssemblyDescriptionAttribute)attributes[0]).Description;
         }
      }
   }
