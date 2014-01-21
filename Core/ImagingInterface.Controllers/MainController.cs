namespace ImagingInterface.Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Plugins;
   using ImagingInterface.Views;

   public class MainController : IMainController
      {
      private IMainView mainView;
      private IImageManagerController imageManagerController;
      private IPluginManagerController pluginManagerController;

      public MainController(IMainView mainView)
         {
         this.mainView = mainView;

         this.mainView.ApplicationClosing += this.MainView_ApplicationClosing;
         }

      public void AddImageManager(IImageManagerController imageManagerController, IImageManagerView imageManagerView)
         {
         this.imageManagerController = imageManagerController;

         this.mainView.AddImageManagerView(imageManagerView);
         }

      public void AddPluginManager(IPluginManagerController pluginManagerController, IPluginManagerView pluginManagerView)
         {
         this.pluginManagerController = pluginManagerController;

         this.mainView.AddPluginManagerView(pluginManagerView);
         }

      private void MainView_ApplicationClosing(object sender, CancelEventArgs e)
         {
         if (this.imageManagerController != null)
            {
            IList<IImageController> imageControllers = this.imageManagerController.GetAllImages();

            if (imageControllers != null)
               {
               foreach (IImageController imageController in imageControllers)
                  {
                  imageController.Closed += this.ImageController_Closed;
                  imageController.Close();
                  }

               e.Cancel = true;
               }
            }

         if (this.pluginManagerController != null)
            {
            IList<IPluginController> pluginControllers = this.pluginManagerController.GetAllPlugins();

            if (pluginControllers != null)
               {
               foreach (IPluginController pluginController in pluginControllers)
                  {
                  pluginController.Closed += this.PluginController_Closed;
                  pluginController.Close();
                  }

               e.Cancel = true;
               }
            }
         }

      private void ImageController_Closed(object sender, EventArgs e)
         {
         IImageController imageController = sender as IImageController;

         imageController.Closed -= this.ImageController_Closed;

         IImageController activeImageController = this.imageManagerController.GetActiveImage();

         if (activeImageController == null)
            {
            IPluginController activePluginController = this.pluginManagerController.GetActivePlugin();

            if (activePluginController == null)
               {
               this.mainView.Close();
               }
            }
         }

      private void PluginController_Closed(object sender, EventArgs e)
         {
         IPluginController pluginController = sender as IPluginController;

         pluginController.Closed -= this.PluginController_Closed;

         IPluginController activePluginController = this.pluginManagerController.GetActivePlugin();
         
         if (activePluginController == null)
            {
            IImageController activeImageController = this.imageManagerController.GetActiveImage();

            if (activeImageController == null)
               {
               this.mainView.Close();
               }
            }
         }
      }
   }
