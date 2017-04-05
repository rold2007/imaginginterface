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

            Assert.That(aboutBoxController.AboutBoxModel.ProductName, Is.Not.Null.Or.Empty);
            Assert.That(aboutBoxController.AboutBoxModel.Version, Is.Not.Null.Or.Empty);
            Assert.That(aboutBoxController.AboutBoxModel.Copyright, Is.Not.Null.Or.Empty);
            Assert.That(aboutBoxController.AboutBoxModel.ProductDescription, Is.Not.Null.Or.Empty);
        }
    }
}
