namespace ImagingInterface.Controllers.Tests
{
   using ImagingInterface.Controllers.Services;
   using NUnit.Framework;

   [TestFixture]
   public class ApplicationPropertiesServiceTest
   {
      [Test]
      public void Constructor()
      {
         ApplicationPropertiesService applicationPropertiesService = new ApplicationPropertiesService();

         Assert.That(applicationPropertiesService.ProductName, Is.Not.Null.Or.Empty);
         Assert.That(applicationPropertiesService.Version, Is.Not.Null.Or.Empty);
         Assert.That(applicationPropertiesService.Copyright, Is.Not.Null.Or.Empty);
         Assert.That(applicationPropertiesService.ProductDescription, Is.Not.Null.Or.Empty);
      }
   }
}
