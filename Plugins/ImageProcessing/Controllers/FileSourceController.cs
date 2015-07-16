namespace ImageProcessing.Controllers
   {
   using System;
   using System.ComponentModel;
   using System.Drawing;
   using ImageProcessor;
   using ImageProcessor.Common.Exceptions;
   using ImageProcessor.Imaging;
   using ImagingInterface.Plugins;

   public class FileSourceController : IFileSourceController
      {
      private IFileSourceModel fileSourceModel;

      public FileSourceController(IFileSourceModel fileSourceModel)
         {
         this.fileSourceModel = fileSourceModel;
         }

      public event CancelEventHandler Closing;

      public event EventHandler Closed;

      public IRawPluginView RawPluginView
         {
         get;
         private set;
         }

      public IRawPluginModel RawPluginModel
         {
         get
            {
            return this.fileSourceModel;
            }
         }

      public bool Active
         {
         get
            {
            return false;
            }
         }

      public string Filename
         {
         get
            {
            return this.fileSourceModel.Filename;
            }

         set
            {
            this.fileSourceModel.Filename = value;
            }
         }

      public void Initialize()
         {
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

      public byte[, ,] NextImageData(IRawPluginModel rawPluginModel)
         {
         IFileSourceModel fileSourceModel = rawPluginModel as IFileSourceModel;

         if (fileSourceModel.ImageData == null)
            {
            this.LoadImage();
            }

         return fileSourceModel.ImageData;
         }

      public void Disconnected()
         {
         }

      private void LoadImage()
         {
         if (this.fileSourceModel.Filename != null)
            {
            try
               {
               using (ImageFactory imageFactory = new ImageFactory())
                  {
                  imageFactory.Load(this.fileSourceModel.Filename);
                  Image image = imageFactory.Image;

                  using (FastBitmap fastBitmap = new FastBitmap(image))
                     {
                     this.fileSourceModel.ImageData = new byte[image.Size.Height, image.Size.Width, 3];

                     int imageDataIndex = 0;

                     for (int y = 0; y < image.Size.Height; y++)
                        {
                        for (int x = 0; x < image.Size.Width; x++)
                           {
                           Color color = fastBitmap.GetPixel(x, y);

                           this.fileSourceModel.ImageData[y, x, 0] = color.R;
                           this.fileSourceModel.ImageData[y, x, 1] = color.G;
                           this.fileSourceModel.ImageData[y, x, 2] = color.B;

                           imageDataIndex++;
                           }
                        }
                     }
                  }
               }
            catch (ImageFormatException)
               {
               this.fileSourceModel.ImageData = null;
               }
            }
         else
            {
            this.fileSourceModel.ImageData = null;
            }
         }
      }
   }
