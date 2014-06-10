namespace ImageProcessing.Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Drawing;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImageMagick;
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

      public void Close()
         {
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
         try
            {
            byte[] imageData;
            Size imageSize;

            using (MagickImage magickImage = new MagickImage(this.fileSourceModel.Filename))
               {
               magickImage.Format = MagickFormat.Rgb;

               imageData = magickImage.ToByteArray();
               imageSize = new Size(magickImage.Width, magickImage.Height);
               }

            this.fileSourceModel.ImageData = new byte[imageSize.Height, imageSize.Width, 3];

            int imageDataIndex = 0;

            for (int y = 0; y < imageSize.Height; y++)
               {
               for (int x = 0; x < imageSize.Width; x++)
                  {
                  for (int channel = 0; channel < 3; channel++)
                     {
                     this.fileSourceModel.ImageData[y, x, channel] = imageData[imageDataIndex];

                     imageDataIndex++;
                     }
                  }
               }
            }
         catch (ArgumentException)
            {
            this.fileSourceModel.ImageData = null;
            }
         catch (MagickMissingDelegateErrorException)
            {
            this.fileSourceModel.ImageData = null;
            }
         }
      }
   }
