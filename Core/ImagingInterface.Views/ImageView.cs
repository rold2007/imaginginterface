namespace ImagingInterface.Views
   {
   using System;
   using System.Diagnostics;
   using System.Drawing;
   using System.Windows.Forms;
   using ImagingInterface.Models;
   using OpenTK;
   using OpenTK.Graphics;
   using OpenTK.Graphics.OpenGL;

   public partial class ImageView : UserControl, IImageView
      {
      private bool glControlLoaded = false;
      private bool glControlInitialized = false;
      private int texture;
      private IImageModel imageModel;

      public ImageView()
         {
         this.InitializeComponent();
         }

      public void AssignImageModel(IImageModel imageModel)
         {
         this.imageModel = imageModel;

         if (imageModel.ImageData != null)
            {
            this.glControl.Size = imageModel.Size;

            // Force reset of GLControl when assigning new image
            this.glControlInitialized = false;

            this.InitializeGLControl();

            this.glControl.Invalidate();
            }
         }

      public void Close()
         {
         if (this.texture != 0)
            {
            GL.DeleteTexture(this.texture);

            this.texture = 0;
            }

         this.Dispose();
         }

      private void GLControl_Load(object sender, System.EventArgs e)
         {
         this.glControlLoaded = true;

         this.InitializeGLControl();
         }

      private void InitializeGLControl()
         {
         if (this.glControlLoaded)
            {
            if (!this.glControlInitialized)
               {
               this.glControl.MakeCurrent();

               GL.ClearColor(SystemColors.Control);

               GL.MatrixMode(MatrixMode.Projection);
               GL.LoadIdentity();
               GL.Ortho(0.0, 1.0, 1.0, 0.0, -1.0, 1.0);
               GL.Viewport(0, 0, this.glControl.Width, this.glControl.Height);

               if (this.texture != 0)
                  {
                  GL.DeleteTexture(this.texture);
                  }

               this.texture = GL.GenTexture();

               GL.BindTexture(TextureTarget.Texture2D, this.texture);

               GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureBaseLevel, 0);
               GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMaxLevel, 0);
               GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToBorder);
               GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToBorder);

               GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, this.imageModel.Size.Width, this.imageModel.Size.Height, 0, PixelFormat.Rgb, PixelType.UnsignedByte, this.imageModel.ImageData);
               }

            this.glControlInitialized = true;
            }
         }

      private void GLControl_Resize(object sender, System.EventArgs e)
         {
         if (!this.glControlLoaded)
            {
            return;
            }
         }

      private void GLControl_Paint(object sender, PaintEventArgs e)
         {
         if (!this.glControlLoaded)
            {
            return;
            }

         this.glControl.MakeCurrent();

         GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

         GL.MatrixMode(MatrixMode.Texture);

         GL.Enable(EnableCap.Texture2D);

         GL.BindTexture(TextureTarget.Texture2D, this.texture);

         GL.Begin(PrimitiveType.Quads);

         GL.TexCoord2(0, 0);
         GL.Vertex2(0, 0);
         GL.TexCoord2(this.imageModel.Size.Width, 0);
         GL.Vertex2(this.imageModel.Size.Width, 0);
         GL.TexCoord2(this.imageModel.Size.Width, this.imageModel.Size.Height);
         GL.Vertex2(this.imageModel.Size.Width, this.imageModel.Size.Height);
         GL.TexCoord2(0, this.imageModel.Size.Height);
         GL.Vertex2(0, this.imageModel.Size.Height);

         GL.End();

         this.glControl.SwapBuffers();
         }
      }
   }
