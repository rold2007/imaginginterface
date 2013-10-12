namespace ImagingInterface.Views.EventArguments
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;

   public class DragDropEventArgs : EventArgs
      {
      public string[] Data
         {
         get;
         set;
         }

      public DragDropEventArgs(string[] data)
         {
         this.Data = data;
         }
      }
   }
