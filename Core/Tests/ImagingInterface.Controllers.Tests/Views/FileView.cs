namespace ImagingInterface.Controllers.Tests.Views
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Models;
   using ImagingInterface.Views;
   using ImagingInterface.Views.EventArguments;
   using NUnit.Framework;

   public class FileView : IFileView
      {
      public event EventHandler FileOpen;

      public event EventHandler FileClose;

      public event EventHandler FileCloseAll;

      public event EventHandler<DragDropEventArgs> DragDropFile;

      public string[] Files
         {
         get;
         set;
         }

      public EventHandler OpenFileEventHandler()
         {
         return this.FileOpen;
         }

      public void TriggerOpenFileEvent()
         {
         Assert.IsNotNull(this.FileOpen);

         this.FileOpen(null, EventArgs.Empty);
         }

      public void TriggerCloseFileEvent()
         {
         Assert.IsNotNull(this.FileClose);

         this.FileClose(null, EventArgs.Empty);
         }

      public void TriggerCloseAllFileEvent()
         {
         Assert.IsNotNull(this.FileCloseAll);

         this.FileCloseAll(null, EventArgs.Empty);
         }

      public void TriggerDragDropFileEvent(string[] files)
         {
         Assert.IsNotNull(this.DragDropFile);

         this.DragDropFile(null, new DragDropEventArgs(files));
         }

      public string[] OpenFile()
         {
         return this.Files;
         }
      }
   }
