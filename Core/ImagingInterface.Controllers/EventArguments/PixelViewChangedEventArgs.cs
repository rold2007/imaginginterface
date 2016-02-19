namespace ImagingInterface.Controllers.EventArguments
   {
   using System;
   using System.Collections.Generic;
   using System.Drawing;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;

   public class PixelViewChangedEventArgs : EventArgs
      {
      public PixelViewChangedEventArgs(Point pixelPosition)
         {
         this.PixelPosition = pixelPosition;
         }

      public Point PixelPosition
         {
         get;
         private set;
         }
      }
   }
