namespace ImagingInterface.Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Plugins;

   public class MainController
      {
      ////private IMainView mainView;
      private ImageManagerController imageManagerController;
      private PluginManagerController pluginManagerController;

      public MainController()
         {
         ////this.mainView = mainView;

         ////this.mainView.ApplicationClosing += this.MainView_ApplicationClosing;
         }

      public void AddImageManager(ImageManagerController imageManagerController)
         {
         this.imageManagerController = imageManagerController;

         ////this.mainView.AddImageManagerView(imageManagerView);
         }

      public void AddPluginManager(PluginManagerController pluginManagerController)
         {
         this.pluginManagerController = pluginManagerController;

         ////this.mainView.AddPluginManagerView(pluginManagerView);
         }

      private void MainView_ApplicationClosing(object sender, CancelEventArgs e)
         {
         if (this.imageManagerController != null)
            {
            IList<ImageController> imageControllers = this.imageManagerController.GetAllImages();

            if (imageControllers != null)
               {
               if (imageControllers.Count != 0)
                  {
                  foreach (ImageController imageController in imageControllers)
                     {
                     imageController.Closed += this.ImageController_Closed;
                     imageController.Close();
                     }

                  e.Cancel = true;
                  }
               }
            }

         if (this.pluginManagerController != null)
            {
            IList<IPluginController> pluginControllers = this.pluginManagerController.GetAllPlugins();

            if (pluginControllers != null)
               {
               if (pluginControllers.Count != 0)
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
         }

      private void ImageController_Closed(object sender, EventArgs e)
         {
         ImageController imageController = sender as ImageController;

         imageController.Closed -= this.ImageController_Closed;

         ImageController activeImageController = this.imageManagerController.GetActiveImage();

         if (activeImageController == null)
            {
            IPluginController activePluginController = this.pluginManagerController.GetActivePlugin();

            if (activePluginController == null)
               {
               ////this.mainView.Close();
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
            ImageController activeImageController = this.imageManagerController.GetActiveImage();

            if (activeImageController == null)
               {
               ////this.mainView.Close();
               }
            }
         }
      }
   }
