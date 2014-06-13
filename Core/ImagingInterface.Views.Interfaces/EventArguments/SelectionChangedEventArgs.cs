namespace ImagingInterface.Views.EventArguments
   {
   using System;
   using System.Collections.Generic;
   using System.Drawing;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;

   public class SelectionChangedEventArgs : EventArgs
      {
      public SelectionChangedEventArgs(Point pixelPosition, bool select)
         {
         this.PixelPosition = pixelPosition;
         this.Select = select;
         }

      public Point PixelPosition
         {
         get;
         private set;
         }

      public bool Select
         {
         get;
         private set;
         }
      }
   }
