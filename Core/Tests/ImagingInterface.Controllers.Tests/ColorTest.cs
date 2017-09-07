// <copyright file="ColorTest.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.Tests
{
   using System;
   using ImagingInterface.Plugins.Utilities;
   using Xunit;

   public class ColorTest : ControllersBaseTest
      {
      [Fact]
      public void AllRGBColors()
         {
         double[] rgb;
         double[] hsv;
         double[] outputRGB;

         for (int r = 0; r <= 256; r += 8)
            {
            for (int g = 0; g <= 256; g += 8)
               {
               for (int b = 0; b <= 256; b += 8)
                  {
                  rgb = new double[3] { Math.Min(r, 255), Math.Min(g, 255), Math.Min(b, 255) };
                  hsv = ColorConversion.RGBToHSV(rgb);

                  outputRGB = ColorConversion.HSVToRGB(hsv);

                  Assert.InRange(rgb[0], outputRGB[0] - 0.01, outputRGB[0] + 0.01);
                  Assert.InRange(rgb[1], outputRGB[1] - 0.01, outputRGB[1] + 0.01);
                  Assert.InRange(rgb[2], outputRGB[2] - 0.01, outputRGB[2] + 0.01);
                  }
               }
            }
         }

      [Fact]
      public void AllHSVColors()
         {
         double[] hsv;
         double[] rgb;
         double[] outputHSV;

         for (double hue = 0.0; hue <= 360.0; hue += 16.0)
            {
            for (double saturation = 0.05; saturation <= 1.0; saturation += 0.05)
               {
               for (double value = 16; value <= 255.0; value += 16.0)
                  {
                  hsv = new double[3] { hue, saturation, Math.Min(value, 255) };
                  rgb = ColorConversion.HSVToRGB(hsv);

                  outputHSV = ColorConversion.RGBToHSV(rgb);

                  Assert.InRange(hsv[0], outputHSV[0] - 0.01, outputHSV[0] + 0.01);
                  Assert.InRange(hsv[1], outputHSV[1] - 0.01, outputHSV[1] + 0.01);
                  Assert.InRange(hsv[2], outputHSV[2] - 0.01, outputHSV[2] + 0.01);
                  }
               }
            }
         }

      [Fact]
      public void InvalidColor()
         {
         double[] hsv = new double[3] { 360.0, 0.0, 0.0 };
         double[] outputRGB;

         outputRGB = ColorConversion.HSVToRGB(hsv);

         Assert.Equal(double.MinValue, outputRGB[0]);
         Assert.Equal(double.MinValue, outputRGB[1]);
         Assert.Equal(double.MinValue, outputRGB[2]);
         }
      }
   }
