namespace ImagingInterface.Controllers.Tests
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Controllers.Tests.Mocks;
   using ImagingInterface.Controllers.Tests.Views;
   using ImagingInterface.Tests.Common.Views;
   using ImagingInterface.Views;
   using NUnit.Framework;
   using Plugins;

   [TestFixture]
   public class MainControllerTest : ControllersBaseTest
      {
      [Test]
      public void MainView_ApplicationClosing()
         {
         MainView mainView = this.ServiceLocator.GetInstance<IMainView>() as MainView;
         IMainController mainController = this.ServiceLocator.GetInstance<IMainController>();
         IImageManagerController imageManagerController = this.ServiceLocator.GetInstance<IImageManagerController>();
         IPluginManagerController pluginManagerController = this.ServiceLocator.GetInstance<IPluginManagerController>();
         IImageController imageController1 = this.ServiceLocator.GetInstance<IImageController>();
         IImageController imageController2 = this.ServiceLocator.GetInstance<IImageController>();
         IPluginController pluginController1 = this.ServiceLocator.GetInstance<PluginController1>();
         IPluginController pluginController2 = this.ServiceLocator.GetInstance<PluginController1>();
         bool imageController1Closing = false;
         bool imageController2Closing = false;
         bool pluginController1Closing = false;
         bool pluginController2Closing = false;

         imageController1.Closing += (sender, eventArgs) => { imageController1Closing = true; };
         imageController2.Closing += (sender, eventArgs) => { imageController2Closing = true; };
         pluginController1.Closing += (sender, eventArgs) => { pluginController1Closing = true; };
         pluginController2.Closing += (sender, eventArgs) => { pluginController2Closing = true; };

         Assert.IsNull(imageManagerController.GetActiveImage());
         Assert.IsNull(pluginManagerController.GetActivePlugin());

         imageManagerController.AddImage(imageController1);
         imageManagerController.AddImage(imageController2);
         pluginManagerController.AddPlugin(pluginController1);
         pluginManagerController.AddPlugin(pluginController2);

         Assert.IsNotNull(imageManagerController.GetActiveImage());
         Assert.IsNotNull(pluginManagerController.GetActivePlugin());
         Assert.IsFalse(imageController1Closing);
         Assert.IsFalse(imageController2Closing);
         Assert.IsFalse(pluginController1Closing);
         Assert.IsFalse(pluginController2Closing);

         mainView.Close();

         Assert.IsNull(imageManagerController.GetActiveImage());
         Assert.IsNull(pluginManagerController.GetActivePlugin());
         Assert.IsTrue(imageController1Closing);
         Assert.IsTrue(imageController2Closing);
         Assert.IsTrue(pluginController1Closing);
         Assert.IsTrue(pluginController2Closing);
         }

      [Test]
      public void MainView_ApplicationClosingNoPlugin()
         {
         MainView mainView = this.ServiceLocator.GetInstance<IMainView>() as MainView;
         IMainController mainController = this.ServiceLocator.GetInstance<IMainController>();
         IImageManagerController imageManagerController = this.ServiceLocator.GetInstance<IImageManagerController>();
         IPluginManagerController pluginManagerController = this.ServiceLocator.GetInstance<IPluginManagerController>();
         IImageController imageController = this.ServiceLocator.GetInstance<IImageController>();
         bool imageControllerClosing = false;

         imageController.Closing += (sender, eventArgs) => { imageControllerClosing = true; };

         Assert.IsNull(imageManagerController.GetActiveImage());
         Assert.IsNull(pluginManagerController.GetActivePlugin());

         imageManagerController.AddImage(imageController);

         Assert.IsNotNull(imageManagerController.GetActiveImage());
         Assert.IsNull(pluginManagerController.GetActivePlugin());
         Assert.IsFalse(imageControllerClosing);

         mainView.Close();

         Assert.IsNull(imageManagerController.GetActiveImage());
         Assert.IsNull(pluginManagerController.GetActivePlugin());
         Assert.IsTrue(imageControllerClosing);
         }
      }
   }
