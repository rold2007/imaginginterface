namespace ImagingInterface.Controllers.Tests
{
    using System.Collections.Generic;
    using ImagingInterface.Controllers.Tests.Mocks;
    using ImagingInterface.Models;
    using ImagingInterface.Plugins;
    using NUnit.Framework;

    [TestFixture]
    public class FileOperationControllerTest : ControllersBaseTest
    {
        [Test]
        public void Constructor()
        {
            FileOperationModel fileOperationModel = new FileOperationModel();
            FileSourceFactory fileSourceFactory = new FileSourceFactory();
            ImageSourceManager imageSourceManager = new ImageSourceManager(fileSourceFactory);

            FileOperationController fileOperationController = new FileOperationController(fileOperationModel, imageSourceManager);
        }

        [Test]
        public void FileOpen()
        {
            FileOperationModel fileOperationModel = new FileOperationModel();
            FileSourceFactory fileSourceFactory = new FileSourceFactory();
            ImageSourceManager imageSourceManager = new ImageSourceManager(fileSourceFactory);

            FileOperationController fileOperationController = new FileOperationController(fileOperationModel, imageSourceManager);

            string[] files = new string[3] { "ValidFile", "InvalidFile", "ValidFile" };
            //IList<IFileSource> imageSourceControllers = null;

            fileOperationController.OpenFiles(files);

            //Assert.AreEqual(2, imageSourceControllers.Count);
            //Assert.AreEqual(files[0], imageSourceControllers[0].Filename);

            //imageSourceControllers.Clear();
            files = null;

            // Make sure it doesn't crash with these parameters
            fileOperationController.OpenFiles(files);

            //Assert.AreEqual(0, imageSourceControllers.Count);

            files = new string[0];

            // Make sure it doesn't crash with these parameters
            fileOperationController.OpenFiles(files);

            //Assert.AreEqual(0, imageSourceControllers.Count);
        }

        [Test]
        public void CloseActiveFile()
        {
            //FileOperationController fileOperationController = this.ServiceLocator.GetInstance<FileOperationController>();
            ////ImageManagerController imageManagerController = this.ServiceLocator.GetInstance<ImageManagerController>();

            //string[] files = new string[1] { "ValidFile" };
            //List<IFileSource> imageSourceControllers = new List<IFileSource>();

            //////fileOperationController.OpenFile += (sender, eventArgs) => { imageSourceControllers.AddRange(eventArgs.ImageSourceControllers); };
            //////fileOperationController.CloseFile += (sender, eventArgs) => { fileClosed = true; };

            //fileOperationController.OpenFiles(files);

            ////Assert.AreEqual(1, fileSources.Count);

            ////imageManagerController.AddImage(fileSources[0]);

            ////Assert.AreEqual(1, imageManagerController.ImageCount);

            //fileOperationController.CloseFile();

            ////Assert.AreEqual(0, imageManagerController.ImageCount);
        }

        [Test]
        public void CloseAllFiles()
        {
            FileOperationModel fileOperationModel = new FileOperationModel();
            FileSourceFactory fileSourceFactory = new FileSourceFactory();
            ImageSourceManager imageSourceManager = new ImageSourceManager(fileSourceFactory);

            FileOperationController fileOperationController = new FileOperationController(fileOperationModel, imageSourceManager);

            string[] files = new string[1] { "ValidFile" };
            List<IFileSource> imageSourceControllers = new List<IFileSource>();
            //bool fileClosed = false;

            ////fileOperationController.OpenFile += (sender, eventArgs) => { imageSourceControllers.AddRange(eventArgs.ImageSourceControllers); };
            ////fileOperationController.CloseAllFiles += (sender, eventArgs) => { fileClosed = true; };

            fileOperationController.OpenFiles(files);
            fileOperationController.CloseAllFiles();

            //Assert.IsTrue(fileClosed);
        }

        [Test]
        public void DragDrop()
        {
            FileOperationModel fileOperationModel = new FileOperationModel();
            FileSourceFactory fileSourceFactory = new FileSourceFactory();
            ImageSourceManager imageSourceManager = new ImageSourceManager(fileSourceFactory);

            FileOperationController fileOperationController = new FileOperationController(fileOperationModel, imageSourceManager);

            string[] files = new string[1] { "ValidFile" };
            //IList<IFileSource> imageSourceControllers = new List<IFileSource>();

            fileOperationController.OpenFiles(files);

            fileOperationController.RequestDragDropFile(files);

            //Assert.AreEqual(1, imageSourceControllers.Count);
            //Assert.AreEqual(files[0], imageSourceControllers[0].Filename);

            //imageSourceControllers.Clear();
            files = null;

            // Make sure it doesn't crash with these parameters
            fileOperationController.RequestDragDropFile(files);

            //Assert.AreEqual(0, imageSourceControllers.Count);

            files = new string[0];

            // Make sure it doesn't crash with these parameters
            fileOperationController.RequestDragDropFile(files);

            //Assert.AreEqual(0, imageSourceControllers.Count);
        }
    }
}
