namespace ImageProcessing.Controllers.EventArguments
   {
   using System;
   using System.Collections.Generic;
   using System.Drawing;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Plugins;

   public class TagPointChangedEventArgs : EventArgs
      {
      public TagPointChangedEventArgs(IImageController imageController, string label, Point tagPoint, bool added)
         {
         this.ImageController = imageController;
         this.Label = label;
         this.TagPoint = tagPoint;
         this.Added = added;
         }

      public IImageController ImageController
         {
         get; // ncrunch: no coverage
         private set;
         }

      public string Label
         {
         get;
         private set;
         }

      public Point TagPoint
         {
         get;
         private set;
         }

      public bool Added
         {
         get;
         private set;
         }
      }
   }
