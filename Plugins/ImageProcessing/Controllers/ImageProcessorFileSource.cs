namespace ImageProcessing.Controllers
{
   using System;
   using System.ComponentModel;
   using System.Diagnostics;
   using System.Drawing;
   using ImageProcessor;
   using ImageProcessor.Common.Exceptions;
   using ImageProcessor.Imaging;
   using ImagingInterface.Plugins;

   public class ImageProcessorFileSource : IFileSource
   {
      ////public string DisplayName
      ////   {
      ////   get; // ncrunch: no coverage
      ////   set; // ncrunch: no coverage
      ////   }

      ////public string Filename
      ////   {
      ////   get;
      ////   set;
      ////   }

      public ImageProcessorFileSource()
      {
      }

      public event CancelEventHandler Closing;

      public event EventHandler Closed;

      public bool Active
      {
         get
         {
            return false;
         }
      }

      public string Filename
      {
         get;
         private set;
      }

      public byte[,,] ImageData
      {
         get;
         set;
      }

      public string ImageName
      {
         get
         {
            return this.Filename;
         }
      }

      public void Initialize()
      {
      }

      public bool LoadFile(string file)
      {
         Debug.Assert(this.Filename == null, "The source has already been initialized.");

         this.Filename = file;

         // This FileSource class directly loads the image. If this causes performance issues at some point
         //  new FileSource class should be created with some kind of "delay load".
         this.LoadImage();

         if (this.ImageData != null)
         {
            return true;
         }
         else
         {
            return false;
         }
      }

      public void Close()
      {
         CancelEventArgs cancelEventArgs = new CancelEventArgs();

         if (this.Closing != null)
         {
            this.Closing(this, cancelEventArgs);
         }

         if (!cancelEventArgs.Cancel)
         {
            if (this.Closed != null)
            {
               this.Closed(this, EventArgs.Empty);
            }
         }
      }

      public bool IsDynamic(IRawPluginModel rawPluginModel)
      {
         return false;
      }

      public byte[,,] NextImageData(IRawPluginModel rawPluginModel)
      {
         ////IFileSourceModel fileSourceModel = rawPluginModel as IFileSourceModel;

         ////if (fileSourceModel.ImageData == null)
         ////   {
         ////   this.LoadImage();
         ////   }

         ////return fileSourceModel.ImageData;
         return null;
      }

      public void Disconnected()
      {
      }

      private void LoadImage()
      {
         if (this.Filename != null)
         {
            try
            {
               using (ImageFactory imageFactory = new ImageFactory())
               {
                  imageFactory.Load(this.Filename);

                  Image image = imageFactory.Image;

                  using (FastBitmap fastBitmap = new FastBitmap(image))
                  {
                     this.ImageData = new byte[image.Size.Height, image.Size.Width, 3];

                     for (int y = 0; y < image.Size.Height; y++)
                     {
                        for (int x = 0; x < image.Size.Width; x++)
                        {
                           Color color = fastBitmap.GetPixel(x, y);

                           this.ImageData[y, x, 0] = color.R;
                           this.ImageData[y, x, 1] = color.G;
                           this.ImageData[y, x, 2] = color.B;
                        }
                     }
                  }
               }
            }
            catch (ImageFormatException)
            {
               this.ImageData = null;
            }
         }
         else
         {
            this.ImageData = null;
         }
      }
   }
}
