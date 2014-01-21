namespace ImagingInterface.Controllers.Tests.Mocks
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;

   public class ImageSourceController : IImageSourceController
      {
      public int NextImageDataCalls
         {
         get;
         private set;
         }

      public byte[,,] NextImageData()
         {
         this.NextImageDataCalls++;

         return new byte[1, 1, 1];
         }
      }
   }
