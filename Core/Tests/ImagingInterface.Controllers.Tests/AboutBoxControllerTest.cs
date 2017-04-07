namespace ImagingInterface.Controllers.Tests
{
    using ImagingInterface.Models;
    using NUnit.Framework;

    [TestFixture]
    public class AboutBoxControllerTest : ControllersBaseTest
    {
        [Test]
        public void Constructor()
        {
            AboutBoxModel aboutBoxModel = new AboutBoxModel();
            AboutBoxController aboutBoxController = new AboutBoxController(aboutBoxModel);

            Assert.That(aboutBoxController.Product.ProductName, Is.Not.Null.Or.Empty);
            Assert.That(aboutBoxController.Product.Version, Is.Not.Null.Or.Empty);
            Assert.That(aboutBoxController.Product.Copyright, Is.Not.Null.Or.Empty);
            Assert.That(aboutBoxController.Product.ProductDescription, Is.Not.Null.Or.Empty);
        }
    }
}
