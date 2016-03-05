namespace ImagingInterface.Plugins
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;

   public interface IFileSource : IImageSource
      {
      string Filename
         {
         get;
         }

      void SetImageSource(string file);
      }
   }
