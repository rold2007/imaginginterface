﻿// <copyright file="CudafyView.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImageProcessing.Views
{
   using System;
   using System.Collections.Generic;
   using System.Diagnostics.CodeAnalysis;
   using System.Drawing;
   using System.Windows.Forms;
   using ImageProcessing.Controllers;
   using ImageProcessing.Controllers.EventArguments;
   using ImagingInterface.Plugins;

   public partial class CudafyView : UserControl, IPluginView
   {
      private readonly ShowBenchmarkDelegate showBenchmarkAddCudafyDelegate;
      private readonly ShowBenchmarkDelegate showBenchmarkAddOpenCVDelegate;
      private bool closed;
      private CudafyController cudafyController;

      public CudafyView(CudafyController cudafyController)
      {
         this.InitializeComponent();

         this.cudafyController = cudafyController;

         this.showBenchmarkAddCudafyDelegate = this.ShowBenchmarkAddCudafy;
         this.showBenchmarkAddOpenCVDelegate = this.ShowBenchmarkAddOpenCV;
      }

      private delegate void ShowBenchmarkDelegate(float benchmark);

      public event EventHandler<CudafyAddEventArgs> Add;

      public event EventHandler<CudafyGPUChangedEventArgs> GPUChanged;

      public event EventHandler<CudafyGridSizeChangedEventArgs> GridSizeChanged;

      public event EventHandler BlockSizeXChanged;

      public event EventHandler BlockSizeYChanged;

      public string DisplayName
      {
         get
         {
            return this.cudafyController.DisplayName;
         }
      }

      public int GridSizeX
      {
         get
         {
            return Convert.ToInt32(this.gridSizeXNumericUpDown.Value);
         }

         set
         {
            this.gridSizeXNumericUpDown.Value = value;
         }
      }

      public int GridSizeY
      {
         get
         {
            return Convert.ToInt32(this.gridSizeYNumericUpDown.Value);
         }

         set
         {
            this.gridSizeYNumericUpDown.Value = value;
         }
      }

      public int GridSizeZ
      {
         get
         {
            return Convert.ToInt32(this.gridSizeZNumericUpDown.Value);
         }

         set
         {
            this.gridSizeZNumericUpDown.Value = value;
         }
      }

      public int MaxGridSizeX
      {
         get
         {
            return Convert.ToInt32(this.gridSizeXNumericUpDown.Maximum);
         }

         set
         {
            this.gridSizeXNumericUpDown.Maximum = value;
         }
      }

      public int MaxGridSizeY
      {
         get
         {
            return Convert.ToInt32(this.gridSizeYNumericUpDown.Maximum);
         }

         set
         {
            this.gridSizeYNumericUpDown.Maximum = value;
         }
      }

      public int MaxGridSizeZ
      {
         get
         {
            return Convert.ToInt32(this.gridSizeZNumericUpDown.Maximum);
         }

         set
         {
            this.gridSizeZNumericUpDown.Maximum = value;
         }
      }

      public int BlockSizeX
      {
         get
         {
            return Convert.ToInt32(this.blockSizeXNumericUpDown.Value);
         }

         set
         {
            this.blockSizeXNumericUpDown.Value = value;
         }
      }

      public int BlockSizeY
      {
         get
         {
            return Convert.ToInt32(this.blockSizeYNumericUpDown.Value);
         }

         set
         {
            this.blockSizeYNumericUpDown.Value = value;
         }
      }

      public int BlockSizeZ
      {
         get
         {
            return Convert.ToInt32(this.blockSizeZNumericUpDown.Value);
         }

         set
         {
            this.blockSizeZNumericUpDown.Value = value;
         }
      }

      public int MaxBlockSizeX
      {
         get
         {
            return Convert.ToInt32(this.blockSizeXNumericUpDown.Maximum);
         }

         set
         {
            this.blockSizeXNumericUpDown.Maximum = value;
         }
      }

      public int MaxBlockSizeY
      {
         get
         {
            return Convert.ToInt32(this.blockSizeYNumericUpDown.Maximum);
         }

         set
         {
            this.blockSizeYNumericUpDown.Maximum = value;
         }
      }

      public int MaxBlockSizeZ
      {
         get
         {
            return Convert.ToInt32(this.blockSizeZNumericUpDown.Maximum);
         }

         set
         {
            this.blockSizeZNumericUpDown.Maximum = value;
         }
      }

      public void Close()
      {
         this.closed = true;
      }

      public void SelectPixel(IImageSource imageSource, Point pixelPosition)
      {
      }

      public void SetGPUs(List<string> gpus)
      {
         if (gpus.Count > 0)
         {
            this.gpuComboBox.Items.AddRange(gpus.ToArray());

            this.gpuComboBox.SelectedIndex = 0;
         }
      }

      public void SetBenchmarkAddCudafy(long benchmark)
      {
         if (!this.closed)
         {
            this.BeginInvoke(this.showBenchmarkAddCudafyDelegate, benchmark);
         }
      }

      public void SetBenchmarkAddOpenCV(long benchmark)
      {
         if (!this.closed)
         {
            this.BeginInvoke(this.showBenchmarkAddOpenCVDelegate, benchmark);
         }
      }

      private void AddTrackBar_Scroll(object sender, EventArgs e)
      {
         int add = this.addTrackBar.Value;

         this.addTextBox.Text = FormattableString.Invariant($"{add}");

         this.Add?.Invoke(this, new CudafyAddEventArgs(add));
      }

      private void ShowBenchmarkAddCudafy(float benchmark)
      {
         if (!this.closed)
         {
            this.benchmarkAddCudafyLabel.Text = FormattableString.Invariant($"{benchmark} ms");
         }
      }

      private void ShowBenchmarkAddOpenCV(float benchmark)
      {
         if (!this.closed)
         {
            this.benchmarkAddOpenCVLabel.Text = FormattableString.Invariant($"{benchmark} ms");
         }
      }

      private void GPUComboBox_DropDown(object sender, EventArgs e)
      {
         ComboBox comboBox = sender as ComboBox;
         int dropDownWidth = comboBox.DropDownWidth;

         using (Graphics graphics = comboBox.CreateGraphics())
         {
            Font font = comboBox.Font;
            int vertScrollBarWidth = (comboBox.Items.Count > comboBox.MaxDropDownItems) ? SystemInformation.VerticalScrollBarWidth : 0;

            foreach (object comboBoxItem in comboBox.Items)
            {
               string comboBoxItemString = comboBoxItem.ToString();
               int comboBoxItemWidth = (int)graphics.MeasureString(comboBoxItemString, font).Width + vertScrollBarWidth;

               if (dropDownWidth < comboBoxItemWidth)
               {
                  dropDownWidth = comboBoxItemWidth;
               }
            }
         }

         comboBox.DropDownWidth = dropDownWidth;
      }

      private void BlockSizeXNumericUpDown_ValueChanged(object sender, EventArgs e)
      {
         this.BlockSizeXChanged?.Invoke(this, EventArgs.Empty);
      }

      private void BlockSizeYNumericUpDown_ValueChanged(object sender, EventArgs e)
      {
         this.BlockSizeYChanged?.Invoke(this, EventArgs.Empty);
      }

      private void GridSizeNumericUpDown_ValueChanged(object sender, EventArgs e)
      {
         this.GridSizeChanged?.Invoke(this, new CudafyGridSizeChangedEventArgs(this.GridSizeX, this.GridSizeY, this.GridSizeZ));
      }

      private void GPUComboBox_SelectedIndexChanged(object sender, EventArgs e)
      {
         this.GPUChanged?.Invoke(this, new CudafyGPUChangedEventArgs(this.gpuComboBox.SelectedItem.ToString()));
      }
   }
}
