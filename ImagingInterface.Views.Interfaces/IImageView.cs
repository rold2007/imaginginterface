namespace ImagingInterface.Views
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Models;

   public interface IImageView
      {
      void AssignImageModel(IImageModel imageModel);

      void Close();
      }
   }
