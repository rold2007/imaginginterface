﻿namespace ImageProcessing.Views
   {
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Data;
   using System.Drawing;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using System.Windows.Forms;
   using ImageProcessing.Views;
   using ImageProcessing.Views.EventArguments;

   public partial class RotateView : UserControl, IRotateView
      {
      public RotateView()
         {
         this.InitializeComponent();
         }

      public event EventHandler<RotateEventArgs> Rotate;

      public void Close()
         {
         }

      private void SetRotationAngle(double angle)
         {
         this.rotationAngleTrackBar.Value = Convert.ToInt32(angle * 100);
         this.rotationAngleNumericUpDown.Value = Convert.ToDecimal(angle);

         if (this.Rotate != null)
            {
            this.Rotate(this, new RotateEventArgs(angle));
            }
         }

      private void rotationAngleTrackBar_Scroll(object sender, EventArgs e)
         {
         double angle = Convert.ToDouble(this.rotationAngleTrackBar.Value / 100);

         this.SetRotationAngle(angle);
         }

      private void rotationAngleNumericUpDown_ValueChanged(object sender, EventArgs e)
         {
         double angle = Convert.ToDouble(this.rotationAngleNumericUpDown.Value);

         this.SetRotationAngle(angle);
         }
      }
   }
