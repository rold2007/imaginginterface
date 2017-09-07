// <copyright file="TaggerControllerTest.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Controllers.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ImageProcessing.Controllers;
    using ImageProcessing.Models;
    using ImagingInterface.Plugins;
   using Xunit;

   public class TaggerControllerTest : ControllersBaseTest
    {
        [Fact]
        public void Constructor()
        {
            ////TaggerController taggerController = this.ServiceLocator.GetInstance<TaggerController>();
        }

        [Fact]
        public void RawPluginView()
        {
            ////TaggerController taggerController = this.ServiceLocator.GetInstance<TaggerController>();
            ////IRawPluginView rotateView = taggerController.RawPluginView;

            ////Assert.IsNotNull(rotateView);
        }

        [Fact]
        public void RawPluginModel()
        {
            ////TaggerController taggerController = this.ServiceLocator.GetInstance<TaggerController>();
            ////IRawPluginModel rawPluginModel = taggerController.RawPluginModel;

            ////Assert.IsNotNull(rawPluginModel);
        }

        [Fact]
        public void DisplayName()
        {
            ////this.Container.RegisterSingleton<ITaggerModel, TaggerModel>();

            ////TaggerController taggerController = this.ServiceLocator.GetInstance<TaggerController>();
            ////ITaggerModel taggerModel = this.ServiceLocator.GetInstance<ITaggerModel>();

            ////Assert.AreEqual("Tagger", taggerModel.DisplayName);
        }

        [Fact]
        public void Active()
        {
            ////TaggerController taggerController = this.ServiceLocator.GetInstance<TaggerController>();

            ////Assert.IsTrue(taggerController.Active);
        }

        [Fact]
        public void Close()
        {
            ////this.Container.RegisterSingleton<ITaggerView, TaggerView>();

            ////TaggerView taggerView = this.ServiceLocator.GetInstance<ITaggerView>() as TaggerView;
            ////TaggerController taggerController = this.ServiceLocator.GetInstance<TaggerController>();
            ////bool closingCalled = false;
            ////bool closedCalled = false;

            ////taggerController.Closing += (sender, eventArgs) => { closingCalled = true; };
            ////taggerController.Closed += (sender, eventArgs) => { closedCalled = true; };

            ////taggerController.Close();

            ////Assert.IsTrue(closingCalled);
            ////Assert.IsTrue(closedCalled);
            ////Assert.IsTrue(taggerView.CloseCalled);
        }

        [Fact]
        public void ProcessImageData()
        {
            ////string displayName = Path.GetRandomFileName();
            ////string directory = Path.GetTempPath() + "Tagger" + @"\";

            ////try
            //   {
            //   ////this.Container.RegisterSingleton<IImageView, ImageView>();
            //   this.Container.RegisterSingleton<ITaggerModel, TaggerModel>();

            ////   TaggerController taggerController = this.ServiceLocator.GetInstance<TaggerController>();
            //   ITaggerModel taggerModel = this.ServiceLocator.GetInstance<ITaggerModel>();
            //   ImageController imageController = this.ServiceLocator.GetInstance<ImageController>();
            //   ImageManagerController imageManagerController = this.ServiceLocator.GetInstance<ImageManagerController>();
            //   ImageSourceController imageSourceController = this.Container.GetInstance<ImageSourceController>();
            //   ////ImageView imageView = this.Container.GetInstance<IImageView>() as ImageView;

            ////   imageSourceController.ImageData = new byte[10, 10, 1];

            ////   taggerController.Initialize();

            ////   ////using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
            //   ////   {
            //   ////   imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

            ////   ////   imageController.SetDisplayName(displayName);

            ////   ////   imageManagerController.AddImage(imageController);

            ////   ////   imageControllerWrapper.WaitForDisplayUpdate();
            ////   ////   }

            ////   string label = "Label";

            ////   taggerModel.Labels.Add(label);
            ////   taggerModel.LabelColors.Add(label, Color.FromArgb(0));
            ////   taggerModel.SelectedLabel = label;

            ////   // Tag a point
            ////   using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
            ////      {
            ////      ////imageView.TriggerSelectionChanged(new Point(1, 1), true);

            ////      imageControllerWrapper.WaitForDisplayUpdate();
            ////      }

            ////   // Add an already tagged point
            ////   ////imageView.TriggerSelectionChanged(new Point(1, 1), true);

            ////   // Try to untag an unexisting point
            ////   ////imageView.TriggerSelectionChanged(new Point(2, 2), false);

            ////   imageSourceController.ImageData = new byte[10, 10, 3];

            ////   // Tag a point in a 3-channels image
            ////   using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
            ////      {
            ////      ////imageView.TriggerSelectionChanged(new Point(2, 2), true);

            ////      imageControllerWrapper.WaitForDisplayUpdate();
            ////      }

            ////   // Untag a point
            ////   using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
            ////      {
            ////      ////imageView.TriggerSelectionChanged(new Point(2, 2), false);

            ////      imageControllerWrapper.WaitForDisplayUpdate();
            ////      }

            ////   // Save points
            ////   using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
            ////      {
            ////      imageController.Close();

            ////      imageControllerWrapper.WaitForClosed();
            ////      }

            ////   imageController = this.ServiceLocator.GetInstance<ImageController>();

            ////   // Load points with first display update
            ////   ////using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
            ////   ////   {
            ////   ////   imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

            ////   ////   imageController.SetDisplayName(displayName);

            ////   ////   imageManagerController.AddImage(imageController);

            ////   ////   imageControllerWrapper.WaitForDisplayUpdate();
            ////   ////   }

            ////   // Close and reopen the plugin to allow to call ExtractPoints from RegisterActiveImage()
            ////   taggerController.Close();

            ////   taggerController = this.ServiceLocator.GetInstance<TaggerController>();

            ////   taggerModel.Labels.Add(label);
            ////   taggerModel.LabelColors.Add(label, Color.FromArgb(0));
            ////   taggerModel.SelectedLabel = label;

            ////   taggerController.Initialize();
            ////   }
            ////finally
            ////   {
            ////   if (Directory.Exists(directory))
            ////      {
            ////      File.Delete(directory + '\\' + Path.GetFileNameWithoutExtension(displayName) + ".imagedata");
            ////      }
            ////   }
        }

        [Fact]
        public void LabelAdded()
        {
            ////this.Container.RegisterSingleton<ITaggerView, TaggerView>();
            ////this.Container.RegisterSingleton<ITaggerModel, TaggerModel>();

            //////TaggerView taggerView = this.ServiceLocator.GetInstance<ITaggerView>() as TaggerView;
            ////TaggerController taggerController = this.ServiceLocator.GetInstance<TaggerController>();
            ////ITaggerModel taggerModel = this.ServiceLocator.GetInstance<ITaggerModel>();

            ////taggerModel.AddedLabel = "AddedLabel";

            ////taggerController.Initialize();

            //////taggerView.TriggerLabelAdded();

            ////Assert.AreEqual(1, taggerModel.Labels.Count());
            ////Assert.AreEqual(1, taggerModel.LabelColors.Count());
        }

        [Fact]
        public void RegisterActiveImage()
        {
            ////string displayName = "temp";

            //////this.Container.RegisterSingleton<ITaggerView, TaggerView>();
            ////this.Container.RegisterSingleton<ITaggerModel, TaggerModel>();

            //////TaggerView taggerView = this.ServiceLocator.GetInstance<ITaggerView>() as TaggerView;
            ////TaggerController taggerController = this.ServiceLocator.GetInstance<TaggerController>();
            ////ITaggerModel taggerModel = this.ServiceLocator.GetInstance<ITaggerModel>();
            ////ImageController imageController = this.ServiceLocator.GetInstance<ImageController>();
            ////ImageSourceController imageSourceController = this.Container.GetInstance<ImageSourceController>();

            ////using (ImageControllerWrapper imageControllerWrapper = new ImageControllerWrapper(imageController))
            ////   {
            ////   imageController.InitializeImageSourceController(imageSourceController, imageSourceController.RawPluginModel);

            ////   imageController.SetDisplayName(displayName);

            ////   imageControllerWrapper.WaitForDisplayUpdate();
            ////   }
        }
    }
}
