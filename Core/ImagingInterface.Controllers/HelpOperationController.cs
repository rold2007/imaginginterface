namespace ImagingInterface.Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using Microsoft.Practices.ServiceLocation;

   public class HelpOperationController
      {
      ////private IServiceLocator serviceLocator;

      public HelpOperationController(/*IServiceLocator serviceLocator*/)
         {
         ////this.serviceLocator = serviceLocator;

         ////helpOperationView.HelpAbout += this.HelpOperationView_HelpAbout;
         }

      public event EventHandler DisplayAbouxBox;

      public void RequestDisplayAbouxBox()
         {
         if (this.DisplayAbouxBox != null)
            {
            this.DisplayAbouxBox(this, EventArgs.Empty);
            }
         }

      ////private void HelpOperationView_HelpAbout(object sender, EventArgs e)
      ////{
      ////IAboutBoxView aboutBoxView = this.serviceLocator.GetInstance<IAboutBoxView>();

      ////aboutBoxView.Display();

      ////this.ShowAboutBox(this, EventArgs.Empty);
      ////AboutBoxController aboutBoxController = this.serviceLocator.GetInstance<AboutBoxController>();

      ////aboutBoxController.Show();
      ////}
      }
   }
