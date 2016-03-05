namespace ImagingInterface.Models
   {
   using System.Reflection;

   public class AboutBoxModel : IAboutBoxModel
      {
      public AboutBoxModel()
         {
         this.InitializeApplicationProperties();
         }

      public string ProductName
         {
         get;
         private set;
         }

      public string Version
         {
         get;
         private set;
         }

      public string Copyright
         {
         get;
         private set;
         }

      public string ProductDescription
         {
         get;
         private set;
         }

      private void InitializeApplicationProperties()
         {
         Assembly executable = Assembly.GetEntryAssembly();

         if (executable == null)
            {
            executable = Assembly.GetExecutingAssembly();
            }

         object[] attributes;

         attributes = executable.GetCustomAttributes(typeof(AssemblyProductAttribute), false);

         this.ProductName = ((AssemblyProductAttribute)attributes[0]).Product;
         this.Version = executable.GetName().Version.ToString();

         attributes = executable.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);

         this.Copyright = ((AssemblyCopyrightAttribute)attributes[0]).Copyright;

         attributes = executable.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);

         this.ProductDescription = ((AssemblyDescriptionAttribute)attributes[0]).Description;
         }
      }
   }
