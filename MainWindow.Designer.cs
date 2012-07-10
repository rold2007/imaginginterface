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
         this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
         this.blobSplitContainer = new System.Windows.Forms.SplitContainer();
         this.blobListView = new System.Windows.Forms.ListView();
         this.blobImageList = new System.Windows.Forms.ImageList(this.components);
         this.whiteBackgroundCheckBox = new System.Windows.Forms.CheckBox();
         this.blobAnalysisButton = new System.Windows.Forms.Button();
         this.maxAreaThresholdLabel = new System.Windows.Forms.Label();
         this.backgroundColorLabel = new System.Windows.Forms.Label();
         this.minAreaThresholdLabel = new System.Windows.Forms.Label();
         ((System.ComponentModel.ISupportInitialize)(this.mainImageBox)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.backgroundColorTrackBar)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.maxAreaThresholdTrackBar)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.minAreaThresholdTrackBar)).BeginInit();
         this.mainSplitContainer.Panel1.SuspendLayout();
         this.mainSplitContainer.Panel2.SuspendLayout();
         this.mainSplitContainer.SuspendLayout();
         this.blobSplitContainer.Panel1.SuspendLayout();
         this.blobSplitContainer.Panel2.SuspendLayout();
         this.blobSplitContainer.SuspendLayout();
         this.SuspendLayout();
         // 
         // mainImageBox
         // 
         this.mainImageBox.Dock = System.Windows.Forms.DockStyle.Fill;
         this.mainImageBox.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.PanAndZoom;
         this.mainImageBox.Location = new System.Drawing.Point(0, 0);
         this.mainImageBox.Name = "mainImageBox";
         this.mainImageBox.Size = new System.Drawing.Size(232, 466);
         this.mainImageBox.TabIndex = 0;
         this.mainImageBox.TabStop = false;
         // 
         // backgroundColorTrackBar
         // 
         this.backgroundColorTrackBar.Location = new System.Drawing.Point(3, 19);
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
         this.maxAreaThresholdTrackBar.Location = new System.Drawing.Point(172, 83);
         this.maxAreaThresholdTrackBar.Maximum = 2147483647;
         this.maxAreaThresholdTrackBar.Minimum = 2147483547;
         this.maxAreaThresholdTrackBar.Name = "maxAreaThresholdTrackBar";
         this.maxAreaThresholdTrackBar.Size = new System.Drawing.Size(263, 45);
         this.maxAreaThresholdTrackBar.TabIndex = 2;
         this.maxAreaThresholdTrackBar.TickFrequency = 5;
         this.imagingInterfaceToolTip.SetToolTip(this.maxAreaThresholdTrackBar, "Filter blobs with area higher than max value");
         this.maxAreaThresholdTrackBar.Value = 2147483647;
         this.maxAreaThresholdTrackBar.ValueChanged += new System.EventHandler(this.maxAreaThresholdTrackBar_ValueChanged);
         // 
         // minAreaThresholdTrackBar
         // 
         this.minAreaThresholdTrackBar.Enabled = false;
         this.minAreaThresholdTrackBar.Location = new System.Drawing.Point(172, 19);
         this.minAreaThresholdTrackBar.Maximum = 100;
         this.minAreaThresholdTrackBar.Name = "minAreaThresholdTrackBar";
         this.minAreaThresholdTrackBar.Size = new System.Drawing.Size(263, 45);
         this.minAreaThresholdTrackBar.TabIndex = 1;
         this.minAreaThresholdTrackBar.TickFrequency = 5;
         this.imagingInterfaceToolTip.SetToolTip(this.minAreaThresholdTrackBar, "Filter blobs with area lower than min value");
         this.minAreaThresholdTrackBar.ValueChanged += new System.EventHandler(this.minAreaThresholdTrackBar_ValueChanged);
         // 
         // mainSplitContainer
         // 
         this.mainSplitContainer.BackColor = System.Drawing.SystemColors.ControlText;
         this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
         this.mainSplitContainer.Location = new System.Drawing.Point(0, 0);
         this.mainSplitContainer.Name = "mainSplitContainer";
         // 
         // mainSplitContainer.Panel1
         // 
         this.mainSplitContainer.Panel1.BackColor = System.Drawing.SystemColors.Control;
         this.mainSplitContainer.Panel1.Controls.Add(this.mainImageBox);
         // 
         // mainSplitContainer.Panel2
         // 
         this.mainSplitContainer.Panel2.BackColor = System.Drawing.SystemColors.Control;
         this.mainSplitContainer.Panel2.Controls.Add(this.blobSplitContainer);
         this.mainSplitContainer.Size = new System.Drawing.Size(737, 466);
         this.mainSplitContainer.SplitterDistance = 232;
         this.mainSplitContainer.TabIndex = 1;
         // 
         // blobSplitContainer
         // 
         this.blobSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
         this.blobSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
         this.blobSplitContainer.IsSplitterFixed = true;
         this.blobSplitContainer.Location = new System.Drawing.Point(0, 0);
         this.blobSplitContainer.Name = "blobSplitContainer";
         this.blobSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
         // 
         // blobSplitContainer.Panel1
         // 
         this.blobSplitContainer.Panel1.Controls.Add(this.blobListView);
         // 
         // blobSplitContainer.Panel2
         // 
         this.blobSplitContainer.Panel2.Controls.Add(this.whiteBackgroundCheckBox);
         this.blobSplitContainer.Panel2.Controls.Add(this.blobAnalysisButton);
         this.blobSplitContainer.Panel2.Controls.Add(this.backgroundColorTrackBar);
         this.blobSplitContainer.Panel2.Controls.Add(this.maxAreaThresholdLabel);
         this.blobSplitContainer.Panel2.Controls.Add(this.minAreaThresholdTrackBar);
         this.blobSplitContainer.Panel2.Controls.Add(this.maxAreaThresholdTrackBar);
         this.blobSplitContainer.Panel2.Controls.Add(this.backgroundColorLabel);
         this.blobSplitContainer.Panel2.Controls.Add(this.minAreaThresholdLabel);
         this.blobSplitContainer.Size = new System.Drawing.Size(501, 466);
         this.blobSplitContainer.SplitterDistance = 335;
         this.blobSplitContainer.TabIndex = 9;
         // 
         // blobListView
         // 
         this.blobListView.Dock = System.Windows.Forms.DockStyle.Fill;
         this.blobListView.LargeImageList = this.blobImageList;
         this.blobListView.Location = new System.Drawing.Point(0, 0);
         this.blobListView.Name = "blobListView";
         this.blobListView.Size = new System.Drawing.Size(501, 335);
         this.blobListView.SmallImageList = this.blobImageList;
         this.blobListView.TabIndex = 1;
         this.blobListView.UseCompatibleStateImageBehavior = false;
         // 
         // blobImageList
         // 
         this.blobImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
         this.blobImageList.ImageSize = new System.Drawing.Size(64, 64);
         this.blobImageList.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // whiteBackgroundCheckBox
         // 
         this.whiteBackgroundCheckBox.AutoSize = true;
         this.whiteBackgroundCheckBox.Location = new System.Drawing.Point(6, 67);
         this.whiteBackgroundCheckBox.Name = "whiteBackgroundCheckBox";
         this.whiteBackgroundCheckBox.Size = new System.Drawing.Size(115, 17);
         this.whiteBackgroundCheckBox.TabIndex = 9;
         this.whiteBackgroundCheckBox.Text = "White Background";
         this.whiteBackgroundCheckBox.UseVisualStyleBackColor = true;
         // 
         // blobAnalysisButton
         // 
         this.blobAnalysisButton.AutoSize = true;
         this.blobAnalysisButton.Enabled = false;
         this.blobAnalysisButton.Location = new System.Drawing.Point(6, 92);
         this.blobAnalysisButton.Name = "blobAnalysisButton";
         this.blobAnalysisButton.Size = new System.Drawing.Size(94, 23);
         this.blobAnalysisButton.TabIndex = 8;
         this.blobAnalysisButton.Text = "Blob Analysis";
         this.blobAnalysisButton.UseVisualStyleBackColor = true;
         this.blobAnalysisButton.Click += new System.EventHandler(this.blobAnalysisButton_Click);
         // 
         // maxAreaThresholdLabel
         // 
         this.maxAreaThresholdLabel.AutoSize = true;
         this.maxAreaThresholdLabel.Enabled = false;
         this.maxAreaThresholdLabel.Location = new System.Drawing.Point(172, 67);
         this.maxAreaThresholdLabel.Name = "maxAreaThresholdLabel";
         this.maxAreaThresholdLabel.Size = new System.Drawing.Size(100, 13);
         this.maxAreaThresholdLabel.TabIndex = 7;
         this.maxAreaThresholdLabel.Text = "Max area threshold:";
         // 
         // backgroundColorLabel
         // 
         this.backgroundColorLabel.AutoSize = true;
         this.backgroundColorLabel.Location = new System.Drawing.Point(3, 3);
         this.backgroundColorLabel.Name = "backgroundColorLabel";
         this.backgroundColorLabel.Size = new System.Drawing.Size(103, 13);
         this.backgroundColorLabel.TabIndex = 4;
         this.backgroundColorLabel.Text = "Background color: 0";
         // 
         // minAreaThresholdLabel
         // 
         this.minAreaThresholdLabel.AutoSize = true;
         this.minAreaThresholdLabel.Enabled = false;
         this.minAreaThresholdLabel.Location = new System.Drawing.Point(172, 3);
         this.minAreaThresholdLabel.Name = "minAreaThresholdLabel";
         this.minAreaThresholdLabel.Size = new System.Drawing.Size(97, 13);
         this.minAreaThresholdLabel.TabIndex = 5;
         this.minAreaThresholdLabel.Text = "Min area threshold:";
         // 
         // mainWindow
         // 
         this.AllowDrop = true;
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(737, 466);
         this.Controls.Add(this.mainSplitContainer);
         this.Name = "mainWindow";
         this.Text = "ImagingInterface";
         this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
         this.DragDrop += new System.Windows.Forms.DragEventHandler(this.mainWindow_DragDrop);
         this.DragEnter += new System.Windows.Forms.DragEventHandler(this.mainWindow_DragEnter);
         ((System.ComponentModel.ISupportInitialize)(this.mainImageBox)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.backgroundColorTrackBar)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.maxAreaThresholdTrackBar)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.minAreaThresholdTrackBar)).EndInit();
         this.mainSplitContainer.Panel1.ResumeLayout(false);
         this.mainSplitContainer.Panel2.ResumeLayout(false);
         this.mainSplitContainer.ResumeLayout(false);
         this.blobSplitContainer.Panel1.ResumeLayout(false);
         this.blobSplitContainer.Panel2.ResumeLayout(false);
         this.blobSplitContainer.Panel2.PerformLayout();
         this.blobSplitContainer.ResumeLayout(false);
         this.ResumeLayout(false);

         }

      #endregion

      private Emgu.CV.UI.ImageBox mainImageBox;
      private System.Windows.Forms.ToolTip imagingInterfaceToolTip;
      private System.Windows.Forms.SplitContainer mainSplitContainer;
      private System.Windows.Forms.TrackBar minAreaThresholdTrackBar;
      private System.Windows.Forms.TrackBar backgroundColorTrackBar;
      private System.Windows.Forms.Label minAreaThresholdLabel;
      private System.Windows.Forms.Label backgroundColorLabel;
      private System.Windows.Forms.Label maxAreaThresholdLabel;
      private System.Windows.Forms.TrackBar maxAreaThresholdTrackBar;
      private System.Windows.Forms.Button blobAnalysisButton;
      private System.Windows.Forms.ListView blobListView;
      private System.Windows.Forms.ImageList blobImageList;
      private System.Windows.Forms.SplitContainer blobSplitContainer;
      private System.Windows.Forms.CheckBox whiteBackgroundCheckBox;
      }
   }

