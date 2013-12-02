namespace ImagingInterface.Views
   {
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Data;
   using System.Diagnostics;
   using System.Drawing;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using System.Windows.Forms;
   using Emgu.CV;
   using Emgu.CV.Structure;
   using ImagingInterface.Models;
   using ImagingInterface.Plugins;
   using ImagingInterface.Views.EventArguments;

   public partial class ImageManagerView : UserControl, IImageManagerView
      {
      private static bool checkSingleton = false;
      private Dictionary<IImageView, TabPage> imageViewTabPage;
      private Dictionary<IImageView, ToolTip> imageViewToolTip;

      public ImageManagerView()
         {
         // This help detect misconfiguration in IoC
         Debug.Assert(ImageManagerView.checkSingleton == false, "A singleton shoudn't be constructed twice.");

         ImageManagerView.checkSingleton = true;

         this.InitializeComponent();
         
         this.imageViewTabPage = new Dictionary<IImageView, TabPage>();
         this.imageViewToolTip = new Dictionary<IImageView, ToolTip>();

         this.Dock = DockStyle.Fill;
         }

      public void AddImageView(IImageView imageView, IImageModel imageModel)
         {
         TabPage tabPage = new TabPage(imageModel.DisplayName);
         ToolTip toolTip = new ToolTip();

         this.imageViewTabPage.Add(imageView, tabPage);
         this.imageViewToolTip.Add(imageView, toolTip);

         // Attach a new ToolTip because there's no way to detach a global (form) ToolTip
         // when closing the image
         toolTip.SetToolTip(tabPage, imageModel.DisplayName);

         Control imageViewControl = imageView as Control;

         tabPage.Controls.Add(imageViewControl);

         Size tabPageSize = this.imagesTabControl.DisplayRectangle.Size;

         tabPageSize.Height -= this.imagesTabControl.ItemSize.Height;

         tabPage.Size = tabPageSize;

         this.UpdateImageTabPageProperties(imageView);

         this.imagesTabControl.Controls.Add(tabPage);
         }

      public IImageView GetActiveImageView()
         {
         if (this.imagesTabControl.SelectedTab != null)
            {
            return this.imagesTabControl.SelectedTab.Controls[0] as IImageView;
            }
         else
            {
            return null;
            }
         }

      public void RemoveImageView(IImageView imageView)
         {
         TabPage tabPage = this.imageViewTabPage[imageView];
         ToolTip toolTip = this.imageViewToolTip[imageView];

         this.imagesTabControl.Controls.Remove(tabPage);
         this.imageViewTabPage.Remove(imageView);
         this.imageViewToolTip.Remove(imageView);

         tabPage.Dispose();
         toolTip.Dispose();
         }

      private void UpdateImageTabPageProperties(IImageView imageView)
         {
         TabPage tabPage = this.imageViewTabPage[imageView];
         Size size = tabPage.ClientSize;
         Control imageViewControl = imageView as Control;

         imageViewControl.Size = size;
         }

      private void ImagesTabControl_SizeChanged(object sender, EventArgs e)
         {
         if (this.imagesTabControl.TabCount != 0 && this.imagesTabControl.SelectedTab != null)
            {
            this.UpdateImageTabPageProperties(this.imagesTabControl.SelectedTab.Controls[0] as IImageView);
            }
         }
      }
   }
