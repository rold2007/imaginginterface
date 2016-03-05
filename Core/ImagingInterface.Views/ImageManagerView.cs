namespace ImagingInterface.Views
   {
   using System;
   using System.Collections.Generic;
   using System.Drawing;
   using System.Windows.Forms;
   using ImagingInterface.Controllers;

   public partial class ImageManagerView : UserControl
      {
      private List<ImageView> imageViews;
      private Dictionary<ImageView, TabPage> imageViewTabPage;
      private Dictionary<ImageView, ToolTip> imageViewToolTip;
      private ImageManagerController imageManagerController;

      public ImageManagerView(ImageManagerController imageManagerController)
         {
         this.InitializeComponent();

         this.imageViews = new List<ImageView>();
         this.imageViewTabPage = new Dictionary<ImageView, TabPage>();
         this.imageViewToolTip = new Dictionary<ImageView, ToolTip>();

         this.Dock = DockStyle.Fill;

         this.imageManagerController = imageManagerController;
         this.imageManagerController.ActiveImageChanged += this.ImageManagerModel_ActiveImageChanged;
         this.imageManagerController.RemoveActiveImageIndex += this.ImageManagerController_RemoveActiveImageIndex;
         }

      public void AddImageView(ImageView imageView)
         {
         this.AddImageToNewtab(imageView);

         this.imageManagerController.AddImage();
         }

      ////public ImageView GetActiveImageView()
      ////   {
         ////if (this.imagesTabControl.IsHandleCreated)
         ////   {
         ////   if (this.imagesTabControl.SelectedTab != null)
         ////      {
         ////      if (this.imagesTabControl.SelectedTab.Controls.Count > 0)
         ////         {
         ////         return this.imagesTabControl.SelectedTab.Controls[0] as ImageView;
         ////         }
         ////      }
         ////   }

         ////return null;
         ////}

      public void RemoveActiveImageView()
         {
         this.imageManagerController.RemoveActiveImage();
         }

      private void AddImageToNewtab(ImageView imageView)
         {
         TabPage tabPage = new TabPage(imageView.DisplayName);
         ToolTip toolTip = new ToolTip();

         this.imageViews.Add(imageView);
         this.imageViewTabPage.Add(imageView, tabPage);
         this.imageViewToolTip.Add(imageView, toolTip);

         // Attach a new ToolTip because there's no way to detach a global (form) ToolTip
         // when closing the image
         toolTip.SetToolTip(tabPage, imageView.DisplayName);

         tabPage.Controls.Add(imageView);

         Size tabPageSize = this.imagesTabControl.DisplayRectangle.Size;

         tabPageSize.Height -= this.imagesTabControl.ItemSize.Height;

         tabPage.Size = tabPageSize;

         this.UpdateImageTabPageProperties(imageView);

         this.imagesTabControl.Controls.Add(tabPage);
         }

      private void UpdateImageTabPageProperties(ImageView imageView)
         {
         TabPage tabPage = this.imageViewTabPage[imageView];
         Size size = tabPage.ClientSize;

         imageView.Size = size;
         }

      private void ImagesTabControl_SizeChanged(object sender, EventArgs e)
         {
         if (this.imagesTabControl.TabCount != 0 && this.imagesTabControl.SelectedTab != null)
            {
            this.UpdateImageTabPageProperties(this.imagesTabControl.SelectedTab.Controls[0] as ImageView);
            }
         }

      private void ImagesTabControl_SelectedIndexChanged(object sender, EventArgs e)
         {
         this.imageManagerController.SetActiveImageIndex(this.imagesTabControl.SelectedIndex);
         }

      private void ImageManagerModel_ActiveImageChanged(object sender, EventArgs e)
         {
         this.imagesTabControl.SelectedIndex = this.imageManagerController.ImageManagerModel.ActiveImageIndex;
         }

      private void ImageManagerController_RemoveActiveImageIndex(object sender, EventArgs e)
         {
         int activeImageIndex = this.imageManagerController.ImageManagerModel.ActiveImageIndex;

         using (ImageView activeImageView = this.imageViews[activeImageIndex])
         using (TabPage tabPage = this.imageViewTabPage[activeImageView])
         using (ToolTip toolTip = this.imageViewToolTip[activeImageView])
            {
            this.imagesTabControl.Controls.Remove(tabPage);
            this.imageViews.RemoveAt(activeImageIndex);
            this.imageViewTabPage.Remove(activeImageView);
            this.imageViewToolTip.Remove(activeImageView);
            }
         }
      }
   }
