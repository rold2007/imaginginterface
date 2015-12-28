namespace ImagingInterface.Views
   {
   using System;
   using System.Collections.Generic;
   using System.Diagnostics;
   using System.Drawing;
   using System.Windows.Forms;
   using ImagingInterface.Models;
   using ImagingInterface.Views.EventArguments;
   using OpenTK.Graphics;
   using OpenTK.Graphics.OpenGL;

   public partial class ImageView : UserControl, IImageView
      {
      private bool openGLControlLoaded = false;
      private Dictionary<Texture, int> textures;
      private IImageModel imageModel;
      private bool isFirstPaint = true;
      private bool openGLControlSizeUpdated = true;
      private bool zoomMode = true;
      private double translateX = 0.0;
      private double translateY = 0.0;
      private ToolStripItem viewModeToolStripItem;
      private Point viewCenter;

      public ImageView()
         {
         this.InitializeComponent();

         this.Dock = DockStyle.Fill;

         this.CreateStatusStripSeparators();

         this.viewModeToolStripItem = this.zoomModeToolStripMenuItem;

         this.textures = new Dictionary<Texture, int>();

         this.ResetMousePosition();
         }

      public event EventHandler ZoomLevelIncreased;

      public event EventHandler ZoomLevelDecreased;

      public event EventHandler<PixelViewChangedEventArgs> PixelViewChanged;

      public event EventHandler<SelectionChangedEventArgs> SelectionChanged;

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

      public double UpdateFrequency
         {
         get
            {
            return OpenTK.DisplayDevice.GetDisplay(0).RefreshRate;
            }
         }

      public void AssignImageModel(IImageModel imageModel)
         {
         this.imageModel = imageModel;
         }

      public void UpdateDisplay()
         {
         if (this.imageModel.DisplayImageData != null)
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
            if (this.isFirstPaint == true)
               {
               this.isFirstPaint = false;

               this.glControl.Invalidate();
               }
            else
               {
               // Optimization suggested in OpenTK forum instead of calling this.glControl.Invalidate()
               this.GLControl_Paint(this, null);
               }
            }
         }

      public void UpdateZoomLevel()
         {
         this.zoomLevelToolStripStatusLabel.Text = string.Format("{0}x", this.imageModel.ZoomLevel);

         this.UpdateStatusStripSeparators();

         this.AdjustScrollBars();

         this.CenterView(this.viewCenter);

         this.PrepareView();

         this.GLControl_Paint(this, null);
         }

      public void UpdatePixelView(Point pixelPosition, int gray, int[] rgb, double[] hsv)
         {
         this.UpdateMousePosition(pixelPosition);
         this.UpdatePixelColor(pixelPosition, gray, rgb, hsv);
         }

      public void Close()
         {
         this.FreeTextures();

         this.Dispose();
         }

      private void GLControl_Load(object sender, System.EventArgs e)
         {
         this.openGLControlLoaded = true;

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
         if (this.openGLControlLoaded)
            {
            this.glControl.MakeCurrent();

            GL.ClearColor(SystemColors.Control);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0.0, this.glControl.Width, this.glControl.Height, 0.0, -1.0, 1.0);
            GL.Viewport(0, 0, this.glControl.Width, this.glControl.Height);

            GL.Translate(this.translateX, this.translateY, 0);

            GL.Scale(this.imageModel.ZoomLevel, this.imageModel.ZoomLevel, 1.0);

            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            // The alpha can be toggled to make the overlay more/less transparent
            GL.Color4(0.0, 0.0, 0.0, 0.75);
            }
         }

      private void AdjustScrollBars()
         {
         if (this.openGLControlLoaded)
            {
            bool horizontalScrollBarVisible = false;
            bool vertizontalScrollBarVisible = false;
            Size neededViewSize = new Size(Convert.ToInt32(this.imageModel.Size.Width * this.imageModel.ZoomLevel), Convert.ToInt32(this.imageModel.Size.Height * this.imageModel.ZoomLevel));
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
         if (this.openGLControlLoaded)
            {
            this.FreeTextures();

            int unpackAlignment = GL.GetInteger(GetPName.UnpackAlignment);

            GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1);

            this.textures.Add(Texture.Underlay, GL.GenTexture());

            this.InitializeTexture(this.textures[Texture.Underlay]);

            if (this.imageModel.IsGrayscale)
               {
               // Camera (capture) images are retrieved as grayscale
               GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Luminance, this.imageModel.Size.Width, this.imageModel.Size.Height, 0, PixelFormat.Luminance, PixelType.UnsignedByte, this.imageModel.DisplayImageData);
               }
            else
               {
               // Static images are loaded as color
               GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, this.imageModel.Size.Width, this.imageModel.Size.Height, 0, PixelFormat.Rgb, PixelType.UnsignedByte, this.imageModel.DisplayImageData);
               }

            GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvMode, (int)TextureEnvMode.Replace);

            if (this.imageModel.OverlayImageData != null)
               {
               this.textures.Add(Texture.Overlay, GL.GenTexture());

               this.InitializeTexture(this.textures[Texture.Overlay]);

               GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, this.imageModel.Size.Width, this.imageModel.Size.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, this.imageModel.OverlayImageData);

               GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvColor, Color.White);
               GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvMode, (int)TextureEnvMode.Blend);
               }

            GL.PixelStore(PixelStoreParameter.UnpackAlignment, unpackAlignment);

            this.UpdateImageSize();
            }
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

      private void GLControl_Paint(object sender, PaintEventArgs e)
         {
         if (!this.openGLControlLoaded)
            {
            return;
            }

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
         GL.Vertex2(this.imageModel.Size.Width, 0);
         GL.TexCoord2(1, 1);
         GL.Vertex2(this.imageModel.Size.Width, this.imageModel.Size.Height);
         GL.TexCoord2(0, 1);
         GL.Vertex2(0, this.imageModel.Size.Height);

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
            GL.Vertex2(this.imageModel.Size.Width, 0);
            GL.TexCoord2(1, 1);
            GL.Vertex2(this.imageModel.Size.Width, this.imageModel.Size.Height);
            GL.TexCoord2(0, 1);
            GL.Vertex2(0, this.imageModel.Size.Height);

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

         if (this.PixelViewChanged != null)
            {
            this.PixelViewChanged(this, new PixelViewChangedEventArgs(pixelPosition));
            }
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

         this.GLControl_Paint(this, null);
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

         this.GLControl_Paint(this, null);
         }

      private void VerticalScrollBar_ValueChanged(object sender, EventArgs e)
         {
         this.translateY = -this.verticalScrollBar.Value;

         this.PrepareView();

         this.GLControl_Paint(this, null);
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
               if (this.ZoomLevelIncreased != null)
                  {
                  this.viewCenter = mouseClickPixel;

                  this.ZoomLevelIncreased(this, EventArgs.Empty);
                  }
               }
            else
               {
               if (this.SelectionChanged != null)
                  {
                  this.SelectionChanged(this, new SelectionChangedEventArgs(mouseClickPixel, true));
                  }
               }
            }
         else if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
            {
            if (this.zoomMode)
               {
               if (this.glControl.Size.Width > 1 && this.glControl.Size.Height > 1)
                  {
                  if (this.ZoomLevelDecreased != null)
                     {
                     this.viewCenter = mouseClickPixel;

                     this.ZoomLevelDecreased(this, EventArgs.Empty);
                     }
                  }
               }
            else
               {
               if (this.SelectionChanged != null)
                  {
                  this.SelectionChanged(this, new SelectionChangedEventArgs(mouseClickPixel, false));
                  }
               }
            }
         }

      private Point GetPointPositionInImage(int mouseX, int mouseY)
         {
         int imageX;
         int imageY;

         if (mouseX > this.glControl.Width)
            {
            imageX = this.imageModel.Size.Width - 1;
            }
         else
            {
            imageX = Convert.ToInt32(((mouseX - this.translateX) / this.imageModel.ZoomLevel) - 0.5);
            }

         if (mouseY > this.glControl.Height)
            {
            imageY = this.imageModel.Size.Height - 1;
            }
         else
            {
            imageY = Convert.ToInt32(((mouseY - this.translateY) / this.imageModel.ZoomLevel) - 0.5);
            }

         return new Point(imageX, imageY);
         }

      private void CenterView(Point center)
         {
         if (this.horizontalScrollBar.Visible)
            {
            double horizontalTranslation = ((center.X + 0.5) * this.imageModel.ZoomLevel) - (this.glControl.Width / 2);

            // The maximum value of a scroll bar using user interaction is Maximum-LargeChange+1. (see http://msdn.microsoft.com/en-us/library/system.windows.forms.scrollbar.maximum.aspx)
            this.horizontalScrollBar.Value = Convert.ToInt32(Math.Max(0, Math.Min(horizontalTranslation, this.horizontalScrollBar.Maximum - this.horizontalScrollBar.LargeChange + 1)));
            }
         else
            {
            this.horizontalScrollBar.Value = 0;
            }

         if (this.verticalScrollBar.Visible)
            {
            double verticalTranslation = ((center.Y + 0.5) * this.imageModel.ZoomLevel) - (this.glControl.Height / 2);

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

      private void UpdatePixelColor(Point pixelPosition, int gray, int[] rgb, double[] hsv)
         {
         if (this.imageModel.IsGrayscale)
            {
            this.rgbGrayColorToolStripStatusLabel.Text = string.Format("Gray: {0} ", gray);
            this.hsvColorToolStripStatusLabel.Text = string.Empty;
            }
         else
            {
            this.rgbGrayColorToolStripStatusLabel.Text = string.Format("R: {0} G: {1} B: {2}", rgb[0], rgb[1], rgb[2]);
            this.hsvColorToolStripStatusLabel.Text = string.Format("H: {0} S: {1} V: {2}", hsv[0], hsv[1], hsv[2]);
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
         this.imageSizeToolStripStatusLabel.Text = string.Format("({0}x{1})", this.imageModel.Size.Width, this.imageModel.Size.Height);

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
         }

      private void GLControl_Layout(object sender, LayoutEventArgs e)
         {
         this.openGLControlSizeUpdated = true;
         }
      }
   }
