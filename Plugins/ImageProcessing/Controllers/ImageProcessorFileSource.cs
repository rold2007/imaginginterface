// <copyright file="ImageProcessorFileSource.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Controllers
{
   using System;
   using System.ComponentModel;
   using System.Diagnostics;
   using System.Diagnostics.CodeAnalysis;
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

      public event EventHandler ImageDataUpdated;

      public event CancelEventHandler Closing;

      public event EventHandler Closed;

      [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Will be fixed when done refactoring.")]
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

      [SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", Justification = "Too much work for now.")]
      [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Too much work for now.")]
      public byte[,,] OriginalImageData
      {
         get;
         set;
      }

      [SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", Justification = "Too much work for now.")]
      [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Too much work for now.")]
      public byte[,,] UpdatedImageData
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

      [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Will be fixed when done refactoring.")]
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

         if (this.OriginalImageData != null)
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

         this.Closing?.Invoke(this, cancelEventArgs);

         if (!cancelEventArgs.Cancel)
         {
            this.Closed?.Invoke(this, EventArgs.Empty);
         }
      }

      ////public bool IsDynamic(IRawPluginModel rawPluginModel)
      ////{
      ////   return false;
      ////}

      ////public byte[,,] NextImageData(IRawPluginModel rawPluginModel)
      ////{
      ////   ////IFileSourceModel fileSourceModel = rawPluginModel as IFileSourceModel;

      ////   ////if (fileSourceModel.ImageData == null)
      ////   ////   {
      ////   ////   this.LoadImage();
      ////   ////   }

      ////   ////return fileSourceModel.ImageData;
      ////   return null;
      ////}

      [SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", Justification = "Too much work for now.")]
      public void UpdateImageData(byte[,,] updatedImageData)
      {
         this.UpdatedImageData = updatedImageData;

         this.TriggerImageDataUpdated();
      }

      public void Disconnected()
      {
      }

      [SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", Justification = "Too much work for now.")]
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
                     this.OriginalImageData = new byte[image.Size.Height, image.Size.Width, 3];

                     for (int y = 0; y < image.Size.Height; y++)
                     {
                        for (int x = 0; x < image.Size.Width; x++)
                        {
                           Color color = fastBitmap.GetPixel(x, y);

                           this.OriginalImageData[y, x, 0] = color.R;
                           this.OriginalImageData[y, x, 1] = color.G;
                           this.OriginalImageData[y, x, 2] = color.B;
                        }
                     }
                  }
               }

               this.UpdatedImageData = this.OriginalImageData.Clone() as byte[,,];

               this.TriggerImageDataUpdated();
            }
            catch (ImageFormatException)
            {
               this.OriginalImageData = null;
               this.UpdatedImageData = null;
            }
         }
         else
         {
            this.OriginalImageData = null;
            this.UpdatedImageData = null;
         }
      }

      private void TriggerImageDataUpdated()
      {
         this.ImageDataUpdated?.Invoke(this, EventArgs.Empty);
      }
   }
}
