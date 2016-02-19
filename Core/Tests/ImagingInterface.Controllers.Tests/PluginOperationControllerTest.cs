namespace ImagingInterface.Controllers.Tests
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Controllers;
   using ImagingInterface.Controllers.Tests.Mocks;
   using ImagingInterface.Plugins;
   using NUnit.Framework;

   [TestFixture]
   public class PluginOperationControllerTest : ControllersBaseTest
      {
      [Test]
      public void Constructor()
         {
         PluginOperationController pluginOperationController = this.ServiceLocator.GetInstance<PluginOperationController>();
         }

      [Test]
      public void PluginCreate()
         {
         PluginOperationController pluginOperationController = this.ServiceLocator.GetInstance<PluginOperationController>();
         ////PluginOperationsView pluginOperationsView = this.ServiceLocator.GetInstance<IPluginOperationView>() as PluginOperationsView;
         IPluginController pluginController1 = this.ServiceLocator.GetInstance<PluginController1>();
         IPluginController pluginController2 = this.ServiceLocator.GetInstance<PluginController2>();
         ////PluginManagerView pluginManagerView = this.ServiceLocator.GetInstance<IPluginManagerView>() as PluginManagerView;

         ////Assert.AreEqual(0, pluginManagerView.RawPluginViews.Count);

         ////pluginOperationsView.TriggerPluginCreate(pluginController1.RawPluginModel.DisplayName);

         ////Assert.AreEqual(1, pluginManagerView.RawPluginViews.Count);

         ////pluginOperationsView.TriggerPluginCreate(pluginController2.RawPluginModel.DisplayName);

         ////Assert.AreEqual(2, pluginManagerView.RawPluginViews.Count);

         ////pluginOperationsView.TriggerPluginCreate("Close plugin");

         ////Assert.AreEqual(1, pluginManagerView.RawPluginViews.Count);
         }
      }
   }
