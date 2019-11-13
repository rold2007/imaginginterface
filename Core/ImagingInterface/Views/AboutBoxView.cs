// <copyright file="AboutBoxView.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Views
{
   using System;
   using System.Windows.Forms;
   using ImagingInterface.Controllers;
   using ImagingInterface.Controllers.Models;

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
         ProductInformations product = this.aboutBoxController.Product;

         this.Text = FormattableString.Invariant($"About {product.ProductName}");
         this.labelProductName.Text = product.ProductName;
         this.labelVersion.Text = FormattableString.Invariant($"Version {product.Version}");
         this.labelCopyright.Text = product.Copyright;
         this.textBoxDescription.Text = product.ProductDescription;
      }
   }
}
