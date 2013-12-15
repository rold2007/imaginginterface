namespace ImagingInterface.Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Views;
   using Microsoft.Practices.ServiceLocation;

   public class HelpOperationController : IHelpOperationController
      {
      private IServiceLocator serviceLocator;
      
      public HelpOperationController(IHelpOperationView helpOperationView, IServiceLocator serviceLocator)
         {
         this.serviceLocator = serviceLocator;

         helpOperationView.HelpAbout += this.HelpOperationView_HelpAbout;
         }

      private void HelpOperationView_HelpAbout(object sender, EventArgs e)
         {
         IAboutBoxController aboutBoxController = this.serviceLocator.GetInstance<IAboutBoxController>();

         aboutBoxController.Show();
         }
      }
   }
