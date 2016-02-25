namespace ImagingInterface.Models
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Controllers;

   public class ImageManagerModel
      {
      ////private HashSet<ImageController> imageControllers;
      ////private 

      public ImageManagerModel()
         {
         this.ImageControllers = new HashSet<ImageController>();
         }

      public ICollection<ImageController> ImageControllers
         {
         get;
         private set;
         }

      ////public ImageController ActiveImageController
      ////   {
      ////   get;
      ////   set;
      ////   }
      }
   }
