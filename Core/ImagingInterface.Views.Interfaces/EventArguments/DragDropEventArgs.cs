namespace ImagingInterface.Views.EventArguments
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;

   public class DragDropEventArgs : EventArgs
      {
      public DragDropEventArgs(string[] data)
         {
         this.Data = data;
         }

      public string[] Data
         {
         get;
         private set;
         }
      }
   }
