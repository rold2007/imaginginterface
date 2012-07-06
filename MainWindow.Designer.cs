namespace ImagingInterface
   {
   partial class mainWindow
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

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
         {
         this.components = new System.ComponentModel.Container();
         this.mainImageBox = new Emgu.CV.UI.ImageBox();
         this.imagingInterfaceToolTip = new System.Windows.Forms.ToolTip(this.components);
         this.backgroundColorTrackBar = new System.Windows.Forms.TrackBar();
         this.maxAreaThresholdTrackBar = new System.Windows.Forms.TrackBar();
         this.minAreaThresholdTrackBar = new System.Windows.Forms.TrackBar();
         this.splitContainer = new System.Windows.Forms.SplitContainer();
         this.maxAreaThresholdLabel = new System.Windows.Forms.Label();
         this.minAreaThresholdLabel = new System.Windows.Forms.Label();
         this.backgroundColorLabel = new System.Windows.Forms.Label();
         this.blobAnalysisButton = new System.Windows.Forms.Button();
         ((System.ComponentModel.ISupportInitialize)(this.mainImageBox)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.backgroundColorTrackBar)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.maxAreaThresholdTrackBar)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.minAreaThresholdTrackBar)).BeginInit();
         this.splitContainer.Panel1.SuspendLayout();
         this.splitContainer.Panel2.SuspendLayout();
         this.splitContainer.SuspendLayout();
         this.SuspendLayout();
         // 
         // mainImageBox
         // 
         this.mainImageBox.Dock = System.Windows.Forms.DockStyle.Fill;
         this.mainImageBox.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.PanAndZoom;
         this.mainImageBox.Location = new System.Drawing.Point(0, 0);
         this.mainImageBox.Name = "mainImageBox";
         this.mainImageBox.Size = new System.Drawing.Size(455, 466);
         this.mainImageBox.TabIndex = 0;
         this.mainImageBox.TabStop = false;
         // 
         // backgroundColorTrackBar
         // 
         this.backgroundColorTrackBar.Location = new System.Drawing.Point(3, 51);
         this.backgroundColorTrackBar.Maximum = 254;
         this.backgroundColorTrackBar.Name = "backgroundColorTrackBar";
         this.backgroundColorTrackBar.Size = new System.Drawing.Size(163, 45);
         this.backgroundColorTrackBar.TabIndex = 0;
         this.backgroundColorTrackBar.TickFrequency = 20;
         this.imagingInterfaceToolTip.SetToolTip(this.backgroundColorTrackBar, "Blob background color threshold");
         this.backgroundColorTrackBar.Scroll += new System.EventHandler(this.backgroundColorTrackBar_Scroll);
         // 
         // maxAreaThresholdTrackBar
         // 
         this.maxAreaThresholdTrackBar.Enabled = false;
         this.maxAreaThresholdTrackBar.Location = new System.Drawing.Point(3, 179);
         this.maxAreaThresholdTrackBar.Maximum = 2147483647;
         this.maxAreaThresholdTrackBar.Minimum = 2147483547;
         this.maxAreaThresholdTrackBar.Name = "maxAreaThresholdTrackBar";
         this.maxAreaThresholdTrackBar.Size = new System.Drawing.Size(163, 45);
         this.maxAreaThresholdTrackBar.TabIndex = 2;
         this.maxAreaThresholdTrackBar.TickFrequency = 5;
         this.imagingInterfaceToolTip.SetToolTip(this.maxAreaThresholdTrackBar, "Filter blobs with area higher than max value");
         this.maxAreaThresholdTrackBar.Value = 2147483647;
         this.maxAreaThresholdTrackBar.ValueChanged += new System.EventHandler(this.maxAreaThresholdTrackBar_ValueChanged);
         // 
         // minAreaThresholdTrackBar
         // 
         this.minAreaThresholdTrackBar.Enabled = false;
         this.minAreaThresholdTrackBar.Location = new System.Drawing.Point(3, 115);
         this.minAreaThresholdTrackBar.Maximum = 100;
         this.minAreaThresholdTrackBar.Name = "minAreaThresholdTrackBar";
         this.minAreaThresholdTrackBar.Size = new System.Drawing.Size(163, 45);
         this.minAreaThresholdTrackBar.TabIndex = 1;
         this.minAreaThresholdTrackBar.TickFrequency = 5;
         this.imagingInterfaceToolTip.SetToolTip(this.minAreaThresholdTrackBar, "Filter blobs with area lower than min value");
         this.minAreaThresholdTrackBar.ValueChanged += new System.EventHandler(this.minAreaThresholdTrackBar_ValueChanged);
         // 
         // splitContainer
         // 
         this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
         this.splitContainer.Location = new System.Drawing.Point(0, 0);
         this.splitContainer.Name = "splitContainer";
         // 
         // splitContainer.Panel1
         // 
         this.splitContainer.Panel1.Controls.Add(this.mainImageBox);
         // 
         // splitContainer.Panel2
         // 
         this.splitContainer.Panel2.Controls.Add(this.blobAnalysisButton);
         this.splitContainer.Panel2.Controls.Add(this.maxAreaThresholdLabel);
         this.splitContainer.Panel2.Controls.Add(this.maxAreaThresholdTrackBar);
         this.splitContainer.Panel2.Controls.Add(this.minAreaThresholdLabel);
         this.splitContainer.Panel2.Controls.Add(this.backgroundColorLabel);
         this.splitContainer.Panel2.Controls.Add(this.minAreaThresholdTrackBar);
         this.splitContainer.Panel2.Controls.Add(this.backgroundColorTrackBar);
         this.splitContainer.Size = new System.Drawing.Size(637, 466);
         this.splitContainer.SplitterDistance = 455;
         this.splitContainer.TabIndex = 1;
         // 
         // maxAreaThresholdLabel
         // 
         this.maxAreaThresholdLabel.AutoSize = true;
         this.maxAreaThresholdLabel.Enabled = false;
         this.maxAreaThresholdLabel.Location = new System.Drawing.Point(3, 163);
         this.maxAreaThresholdLabel.Name = "maxAreaThresholdLabel";
         this.maxAreaThresholdLabel.Size = new System.Drawing.Size(100, 13);
         this.maxAreaThresholdLabel.TabIndex = 7;
         this.maxAreaThresholdLabel.Text = "Max area threshold:";
         // 
         // minAreaThresholdLabel
         // 
         this.minAreaThresholdLabel.AutoSize = true;
         this.minAreaThresholdLabel.Enabled = false;
         this.minAreaThresholdLabel.Location = new System.Drawing.Point(3, 99);
         this.minAreaThresholdLabel.Name = "minAreaThresholdLabel";
         this.minAreaThresholdLabel.Size = new System.Drawing.Size(97, 13);
         this.minAreaThresholdLabel.TabIndex = 5;
         this.minAreaThresholdLabel.Text = "Min area threshold:";
         // 
         // backgroundColorLabel
         // 
         this.backgroundColorLabel.AutoSize = true;
         this.backgroundColorLabel.Location = new System.Drawing.Point(3, 35);
         this.backgroundColorLabel.Name = "backgroundColorLabel";
         this.backgroundColorLabel.Size = new System.Drawing.Size(103, 13);
         this.backgroundColorLabel.TabIndex = 4;
         this.backgroundColorLabel.Text = "Background color: 0";
         // 
         // blobAnalysisButton
         // 
         this.blobAnalysisButton.AutoSize = true;
         this.blobAnalysisButton.Enabled = false;
         this.blobAnalysisButton.Location = new System.Drawing.Point(6, 230);
         this.blobAnalysisButton.Name = "blobAnalysisButton";
         this.blobAnalysisButton.Size = new System.Drawing.Size(94, 23);
         this.blobAnalysisButton.TabIndex = 8;
         this.blobAnalysisButton.Text = "Blob Analysis";
         this.blobAnalysisButton.UseVisualStyleBackColor = true;
         this.blobAnalysisButton.Click += new System.EventHandler(this.blobAnalysisButton_Click);
         // 
         // mainWindow
         // 
         this.AllowDrop = true;
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(637, 466);
         this.Controls.Add(this.splitContainer);
         this.Name = "mainWindow";
         this.Text = "ImagingInterface";
         this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
         this.DragDrop += new System.Windows.Forms.DragEventHandler(this.mainWindow_DragDrop);
         this.DragEnter += new System.Windows.Forms.DragEventHandler(this.mainWindow_DragEnter);
         ((System.ComponentModel.ISupportInitialize)(this.mainImageBox)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.backgroundColorTrackBar)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.maxAreaThresholdTrackBar)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.minAreaThresholdTrackBar)).EndInit();
         this.splitContainer.Panel1.ResumeLayout(false);
         this.splitContainer.Panel2.ResumeLayout(false);
         this.splitContainer.Panel2.PerformLayout();
         this.splitContainer.ResumeLayout(false);
         this.ResumeLayout(false);

         }

      #endregion

      private Emgu.CV.UI.ImageBox mainImageBox;
      private System.Windows.Forms.ToolTip imagingInterfaceToolTip;
      private System.Windows.Forms.SplitContainer splitContainer;
      private System.Windows.Forms.TrackBar minAreaThresholdTrackBar;
      private System.Windows.Forms.TrackBar backgroundColorTrackBar;
      private System.Windows.Forms.Label minAreaThresholdLabel;
      private System.Windows.Forms.Label backgroundColorLabel;
      private System.Windows.Forms.Label maxAreaThresholdLabel;
      private System.Windows.Forms.TrackBar maxAreaThresholdTrackBar;
      private System.Windows.Forms.Button blobAnalysisButton;
      }
   }

