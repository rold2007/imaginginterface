// <copyright file="AboutBoxController.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers
{
   using ImagingInterface.Controllers.Services;

   public class AboutBoxController
   {
      private ApplicationPropertiesService applicationPropertiesService;

      public AboutBoxController(ApplicationPropertiesService applicationPropertiesService)
      {
         this.applicationPropertiesService = applicationPropertiesService;
      }

      public ProductInformations Product
      {
         get
         {
            ProductInformations productInformations = new ProductInformations()
            {
               ProductName = this.applicationPropertiesService.ProductName,
               Version = this.applicationPropertiesService.Version,
               Copyright = this.applicationPropertiesService.Copyright,
               ProductDescription = this.applicationPropertiesService.ProductDescription
            };

            return productInformations;
         }
      }

      public struct ProductInformations
      {
         public string ProductName;
         public string Version;
         public string Copyright;
         public string ProductDescription;
      }
   }
}
