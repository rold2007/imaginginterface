namespace ImagingInterface.Controllers.Tests.Mocks
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Plugins;

   public class PluginModel2 : IRawPluginModel
      {
      public string DisplayName
         {
         get;
         set;
         }
      }
   }
