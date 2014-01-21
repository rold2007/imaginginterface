namespace ImageProcessing.Controllers.Tests.Views
   {
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Views;

   public class MainView : IMainView
      {
      public event CancelEventHandler ApplicationClosing;

      public void AddImageManagerView(IImageManagerView imageManagerView)
         {
         }

      public void AddPluginManagerView(IPluginManagerView pluginManagerView)
         {
         }

      public void Close()
         {
         }
      }
   }
