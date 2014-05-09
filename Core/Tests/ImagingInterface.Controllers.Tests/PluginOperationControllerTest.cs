﻿namespace ImagingInterface.Controllers.Tests
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Controllers;
   using ImagingInterface.Controllers.Tests.Mocks;
   using ImagingInterface.Controllers.Tests.Views;
   using ImagingInterface.Plugins;
   using ImagingInterface.Views;
   using NUnit.Framework;

   [TestFixture]
   public class PluginOperationControllerTest : ControllersBaseTest
      {
      [Test]
      public void Constructor()
         {
         IPluginOperationController pluginOperationController = this.ServiceLocator.GetInstance<IPluginOperationController>();
         }

      [Test]
      public void PluginCreate()
         {
         IPluginOperationController pluginOperationController = this.ServiceLocator.GetInstance<IPluginOperationController>();
         PluginOperationsView pluginOperationsView = this.ServiceLocator.GetInstance<IPluginOperationView>() as PluginOperationsView;
         IPluginController pluginController1 = this.ServiceLocator.GetInstance<PluginController1>();
         IPluginController pluginController2 = this.ServiceLocator.GetInstance<PluginController2>();
         PluginManagerView pluginManagerView = this.ServiceLocator.GetInstance<IPluginManagerView>() as PluginManagerView;

         Assert.AreEqual(0, pluginManagerView.RawPluginViews.Count);

         pluginOperationsView.TriggerPluginCreate(pluginController1.RawPluginModel.DisplayName);

         Assert.AreEqual(1, pluginManagerView.RawPluginViews.Count);

         pluginOperationsView.TriggerPluginCreate(pluginController2.RawPluginModel.DisplayName);

         Assert.AreEqual(2, pluginManagerView.RawPluginViews.Count);

         // Add the same plugin a second time on purpose, for now it creates a
         // third plugin but this may change in the future
         pluginOperationsView.TriggerPluginCreate(pluginController2.RawPluginModel.DisplayName);

         Assert.AreEqual(3, pluginManagerView.RawPluginViews.Count);

         pluginOperationsView.TriggerPluginCreate("Close plugin");

         Assert.AreEqual(2, pluginManagerView.RawPluginViews.Count);
         }
      }
   }
