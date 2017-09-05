// <copyright file="ApplicationLogic.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Views
{
   using System;
   using System.Collections.Generic;
   using ImagingInterface.Controllers.Interfaces;
   using ImagingInterface.Plugins;

   public class ApplicationLogic : IApplicationLogic
   {
      private IImageViewFactory imageViewFactory;
      private ImageManagerView imageManagerView;
      private IPluginViewFactory pluginViewFactory;
      private PluginManagerView pluginManagerView;

      public ApplicationLogic(IImageViewFactory imageViewFactory, IPluginViewFactory pluginViewFactory)
      {
         this.imageViewFactory = imageViewFactory;
         this.pluginViewFactory = pluginViewFactory;
      }

      public void AddImageManagerView(ImageManagerView imageManagerView)
      {
         if (this.imageManagerView != null)
         {
            throw new InvalidOperationException("The ImageManagerView is already initialized.");
         }

         this.imageManagerView = imageManagerView;
      }

      public void AddPluginManagerView(PluginManagerView pluginManagerView)
      {
         if (this.pluginManagerView != null)
         {
            throw new InvalidOperationException("The PluginManagerView is already initialized.");
         }

         this.pluginManagerView = pluginManagerView;
      }

      public void ManageNewImageSources(IEnumerable<IImageSource> imageSources)
      {
         this.ValidateImageManagerView();

         this.AddImageViewToCurrentImageManagerView(imageSources);
      }

      public void RemoveImage(IImageView imageView)
      {
         this.ValidateImageManagerView();

         this.RemoveImageViewFromCurrentImageManagerView(imageView);
      }

      public void ManageNewPlugin(string pluginName)
      {
         this.ValidatePluginManagerView();

         IPluginView pluginView = this.pluginViewFactory.CreateNew(pluginName);

         if (pluginView != null)
         {
            this.pluginManagerView.AddPlugin(pluginView);
         }
      }

      public void RemovePlugin(IPluginView pluginView)
      {
         this.ValidatePluginManagerView();

         this.RemovePluginViewFromCurrentPluginManagerView(pluginView);
      }

      private void ValidateImageManagerView()
      {
         if (this.imageManagerView == null)
         {
            throw new InvalidOperationException("The ImageManagerView was not initialized.");
         }
      }

      private void ValidatePluginManagerView()
      {
         if (this.pluginManagerView == null)
         {
            throw new InvalidOperationException("The PluginManagerView was not initialized.");
         }
      }

      private void AddImageViewToCurrentImageManagerView(IEnumerable<IImageSource> imageSources)
      {
         foreach (IImageSource imageSource in imageSources)
         {
            if (this.imageViewFactory.CreateNew() is ImageView imageView)
            {
               imageView.ImageSource = imageSource;

               this.imageManagerView.AddImageToNewtab(imageView);
            }
            else
            {
               throw new InvalidOperationException("Unable to create ImageView.");
            }
         }
      }

      private void RemoveImageViewFromCurrentImageManagerView(IImageView imageView)
      {
         ImageView imageViewObject = imageView as ImageView;

         if (imageViewObject == null)
         {
            throw new ArgumentException("imageView");
         }

         this.imageManagerView.RemoveImageView(imageViewObject);

         imageViewObject.Close();
      }

      private void RemovePluginViewFromCurrentPluginManagerView(IPluginView pluginView)
      {
         this.pluginManagerView.RemovePlugin(pluginView);

         pluginView.Close();
      }
   }
}
