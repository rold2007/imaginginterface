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

         Assert.IsNotNullOrEmpty(aboutBoxModel.ProductName);
         Assert.IsNotNullOrEmpty(aboutBoxModel.Version);
         Assert.IsNotNullOrEmpty(aboutBoxModel.Copyright);
         Assert.IsNotNullOrEmpty(aboutBoxModel.ProductDescription);
         }
      }
   }
