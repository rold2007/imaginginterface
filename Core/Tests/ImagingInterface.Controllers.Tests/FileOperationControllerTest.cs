﻿// <copyright file="FileOperationControllerTest.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.Tests
{
   using System;
   using System.Collections.Generic;
   using ImagingInterface.Controllers.Services;
   using ImagingInterface.Controllers.Tests.Mocks;
   using ImagingInterface.Plugins;
   using Xunit;

   public class FileOperationControllerTest : ControllersBaseTest
   {
      [Fact]
      public void Constructor()
      {
         FileSourceFactory fileSourceFactory = new FileSourceFactory();
         ImageSourceService imageSourceManager = new ImageSourceService(fileSourceFactory);
         FileOperationService fileOperationService = new FileOperationService(imageSourceManager);
         ApplicationLogic applicationLogic = new ApplicationLogic();

         FileOperationController fileOperationController = new FileOperationController(fileOperationService, applicationLogic);
      }

      [Fact]
      public void FileOpen()
      {
         FileSourceFactory fileSourceFactory = new FileSourceFactory();
         ImageSourceService imageSourceManager = new ImageSourceService(fileSourceFactory);
         FileOperationService fileOperationService = new FileOperationService(imageSourceManager);
         ApplicationLogic applicationLogic = new ApplicationLogic();

         FileOperationController fileOperationController = new FileOperationController(fileOperationService, applicationLogic);

         string[] files = new string[3] { "ValidFile", "InvalidFile", "ValidFile" };

         // IList<IFileSource> imageSourceControllers = null;
         fileOperationController.OpenFiles(files);

         // Assert.AreEqual(2, imageSourceControllers.Count);
         // Assert.AreEqual(files[0], imageSourceControllers[0].Filename);

         // imageSourceControllers.Clear();
         files = null;

         // Make sure it doesn't crash with these parameters
         fileOperationController.OpenFiles(files);

         // Assert.AreEqual(0, imageSourceControllers.Count);
         files = Array.Empty<string>();

         // Make sure it doesn't crash with these parameters
         fileOperationController.OpenFiles(files);

         // Assert.AreEqual(0, imageSourceControllers.Count);
      }

      [Fact]
      public void CloseActiveFile()
      {
         // FileOperationController fileOperationController = this.ServiceLocator.GetInstance<FileOperationController>();
         ////ImageManagerController imageManagerController = this.ServiceLocator.GetInstance<ImageManagerController>();

         // string[] files = new string[1] { "ValidFile" };
         // List<IFileSource> imageSourceControllers = new List<IFileSource>();

         //////fileOperationController.OpenFile += (sender, eventArgs) => { imageSourceControllers.AddRange(eventArgs.ImageSourceControllers); };
         //////fileOperationController.CloseFile += (sender, eventArgs) => { fileClosed = true; };

         // fileOperationController.OpenFiles(files);

         ////Assert.AreEqual(1, fileSources.Count);

         ////imageManagerController.AddImage(fileSources[0]);

         ////Assert.AreEqual(1, imageManagerController.ImageCount);

         // fileOperationController.CloseFile();

         ////Assert.AreEqual(0, imageManagerController.ImageCount);
      }

      [Fact]
      public void CloseAllFiles()
      {
         FileSourceFactory fileSourceFactory = new FileSourceFactory();
         ImageSourceService imageSourceManager = new ImageSourceService(fileSourceFactory);
         FileOperationService fileOperationService = new FileOperationService(imageSourceManager);
         ApplicationLogic applicationLogic = new ApplicationLogic();

         FileOperationController fileOperationController = new FileOperationController(fileOperationService, applicationLogic);

         string[] files = new string[1] { "ValidFile" };
         List<IFileSource> imageSourceControllers = new List<IFileSource>();

         // bool fileClosed = false;

         ////fileOperationController.OpenFile += (sender, eventArgs) => { imageSourceControllers.AddRange(eventArgs.ImageSourceControllers); };
         ////fileOperationController.CloseAllFiles += (sender, eventArgs) => { fileClosed = true; };

         fileOperationController.OpenFiles(files);
         ////fileOperationController.CloseFiles();

         // Assert.IsTrue(fileClosed);
      }

      [Fact]
      public void DragDrop()
      {
         FileSourceFactory fileSourceFactory = new FileSourceFactory();
         ImageSourceService imageSourceManager = new ImageSourceService(fileSourceFactory);
         FileOperationService fileOperationService = new FileOperationService(imageSourceManager);
         ApplicationLogic applicationLogic = new ApplicationLogic();

         FileOperationController fileOperationController = new FileOperationController(fileOperationService, applicationLogic);

         string[] files = new string[1] { "ValidFile" };

         // IList<IFileSource> imageSourceControllers = new List<IFileSource>();
         fileOperationController.OpenFiles(files);

         fileOperationController.RequestDragDropFile(files);

         // Assert.AreEqual(1, imageSourceControllers.Count);
         // Assert.AreEqual(files[0], imageSourceControllers[0].Filename);

         // imageSourceControllers.Clear();
         files = null;

         // Make sure it doesn't crash with these parameters
         fileOperationController.RequestDragDropFile(files);

         // Assert.AreEqual(0, imageSourceControllers.Count);
         files = Array.Empty<string>();

         // Make sure it doesn't crash with these parameters
         fileOperationController.RequestDragDropFile(files);

         // Assert.AreEqual(0, imageSourceControllers.Count);
      }
   }
}
