namespace ImagingInterface.Controllers.Tests
   {
   using System.Collections.Generic;
   using ImagingInterface.Plugins;
   using NUnit.Framework;

   [TestFixture]
   public class FileOperationControllerTest : ControllersBaseTest
      {
      [Test]
      public void Constructor()
         {
         FileOperationController fileOperationController = this.ServiceLocator.GetInstance<FileOperationController>();

         Assert.IsNotNull(fileOperationController);
         }

      [Test]
      public void FileOpen()
         {
         ////FileOperationController fileOperationController = this.ServiceLocator.GetInstance<FileOperationController>();

         ////string[] files = new string[1] { "ValidFile" };
         ////IList<IFileSource> imageSourceControllers = null;

         ////imageSourceControllers = fileOperationController.OpenFiles(files);

         ////Assert.AreEqual(1, imageSourceControllers.Count);
         ////Assert.AreEqual(files[0], imageSourceControllers[0].Filename);

         ////imageSourceControllers.Clear();
         ////files = null;

         ////// Make sure it doesn't crash with these parameters
         ////fileOperationController.OpenFiles(files);

         ////Assert.AreEqual(0, imageSourceControllers.Count);

         ////files = new string[0];

         ////// Make sure it doesn't crash with these parameters
         ////fileOperationController.OpenFiles(files);

         ////Assert.AreEqual(0, imageSourceControllers.Count);
         }

      [Test]
      public void FileClose()
         {
         FileOperationController fileOperationController = this.ServiceLocator.GetInstance<FileOperationController>();

         string[] files = new string[1] { "ValidFile" };
         List<IFileSource> imageSourceControllers = new List<IFileSource>();
         bool fileClosed = false;

         ////fileOperationController.OpenFile += (sender, eventArgs) => { imageSourceControllers.AddRange(eventArgs.ImageSourceControllers); };
         ////fileOperationController.CloseFile += (sender, eventArgs) => { fileClosed = true; };

         fileOperationController.OpenFiles(files);
         fileOperationController.RequestCloseFile();

         Assert.IsTrue(fileClosed);
         }

      [Test]
      public void FileCloseAll()
         {
         FileOperationController fileOperationController = this.ServiceLocator.GetInstance<FileOperationController>();

         string[] files = new string[1] { "ValidFile" };
         List<IFileSource> imageSourceControllers = new List<IFileSource>();
         bool fileClosed = false;

         ////fileOperationController.OpenFile += (sender, eventArgs) => { imageSourceControllers.AddRange(eventArgs.ImageSourceControllers); };
         ////fileOperationController.CloseAllFiles += (sender, eventArgs) => { fileClosed = true; };

         fileOperationController.OpenFiles(files);
         fileOperationController.RequestCloseAllFiles();

         Assert.IsTrue(fileClosed);
         }

      [Test]
      public void DragDrop()
         {
         FileOperationController fileOperationController = this.ServiceLocator.GetInstance<FileOperationController>();

         string[] files = new string[1] { "ValidFile" };
         IList<IFileSource> imageSourceControllers = new List<IFileSource>();

         imageSourceControllers = fileOperationController.OpenFiles(files);

         fileOperationController.RequestDragDropFile(files);

         Assert.AreEqual(1, imageSourceControllers.Count);
         Assert.AreEqual(files[0], imageSourceControllers[0].Filename);

         imageSourceControllers.Clear();
         files = null;

         // Make sure it doesn't crash with these parameters
         fileOperationController.RequestDragDropFile(files);

         Assert.AreEqual(0, imageSourceControllers.Count);

         files = new string[0];

         // Make sure it doesn't crash with these parameters
         fileOperationController.RequestDragDropFile(files);

         Assert.AreEqual(0, imageSourceControllers.Count);
         }
      }
   }
