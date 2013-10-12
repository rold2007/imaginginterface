namespace ImagingInterface.Views
   {
   public partial class MainWindow
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
         this.imagingInterfaceToolTip = new System.Windows.Forms.ToolTip(this.components);
         this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
         this.imagesTabControl = new System.Windows.Forms.TabControl();
         this.splitContainer = new System.Windows.Forms.SplitContainer();
         this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
         this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
         this.mainSplitContainer.Panel1.SuspendLayout();
         this.mainSplitContainer.Panel2.SuspendLayout();
         this.mainSplitContainer.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
         this.splitContainer.SuspendLayout();
         this.mainMenuStrip.SuspendLayout();
         this.SuspendLayout();
         // 
         // mainSplitContainer
         // 
         this.mainSplitContainer.BackColor = System.Drawing.SystemColors.ControlText;
         this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
         this.mainSplitContainer.Location = new System.Drawing.Point(0, 24);
         this.mainSplitContainer.Name = "mainSplitContainer";
         // 
         // mainSplitContainer.Panel1
         // 
         this.mainSplitContainer.Panel1.BackColor = System.Drawing.SystemColors.Control;
         this.mainSplitContainer.Panel1.Controls.Add(this.imagesTabControl);
         // 
         // mainSplitContainer.Panel2
         // 
         this.mainSplitContainer.Panel2.BackColor = System.Drawing.SystemColors.Control;
         this.mainSplitContainer.Panel2.Controls.Add(this.splitContainer);
         this.mainSplitContainer.Size = new System.Drawing.Size(737, 442);
         this.mainSplitContainer.SplitterDistance = 524;
         this.mainSplitContainer.TabIndex = 1;
         this.mainSplitContainer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainSplitContainer_MouseDown);
         this.mainSplitContainer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainSplitContainer_MouseMove);
         this.mainSplitContainer.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainSplitContainer_MouseUp);
         // 
         // imagesTabControl
         // 
         this.imagesTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
         this.imagesTabControl.ItemSize = new System.Drawing.Size(0, 18);
         this.imagesTabControl.Location = new System.Drawing.Point(0, 0);
         this.imagesTabControl.Name = "imagesTabControl";
         this.imagesTabControl.SelectedIndex = 0;
         this.imagesTabControl.Size = new System.Drawing.Size(524, 442);
         this.imagesTabControl.TabIndex = 0;
         this.imagesTabControl.SizeChanged += new System.EventHandler(this.ImagesTabControl_SizeChanged);
         // 
         // splitContainer
         // 
         this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
         this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
         this.splitContainer.IsSplitterFixed = true;
         this.splitContainer.Location = new System.Drawing.Point(0, 0);
         this.splitContainer.Name = "splitContainer";
         this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
         this.splitContainer.Size = new System.Drawing.Size(209, 442);
         this.splitContainer.SplitterDistance = 281;
         this.splitContainer.TabIndex = 9;
         // 
         // mainMenuStrip
         // 
         this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
         this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
         this.mainMenuStrip.Name = "mainMenuStrip";
         this.mainMenuStrip.Size = new System.Drawing.Size(737, 24);
         this.mainMenuStrip.TabIndex = 2;
         this.mainMenuStrip.Text = "mainMenuStrip";
         // 
         // fileToolStripMenuItem
         // 
         this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.closeAllToolStripMenuItem,
            this.exitToolStripMenuItem});
         this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
         this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
         this.fileToolStripMenuItem.Text = "File";
         // 
         // openToolStripMenuItem
         // 
         this.openToolStripMenuItem.Name = "openToolStripMenuItem";
         this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
         this.openToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
         this.openToolStripMenuItem.Text = "Open...";
         this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
         // 
         // closeToolStripMenuItem
         // 
         this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
         this.closeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
         this.closeToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
         this.closeToolStripMenuItem.Text = "Close";
         this.closeToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItem_Click);
         // 
         // closeAllToolStripMenuItem
         // 
         this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
         this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
         this.closeAllToolStripMenuItem.Text = "Close All";
         this.closeAllToolStripMenuItem.Click += new System.EventHandler(this.CloseAllToolStripMenuItem_Click);
         // 
         // exitToolStripMenuItem
         // 
         this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
         this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
         this.exitToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
         this.exitToolStripMenuItem.Text = "Exit";
         this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
         // 
         // helpToolStripMenuItem
         // 
         this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
         this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
         this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
         this.helpToolStripMenuItem.Text = "Help";
         // 
         // aboutToolStripMenuItem
         // 
         this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
         this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
         this.aboutToolStripMenuItem.Text = "About...";
         this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
         // 
         // MainWindow
         // 
         this.AllowDrop = true;
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(737, 466);
         this.Controls.Add(this.mainSplitContainer);
         this.Controls.Add(this.mainMenuStrip);
         this.MainMenuStrip = this.mainMenuStrip;
         this.Name = "MainWindow";
         this.Text = "ImagingInterface";
         this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
         this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainWindow_DragDrop);
         this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainWindow_DragEnter);
         this.mainSplitContainer.Panel1.ResumeLayout(false);
         this.mainSplitContainer.Panel2.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
         this.mainSplitContainer.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
         this.splitContainer.ResumeLayout(false);
         this.mainMenuStrip.ResumeLayout(false);
         this.mainMenuStrip.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

         }

      #endregion

      private System.Windows.Forms.ToolTip imagingInterfaceToolTip;
      private System.Windows.Forms.SplitContainer mainSplitContainer;
      private System.Windows.Forms.SplitContainer splitContainer;
      private System.Windows.Forms.MenuStrip mainMenuStrip;
      private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
      private System.Windows.Forms.TabControl imagesTabControl;
      private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
      }
   }