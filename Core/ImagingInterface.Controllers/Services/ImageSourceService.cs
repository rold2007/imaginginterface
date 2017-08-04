namespace ImagingInterface.Controllers.Services
{
   using System;
   using System.Collections.Generic;
   using ImagingInterface.Controllers.EventArguments;
   using ImagingInterface.Plugins;

   public class ImageSourceService
   {
      private IFileSourceFactory fileSourceFactory;

      public ImageSourceService(IFileSourceFactory fileSourceFactory)
      {
         this.ImageSources = new List<IImageSource>();

         this.fileSourceFactory = fileSourceFactory;
      }

      //public event EventHandler<ImageSourceAddedEventArgs> ImageSourceAdded;

      //public event EventHandler<ImageSourceRemovedEventArgs> ImageSourceRemoved;

      private List<IImageSource> ImageSources
      {
         get;
         set;
      }

      public IEnumerable<IImageSource> AddImageFiles(IEnumerable<string> files)
      {
         if (files == null)
         {
            throw new ArgumentNullException("files");
         }

         List<IImageSource> fileSources = new List<IImageSource>();

         foreach (string file in files)
         {
            IFileSource fileSource = this.OpenFile(file);

            if (fileSource != null)
            {
               fileSources.Add(fileSource);
            }
         }

         this.AddImageSources(fileSources);

         return fileSources;
      }

      public void RemoveImageSource(IImageSource imageSource)
      {
         if (imageSource == null)
         {
            throw new ArgumentNullException("imageSource");
         }

         this.ImageSources.Remove(imageSource);
      }

      private void AddImageSources(IList<IImageSource> imageSources)
      {
         this.ImageSources.AddRange(imageSources);
      }

      private IFileSource OpenFile(string file)
      {
         IFileSource fileSource = this.fileSourceFactory.CreateNew();

         if (fileSource.LoadFile(file))
         {
            return fileSource;
         }
         else
         {
            return null;
         }
      }
   }
}
