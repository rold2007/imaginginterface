namespace ImagingInterface.Controllers.Tests
   {
   using System;
   using System.Collections.Generic;
   using System.IO;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using Emgu.CV;
   using Emgu.CV.Structure;
   using ImagingInterface.Controllers.Tests.Views;
   using ImagingInterface.Models;
   using ImagingInterface.Views;
   using ImagingInterface.Views.EventArguments;
   using NUnit.Framework;

   [TestFixture]
   public class FileControllerTest : ControllersBaseTest
      {
      [Test]
      public void Constructor()
         {
         FileView fileView = this.ServiceLocator.GetInstance<IFileView>() as FileView;

         Assert.IsNull(fileView.OpenFileEventHandler());

         FileController fileController = new FileController(fileView, this.ServiceLocator);

         Assert.IsNotNull(fileView.OpenFileEventHandler());
         }

      [Test]
      public void FileOpen()
         {
         FileView fileView = this.ServiceLocator.GetInstance<IFileView>() as FileView;
         FileController fileController = this.ServiceLocator.GetInstance<IFileController>() as FileController;
         ImageManagerController imageViewManagerController = this.ServiceLocator.GetInstance<IImageManagerController>() as ImageManagerController;

         string tempFileName = string.Empty;

         try
            {
            tempFileName = Path.GetRandomFileName() + ".png";

            using (Image<Gray, byte> image = new Image<Gray, byte>(1, 1))
               {
               image.Save(tempFileName);

               fileView.Files = new string[1] { tempFileName };

               Assert.IsNull(imageViewManagerController.GetActiveImageController());

               fileView.TriggerOpenFileEvent();

               Assert.IsNotNull(imageViewManagerController.GetActiveImageController());

               fileView.TriggerCloseFileEvent();
               fileView.Files = null;
               fileView.TriggerOpenFileEvent();

               Assert.IsNull(imageViewManagerController.GetActiveImageController(), "The image should not be loaded when Files is null.");

               fileView.Files = new string[1] { "Dummy" };

               fileView.TriggerOpenFileEvent();

               Assert.IsNull(imageViewManagerController.GetActiveImageController(), "The image should not be loaded when Files is invalid.");
               }
            }
         finally
            {
            if (!string.IsNullOrEmpty(tempFileName))
               {
               File.Delete(tempFileName);
               }
            }
         }

      [Test]
      public void FileClose()
         {
         IImageManagerController imageManagerController = this.Container.GetInstance<IImageManagerController>();
         IFileController fileController = this.Container.GetInstance<IFileController>();
         FileView fileView = this.Container.GetInstance<IFileView>() as FileView;

         string tempFileName = string.Empty;

         try
            {
            tempFileName = Path.GetRandomFileName() + ".png";

            using (Image<Gray, byte> image = new Image<Gray, byte>(1, 1))
               {
               image.Save(tempFileName);

               fileView.Files = new string[2] { tempFileName, tempFileName };

               // Make sure closing without any open file doesn't crash
               fileView.TriggerCloseFileEvent();

               fileView.TriggerOpenFileEvent();

               IImageController activeImageControllerToClose = imageManagerController.GetActiveImageController();

               // CloseFile should close the active image controller
               fileView.TriggerCloseFileEvent();

               // Make sure there is still an active image controller
               Assert.IsNotNull(imageManagerController.GetActiveImageController());

               // Make sure the previously active image controller isn't active anymore
               Assert.AreNotSame(activeImageControllerToClose, imageManagerController.GetActiveImageController());

               // Close the remaining image controller
               fileView.TriggerCloseFileEvent();

               // There should be no more open image controller
               Assert.IsNull(imageManagerController.GetActiveImageController());
               }
            }
         finally
            {
            if (!string.IsNullOrEmpty(tempFileName))
               {
               File.Delete(tempFileName);
               }
            }
         }

      [Test]
      public void FileCloseAll()
         {
         IImageManagerController imageManagerController = this.Container.GetInstance<IImageManagerController>();
         IFileController fileController = this.Container.GetInstance<IFileController>();
         FileView fileView = this.Container.GetInstance<IFileView>() as FileView;

         string tempFileName = string.Empty;

         try
            {
            tempFileName = Path.GetRandomFileName() + ".png";

            using (Image<Gray, byte> image = new Image<Gray, byte>(1, 1))
               {
               image.Save(tempFileName);

               fileView.Files = new string[2] { tempFileName, tempFileName };

               // Make sure closing without any open file doesn't crash
               fileView.TriggerCloseAllFileEvent();

               fileView.TriggerOpenFileEvent();

               Assert.IsNotNull(imageManagerController.GetActiveImageController());

               fileView.TriggerCloseAllFileEvent();

               Assert.IsNull(imageManagerController.GetActiveImageController());
               }
            }
         finally
            {
            if (!string.IsNullOrEmpty(tempFileName))
               {
               File.Delete(tempFileName);
               }
            }
         }

      [Test]
      public void DragDrop()
         {
         IImageManagerController imageManagerController = this.Container.GetInstance<IImageManagerController>();
         IFileController fileController = this.Container.GetInstance<IFileController>();
         FileView fileView = this.Container.GetInstance<IFileView>() as FileView;

         string tempFileName = string.Empty;

         try
            {
            tempFileName = Path.GetRandomFileName() + ".png";

            using (Image<Gray, byte> image = new Image<Gray, byte>(1, 1))
               {
               image.Save(tempFileName);

               fileView.Files = new string[1] { tempFileName };

               Assert.IsNull(imageManagerController.GetActiveImageController());

               fileView.TriggerDragDropFileEvent(fileView.Files);

               Assert.IsNotNull(imageManagerController.GetActiveImageController());

               fileView.TriggerCloseFileEvent();

               fileView.TriggerDragDropFileEvent(null);

               Assert.IsNull(imageManagerController.GetActiveImageController(), "The image should not be loaded when Files is null.");

               fileView.TriggerDragDropFileEvent(new string[] { "Dummy" });

               Assert.IsNull(imageManagerController.GetActiveImageController(), "The image should not be loaded when Files is invalid.");
               }
            }
         finally
            {
            if (!string.IsNullOrEmpty(tempFileName))
               {
               File.Delete(tempFileName);
               }
            }
         }
      }
   }
