namespace ImagingInterface.Plugins.Utilities
   {
   using System;
   using System.Collections.Generic;
   using System.Diagnostics;
   using System.Drawing;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;

   public static class ColorConversion
      {
      public static double[] RGBToHSV(double[] rgb)
         {
         double[] hsv = new double[3];
         double maximumColorValues = Math.Max(Math.Max(rgb[0], rgb[1]), rgb[2]);

         hsv[2] = maximumColorValues;

         if (hsv[2] == 0)
            {
            hsv[0] = 0;
            hsv[1] = 0;
            }
         else
            {
            double minimumColorValues = Math.Min(Math.Min(rgb[0], rgb[1]), rgb[2]);
            double chroma = maximumColorValues - minimumColorValues;

            hsv[1] = chroma / hsv[2];

            if (chroma == 0)
               {
               hsv[0] = 0;
               }
            else
               {
               double huePrime;

               if (maximumColorValues == rgb[0])
                  {
                  huePrime = ((double)(rgb[1] - rgb[2]) / chroma) % 6;
                  }
               else if (maximumColorValues == rgb[1])
                  {
                  huePrime = ((double)(rgb[2] - rgb[0]) / chroma) + 2;
                  }
               else
                  {
                  Debug.Assert(maximumColorValues == rgb[2], "There should be no other cases. Helps skip huePrime initialization.");

                  huePrime = ((double)(rgb[0] - rgb[1]) / chroma) + 4;
                  }

               hsv[0] = 60 * huePrime;

               if (hsv[0] < 0.0)
                  {
                  hsv[0] += 360;
                  }
               }
            }

         return hsv;
         }

      public static double[] HSVToRGB(double[] hsv)
         {
         // Based on http://en.wikipedia.org/wiki/HSL_and_HSV#Converting_to_RGB
         double chroma = hsv[1] * hsv[2];
         double huePrime = hsv[0] / 60;
         double x = chroma * (1 - Math.Abs((huePrime % 2) - 1));
         double m = hsv[2] - chroma;
         double[] rgb;

         if (huePrime < 1)
            {
            rgb = new double[3] { chroma, x, 0 };
            }
         else if (huePrime < 2)
            {
            rgb = new double[3] { x, chroma, 0 };
            }
         else if (huePrime < 3)
            {
            rgb = new double[3] { 0, chroma, x };
            }
         else if (huePrime < 4)
            {
            rgb = new double[3] { 0, x, chroma };
            }
         else if (huePrime < 5)
            {
            rgb = new double[3] { x, 0, chroma };
            }
         else if (huePrime < 6)
            {
            rgb = new double[3] { chroma, 0, x };
            }
         else
            {
            rgb = new double[3] { double.MinValue, double.MinValue, double.MinValue };

            return rgb;
            }

         rgb[0] += m;
         rgb[1] += m;
         rgb[2] += m;

         return rgb;
         }

      public static Color FromHSV(double[] hsv)
         {
         double[] rgb = HSVToRGB(hsv);

         return Color.FromArgb(Convert.ToInt32(rgb[0]), Convert.ToInt32(rgb[1]), Convert.ToInt32(rgb[2]));
         }
      }
   }
