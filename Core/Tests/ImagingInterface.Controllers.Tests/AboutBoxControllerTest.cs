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
         AboutBoxController aboutBoxController = this.ServiceLocator.GetInstance<AboutBoxController>();
         IAboutBoxModel aboutBoxModel = aboutBoxController.AboutBoxModel;

         Assert.That(aboutBoxModel.ProductName, Is.Not.Null.Or.Empty);
         Assert.That(aboutBoxModel.Version, Is.Not.Null.Or.Empty);
         Assert.That(aboutBoxModel.Copyright, Is.Not.Null.Or.Empty);
         Assert.That(aboutBoxModel.ProductDescription, Is.Not.Null.Or.Empty);
         }
      }
   }
