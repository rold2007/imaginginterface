namespace ImageProcessing.Cudafied
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using Cudafy;
   using Cudafy.Host;
   using Cudafy.Translator;

   public class Primitives
    {
    [Cudafy]
    public static void Add(GThread thread, byte[, ,] sourceData, int lengthX, int lengthY, int add, int channel, byte[, ,] destinationData)
       {
       int x = thread.threadIdx.x + (thread.blockIdx.x * thread.blockDim.x);
       int y = thread.threadIdx.y + (thread.blockIdx.y * thread.blockDim.y);

       if ((x < lengthX) && (y < lengthY))
          {
          destinationData[y, x, channel] = (byte)((sourceData[y, x, channel] + add) % byte.MaxValue);
          }
       }
    }
}
