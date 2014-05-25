namespace ImageProcessing.Views
   {
   partial class CudafyView
      {
      /// <summary> 
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary> 
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
         {
         if (disposing && (components != null))
            {
            components.Dispose();
            }
         base.Dispose(disposing);
         }

      #region Component Designer generated code

      /// <summary> 
      /// Required method for Designer support - do not modify 
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
         {
         this.label1 = new System.Windows.Forms.Label();
         this.benchmarkAddCudafyLabel = new System.Windows.Forms.Label();
         this.addTrackBar = new System.Windows.Forms.TrackBar();
         this.addLabel = new System.Windows.Forms.Label();
         this.addTextBox = new System.Windows.Forms.TextBox();
         this.benchmarkAddOpenCVLabel = new System.Windows.Forms.Label();
         this.label4 = new System.Windows.Forms.Label();
         this.gpuLabel = new System.Windows.Forms.Label();
         this.gpuComboBox = new System.Windows.Forms.ComboBox();
         this.gridSizeGroupBox = new System.Windows.Forms.GroupBox();
         this.label8 = new System.Windows.Forms.Label();
         this.label7 = new System.Windows.Forms.Label();
         this.label6 = new System.Windows.Forms.Label();
         this.gridSizeZNumericUpDown = new System.Windows.Forms.NumericUpDown();
         this.gridSizeYNumericUpDown = new System.Windows.Forms.NumericUpDown();
         this.gridSizeXNumericUpDown = new System.Windows.Forms.NumericUpDown();
         this.blockSizeGroupBox = new System.Windows.Forms.GroupBox();
         this.label2 = new System.Windows.Forms.Label();
         this.label3 = new System.Windows.Forms.Label();
         this.label5 = new System.Windows.Forms.Label();
         this.blockSizeZNumericUpDown = new System.Windows.Forms.NumericUpDown();
         this.blockSizeYNumericUpDown = new System.Windows.Forms.NumericUpDown();
         this.blockSizeXNumericUpDown = new System.Windows.Forms.NumericUpDown();
         ((System.ComponentModel.ISupportInitialize)(this.addTrackBar)).BeginInit();
         this.gridSizeGroupBox.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.gridSizeZNumericUpDown)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.gridSizeYNumericUpDown)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.gridSizeXNumericUpDown)).BeginInit();
         this.blockSizeGroupBox.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.blockSizeZNumericUpDown)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.blockSizeYNumericUpDown)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.blockSizeXNumericUpDown)).BeginInit();
         this.SuspendLayout();
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(3, 287);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(122, 13);
         this.label1.TabIndex = 1;
         this.label1.Text = "Benchmark Add Cudafy:";
         // 
         // benchmarkAddCudafyLabel
         // 
         this.benchmarkAddCudafyLabel.AutoSize = true;
         this.benchmarkAddCudafyLabel.Location = new System.Drawing.Point(132, 287);
         this.benchmarkAddCudafyLabel.Name = "benchmarkAddCudafyLabel";
         this.benchmarkAddCudafyLabel.Size = new System.Drawing.Size(0, 13);
         this.benchmarkAddCudafyLabel.TabIndex = 2;
         // 
         // addTrackBar
         // 
         this.addTrackBar.LargeChange = 8;
         this.addTrackBar.Location = new System.Drawing.Point(41, 239);
         this.addTrackBar.Maximum = 255;
         this.addTrackBar.Name = "addTrackBar";
         this.addTrackBar.Size = new System.Drawing.Size(212, 45);
         this.addTrackBar.TabIndex = 3;
         this.addTrackBar.TickFrequency = 8;
         this.addTrackBar.Scroll += new System.EventHandler(this.AddTrackBar_Scroll);
         // 
         // addLabel
         // 
         this.addLabel.AutoSize = true;
         this.addLabel.Location = new System.Drawing.Point(3, 238);
         this.addLabel.Name = "addLabel";
         this.addLabel.Size = new System.Drawing.Size(32, 13);
         this.addLabel.TabIndex = 4;
         this.addLabel.Text = "Add: ";
         // 
         // addTextBox
         // 
         this.addTextBox.Enabled = false;
         this.addTextBox.Location = new System.Drawing.Point(259, 239);
         this.addTextBox.Name = "addTextBox";
         this.addTextBox.Size = new System.Drawing.Size(24, 20);
         this.addTextBox.TabIndex = 5;
         this.addTextBox.Text = "0";
         // 
         // benchmarkAddOpenCVLabel
         // 
         this.benchmarkAddOpenCVLabel.AutoSize = true;
         this.benchmarkAddOpenCVLabel.Location = new System.Drawing.Point(138, 310);
         this.benchmarkAddOpenCVLabel.Name = "benchmarkAddOpenCVLabel";
         this.benchmarkAddOpenCVLabel.Size = new System.Drawing.Size(0, 13);
         this.benchmarkAddOpenCVLabel.TabIndex = 7;
         // 
         // label4
         // 
         this.label4.AutoSize = true;
         this.label4.Location = new System.Drawing.Point(3, 310);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(129, 13);
         this.label4.TabIndex = 6;
         this.label4.Text = "Benchmark Add OpenCV:";
         // 
         // gpuLabel
         // 
         this.gpuLabel.AutoSize = true;
         this.gpuLabel.Location = new System.Drawing.Point(3, 6);
         this.gpuLabel.Name = "gpuLabel";
         this.gpuLabel.Size = new System.Drawing.Size(36, 13);
         this.gpuLabel.TabIndex = 8;
         this.gpuLabel.Text = "GPU: ";
         // 
         // gpuComboBox
         // 
         this.gpuComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.gpuComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.gpuComboBox.DropDownWidth = 238;
         this.gpuComboBox.FormattingEnabled = true;
         this.gpuComboBox.Location = new System.Drawing.Point(45, 3);
         this.gpuComboBox.Name = "gpuComboBox";
         this.gpuComboBox.Size = new System.Drawing.Size(238, 21);
         this.gpuComboBox.TabIndex = 9;
         this.gpuComboBox.DropDown += new System.EventHandler(this.GPUComboBox_DropDown);
         this.gpuComboBox.SelectedIndexChanged += new System.EventHandler(this.GPUComboBox_SelectedIndexChanged);
         // 
         // gridSizeGroupBox
         // 
         this.gridSizeGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.gridSizeGroupBox.Controls.Add(this.label8);
         this.gridSizeGroupBox.Controls.Add(this.label7);
         this.gridSizeGroupBox.Controls.Add(this.label6);
         this.gridSizeGroupBox.Controls.Add(this.gridSizeZNumericUpDown);
         this.gridSizeGroupBox.Controls.Add(this.gridSizeYNumericUpDown);
         this.gridSizeGroupBox.Controls.Add(this.gridSizeXNumericUpDown);
         this.gridSizeGroupBox.Location = new System.Drawing.Point(3, 30);
         this.gridSizeGroupBox.Name = "gridSizeGroupBox";
         this.gridSizeGroupBox.Size = new System.Drawing.Size(280, 100);
         this.gridSizeGroupBox.TabIndex = 10;
         this.gridSizeGroupBox.TabStop = false;
         this.gridSizeGroupBox.Text = "Grid Size";
         // 
         // label8
         // 
         this.label8.AutoSize = true;
         this.label8.Enabled = false;
         this.label8.Location = new System.Drawing.Point(3, 69);
         this.label8.Name = "label8";
         this.label8.Size = new System.Drawing.Size(17, 13);
         this.label8.TabIndex = 5;
         this.label8.Text = "Z:";
         // 
         // label7
         // 
         this.label7.AutoSize = true;
         this.label7.Location = new System.Drawing.Point(3, 43);
         this.label7.Name = "label7";
         this.label7.Size = new System.Drawing.Size(17, 13);
         this.label7.TabIndex = 4;
         this.label7.Text = "Y:";
         // 
         // label6
         // 
         this.label6.AutoSize = true;
         this.label6.Location = new System.Drawing.Point(3, 17);
         this.label6.Name = "label6";
         this.label6.Size = new System.Drawing.Size(17, 13);
         this.label6.TabIndex = 3;
         this.label6.Text = "X:";
         // 
         // gridSizeZNumericUpDown
         // 
         this.gridSizeZNumericUpDown.Enabled = false;
         this.gridSizeZNumericUpDown.Location = new System.Drawing.Point(26, 67);
         this.gridSizeZNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
         this.gridSizeZNumericUpDown.Name = "gridSizeZNumericUpDown";
         this.gridSizeZNumericUpDown.Size = new System.Drawing.Size(120, 20);
         this.gridSizeZNumericUpDown.TabIndex = 2;
         this.gridSizeZNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
         // 
         // gridSizeYNumericUpDown
         // 
         this.gridSizeYNumericUpDown.Location = new System.Drawing.Point(26, 41);
         this.gridSizeYNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
         this.gridSizeYNumericUpDown.Name = "gridSizeYNumericUpDown";
         this.gridSizeYNumericUpDown.Size = new System.Drawing.Size(120, 20);
         this.gridSizeYNumericUpDown.TabIndex = 1;
         this.gridSizeYNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
         this.gridSizeYNumericUpDown.ValueChanged += new System.EventHandler(this.GridSizeNumericUpDown_ValueChanged);
         // 
         // gridSizeXNumericUpDown
         // 
         this.gridSizeXNumericUpDown.Location = new System.Drawing.Point(26, 15);
         this.gridSizeXNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
         this.gridSizeXNumericUpDown.Name = "gridSizeXNumericUpDown";
         this.gridSizeXNumericUpDown.Size = new System.Drawing.Size(120, 20);
         this.gridSizeXNumericUpDown.TabIndex = 0;
         this.gridSizeXNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
         this.gridSizeXNumericUpDown.ValueChanged += new System.EventHandler(this.GridSizeNumericUpDown_ValueChanged);
         // 
         // blockSizeGroupBox
         // 
         this.blockSizeGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.blockSizeGroupBox.Controls.Add(this.label2);
         this.blockSizeGroupBox.Controls.Add(this.label3);
         this.blockSizeGroupBox.Controls.Add(this.label5);
         this.blockSizeGroupBox.Controls.Add(this.blockSizeZNumericUpDown);
         this.blockSizeGroupBox.Controls.Add(this.blockSizeYNumericUpDown);
         this.blockSizeGroupBox.Controls.Add(this.blockSizeXNumericUpDown);
         this.blockSizeGroupBox.Location = new System.Drawing.Point(3, 136);
         this.blockSizeGroupBox.Name = "blockSizeGroupBox";
         this.blockSizeGroupBox.Size = new System.Drawing.Size(277, 97);
         this.blockSizeGroupBox.TabIndex = 0;
         this.blockSizeGroupBox.TabStop = false;
         this.blockSizeGroupBox.Text = "Block Size";
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Enabled = false;
         this.label2.Location = new System.Drawing.Point(3, 73);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(17, 13);
         this.label2.TabIndex = 11;
         this.label2.Text = "Z:";
         // 
         // label3
         // 
         this.label3.AutoSize = true;
         this.label3.Location = new System.Drawing.Point(3, 47);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(17, 13);
         this.label3.TabIndex = 10;
         this.label3.Text = "Y:";
         // 
         // label5
         // 
         this.label5.AutoSize = true;
         this.label5.Location = new System.Drawing.Point(3, 21);
         this.label5.Name = "label5";
         this.label5.Size = new System.Drawing.Size(17, 13);
         this.label5.TabIndex = 9;
         this.label5.Text = "X:";
         // 
         // blockSizeZNumericUpDown
         // 
         this.blockSizeZNumericUpDown.Enabled = false;
         this.blockSizeZNumericUpDown.Location = new System.Drawing.Point(26, 71);
         this.blockSizeZNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
         this.blockSizeZNumericUpDown.Name = "blockSizeZNumericUpDown";
         this.blockSizeZNumericUpDown.Size = new System.Drawing.Size(120, 20);
         this.blockSizeZNumericUpDown.TabIndex = 8;
         this.blockSizeZNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
         // 
         // blockSizeYNumericUpDown
         // 
         this.blockSizeYNumericUpDown.Location = new System.Drawing.Point(26, 45);
         this.blockSizeYNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
         this.blockSizeYNumericUpDown.Name = "blockSizeYNumericUpDown";
         this.blockSizeYNumericUpDown.Size = new System.Drawing.Size(120, 20);
         this.blockSizeYNumericUpDown.TabIndex = 7;
         this.blockSizeYNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
         this.blockSizeYNumericUpDown.ValueChanged += new System.EventHandler(this.BlockSizeYNumericUpDown_ValueChanged);
         // 
         // blockSizeXNumericUpDown
         // 
         this.blockSizeXNumericUpDown.Location = new System.Drawing.Point(26, 19);
         this.blockSizeXNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
         this.blockSizeXNumericUpDown.Name = "blockSizeXNumericUpDown";
         this.blockSizeXNumericUpDown.Size = new System.Drawing.Size(120, 20);
         this.blockSizeXNumericUpDown.TabIndex = 6;
         this.blockSizeXNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
         this.blockSizeXNumericUpDown.ValueChanged += new System.EventHandler(this.BlockSizeXNumericUpDown_ValueChanged);
         // 
         // CudafyView
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.blockSizeGroupBox);
         this.Controls.Add(this.gridSizeGroupBox);
         this.Controls.Add(this.gpuComboBox);
         this.Controls.Add(this.gpuLabel);
         this.Controls.Add(this.benchmarkAddOpenCVLabel);
         this.Controls.Add(this.label4);
         this.Controls.Add(this.addTextBox);
         this.Controls.Add(this.addLabel);
         this.Controls.Add(this.addTrackBar);
         this.Controls.Add(this.benchmarkAddCudafyLabel);
         this.Controls.Add(this.label1);
         this.Name = "CudafyView";
         this.Size = new System.Drawing.Size(286, 466);
         ((System.ComponentModel.ISupportInitialize)(this.addTrackBar)).EndInit();
         this.gridSizeGroupBox.ResumeLayout(false);
         this.gridSizeGroupBox.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.gridSizeZNumericUpDown)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.gridSizeYNumericUpDown)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.gridSizeXNumericUpDown)).EndInit();
         this.blockSizeGroupBox.ResumeLayout(false);
         this.blockSizeGroupBox.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.blockSizeZNumericUpDown)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.blockSizeYNumericUpDown)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.blockSizeXNumericUpDown)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

         }

      #endregion

      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label benchmarkAddCudafyLabel;
      private System.Windows.Forms.TrackBar addTrackBar;
      private System.Windows.Forms.Label addLabel;
      private System.Windows.Forms.TextBox addTextBox;
      private System.Windows.Forms.Label benchmarkAddOpenCVLabel;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.Label gpuLabel;
      private System.Windows.Forms.ComboBox gpuComboBox;
      private System.Windows.Forms.GroupBox gridSizeGroupBox;
      private System.Windows.Forms.GroupBox blockSizeGroupBox;
      private System.Windows.Forms.Label label8;
      private System.Windows.Forms.Label label7;
      private System.Windows.Forms.Label label6;
      private System.Windows.Forms.NumericUpDown gridSizeZNumericUpDown;
      private System.Windows.Forms.NumericUpDown gridSizeYNumericUpDown;
      private System.Windows.Forms.NumericUpDown gridSizeXNumericUpDown;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.NumericUpDown blockSizeZNumericUpDown;
      private System.Windows.Forms.NumericUpDown blockSizeYNumericUpDown;
      private System.Windows.Forms.NumericUpDown blockSizeXNumericUpDown;
      }
   }
