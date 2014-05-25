namespace ImageProcessing.Views.EventArguments
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;

   public class RotateEventArgs : EventArgs
      {
      public RotateEventArgs(double angle)
         {
         this.Angle = angle;
         }

      public double Angle
         {
         get;
         private set;
         }
      }
   }
