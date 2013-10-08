namespace ImagingInterface.Views
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Controllers;

   public interface IFileView
      {
      event EventHandler FileOpen;

      string[] OpenFile();
      }
   }
