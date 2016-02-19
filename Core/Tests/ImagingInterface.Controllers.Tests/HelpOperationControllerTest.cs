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
   public class HelpOperationControllerTest : ControllersBaseTest
      {
      [Test]
      public void Constructor()
         {
         HelpOperationController helpOperationController = this.ServiceLocator.GetInstance<HelpOperationController>();
         }

      [Test]
      public void HelpAbout()
         {
         ////HelpOperationView helpOperationView = this.ServiceLocator.GetInstance<IHelpOperationView>() as HelpOperationView;
         HelpOperationController helpOperationController = this.ServiceLocator.GetInstance<HelpOperationController>();
         ////AboutBoxView aboutBoxView = this.ServiceLocator.GetInstance<IAboutBoxView>() as AboutBoxView;
         ////IAboutBoxModel aboutBoxModel = this.ServiceLocator.GetInstance<IAboutBoxModel>();

         ////Assert.IsNull(aboutBoxView.DisplayedModel);

         ////helpOperationView.TriggerHelpAbout();

         ////Assert.AreSame(aboutBoxModel, aboutBoxView.DisplayedModel);

         ////helpOperationController.
         }
      }
   }
