namespace ImagingInterface.Views
   {
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Drawing;
   using System.Linq;
   using System.Reflection;
   using System.Windows.Forms;
   using ImagingInterface.Controllers;

   public partial class AboutBoxView : Form, IAboutBoxView
      {
      private AboutBoxController aboutBoxController;

      public AboutBoxView(AboutBoxController aboutBoxController)
         {
         this.InitializeComponent();

         this.aboutBoxController = aboutBoxController;
         this.aboutBoxController.Display += this.AboutBoxController_Display;
         }

      public void Display()
         {
         this.aboutBoxController.DisplayView();
         }

      private void AboutBoxController_Display(object sender, EventArgs e)
         {
         this.Text = string.Format("About {0}", this.aboutBoxController.AboutBoxModel.ProductName);
         this.labelProductName.Text = this.aboutBoxController.AboutBoxModel.ProductName;
         this.labelVersion.Text = string.Format("Version {0}", this.aboutBoxController.AboutBoxModel.Version);
         this.labelCopyright.Text = this.aboutBoxController.AboutBoxModel.Copyright;
         this.textBoxDescription.Text = this.aboutBoxController.AboutBoxModel.ProductDescription;

         this.ShowDialog();
         }
      }
   }
