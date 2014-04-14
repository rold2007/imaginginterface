namespace ImagingInterface.Controllers.Tests
   {
   using System;
   using System.Collections.Generic;
   using System.IO;
   using System.Linq;
   using System.Text;
   using System.Threading;
   using System.Threading.Tasks;
   using Emgu.CV;
   using Emgu.CV.Structure;
   using ImagingInterface.Controllers.Tests.Mocks;
   using ImagingInterface.Controllers.Tests.Views;
   using ImagingInterface.Models;
   using ImagingInterface.Plugins;
   using ImagingInterface.Views;
   using ImagingInterface.Views.EventArguments;
   using NUnit.Framework;

   [TestFixture]
   public class FileOperationControllerTest : ControllersBaseTest
      {
      [Test]
      public void Constructor()
         {
         FileOperationView fileOperationView = this.ServiceLocator.GetInstance<IFileOperationView>() as FileOperationView;

         Assert.IsNull(fileOperationView.OpenFileEventHandler());

         FileOperationController fileOperationController = new FileOperationController(fileOperationView, this.ServiceLocator);

         Assert.IsNotNull(fileOperationView.OpenFileEventHandler());
         }

      [Test]
      public void FileOpen()
         {
         this.Container.RegisterSingle<IImageView, ImageView>();
         this.Container.RegisterSingle<IFileSourceModel, FileSourceModel>();

         ImageView imageView = this.ServiceLocator.GetInstance<IImageView>() as ImageView;
         FileOperationView fileOperationView = this.ServiceLocator.GetInstance<IFileOperationView>() as FileOperationView;
         FileOperationController fileOperationController = this.ServiceLocator.GetInstance<IFileOperationController>() as FileOperationController;
         ImageManagerController imageManagerController = this.ServiceLocator.GetInstance<IImageManagerController>() as ImageManagerController;
         FileSourceModel fileSourceModel = this.Container.GetInstance<IFileSourceModel>() as FileSourceModel;

         fileOperationView.Files = new string[1] { "ValidFile" };

         Assert.IsNull(imageView.AssignedImageModel);
         Assert.IsNull(imageManagerController.GetActiveImage());

         fileSourceModel.ImageData = new byte[1, 1, 1];
         fileOperationView.TriggerOpenFileEvent();

         imageView.WaitForDisplayUpdate();

         Assert.IsNotNull(imageView.AssignedImageModel.DisplayImageData);
         Assert.IsNotNull(imageManagerController.GetActiveImage());

         fileOperationView.TriggerCloseFileEvent();

         fileOperationView.Files = null;
         fileSourceModel.ImageData = null;
         fileOperationView.TriggerOpenFileEvent();

         // Do not call imageView.WaitForDisplayUpdate() as the open will do nothing with null files
         Assert.IsNull(imageView.AssignedImageModel.DisplayImageData, "The array should still be null because no update was really done");
         Assert.IsNull(imageManagerController.GetActiveImage(), "The image should not be loaded when Files is null.");

         // Simulate an existing file that is not a valid image file
         fileOperationView.Files = new string[1] { "Dummy" };
         fileSourceModel.ImageData = null;
         fileOperationView.TriggerOpenFileEvent();

         imageView.WaitForDisplayUpdate();

         Assert.IsNull(imageView.AssignedImageModel.DisplayImageData, "An invalid file will return a null data array");
         Assert.IsNotNull(imageManagerController.GetActiveImage(), "The image should still be opened but empty");

         fileOperationView.TriggerCloseFileEvent();
         }

      [Test]
      public void FileClose()
         {
         IImageManagerController imageManagerController = this.Container.GetInstance<IImageManagerController>();
         IFileOperationController fileOperationController = this.Container.GetInstance<IFileOperationController>();
         FileOperationView fileOperationView = this.Container.GetInstance<IFileOperationView>() as FileOperationView;

         fileOperationView.Files = new string[2] { "ValidFile1", "ValidFile2" };

         // Make sure closing without any open file doesn't crash
         fileOperationView.TriggerCloseFileEvent();

         fileOperationView.TriggerOpenFileEvent();

         IImageController activeImageControllerToClose = imageManagerController.GetActiveImage();

         // CloseFile should close the active image controller
         fileOperationView.TriggerCloseFileEvent();

         // Make sure there is still an active image controller
         Assert.IsNotNull(imageManagerController.GetActiveImage());

         // Make sure the previously active image controller isn't active anymore
         Assert.AreNotSame(activeImageControllerToClose, imageManagerController.GetActiveImage());

         // Close the remaining image controller
         fileOperationView.TriggerCloseFileEvent();

         // There should be no more open image controller
         Assert.IsNull(imageManagerController.GetActiveImage());
         }

      [Test]
      public void FileCloseAll()
         {
         IImageManagerController imageManagerController = this.Container.GetInstance<IImageManagerController>();
         IFileOperationController fileOperationController = this.Container.GetInstance<IFileOperationController>();
         FileOperationView fileOperationView = this.Container.GetInstance<IFileOperationView>() as FileOperationView;

         fileOperationView.Files = new string[2] { "ValidFile1", "ValidFile2" };

         // Make sure closing without any open file doesn't crash
         fileOperationView.TriggerCloseAllFileEvent();

         fileOperationView.TriggerOpenFileEvent();

         Assert.IsNotNull(imageManagerController.GetActiveImage());

         fileOperationView.TriggerCloseAllFileEvent();

         Assert.IsNull(imageManagerController.GetActiveImage());
         }

      [Test]
      public void DragDrop()
         {
         this.Container.RegisterSingle<IImageView, ImageView>();
         this.Container.RegisterSingle<IFileSourceModel, FileSourceModel>();

         ImageView imageView = this.ServiceLocator.GetInstance<IImageView>() as ImageView;
         IImageManagerController imageManagerController = this.Container.GetInstance<IImageManagerController>();
         IFileOperationController fileOperationController = this.Container.GetInstance<IFileOperationController>();
         FileOperationView fileOperationView = this.Container.GetInstance<IFileOperationView>() as FileOperationView;
         FileSourceModel fileSourceModel = this.Container.GetInstance<IFileSourceModel>() as FileSourceModel;

         fileOperationView.Files = new string[1] { "ValidFile" };

         Assert.IsNull(imageView.AssignedImageModel);
         Assert.IsNull(imageManagerController.GetActiveImage());

         fileSourceModel.ImageData = new byte[1, 1, 1];
         fileOperationView.TriggerDragDropFileEvent(fileOperationView.Files);

         imageView.WaitForDisplayUpdate();

         Assert.IsNotNull(imageView.AssignedImageModel.DisplayImageData);
         Assert.IsNotNull(imageManagerController.GetActiveImage());

         fileOperationView.TriggerCloseFileEvent();

         fileOperationView.TriggerDragDropFileEvent(null);

         // Do not call imageView.WaitForDisplayUpdate() as the open will do nothing with null files
         Assert.IsNull(imageView.AssignedImageModel.DisplayImageData, "The array should still be null because no update was really done");
         Assert.IsNull(imageManagerController.GetActiveImage(), "With a null Files no image should get opened");

         // Simulate an existing file that is not a valid image file
         fileSourceModel.ImageData = null;
         fileOperationView.TriggerDragDropFileEvent(new string[] { "Dummy" });

         imageView.WaitForDisplayUpdate();

         Assert.IsNull(imageView.AssignedImageModel.DisplayImageData, "An invalid file will return a null data array");
         Assert.IsNotNull(imageManagerController.GetActiveImage(), "The image should still be opened but empty");

         fileOperationView.TriggerCloseFileEvent();
         }
      }
   }
