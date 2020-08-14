// <copyright file="ImageManagerView.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Views
{
   using System;
   using System.Collections.Generic;
   using System.Diagnostics;
   using System.Diagnostics.CodeAnalysis;
   using System.Drawing;
   using System.Windows.Forms;
   using ImagingInterface.Controllers;
   using ImagingInterface.Controllers.EventArguments;

   public partial class ImageManagerView : UserControl
   {
      private ImageManagerController imageManagerController;
      private List<ImageView> imageViews;
      private Dictionary<ImageView, TabPage> imageViewTabPage;
      private Dictionary<ImageView, ToolTip> imageViewToolTip;

      public ImageManagerView(ImageManagerController imageManagerController)
      {
         this.InitializeComponent();

         this.imageViews = new List<ImageView>();
         this.imageManagerController = imageManagerController;
         this.imageViewTabPage = new Dictionary<ImageView, TabPage>();
         this.imageViewToolTip = new Dictionary<ImageView, ToolTip>();

         this.Dock = DockStyle.Fill;
      }

      public bool HasActiveImageView
      {
         get
         {
            return this.imagesTabControl.SelectedIndex >= 0;
         }
      }

      public ImageView ActiveImageView
      {
         get
         {
            Debug.Assert(this.HasActiveImageView, "Invalid tab index");

            return this.imageViews[this.imagesTabControl.SelectedIndex];

            // return this.imageViews[this.imageManagerController.ActiveImageIndex];
         }
      }

      public IEnumerable<ImageView> ImageViews
      {
         get
         {
            return this.imageViews;
         }
      }

      [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Too much work for now.")]
      [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Will be fixed when done refactoring.")]
      public void AddImageView(ImageView imageView)
      {
         // ImageView imageView = new ImageView();
         // this.AddImageToNewtab(imageView);

         // this.imageManagerController.AddImage();
         System.Diagnostics.Debug.Fail("Not done yet.");
      }

      public void RemoveImageView(ImageView imageView)
      {
         using (TabPage tabPage = this.imageViewTabPage[imageView])
         using (ToolTip toolTip = this.imageViewToolTip[imageView])
         {
            this.imagesTabControl.Controls.Remove(tabPage);
            this.imageViews.Remove(imageView);
            this.imageViewTabPage.Remove(imageView);
            this.imageViewToolTip.Remove(imageView);

            imageView.UnAssignFromImageManager();
         }
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
         throw new NotImplementedException();

         // this.imageManagerController.RemoveActiveImage();
      }

      public void RemoveAllImageViews()
      {
         throw new NotImplementedException();

         // this.imageManagerController.RemoveAllImages();
      }

      public void AddImageToNewtab(ImageView imageView)
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

         imageView.AssignToImageManager();

         this.imagesTabControl.SelectedIndex = this.imageManagerController.ActiveImageIndex;
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
         // this.AddImageToNewtab();
         throw new NotImplementedException();
      }

      // private void ImageManagerModel_ActiveImageChanged(object sender, EventArgs e)
      // {
      //    this.imagesTabControl.SelectedIndex = this.imageManagerController.ActiveImageIndex;
      // }
      private void ImageManagerController_RemoveActiveImageIndex(object sender, EventArgs e)
      {
         // int activeImageIndex = this.imageManagerController.ImageManagerModel.ActiveImageIndex;
         System.Diagnostics.Debug.Fail("Need to replace this code. The model shouldnt be accessed directly anymore.");

         // using (ImageView activeImageView = this.imageViews[activeImageIndex])
         // using (TabPage tabPage = this.imageViewTabPage[activeImageView])
         // using (ToolTip toolTip = this.imageViewToolTip[activeImageView])
         //   {
         //   this.imagesTabControl.Controls.Remove(tabPage);
         //   this.imageViews.RemoveAt(activeImageIndex);
         //   this.imageViewTabPage.Remove(activeImageView);
         //   this.imageViewToolTip.Remove(activeImageView);
         //   }
      }

      ////private void ImageManagerController_ImageAdded(object sender, ImageSourceAddedEventArgs e)
      ////{
      // IImageSource imageSource = e.ImageSource;
      // ImageView imageView = this.imageViewFactory.CreateNew();

      // imageView.ImageSource = imageSource;

      // this.AddImageToNewtab(imageView);

      // this.imageManagerController.AddImage(imageSource, imageView);
      ////}
   }
}
