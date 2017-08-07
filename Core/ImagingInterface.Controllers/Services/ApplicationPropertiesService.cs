// <copyright file="ApplicationPropertiesService.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.Services
{
   using System.Reflection;

   public class ApplicationPropertiesService
   {
      private bool initialized;
      private string productName;
      private string version;
      private string copyright;
      private string productDescription;

      public ApplicationPropertiesService()
      {
         this.initialized = false;
      }

      public string ProductName
      {
         get
         {
            this.InitializeApplicationProperties();

            return this.productName;
         }

         private set
         {
            this.productName = value;
         }
      }

      public string Version
      {
         get
         {
            this.InitializeApplicationProperties();

            return this.version;
         }

         private set
         {
            this.version = value;
         }
      }

      public string Copyright
      {
         get
         {
            this.InitializeApplicationProperties();

            return this.copyright;
         }

         private set
         {
            this.copyright = value;
         }
      }

      public string ProductDescription
      {
         get
         {
            this.InitializeApplicationProperties();

            return this.productDescription;
         }

         private set
         {
            this.productDescription = value;
         }
      }

      private void InitializeApplicationProperties()
      {
         if (!this.initialized)
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

            this.initialized = true;
         }
      }
   }
}
