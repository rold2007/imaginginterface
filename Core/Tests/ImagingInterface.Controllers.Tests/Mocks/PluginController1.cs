namespace ImagingInterface.Controllers.Tests.Mocks
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Plugins;

   public class PluginController1 : IPluginController
      {
      public PluginController1(PluginModel1 pluginModel)
         {
         this.RawPluginModel = pluginModel;

         this.RawPluginModel.DisplayName = "Plugin1";
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
