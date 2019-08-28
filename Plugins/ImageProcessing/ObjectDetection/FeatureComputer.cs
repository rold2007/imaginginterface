// <copyright file="FeatureComputer.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

// TODO: Get to 100% code coverage for the Tagger plugin, starting from the bottom (Tagger class) to the top (UI).
// Increase Tagger class test coverage to 100%. I believe that TaggerService should have been covered directly by tests on TaggerController.
// The tests are acting exactly like the  View layer, they should not access the Service layer directly.
// Need to use SimpleInjector wisely for to keep this clear and simple

// TODO: Tester les 4% code coverage restants
// TODO: Diviser les tests de facon appropriee dans TaggerServiceTest.cs et TaggerTest.cs en fonction de ce que fait vraiment la fonction.

// TODO: This line creates every plugin each time it is called. This will be a mess when there are way more plugins: IPluginView pluginView = this.pluginViewFactory.CreateNew(pluginName);

// TODO: Is there a way to remove most event in my Controller/Service pattern ? It is a mess to remember to disconnect the events when closing an image (for example) and it causes too many back and forth through all the layers. The view shouldn't have to know what needs to be updated after each user's action though...

// TODO: Make all Service classes internal ? Testing should/could be all done through the Controller classes. It would also prevent responsibility leaking through different logical layers.
namespace ImageProcessing.ObjectDetection
{
   using System;
   using System.Drawing;
   using Emgu.CV;
   using Emgu.CV.Structure;
   using ImagingInterface.Plugins.Utilities;

   public class FeatureComputer
      {
      public static readonly int NumberOfFeatures = Convert.ToInt32(Math.Ceiling((double)CentralHaarWindowRadius / 2) + 1);
      private const int WindowHalfHeight = 10;
      private const int WindowHalfWidth = 10;
      private const int CentralHaarWindowRadius = 15;
      private byte[,,] imageData;
      private double[,,] integral;

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
