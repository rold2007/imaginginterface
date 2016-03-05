namespace ImagingInterface.Views
   {
   using System.Windows.Forms;
   using ImagingInterface.Controllers;
   using ImagingInterface.Models;

   public partial class AboutBoxView : Form
      {
      public AboutBoxView(AboutBoxController aboutBoxController)
         {
         this.InitializeComponent();

         this.InitializeFields(aboutBoxController.AboutBoxModel);
         }

      private void InitializeFields(IAboutBoxModel aboutBoxModel)
         {
         this.Text = string.Format("About {0}", aboutBoxModel.ProductName);
         this.labelProductName.Text = aboutBoxModel.ProductName;
         this.labelVersion.Text = string.Format("Version {0}", aboutBoxModel.Version);
         this.labelCopyright.Text = aboutBoxModel.Copyright;
         this.textBoxDescription.Text = aboutBoxModel.ProductDescription;
         }
      }
   }
