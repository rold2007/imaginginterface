// <copyright file="ImageView.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Views
{
   using System;
   using System.Collections.Generic;
   using System.Diagnostics;
   using System.Drawing;
   using System.Windows.Forms;
   using ImageProcessor.Imaging.Colors;
   using ImagingInterface.Controllers;
   using ImagingInterface.Controllers.EventArguments;
   using ImagingInterface.Plugins;
   using OpenTK.Graphics.OpenGL;

   public partial class ImageView : UserControl, IImageView
   {
      private Dictionary<Texture, int> textures;
      private ImageController imageController;
      private bool firstPaint = true;
      private bool zoomMode = true;
      private bool openGLControlSizeUpdated = true;
      private double translateX = 0.0;
      private double translateY = 0.0;
      private ToolStripItem viewModeToolStripItem;
      private Point viewCenter;

      public ImageView(ImageController imageController)
      {
         OpenTK.Toolkit.Init();
         this.InitializeComponent();

         this.Dock = DockStyle.Fill;

         this.CreateStatusStripSeparators();

         this.viewModeToolStripItem = this.zoomModeToolStripMenuItem;

         this.textures = new Dictionary<Texture, int>();

         this.ResetMousePosition();

         this.imageController = imageController;

         this.imageController.UpdateDisplay += this.ImageController_UpdateDisplay;
      }

      private event EventHandler PrepareViewNeeded;

      private event EventHandler AdjustScrollBarsNeeded;

      private event EventHandler AllocateTexturesNeeded;

      private event PaintEventHandler GLControlPaintNeeded;

      private enum Texture
      {
         /// <summary>
         /// The underlay is the bottom-most layer
         /// </summary>
         Underlay,

         /// <summary>
         /// The overlay is displayed over the underlay
         /// </summary>
         Overlay
      }

      public string DisplayName
      {
         get
         {
            return this.imageController.DisplayName;
         }
      }

      public double UpdateFrequency
      {
         get
         {
            return OpenTK.DisplayDevice.GetDisplay(0).RefreshRate;
         }
      }

      public IImageSource ImageSource
      {
         get
         {
            return this.imageController.ImageSource;
         }

         set
         {
            this.imageController.ImageSource = value;
         }
      }

      public void UpdateZoomLevel()
      {
         this.zoomLevelToolStripStatusLabel.Text = string.Format("{0}x", this.imageController.ZoomLevel);

         this.UpdateStatusStripSeparators();

         this.AdjustScrollBars();

         this.CenterView(this.viewCenter);

         this.PrepareView();

         this.ForceGLControlPaint();
      }

      public void UpdatePixelView(Point pixelPosition)
      {
         this.UpdateMousePosition(pixelPosition);

         RgbaColor rgbaColor = this.imageController.GetRgbaPixel(pixelPosition);

         this.UpdatePixelColor(pixelPosition, rgbaColor);
      }

      public void Close()
      {
         this.FreeTextures();

         this.imageController.Close();

         this.Dispose();
      }

      private void ImageController_UpdateDisplay(object sender, EventArgs e)
      {
         if (this.glControl.Visible == false)
         {
            this.glControl.Visible = true;
         }

         this.AdjustScrollBars();

         if (this.openGLControlSizeUpdated)
         {
            this.PrepareView();

            this.openGLControlSizeUpdated = false;
         }

         this.AllocateTextures();

         // The creation of the control already triggers a paint so don't force the first paint
         if (this.firstPaint == true)
         {
            this.firstPaint = false;

            this.glControl.Invalidate();
         }
         else
         {
            // Optimization suggested in OpenTK forum instead of calling this.glControl.Invalidate()
            this.ForceGLControlPaint();
         }
      }

      private void GLControl_Load(object sender, System.EventArgs e)
      {
         this.PrepareViewNeeded += this.ImageView_PrepareViewNeeded;

         this.AdjustScrollBarsNeeded += this.ImageView_AdjustScrollBarsNeeded;

         this.AllocateTexturesNeeded += this.ImageView_AllocateTexturesNeeded;

         this.GLControlPaintNeeded += this.GLControl_Paint;
         this.glControl.Paint += new System.Windows.Forms.PaintEventHandler(this.GLControl_Paint);

         // Display a first buffer of size 1x1x1
         this.InitializeGLControl();
      }

      private void InitializeGLControl()
      {
         Debug.Assert(!this.InvokeRequired, "This should always be called from the Main UI Thread.");

         this.UpdateZoomLevel();

         this.AdjustScrollBars();

         this.PrepareView();

         this.AllocateTextures();
      }

      private void PrepareView()
      {
         this.PrepareViewNeeded?.Invoke(this, EventArgs.Empty);
      }

      private void ImageView_PrepareViewNeeded(object sender, EventArgs e)
      {
         this.glControl.MakeCurrent();

         GL.ClearColor(SystemColors.Control);

         GL.MatrixMode(MatrixMode.Projection);
         GL.LoadIdentity();
         GL.Ortho(0.0, this.glControl.Width, this.glControl.Height, 0.0, -1.0, 1.0);
         GL.Viewport(0, 0, this.glControl.Width, this.glControl.Height);

         GL.Translate(this.translateX, this.translateY, 0);

         GL.Scale(this.imageController.ZoomLevel, this.imageController.ZoomLevel, 1.0);

         GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

         // The alpha can be toggled to make the overlay more/less transparent
         GL.Color4(0.0, 0.0, 0.0, 0.75);
      }

      private void AdjustScrollBars()
      {
         this.AdjustScrollBarsNeeded?.Invoke(this, EventArgs.Empty);
      }

      private void ImageView_AdjustScrollBarsNeeded(object sender, EventArgs e)
      {
         bool horizontalScrollBarVisible = false;
         bool vertizontalScrollBarVisible = false;
         Size neededViewSize = new Size(Convert.ToInt32(this.imageController.Size.Width * this.imageController.ZoomLevel), Convert.ToInt32(this.imageController.Size.Height * this.imageController.ZoomLevel));
         Size clientSize = this.ToolStripContainer.ContentPanel.ClientSize;

         if (neededViewSize.Width > clientSize.Width)
         {
            horizontalScrollBarVisible = true;

            clientSize.Height -= this.horizontalScrollBar.Height;

            if (neededViewSize.Height > clientSize.Height)
            {
               vertizontalScrollBarVisible = true;

               clientSize.Width -= this.verticalScrollBar.Width;
            }
         }
         else
         {
            if (neededViewSize.Height > clientSize.Height)
            {
               vertizontalScrollBarVisible = true;

               clientSize.Width -= this.verticalScrollBar.Width;

               // Manage horizontal scrollbar hiding part of the image
               if (neededViewSize.Width > clientSize.Width)
               {
                  horizontalScrollBarVisible = true;

                  clientSize.Height -= this.horizontalScrollBar.Height;
               }
            }
         }

         this.horizontalScrollBar.Visible = horizontalScrollBarVisible;
         this.verticalScrollBar.Visible = vertizontalScrollBarVisible;

         if (horizontalScrollBarVisible)
         {
            int missingWidth = neededViewSize.Width - clientSize.Width;

            this.UpdateScrollbarValueAndMaximum(missingWidth, this.horizontalScrollBar);
         }
         else
         {
            this.translateX = 0.0;
         }

         if (vertizontalScrollBarVisible)
         {
            int missingHeight = neededViewSize.Height - clientSize.Height;

            this.UpdateScrollbarValueAndMaximum(missingHeight, this.verticalScrollBar);
         }
         else
         {
            this.translateY = 0.0;
         }

         this.glControl.Size = new Size(Math.Min(neededViewSize.Width, clientSize.Width), Math.Min(neededViewSize.Height, clientSize.Height));
      }

      private void UpdateScrollbarValueAndMaximum(int missingSize, ScrollBar scrollBar)
      {
         // Make sure the scrollbar ticker is always big enough
         int largeChange = Math.Max(10, Convert.ToInt32(missingSize * 0.1));

         scrollBar.LargeChange = largeChange;

         // The maximum value of a scroll bar using user interaction is Maximum-LargeChange+1. (see http://msdn.microsoft.com/en-us/library/system.windows.forms.scrollbar.maximum.aspx)
         scrollBar.Maximum = missingSize + scrollBar.LargeChange - 1;

         if (scrollBar.Value > scrollBar.Maximum)
         {
            scrollBar.Value = scrollBar.Maximum;
         }
      }

      private void AllocateTextures()
      {
         this.AllocateTexturesNeeded?.Invoke(this, EventArgs.Empty);
      }

      private void ImageView_AllocateTexturesNeeded(object sender, EventArgs e)
      {
         this.FreeTextures();

         int unpackAlignment = GL.GetInteger(GetPName.UnpackAlignment);

         GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1);

         this.textures.Add(Texture.Underlay, GL.GenTexture());

         this.InitializeTexture(this.textures[Texture.Underlay]);

         if (this.imageController.IsGrayscale)
         {
            // Camera (capture) images are retrieved as grayscale
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Luminance, this.imageController.Size.Width, this.imageController.Size.Height, 0, PixelFormat.Luminance, PixelType.UnsignedByte, this.imageController.DisplayImageData);
         }
         else
         {
            // Static images are loaded as color
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, this.imageController.Size.Width, this.imageController.Size.Height, 0, PixelFormat.Rgb, PixelType.UnsignedByte, this.imageController.DisplayImageData);
         }

         GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvMode, (int)TextureEnvMode.Replace);

         ////if (this.imageController.ImageModel.OverlayImageData != null)
         ////   {
         ////   this.textures.Add(Texture.Overlay, GL.GenTexture());

         ////   this.InitializeTexture(this.textures[Texture.Overlay]);

         ////   GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, this.imageController.ImageModel.Size.Width, this.imageController.ImageModel.Size.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, this.imageController.ImageModel.OverlayImageData);

         ////   GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvColor, Color.White);
         ////   GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvMode, (int)TextureEnvMode.Blend);
         ////   }

         GL.PixelStore(PixelStoreParameter.UnpackAlignment, unpackAlignment);

         this.UpdateImageSize();
      }

      private void FreeTextures()
      {
         foreach (int texture in this.textures.Values)
         {
            GL.DeleteTexture(texture);
         }

         this.textures.Clear();
      }

      private void InitializeTexture(int texture)
      {
         GL.BindTexture(TextureTarget.Texture2D, texture);

         GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
         GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
         GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureBaseLevel, 0);
         GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMaxLevel, 0);
         GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToBorder);
         GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToBorder);
      }

      private void ForceGLControlPaint()
      {
         this.GLControlPaintNeeded?.Invoke(this, null);
      }

      private void GLControl_Paint(object sender, PaintEventArgs e)
      {
         int underlayTexture;
         bool underlayPresent = this.textures.TryGetValue(Texture.Underlay, out underlayTexture);

         this.glControl.MakeCurrent();

         GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
         GL.Clear(ClearBufferMask.AccumBufferBit);
         GL.Clear(ClearBufferMask.StencilBufferBit);

         GL.MatrixMode(MatrixMode.Texture);

         GL.Enable(EnableCap.Texture2D);

         // Draw image (underlay)
         GL.BindTexture(TextureTarget.Texture2D, underlayTexture);

         GL.Begin(PrimitiveType.Quads);

         GL.TexCoord2(0, 0);
         GL.Vertex2(0, 0);
         GL.TexCoord2(1, 0);
         GL.Vertex2(this.imageController.Size.Width, 0);
         GL.TexCoord2(1, 1);
         GL.Vertex2(this.imageController.Size.Width, this.imageController.Size.Height);
         GL.TexCoord2(0, 1);
         GL.Vertex2(0, this.imageController.Size.Height);

         GL.End();

         int overlayTexture;
         bool overlayPresent = this.textures.TryGetValue(Texture.Overlay, out overlayTexture);

         if (overlayPresent)
         {
            GL.Enable(EnableCap.Blend);

            // Draw overlay, with alpha transparency (blending)
            GL.BindTexture(TextureTarget.Texture2D, overlayTexture);

            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(0, 0);
            GL.Vertex2(0, 0);
            GL.TexCoord2(1, 0);
            GL.Vertex2(this.imageController.Size.Width, 0);
            GL.TexCoord2(1, 1);
            GL.Vertex2(this.imageController.Size.Width, this.imageController.Size.Height);
            GL.TexCoord2(0, 1);
            GL.Vertex2(0, this.imageController.Size.Height);

            GL.End();

            GL.Disable(EnableCap.Blend);
         }

         this.glControl.SwapBuffers();

         Debug.Assert(GL.GetError() == ErrorCode.NoError, "Some OpenTK/OpenGL error occured, this can cause performance problems.");
      }

      private void GLControl_MouseMove(object sender, MouseEventArgs e)
      {
         Point mousePosition = new Point(e.X, e.Y);
         Point pixelPosition = this.GetPointPositionInImage(mousePosition.X, mousePosition.Y);

         this.UpdatePixelView(pixelPosition);
      }

      private void GLControl_MouseClick(object sender, MouseEventArgs e)
      {
         this.ManageClick(e);
      }

      private void GLControl_MouseDoubleClick(object sender, MouseEventArgs e)
      {
         this.ManageClick(e);
      }

      private void ViewModeToolStripSplitButton_ButtonClick(object sender, EventArgs e)
      {
         int nextIndex = this.viewModeToolStripSplitButton.DropDownItems.IndexOf(this.viewModeToolStripItem) + 1;

         if (nextIndex >= this.viewModeToolStripSplitButton.DropDownItems.Count)
         {
            nextIndex = 0;
         }

         this.viewModeToolStripItem = this.viewModeToolStripSplitButton.DropDownItems[nextIndex];

         this.viewModeToolStripSplitButton.Text = this.viewModeToolStripItem.Text;

         this.UpdateZoomMode();
      }

      private void ViewModeToolStripSplitButton_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
      {
         this.viewModeToolStripItem = e.ClickedItem;

         this.viewModeToolStripSplitButton.Text = this.viewModeToolStripItem.Text;

         this.UpdateZoomMode();
      }

      private void UpdateZoomMode()
      {
         if (this.viewModeToolStripItem == this.zoomModeToolStripMenuItem)
         {
            this.zoomMode = true;
         }
         else
         {
            this.zoomMode = false;
         }
      }

      private void ToolStripContainer_ContentPanel_Layout(object sender, LayoutEventArgs e)
      {
         this.AdjustScrollBars();

         this.UpdateScrollbarsPositionAndSize();

         if (this.openGLControlSizeUpdated)
         {
            this.PrepareView();

            this.openGLControlSizeUpdated = false;
         }

         this.ForceGLControlPaint();
      }

      private void UpdateScrollbarsPositionAndSize()
      {
         int twoScrollBarsCoefficient;

         if (this.horizontalScrollBar.Visible && this.verticalScrollBar.Visible)
         {
            twoScrollBarsCoefficient = 1;
         }
         else
         {
            twoScrollBarsCoefficient = 0;
         }

         this.horizontalScrollBar.Location = new Point(0, this.ToolStripContainer.ContentPanel.ClientSize.Height - this.horizontalScrollBar.Height);
         this.verticalScrollBar.Location = new Point(this.ToolStripContainer.ContentPanel.ClientSize.Width - this.verticalScrollBar.Width, 0);

         this.horizontalScrollBar.Size = new Size(this.ToolStripContainer.ContentPanel.ClientSize.Width - (twoScrollBarsCoefficient * this.verticalScrollBar.Width), this.horizontalScrollBar.Height);
         this.verticalScrollBar.Size = new Size(this.verticalScrollBar.Width, this.ToolStripContainer.ContentPanel.ClientSize.Height - (twoScrollBarsCoefficient * this.horizontalScrollBar.Height));
      }

      private void HorizontalScrollBar_ValueChanged(object sender, EventArgs e)
      {
         this.translateX = -this.horizontalScrollBar.Value;

         this.PrepareView();

         this.ForceGLControlPaint();
      }

      private void VerticalScrollBar_ValueChanged(object sender, EventArgs e)
      {
         this.translateY = -this.verticalScrollBar.Value;

         this.PrepareView();

         this.ForceGLControlPaint();
      }

      private void ToolStripContainer_ContentPanel_MouseClick(object sender, MouseEventArgs e)
      {
         this.ManageClick(e);
      }

      private void ToolStripContainer_ContentPanel_MouseDoubleClick(object sender, MouseEventArgs e)
      {
         this.ManageClick(e);
      }

      private void ManageClick(MouseEventArgs e)
      {
         Point mouseClickPixel = this.GetPointPositionInImage(e.X, e.Y);

         if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
         {
            if (this.zoomMode)
            {
               this.viewCenter = mouseClickPixel;

               this.imageController.UpdateZoomLevel(this.imageController.ZoomLevel * 2.0);
               this.UpdateZoomLevel();
            }
            else
            {
               this.imageController.SelectPixel(mouseClickPixel);
            }
         }
         else if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
         {
            if (this.zoomMode)
            {
               if (this.glControl.Size.Width > 1 && this.glControl.Size.Height > 1)
               {
                  this.viewCenter = mouseClickPixel;

                  this.imageController.UpdateZoomLevel(this.imageController.ZoomLevel * 0.5);
                  this.UpdateZoomLevel();
               }
            }
         }
         else
         {
            ////this.imageController.SelectPixel();
         }
      }

      private Point GetPointPositionInImage(int mouseX, int mouseY)
      {
         int imageX;
         int imageY;

         if (mouseX > this.glControl.Width)
         {
            imageX = this.imageController.Size.Width - 1;
         }
         else
         {
            imageX = Convert.ToInt32(((mouseX - this.translateX) / this.imageController.ZoomLevel) - 0.5);
         }

         if (mouseY > this.glControl.Height)
         {
            imageY = this.imageController.Size.Height - 1;
         }
         else
         {
            imageY = Convert.ToInt32(((mouseY - this.translateY) / this.imageController.ZoomLevel) - 0.5);
         }

         return new Point(imageX, imageY);
      }

      private void CenterView(Point center)
      {
         if (this.horizontalScrollBar.Visible)
         {
            double horizontalTranslation = ((center.X + 0.5) * this.imageController.ZoomLevel) - (this.glControl.Width / 2);

            // The maximum value of a scroll bar using user interaction is Maximum-LargeChange+1. (see http://msdn.microsoft.com/en-us/library/system.windows.forms.scrollbar.maximum.aspx)
            this.horizontalScrollBar.Value = Convert.ToInt32(Math.Max(0, Math.Min(horizontalTranslation, this.horizontalScrollBar.Maximum - this.horizontalScrollBar.LargeChange + 1)));
         }
         else
         {
            this.horizontalScrollBar.Value = 0;
         }

         if (this.verticalScrollBar.Visible)
         {
            double verticalTranslation = ((center.Y + 0.5) * this.imageController.ZoomLevel) - (this.glControl.Height / 2);

            // The maximum value of a scroll bar using user interaction is Maximum-LargeChange+1. (see http://msdn.microsoft.com/en-us/library/system.windows.forms.scrollbar.maximum.aspx)
            this.verticalScrollBar.Value = Convert.ToInt32(Math.Max(0, Math.Min(verticalTranslation, this.verticalScrollBar.Maximum - this.verticalScrollBar.LargeChange + 1)));
         }
         else
         {
            this.verticalScrollBar.Value = 0;
         }
      }

      private void GLControl_MouseLeave(object sender, EventArgs e)
      {
         this.ResetMousePosition();
         this.ResetPixelColor();
      }

      private void UpdateMousePosition(Point pixelPosition)
      {
         this.mousePositionToolStripStatusLabel.Text = string.Format("({0}, {1})", pixelPosition.X, pixelPosition.Y);

         this.UpdateStatusStripSeparators();
      }

      private void ResetMousePosition()
      {
         this.mousePositionToolStripStatusLabel.Text = string.Empty;

         this.UpdateStatusStripSeparators();
      }

      private void UpdatePixelColor(Point pixelPosition, RgbaColor rgbaColor)
      {
         if (this.imageController.IsGrayscale)
         {
            this.rgbGrayColorToolStripStatusLabel.Text = string.Format("Gray: {0} ", rgbaColor.R.ToString());
            this.hsvColorToolStripStatusLabel.Text = string.Empty;
         }
         else
         {
            this.rgbGrayColorToolStripStatusLabel.Text = rgbaColor.ToString();

            HslaColor hslaColor = rgbaColor;

            this.hsvColorToolStripStatusLabel.Text = hslaColor.ToString();
         }

         this.UpdateStatusStripSeparators();
      }

      private void ResetPixelColor()
      {
         this.rgbGrayColorToolStripStatusLabel.Text = string.Empty;
         this.hsvColorToolStripStatusLabel.Text = string.Empty;

         this.UpdateStatusStripSeparators();
      }

      private void UpdateImageSize()
      {
         this.imageSizeToolStripStatusLabel.Text = string.Format("({0}x{1})", this.imageController.Size.Width, this.imageController.Size.Height);

         this.UpdateStatusStripSeparators();
      }

      private void CreateStatusStripSeparators()
      {
         int separatorIndex = 1;

         while (separatorIndex < this.statusStrip.Items.Count)
         {
            this.statusStrip.Items.Insert(separatorIndex, new ToolStripSeparator());

            separatorIndex += 2;
         }

         this.UpdateStatusStripSeparators();
      }

      private void UpdateStatusStripSeparators()
      {
         bool showNextSeparator = false;

         for (int i = 0; i < this.statusStrip.Items.Count - 1; i++)
         {
            ToolStripItem toolStripItem = this.statusStrip.Items[i];
            ToolStripSeparator toolStripSeparator = toolStripItem as ToolStripSeparator;

            if (toolStripSeparator == null)
            {
               if (!string.IsNullOrEmpty(toolStripItem.Text))
               {
                  showNextSeparator = true;
               }
            }
            else
            {
               toolStripSeparator.Visible = showNextSeparator;
               showNextSeparator = false;
            }
         }

         this.statusStrip.Update();
      }

      private void GLControl_Layout(object sender, LayoutEventArgs e)
      {
         this.openGLControlSizeUpdated = true;
      }
   }
}
