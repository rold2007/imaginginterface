// <copyright file="AboutBoxControllerTest.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.Tests
{
   using ImagingInterface.Controllers.Services;
   using Xunit;

   public class AboutBoxControllerTest : ControllersBaseTest
   {
      [Fact]
      public void Constructor()
      {
         ApplicationPropertiesService applicationPropertiesService = new ApplicationPropertiesService();
         AboutBoxController aboutBoxController = new AboutBoxController(applicationPropertiesService);

         Assert.False(string.IsNullOrEmpty(aboutBoxController.Product.ProductName));
         Assert.False(string.IsNullOrEmpty(aboutBoxController.Product.Version));
         Assert.False(string.IsNullOrEmpty(aboutBoxController.Product.Copyright));
         Assert.False(string.IsNullOrEmpty(aboutBoxController.Product.ProductDescription));
      }
   }
}
