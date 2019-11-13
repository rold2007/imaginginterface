// <copyright file="FeatureComputer.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.ObjectDetection
{
   using System;
   using System.Diagnostics.CodeAnalysis;
   using System.Drawing;
   using Emgu.CV;
   using Emgu.CV.Structure;
   using ImagingInterface.Plugins.Utilities;

   public class FeatureComputer
   {
      public static readonly int NumberOfFeatures = Convert.ToInt32(Math.Ceiling((double)CentralHaarWindowRadius / 2) + 1);
      private const int CentralHaarWindowRadius = 15;

      [SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", Justification = "Too much work for now.")]
      private byte[,,] imageData;

      [SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", Justification = "Too much work for now.")]
      private double[,,] integral;

      [SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", Justification = "Too much work for now.")]
      public FeatureComputer(byte[,,] imageData)
      {
         this.imageData = imageData;

         if (imageData != null)
         {
            this.integral = this.ComputeIntegral(imageData);
         }
      }

      public float[] ComputeFeatures(Point pixelPosition)
      {
         float[] features = new float[NumberOfFeatures];
         int featureIndex = 0;
         int imageWidth = this.imageData.GetLength(1);
         int imageHeight = this.imageData.GetLength(0);
         int channels = this.imageData.GetLength(2);

         int x = pixelPosition.X;
         int y = pixelPosition.Y;
         bool channelsValid = (channels == 1) || (channels == 3);

         if ((x >= 0 && x < imageWidth) && (y >= 0 && y < imageHeight) && channelsValid)
         {
            if (channels == 1)
            {
               features[featureIndex] = this.imageData[y, x, 0];
            }
            else
            {
               double[] rgb = new double[] { this.imageData[y, x, 0], this.imageData[y, x, 1], this.imageData[y, x, 2] };
               double[] hsv = ColorConversion.RGBToHSV(rgb);

               features[featureIndex] = Convert.ToSingle(hsv[2]);
            }
         }
         else
         {
            features[featureIndex] = -1.0f;
         }

         featureIndex++;

         for (int centralHaarWindowRadius = CentralHaarWindowRadius; centralHaarWindowRadius >= 1; centralHaarWindowRadius -= 2)
         {
            int left = x - centralHaarWindowRadius;
            int right = x + centralHaarWindowRadius + 1;
            int top = y - centralHaarWindowRadius;
            int bottom = y + centralHaarWindowRadius + 1;

            if ((left >= 0 && right < imageWidth) && (top >= 0 && bottom < imageHeight))
            {
               double sum = 0.0;

               for (int channel = 0; channel < channels; channel++)
               {
                  sum += this.integral[top, left, channel];
                  sum += this.integral[bottom, right, channel];
                  sum -= this.integral[top, right, channel];
                  sum -= this.integral[bottom, left, channel];
               }

               features[featureIndex] = Convert.ToSingle(sum);
            }
            else
            {
               features[featureIndex] = -1.0f;
            }

            featureIndex++;
         }

         return features;
      }

      [SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", Justification = "Too much work for now.")]
      private double[,,] ComputeIntegral(byte[,,] imageData)
      {
         int channels = imageData.GetLength(2);

         if (channels == 1)
         {
            using (Image<Gray, byte> image = new Image<Gray, byte>(imageData))
            {
               using (Image<Gray, double> integral = image.Integral())
               {
                  return integral.Data;
               }
            }
         }
         else if (channels == 3)
         {
            using (Image<Rgb, byte> image = new Image<Rgb, byte>(imageData))
            {
               using (Image<Rgb, double> integral = image.Integral())
               {
                  return integral.Data;
               }
            }
         }
         else
         {
            return null;
         }
      }
   }
}
