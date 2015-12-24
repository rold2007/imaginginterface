namespace ImagingInterface.Controllers.Tests
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Models;
   using NUnit.Framework;

   [TestFixture]
   public class AboutBoxControllerTest : ControllersBaseTest
      {
      [Test]
      public void Constructor()
         {
         IAboutBoxController aboutBoxController = this.ServiceLocator.GetInstance<IAboutBoxController>();
         IAboutBoxModel aboutBoxModel = this.ServiceLocator.GetInstance<IAboutBoxModel>();

         Assert.That(aboutBoxModel.ProductName, Is.Not.Null.Or.Empty);
         Assert.That(aboutBoxModel.Version, Is.Not.Null.Or.Empty);
         Assert.That(aboutBoxModel.Copyright, Is.Not.Null.Or.Empty);
         Assert.That(aboutBoxModel.ProductDescription, Is.Not.Null.Or.Empty);
         }
      }
   }
