﻿namespace ImagingInterface.Controllers
{
    using System;
    using System.Collections.Generic;
    using ImagingInterface.Plugins;
    using Microsoft.Practices.ServiceLocation;

    public class ImageSourceManager
    {
        private IServiceLocator serviceLocator;

        public ImageSourceManager(IServiceLocator serviceLocator)
        {
            this.ImageSources = new List<IImageSource>();
            this.serviceLocator = serviceLocator;
        }

        public event EventHandler<ImageSourceAddedEventArgs> ImageSourceAdded;

        public event EventHandler<ImageSourceRemovedEventArgs> ImageSourceRemoved;

        private List<IImageSource> ImageSources
        {
            get;
            set;
        }

        public void AddImageFiles(IEnumerable<string> files)
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
        }

        public void RemoveImageSource(IImageSource imageSource)
        {
            if (imageSource == null)
            {
                throw new ArgumentNullException("imageSource");
            }

            this.ImageSources.Remove(imageSource);

            this.TriggerImageSourceRemoved(imageSource);
        }

        private void AddImageSources(IList<IImageSource> imageSources)
        {
            this.ImageSources.AddRange(imageSources);

            foreach (IImageSource imageSource in imageSources)
            {
                this.TriggerImageSourceAdded(imageSource);
            }
        }

        private IFileSource OpenFile(string file)
        {
            IFileSource fileSource = this.serviceLocator.GetInstance<IFileSource>();

            if (fileSource.LoadFile(file))
            {
                return fileSource;
            }
            else
            {
                return null;
            }
        }

        private void TriggerImageSourceAdded(IImageSource imageSource)
        {
            this.ImageSourceAdded?.Invoke(this, new ImageSourceAddedEventArgs(imageSource));
        }

        private void TriggerImageSourceRemoved(IImageSource imageSource)
        {
            this.ImageSourceRemoved?.Invoke(this, new ImageSourceRemovedEventArgs(imageSource));
        }
    }
}
