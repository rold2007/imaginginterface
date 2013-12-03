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
         FileOperationView fileOperationView = this.ServiceLocator.GetInstance<IFileOperationView>() as FileOperationView;
         FileOperationController fileOperationController = this.ServiceLocator.GetInstance<IFileOperationController>() as FileOperationController;
         ImageManagerController imageViewManagerController = this.ServiceLocator.GetInstance<IImageManagerController>() as ImageManagerController;

         string tempFileName = string.Empty;

         try
            {
            tempFileName = Path.GetRandomFileName() + ".png";

            using (Image<Gray, byte> image = new Image<Gray, byte>(1, 1))
               {
               image.Save(tempFileName);

               fileOperationView.Files = new string[1] { tempFileName };

               Assert.IsNull(imageViewManagerController.GetActiveImageController());

               fileOperationView.TriggerOpenFileEvent();

               Assert.IsNotNull(imageViewManagerController.GetActiveImageController());

               fileOperationView.TriggerCloseFileEvent();
               fileOperationView.Files = null;
               fileOperationView.TriggerOpenFileEvent();

               Assert.IsNull(imageViewManagerController.GetActiveImageController(), "The image should not be loaded when Files is null.");

               fileOperationView.Files = new string[1] { "Dummy" };

               fileOperationView.TriggerOpenFileEvent();

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
         IFileOperationController fileOperationController = this.Container.GetInstance<IFileOperationController>();
         FileOperationView fileOperationView = this.Container.GetInstance<IFileOperationView>() as FileOperationView;

         string tempFileName = string.Empty;

         try
            {
            tempFileName = Path.GetRandomFileName() + ".png";

            using (Image<Gray, byte> image = new Image<Gray, byte>(1, 1))
               {
               image.Save(tempFileName);

               fileOperationView.Files = new string[2] { tempFileName, tempFileName };

               // Make sure closing without any open file doesn't crash
               fileOperationView.TriggerCloseFileEvent();

               fileOperationView.TriggerOpenFileEvent();

               IImageController activeImageControllerToClose = imageManagerController.GetActiveImageController();

               // CloseFile should close the active image controller
               fileOperationView.TriggerCloseFileEvent();

               // Make sure there is still an active image controller
               Assert.IsNotNull(imageManagerController.GetActiveImageController());

               // Make sure the previously active image controller isn't active anymore
               Assert.AreNotSame(activeImageControllerToClose, imageManagerController.GetActiveImageController());

               // Close the remaining image controller
               fileOperationView.TriggerCloseFileEvent();

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
         IFileOperationController fileOperationController = this.Container.GetInstance<IFileOperationController>();
         FileOperationView fileOperationView = this.Container.GetInstance<IFileOperationView>() as FileOperationView;

         string tempFileName = string.Empty;

         try
            {
            tempFileName = Path.GetRandomFileName() + ".png";

            using (Image<Gray, byte> image = new Image<Gray, byte>(1, 1))
               {
               image.Save(tempFileName);

               fileOperationView.Files = new string[2] { tempFileName, tempFileName };

               // Make sure closing without any open file doesn't crash
               fileOperationView.TriggerCloseAllFileEvent();

               fileOperationView.TriggerOpenFileEvent();

               Assert.IsNotNull(imageManagerController.GetActiveImageController());

               fileOperationView.TriggerCloseAllFileEvent();

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
         IFileOperationController fileOperationController = this.Container.GetInstance<IFileOperationController>();
         FileOperationView fileOperationView = this.Container.GetInstance<IFileOperationView>() as FileOperationView;

         string tempFileName = string.Empty;

         try
            {
            tempFileName = Path.GetRandomFileName() + ".png";

            using (Image<Gray, byte> image = new Image<Gray, byte>(1, 1))
               {
               image.Save(tempFileName);

               fileOperationView.Files = new string[1] { tempFileName };

               Assert.IsNull(imageManagerController.GetActiveImageController());

               fileOperationView.TriggerDragDropFileEvent(fileOperationView.Files);

               Assert.IsNotNull(imageManagerController.GetActiveImageController());

               fileOperationView.TriggerCloseFileEvent();

               fileOperationView.TriggerDragDropFileEvent(null);

               Assert.IsNull(imageManagerController.GetActiveImageController(), "The image should not be loaded when Files is null.");

               fileOperationView.TriggerDragDropFileEvent(new string[] { "Dummy" });

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
