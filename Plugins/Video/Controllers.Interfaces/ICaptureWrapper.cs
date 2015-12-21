namespace Video.Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using Emgu.CV;
   using Emgu.CV.CvEnum;
   using Emgu.CV.Structure;
   using Emgu.Util;

   public interface ICaptureWrapper : IDisposable
      {
      int Width
         {
         get;
         }

      int Height
         {
         get;
         }

      bool CaptureAllocated
         {
         get;
         }

      double FramePeriod
         {
         get;
         }

      bool Grab();

      UMat RetrieveFrame();
      }
   }
