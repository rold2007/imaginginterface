namespace ImagingInterface.Views
   {
   partial class ImageView
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageView));
         this.glControl = new OpenTK.GLControl();
         this.statusStrip = new System.Windows.Forms.StatusStrip();
         this.viewModeToolStripSplitButton = new System.Windows.Forms.ToolStripSplitButton();
         this.zoomModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.selectModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.zoomLevelToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
         this.imageSizeToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
         this.mousePositionToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
         this.rgbGrayColorToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
         this.hsvColorToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
         this.ToolStripContainer = new System.Windows.Forms.ToolStripContainer();
         this.verticalScrollBar = new System.Windows.Forms.VScrollBar();
         this.horizontalScrollBar = new System.Windows.Forms.HScrollBar();
         this.statusStrip.SuspendLayout();
         this.ToolStripContainer.BottomToolStripPanel.SuspendLayout();
         this.ToolStripContainer.ContentPanel.SuspendLayout();
         this.ToolStripContainer.SuspendLayout();
         this.SuspendLayout();
         // 
         // glControl
         // 
         this.glControl.BackColor = System.Drawing.Color.Black;
         this.glControl.Location = new System.Drawing.Point(0, 0);
         this.glControl.Name = "glControl";
         this.glControl.Size = new System.Drawing.Size(402, 299);
         this.glControl.TabIndex = 0;
         this.glControl.VSync = false;
         this.glControl.Load += new System.EventHandler(this.GLControl_Load);
         this.glControl.Paint += new System.Windows.Forms.PaintEventHandler(this.GLControl_Paint);
         this.glControl.Layout += new System.Windows.Forms.LayoutEventHandler(this.GLControl_Layout);
         this.glControl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GLControl_MouseClick);
         this.glControl.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.GLControl_MouseDoubleClick);
         this.glControl.MouseLeave += new System.EventHandler(this.GLControl_MouseLeave);
         this.glControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GLControl_MouseMove);
         // 
         // statusStrip
         // 
         this.statusStrip.Dock = System.Windows.Forms.DockStyle.None;
         this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewModeToolStripSplitButton,
            this.zoomLevelToolStripStatusLabel,
            this.imageSizeToolStripStatusLabel,
            this.mousePositionToolStripStatusLabel,
            this.rgbGrayColorToolStripStatusLabel,
            this.hsvColorToolStripStatusLabel});
         this.statusStrip.Location = new System.Drawing.Point(0, 0);
         this.statusStrip.Name = "statusStrip";
         this.statusStrip.Size = new System.Drawing.Size(823, 22);
         this.statusStrip.TabIndex = 1;
         this.statusStrip.Text = "statusStrip1";
         // 
         // viewModeToolStripSplitButton
         // 
         this.viewModeToolStripSplitButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
         this.viewModeToolStripSplitButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomModeToolStripMenuItem,
            this.selectModeToolStripMenuItem});
         this.viewModeToolStripSplitButton.Image = ((System.Drawing.Image)(resources.GetObject("viewModeToolStripSplitButton.Image")));
         this.viewModeToolStripSplitButton.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.viewModeToolStripSplitButton.Name = "viewModeToolStripSplitButton";
         this.viewModeToolStripSplitButton.Size = new System.Drawing.Size(89, 20);
         this.viewModeToolStripSplitButton.Text = "Zoom Mode";
         this.viewModeToolStripSplitButton.ToolTipText = "View Mode";
         this.viewModeToolStripSplitButton.ButtonClick += new System.EventHandler(this.ViewModeToolStripSplitButton_ButtonClick);
         this.viewModeToolStripSplitButton.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ViewModeToolStripSplitButton_DropDownItemClicked);
         // 
         // zoomModeToolStripMenuItem
         // 
         this.zoomModeToolStripMenuItem.Name = "zoomModeToolStripMenuItem";
         this.zoomModeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
         this.zoomModeToolStripMenuItem.Tag = "";
         this.zoomModeToolStripMenuItem.Text = "Zoom Mode";
         // 
         // selectModeToolStripMenuItem
         // 
         this.selectModeToolStripMenuItem.Name = "selectModeToolStripMenuItem";
         this.selectModeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
         this.selectModeToolStripMenuItem.Tag = "";
         this.selectModeToolStripMenuItem.Text = "Select Mode";
         // 
         // zoomLevelToolStripStatusLabel
         // 
         this.zoomLevelToolStripStatusLabel.Name = "zoomLevelToolStripStatusLabel";
         this.zoomLevelToolStripStatusLabel.Size = new System.Drawing.Size(64, 17);
         this.zoomLevelToolStripStatusLabel.Text = "zoomLevel";
         // 
         // imageSizeToolStripStatusLabel
         // 
         this.imageSizeToolStripStatusLabel.Name = "imageSizeToolStripStatusLabel";
         this.imageSizeToolStripStatusLabel.Size = new System.Drawing.Size(60, 17);
         this.imageSizeToolStripStatusLabel.Text = "imageSize";
         // 
         // mousePositionToolStripStatusLabel
         // 
         this.mousePositionToolStripStatusLabel.Name = "mousePositionToolStripStatusLabel";
         this.mousePositionToolStripStatusLabel.Size = new System.Drawing.Size(86, 17);
         this.mousePositionToolStripStatusLabel.Text = "mousePosition";
         // 
         // rgbGrayColorToolStripStatusLabel
         // 
         this.rgbGrayColorToolStripStatusLabel.Name = "rgbGrayColorToolStripStatusLabel";
         this.rgbGrayColorToolStripStatusLabel.Size = new System.Drawing.Size(78, 17);
         this.rgbGrayColorToolStripStatusLabel.Text = "rgbGrayColor";
         // 
         // hsvColorToolStripStatusLabel
         // 
         this.hsvColorToolStripStatusLabel.Name = "hsvColorToolStripStatusLabel";
         this.hsvColorToolStripStatusLabel.Size = new System.Drawing.Size(54, 17);
         this.hsvColorToolStripStatusLabel.Text = "hsvColor";
         // 
         // ToolStripContainer
         // 
         // 
         // ToolStripContainer.BottomToolStripPanel
         // 
         this.ToolStripContainer.BottomToolStripPanel.Controls.Add(this.statusStrip);
         // 
         // ToolStripContainer.ContentPanel
         // 
         this.ToolStripContainer.ContentPanel.AutoScroll = true;
         this.ToolStripContainer.ContentPanel.Controls.Add(this.verticalScrollBar);
         this.ToolStripContainer.ContentPanel.Controls.Add(this.horizontalScrollBar);
         this.ToolStripContainer.ContentPanel.Controls.Add(this.glControl);
         this.ToolStripContainer.ContentPanel.Size = new System.Drawing.Size(823, 454);
         this.ToolStripContainer.ContentPanel.Layout += new System.Windows.Forms.LayoutEventHandler(this.ToolStripContainer_ContentPanel_Layout);
         this.ToolStripContainer.ContentPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ToolStripContainer_ContentPanel_MouseClick);
         this.ToolStripContainer.ContentPanel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ToolStripContainer_ContentPanel_MouseDoubleClick);
         this.ToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
         this.ToolStripContainer.LeftToolStripPanelVisible = false;
         this.ToolStripContainer.Location = new System.Drawing.Point(0, 0);
         this.ToolStripContainer.Name = "ToolStripContainer";
         this.ToolStripContainer.RightToolStripPanelVisible = false;
         this.ToolStripContainer.Size = new System.Drawing.Size(823, 476);
         this.ToolStripContainer.TabIndex = 2;
         this.ToolStripContainer.TopToolStripPanelVisible = false;
         // 
         // verticalScrollBar
         // 
         this.verticalScrollBar.Location = new System.Drawing.Point(806, 0);
         this.verticalScrollBar.Name = "verticalScrollBar";
         this.verticalScrollBar.Size = new System.Drawing.Size(17, 420);
         this.verticalScrollBar.TabIndex = 2;
         this.verticalScrollBar.TabStop = true;
         this.verticalScrollBar.Visible = false;
         this.verticalScrollBar.ValueChanged += new System.EventHandler(this.VerticalScrollBar_ValueChanged);
         // 
         // horizontalScrollBar
         // 
         this.horizontalScrollBar.Location = new System.Drawing.Point(0, 420);
         this.horizontalScrollBar.Name = "horizontalScrollBar";
         this.horizontalScrollBar.Size = new System.Drawing.Size(806, 17);
         this.horizontalScrollBar.TabIndex = 1;
         this.horizontalScrollBar.TabStop = true;
         this.horizontalScrollBar.Visible = false;
         this.horizontalScrollBar.ValueChanged += new System.EventHandler(this.HorizontalScrollBar_ValueChanged);
         // 
         // ImageView
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.ToolStripContainer);
         this.Name = "ImageView";
         this.Size = new System.Drawing.Size(823, 476);
         this.statusStrip.ResumeLayout(false);
         this.statusStrip.PerformLayout();
         this.ToolStripContainer.BottomToolStripPanel.ResumeLayout(false);
         this.ToolStripContainer.BottomToolStripPanel.PerformLayout();
         this.ToolStripContainer.ContentPanel.ResumeLayout(false);
         this.ToolStripContainer.ResumeLayout(false);
         this.ToolStripContainer.PerformLayout();
         this.ResumeLayout(false);

         }

      #endregion

      private OpenTK.GLControl glControl;
      private System.Windows.Forms.StatusStrip statusStrip;
      private System.Windows.Forms.ToolStripStatusLabel mousePositionToolStripStatusLabel;
      private System.Windows.Forms.ToolStripContainer ToolStripContainer;
      private System.Windows.Forms.ToolStripSplitButton viewModeToolStripSplitButton;
      private System.Windows.Forms.ToolStripMenuItem zoomModeToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem selectModeToolStripMenuItem;
      private System.Windows.Forms.HScrollBar horizontalScrollBar;
      private System.Windows.Forms.VScrollBar verticalScrollBar;
      private System.Windows.Forms.ToolStripStatusLabel zoomLevelToolStripStatusLabel;
      private System.Windows.Forms.ToolStripStatusLabel rgbGrayColorToolStripStatusLabel;
      private System.Windows.Forms.ToolStripStatusLabel imageSizeToolStripStatusLabel;
      private System.Windows.Forms.ToolStripStatusLabel hsvColorToolStripStatusLabel;

      }
   }
