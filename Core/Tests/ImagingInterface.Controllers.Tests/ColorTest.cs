// <copyright file="ColorTest.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Controllers.Tests
{
   using System;
   using ImagingInterface.Plugins.Utilities;
   using NUnit.Framework;

   [TestFixture]
   public class ColorTest : ControllersBaseTest
      {
      [Test]
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

                  Assert.That(rgb[0], Is.EqualTo(outputRGB[0]).Within(0.01), string.Format("R: {0} G: {1} B: {2}", rgb[0], rgb[1], rgb[2]));
                  Assert.That(rgb[1], Is.EqualTo(outputRGB[1]).Within(0.01), string.Format("R: {0} G: {1} B: {2}", rgb[0], rgb[1], rgb[2]));
                  Assert.That(rgb[2], Is.EqualTo(outputRGB[2]).Within(0.01), string.Format("R: {0} G: {1} B: {2}", rgb[0], rgb[1], rgb[2]));
                  }
               }
            }
         }

      [Test]
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

                  Assert.That(hsv[0], Is.EqualTo(outputHSV[0]).Within(0.01), string.Format("Hue: {0} Saturation: {1} Value: {2}", hsv[0], hsv[1], hsv[2]));
                  Assert.That(hsv[1], Is.EqualTo(outputHSV[1]).Within(0.01), string.Format("Hue: {0} Saturation: {1} Value: {2}", hsv[0], hsv[1], hsv[2]));
                  Assert.That(hsv[2], Is.EqualTo(outputHSV[2]).Within(0.01), string.Format("Hue: {0} Saturation: {1} Value: {2}", hsv[0], hsv[1], hsv[2]));
                  }
               }
            }
         }

      [Test]
      public void InvalidColor()
         {
         double[] hsv = new double[3] { 360.0, 0.0, 0.0 };
         double[] outputRGB;

         outputRGB = ColorConversion.HSVToRGB(hsv);

         Assert.AreEqual(double.MinValue, outputRGB[0]);
         Assert.AreEqual(double.MinValue, outputRGB[1]);
         Assert.AreEqual(double.MinValue, outputRGB[2]);
         }
      }
   }
