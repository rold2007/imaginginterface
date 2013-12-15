namespace ImagingInterface.Views
   {
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Drawing;
   using System.Linq;
   using System.Reflection;
   using System.Windows.Forms;
   using ImagingInterface.Models;

   public partial class AboutBoxView : Form, IAboutBoxView
      {
      public AboutBoxView()
         {
         this.InitializeComponent();
         }

      public void Display(IAboutBoxModel aboutBoxModel)
         {
         this.Text = string.Format("About {0}", aboutBoxModel.ProductName);
         this.labelProductName.Text = aboutBoxModel.ProductName;
         this.labelVersion.Text = string.Format("Version {0}", aboutBoxModel.Version);
         this.labelCopyright.Text = aboutBoxModel.Copyright;
         this.textBoxDescription.Text = aboutBoxModel.ProductDescription;

         this.ShowDialog();
         }
      }
   }
