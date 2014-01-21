namespace ImagingInterface.Views
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Views.EventArguments;

   public interface IFileOperationView
      {
      event EventHandler FileOpen;

      event EventHandler FileClose;

      event EventHandler FileCloseAll;

      event EventHandler<DragDropEventArgs> DragDropFile;

      string[] OpenFile();
      }
   }
