namespace ImageProcessing.Views.EventArguments
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;

   public class InvertEventArgs : EventArgs
      {
      public InvertEventArgs(bool invert)
         {
         this.Invert = invert;
         }

      public bool Invert
         {
         get;
         private set;
         }
      }
   }
