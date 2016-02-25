namespace ImagingInterface.Controllers
   {
   using System.Reflection;
   using ImagingInterface.Models;

   public class AboutBoxController
      {
      public AboutBoxController(AboutBoxModel aboutBoxModel)
         {
         this.InitializeModel(aboutBoxModel);

         this.AboutBoxModel = aboutBoxModel;
         }

      public IAboutBoxModel AboutBoxModel
         {
         get;
         private set;
         }

      private void InitializeModel(AboutBoxModel aboutBoxModel)
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
