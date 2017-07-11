namespace ImagingInterface.Controllers.Tests
{
   using ImagingInterface.Controllers.Services;
   using NUnit.Framework;

   [TestFixture]
   public class AboutBoxControllerTest : ControllersBaseTest
   {
      [Test]
      public void Constructor()
      {
         ApplicationPropertiesService applicationPropertiesService = new ApplicationPropertiesService();
         AboutBoxController aboutBoxController = new AboutBoxController(applicationPropertiesService);

         Assert.That(aboutBoxController.Product.ProductName, Is.Not.Null.Or.Empty);
         Assert.That(aboutBoxController.Product.Version, Is.Not.Null.Or.Empty);
         Assert.That(aboutBoxController.Product.Copyright, Is.Not.Null.Or.Empty);
         Assert.That(aboutBoxController.Product.ProductDescription, Is.Not.Null.Or.Empty);
      }
   }
}
