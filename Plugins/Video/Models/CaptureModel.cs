namespace Video.Models
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;

   public class CaptureModel : ICaptureModel
      {
      public string DisplayName
         {
         get;
         set;
         }

      public byte[,,] LastImageData
         {
         get;
         set;
         }

      public bool LiveGrabRunning
         {
         get;
         set;
         }

      public object Clone()
         {
         return this.MemberwiseClone();
         }
      }
   }
