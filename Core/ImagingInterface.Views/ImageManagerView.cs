namespace ImagingInterface.Views
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using ImagingInterface.Controllers;
    using ImagingInterface.Plugins;
    using Microsoft.Practices.ServiceLocation;

    public partial class ImageManagerView : UserControl
    {
        private List<ImageView> imageViews;
        private Dictionary<ImageView, TabPage> imageViewTabPage;
        private Dictionary<ImageView, ToolTip> imageViewToolTip;
        private ImageManagerController imageManagerController;
        private IServiceLocator serviceLocator;
        private ImageSourceManager imageSourceManager;

        public ImageManagerView(ImageManagerController imageManagerController, IServiceLocator serviceLocator, ImageSourceManager imageSourceManager)
        {
            this.InitializeComponent();

            this.imageViews = new List<ImageView>();
            this.imageViewTabPage = new Dictionary<ImageView, TabPage>();
            this.imageViewToolTip = new Dictionary<ImageView, ToolTip>();

            this.Dock = DockStyle.Fill;

            this.imageManagerController = imageManagerController;

            //this.imageManagerController.ImageAdded += this.ImageManagerController_ImageAdded;
            //this.imageManagerController.ActiveImageChanged += this.ImageManagerModel_ActiveImageChanged;
            //this.imageManagerController.RemoveActiveImageIndex += this.ImageManagerController_RemoveActiveImageIndex;

            this.serviceLocator = serviceLocator;

            this.imageSourceManager = imageSourceManager;
        }

        public void AddImageView(ImageView imageView)
        {
            //ImageView imageView = new ImageView();
            //this.AddImageToNewtab(imageView);

            //this.imageManagerController.AddImage();
            System.Diagnostics.Debug.Fail("Not done yet.");
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

        public void RemoveAllImageViews()
        {
            this.imageManagerController.RemoveAllImages();
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

        private void ImageManagerController_ImageAdded(object sender, EventArgs e)
        {
            //this.AddImageToNewtab();
            throw new NotImplementedException();
        }

        private void ImageManagerModel_ActiveImageChanged(object sender, EventArgs e)
        {
            this.imagesTabControl.SelectedIndex = this.imageManagerController.ActiveImageIndex;
        }

        private void ImageManagerController_RemoveActiveImageIndex(object sender, EventArgs e)
        {
            //int activeImageIndex = this.imageManagerController.ImageManagerModel.ActiveImageIndex;
            System.Diagnostics.Debug.Fail("Need to replace this code. The model shouldnt be accessed directly anymore.");

            //using (ImageView activeImageView = this.imageViews[activeImageIndex])
            //using (TabPage tabPage = this.imageViewTabPage[activeImageView])
            //using (ToolTip toolTip = this.imageViewToolTip[activeImageView])
            //   {
            //   this.imagesTabControl.Controls.Remove(tabPage);
            //   this.imageViews.RemoveAt(activeImageIndex);
            //   this.imageViewTabPage.Remove(activeImageView);
            //   this.imageViewToolTip.Remove(activeImageView);
            //   }
        }

        private void ImageSourceManager_ImageAdded(object sender, ImageSourceAddedEventArgs e)
        {
            IImageSource imageSource = e.ImageSource;

            // ImageView factory
            ImageView imageView = this.serviceLocator.GetInstance<ImageView>();

            imageView.SetImageSource(imageSource);

            this.AddImageToNewtab(imageView);

            this.imageManagerController.AddImage(imageSource);
        }

        private void ImageSourceManager_ImageRemoved(object sender, ImageSourceRemovedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ImageManagerView_Load(object sender, EventArgs e)
        {
            this.imageSourceManager.ImageSourceAdded += this.ImageSourceManager_ImageAdded;
            this.imageSourceManager.ImageSourceRemoved += this.ImageSourceManager_ImageRemoved;
        }
    }
}
