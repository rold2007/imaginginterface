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
   using NUnit.Framework;
   using Microsoft.Practices.ServiceLocation;

   [TestFixture]
   public class FileControllerTests : ControllersBaseTests
      {
      private FileView fileView;
      private ImageView imageView;

      [SetUp]
      public void Bootstrap()
         {
         this.fileView = new FileView();
         this.imageView = new ImageView();

         this.container.RegisterSingle<IFileView>(this.fileView);
         this.container.RegisterSingle<IImageView>(this.imageView);
         this.container.Register<IImageController, ImageController>();
         this.container.Register<IImageModel, ImageModel>();
         }

      [Test]
      public void Constructor()
         {
         Assert.IsNull(this.fileView.OpenFileEventHandler());

         FileController fileController = new FileController(this.fileView);

         Assert.IsNotNull(this.fileView.OpenFileEventHandler());
         }

      [Test]
      public void FileOpen()
         {
         FileController fileController = new FileController(this.fileView);

         string tempFileName = string.Empty;

         try
            {
            tempFileName = Path.GetRandomFileName() + ".png";

            using (Image<Gray, byte> image = new Image<Gray, byte>(1, 1))
               {
               image.Save(tempFileName);

               this.fileView.Files = new string[1] { tempFileName };

               Assert.IsFalse(this.imageView.ImageShown);

               this.fileView.TriggerOpenFileEvent();
               
               Assert.IsTrue(this.imageView.ImageShown);

               this.imageView.ImageShown = false;

               this.fileView.Files = null;

               Assert.IsFalse(this.imageView.ImageShown);

               this.fileView.TriggerOpenFileEvent();

               Assert.IsTrue(this.imageView.ImageShown);
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
         public string[] Files;

         public event EventHandler FileOpen;

         public EventHandler OpenFileEventHandler()
            {
            return this.FileOpen;
            }

         public void TriggerOpenFileEvent()
            {
            Assert.IsNotNull(this.FileOpen);

            this.FileOpen(null, EventArgs.Empty);
            }

         public string[] OpenFile()
            {
            return this.Files;
            }
         }

      private class ImageView : IImageView
         {
         public bool ImageShown
            {
            get;
            set;
            }

         public void Show(IImageModel imageModel)
            {
            this.ImageShown = true;
            }
         }
      }
   }
