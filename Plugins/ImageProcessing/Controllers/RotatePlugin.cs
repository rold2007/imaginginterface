namespace Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Plugins;
   using Microsoft.Practices.ServiceLocation;

   [Obsolete]
   public class RotatePlugin : IPluginController
      {
      public string DisplayName
         {
         get
            {
            return "Rotate";
            }

         private set
            {
            }
         }

      public void CreatePlugin()
         {
         ServiceLocator.Current.GetInstance<IRotateController>();
         }
      }
   }
