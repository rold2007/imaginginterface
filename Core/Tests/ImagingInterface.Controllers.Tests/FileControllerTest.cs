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
   using ImagingInterface.Models;
   using ImagingInterface.Views;
   using ImagingInterface.Views.EventArguments;
   using NUnit.Framework;

   [TestFixture]
   public class FileControllerTest : ControllersBaseTest
      {
      private FileView fileView;
      private ImageViewManager imageViewManager;
      private ImageManagerController imageViewManagerController;

      [SetUp]
      public void SetUp()
         {
         this.fileView = new FileView();
         this.imageViewManager = new ImageViewManager();
         this.imageViewManagerController = new ImageManagerController(this.imageViewManager);

         this.Container.RegisterSingle<IFileView>(this.fileView);
         this.Container.RegisterSingle<IImageManagerView>(this.imageViewManager);
         this.Container.RegisterSingle<IImageManagerController>(this.imageViewManagerController);
         }

      [Test]
      public void Constructor()
         {
         Assert.IsNull(this.fileView.OpenFileEventHandler());

         FileController fileController = new FileController(this.fileView, this.ServiceLocator);

         Assert.IsNotNull(this.fileView.OpenFileEventHandler());
         }

      [Test]
      public void FileOpen()
         {
         FileController fileController = new FileController(this.fileView, this.ServiceLocator);

         string tempFileName = string.Empty;

         try
            {
            tempFileName = Path.GetRandomFileName() + ".png";

            using (Image<Gray, byte> image = new Image<Gray, byte>(1, 1))
               {
               image.Save(tempFileName);

               this.fileView.Files = new string[1] { tempFileName };

               Assert.IsNull(this.imageViewManagerController.GetActiveImageController());

               this.fileView.TriggerOpenFileEvent();

               Assert.IsNotNull(this.imageViewManagerController.GetActiveImageController());

               this.fileView.TriggerCloseFileEvent();

               this.fileView.Files = null;

               this.fileView.TriggerOpenFileEvent();

               Assert.IsNull(this.imageViewManagerController.GetActiveImageController(), "The image should not be loaded when Files is null.");
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
         FileController fileController = new FileController(this.fileView, this.ServiceLocator);

         string tempFileName = string.Empty;

         try
            {
            tempFileName = Path.GetRandomFileName() + ".png";

            using (Image<Gray, byte> image = new Image<Gray, byte>(1, 1))
               {
               image.Save(tempFileName);

               this.fileView.Files = new string[2] { tempFileName, tempFileName };

               // Make sure closing without any open file doesn't crash
               this.fileView.TriggerCloseFileEvent();

               this.fileView.TriggerOpenFileEvent();

               IImageController activeImageControllerToClose = this.imageViewManagerController.GetActiveImageController();

               // CloseFile should close the active image controller
               this.fileView.TriggerCloseFileEvent();

               // Make sure there is still an active image controller
               Assert.IsNotNull(this.imageViewManagerController.GetActiveImageController());

               // Make sure the previously active image controller isn't active anymore
               Assert.AreNotSame(activeImageControllerToClose, this.imageViewManagerController.GetActiveImageController());

               // Close the remaining image controller
               this.fileView.TriggerCloseFileEvent();

               // There should be no more open image controller
               Assert.IsNull(this.imageViewManagerController.GetActiveImageController());
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
         FileController fileController = new FileController(this.fileView, this.ServiceLocator);

         string tempFileName = string.Empty;

         try
            {
            tempFileName = Path.GetRandomFileName() + ".png";

            using (Image<Gray, byte> image = new Image<Gray, byte>(1, 1))
               {
               image.Save(tempFileName);

               this.fileView.Files = new string[2] { tempFileName, tempFileName };

               // Make sure closing without any open file doesn't crash
               this.fileView.TriggerCloseAllFileEvent();

               this.fileView.TriggerOpenFileEvent();

               Assert.IsNotNull(this.imageViewManagerController.GetActiveImageController());

               this.fileView.TriggerCloseAllFileEvent();

               Assert.IsNull(this.imageViewManagerController.GetActiveImageController());
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
         FileController fileController = new FileController(this.fileView, this.ServiceLocator);

         string tempFileName = string.Empty;

         try
            {
            tempFileName = Path.GetRandomFileName() + ".png";

            using (Image<Gray, byte> image = new Image<Gray, byte>(1, 1))
               {
               image.Save(tempFileName);

               this.fileView.Files = new string[1] { tempFileName };

               Assert.IsNull(this.imageViewManagerController.GetActiveImageController());

               this.fileView.TriggerDragDropFileEvent(this.fileView.Files);

               Assert.IsNotNull(this.imageViewManagerController.GetActiveImageController());

               this.fileView.TriggerCloseFileEvent();

               this.fileView.TriggerDragDropFileEvent(null);

               Assert.IsNull(this.imageViewManagerController.GetActiveImageController(), "The image should not be loaded when Files is null.");
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

      private class FileView : IFileView
         {
         public event EventHandler FileOpen;

         public event EventHandler FileClose;
         
         public event EventHandler FileCloseAll;
         
         public event EventHandler<DragDropEventArgs> DragDropFile;

         public string[] Files
            {
            get;
            set;
            }

         public EventHandler OpenFileEventHandler()
            {
            return this.FileOpen;
            }

         public void TriggerOpenFileEvent()
            {
            Assert.IsNotNull(this.FileOpen);

            this.FileOpen(null, EventArgs.Empty);
            }

         public void TriggerCloseFileEvent()
            {
            Assert.IsNotNull(this.FileClose);

            this.FileClose(null, EventArgs.Empty);
            }

         public void TriggerCloseAllFileEvent()
            {
            Assert.IsNotNull(this.FileCloseAll);

            this.FileCloseAll(null, EventArgs.Empty);
            }

         public void TriggerDragDropFileEvent(string[] files)
            {
            Assert.IsNotNull(this.DragDropFile);

            this.DragDropFile(null, new DragDropEventArgs(files));
            }

         public string[] OpenFile()
            {
            return this.Files;
            }
         }

      private class ImageView : IImageView
         {
         public void AssignImageModel(IImageModel imageModel)
            {
            }

         public void Close()
            {
            }
         }

      private class ImageViewManager : IImageManagerView
         {
         private List<IImageView> allImageViews;

         public ImageViewManager()
            {
            this.allImageViews = new List<IImageView>();
            }

         public void AddImageView(IImageView imageView, IImageModel imageModel)
            {
            this.allImageViews.Add(imageView);
            }

         public IImageView GetActiveImageView()
            {
            if (this.allImageViews.Count == 0)
               {
               return null;
               }
            else
               {
               return this.allImageViews[0];
               }
            }

         public void RemoveImageView(IImageView imageView)
            {
            this.allImageViews.Remove(imageView);
            }
         }
      }
   }
