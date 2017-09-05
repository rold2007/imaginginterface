// <copyright file="AboutBoxView.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Views
   {
   using System.Windows.Forms;
   using ImagingInterface.Controllers;

   public partial class AboutBoxView : Form
      {
      private AboutBoxController aboutBoxController;

      public AboutBoxView(AboutBoxController aboutBoxController)
         {
         this.InitializeComponent();

         this.aboutBoxController = aboutBoxController;

         this.InitializeFields();
         }

      private void InitializeFields()
         {
         AboutBoxController.ProductInformations product = this.aboutBoxController.Product;

         this.Text = string.Format("About {0}", product.ProductName);
         this.labelProductName.Text = product.ProductName;
         this.labelVersion.Text = string.Format("Version {0}", product.Version);
         this.labelCopyright.Text = product.Copyright;
         this.textBoxDescription.Text = product.ProductDescription;
      }
      }
   }
