﻿namespace ImagingInterface.Views
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
      private Dictionary<IRawImageView, TabPage> rawImageViewTabPage;
      private Dictionary<IRawImageView, ToolTip> rawImageViewToolTip;

      public ImageManagerView()
         {
         // This help detect misconfiguration in IoC
         Debug.Assert(ImageManagerView.checkSingleton == false, "A singleton shoudn't be constructed twice.");

         ImageManagerView.checkSingleton = true;

         this.InitializeComponent();

         this.rawImageViewTabPage = new Dictionary<IRawImageView, TabPage>();
         this.rawImageViewToolTip = new Dictionary<IRawImageView, ToolTip>();

         this.Dock = DockStyle.Fill;
         }

      public void AddImageView(IRawImageView rawImageView, IRawImageModel rawImageModel)
         {
         TabPage tabPage = new TabPage(rawImageModel.DisplayName);
         ToolTip toolTip = new ToolTip();

         this.rawImageViewTabPage.Add(rawImageView, tabPage);
         this.rawImageViewToolTip.Add(rawImageView, toolTip);

         // Attach a new ToolTip because there's no way to detach a global (form) ToolTip
         // when closing the image
         toolTip.SetToolTip(tabPage, rawImageModel.DisplayName);

         Control imageViewControl = rawImageView as Control;

         tabPage.Controls.Add(imageViewControl);

         Size tabPageSize = this.imagesTabControl.DisplayRectangle.Size;

         tabPageSize.Height -= this.imagesTabControl.ItemSize.Height;

         tabPage.Size = tabPageSize;

         this.UpdateImageTabPageProperties(rawImageView);

         this.imagesTabControl.Controls.Add(tabPage);
         }

      public IRawImageView GetActiveImageView()
         {
         if (this.imagesTabControl.SelectedTab != null)
            {
            return this.imagesTabControl.SelectedTab.Controls[0] as IRawImageView;
            }
         else
            {
            return null;
            }
         }

      public void RemoveImageView(IRawImageView rawImageView)
         {
         TabPage tabPage = this.rawImageViewTabPage[rawImageView];
         ToolTip toolTip = this.rawImageViewToolTip[rawImageView];

         this.imagesTabControl.Controls.Remove(tabPage);
         this.rawImageViewTabPage.Remove(rawImageView);
         this.rawImageViewToolTip.Remove(rawImageView);

         tabPage.Dispose();
         toolTip.Dispose();
         }

      private void UpdateImageTabPageProperties(IRawImageView rawImageView)
         {
         TabPage tabPage = this.rawImageViewTabPage[rawImageView];
         Size size = tabPage.ClientSize;
         Control imageViewControl = rawImageView as Control;

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
