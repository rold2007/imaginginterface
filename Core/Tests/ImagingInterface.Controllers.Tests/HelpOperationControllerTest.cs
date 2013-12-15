namespace ImagingInterface.Controllers.Tests
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Controllers.Tests.Views;
   using ImagingInterface.Models;
   using ImagingInterface.Views;
   using NUnit.Framework;

   [TestFixture]
   public class HelpOperationControllerTest : ControllersBaseTest
      {
      [Test]
      public void Constructor()
         {
         IHelpOperationController helpOperationController = this.ServiceLocator.GetInstance<IHelpOperationController>();
         }

      [Test]
      public void HelpAbout()
         {
         HelpOperationView helpOperationView = this.ServiceLocator.GetInstance<IHelpOperationView>() as HelpOperationView;
         IHelpOperationController helpOperationController = this.ServiceLocator.GetInstance<IHelpOperationController>();
         AboutBoxView aboutBoxView = this.ServiceLocator.GetInstance<IAboutBoxView>() as AboutBoxView;
         IAboutBoxModel aboutBoxModel = this.ServiceLocator.GetInstance<IAboutBoxModel>();

         Assert.IsNull(aboutBoxView.DisplayedModel);

         helpOperationView.TriggerHelpAbout();

         Assert.AreSame(aboutBoxModel, aboutBoxView.DisplayedModel);
         }
      }
   }
