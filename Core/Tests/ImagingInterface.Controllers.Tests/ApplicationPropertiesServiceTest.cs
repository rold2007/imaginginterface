// <copyright file="ApplicationPropertiesServiceTest.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.Tests
{
   using ImagingInterface.Controllers.Services;
   using Xunit;

   public class ApplicationPropertiesServiceTest
   {
      [Fact]
      public void Constructor()
      {
         ApplicationPropertiesService applicationPropertiesService = new ApplicationPropertiesService();

         Assert.False(string.IsNullOrEmpty(applicationPropertiesService.ProductName));
         Assert.False(string.IsNullOrEmpty(applicationPropertiesService.Version));
         Assert.False(string.IsNullOrEmpty(applicationPropertiesService.Copyright));
         Assert.False(string.IsNullOrEmpty(applicationPropertiesService.ProductDescription));
      }
   }
}
