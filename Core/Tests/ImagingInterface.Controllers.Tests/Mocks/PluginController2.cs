namespace ImagingInterface.Controllers.Tests.Mocks
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Plugins;

   public class PluginController2 : IPluginController
      {
      public PluginController2(PluginModel2 pluginModel)
         {
         this.RawPluginModel = pluginModel;

         this.RawPluginModel.DisplayName = "Plugin2";
         }

      public IRawPluginView RawPluginView
         {
         get;
         private set;
         }

      public IRawPluginModel RawPluginModel
         {
         get;
         private set;
         }
      }
   }
