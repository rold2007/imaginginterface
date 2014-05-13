namespace ImagingInterface.Controllers
   {
   using ImagingInterface.Plugins;
   using ImagingInterface.Views;

   public interface IMainController
      {
      void AddImageManager(IImageManagerController imageManagerController, IImageManagerView imageManagerView);

      void AddPluginManager(IPluginManagerController pluginManagerController, IPluginManagerView pluginManagerView);
      }
   }
