namespace ImagingInterface.Controllers.EventArguments
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Plugins;

   public class OpenFileEventArgs : EventArgs
      {
      public OpenFileEventArgs(IEnumerable<IFileSourceController> imageSourceControllers)
         {
         this.ImageSourceControllers = imageSourceControllers;
         }

      public IEnumerable<IFileSourceController> ImageSourceControllers
         {
         get;
         private set;
         }
      }
   }
