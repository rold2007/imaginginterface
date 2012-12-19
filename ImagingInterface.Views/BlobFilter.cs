namespace ImagingInterface
   {
   using AForge.Imaging;

   public class BlobFilter : IBlobsFilter
      {
      public BlobFilter(int minAreaThreshold, int maxAreaThreshold)
         : this()
         {
         this.MinAreaThreshold = minAreaThreshold;
         this.MaxAreaThreshold = maxAreaThreshold;
         }

      private BlobFilter()
         {
         }

      public int MinAreaThreshold
         {
         get;
         private set;
         }

      public int MaxAreaThreshold
         {
         get;
         private set;
         }

      public bool Check(Blob blob)
         {
         if (blob.Area < this.MinAreaThreshold)
            {
            return false;
            }
         else if (blob.Area > this.MaxAreaThreshold)
            {
            return false;
            }

         return true;
         }
      }
   }
