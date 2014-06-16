namespace ImageProcessing.Controllers.EventArguments
   {
   using System;
   using System.Collections.Generic;
   using System.Drawing;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;

   public class TagPointChangedEventArgs : EventArgs
      {
      public TagPointChangedEventArgs(string uniqueIdentifier, string label, Point tagPoint, bool added)
         {
         this.UniqueIdentifier = uniqueIdentifier;
         this.Label = label;
         this.TagPoint = tagPoint;
         this.Added = added;
         }

      public string UniqueIdentifier
         {
         get;
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
