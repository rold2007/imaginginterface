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
         this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
         this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.pluginsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
         this.mainSplitContainer.SuspendLayout();
         this.mainMenuStrip.SuspendLayout();
         this.SuspendLayout();
         // 
         // mainSplitContainer
         // 
         this.mainSplitContainer.BackColor = System.Drawing.SystemColors.ControlText;
         this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
         this.mainSplitContainer.Location = new System.Drawing.Point(0, 55);
         this.mainSplitContainer.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
         this.mainSplitContainer.Name = "mainSplitContainer";
         // 
         // mainSplitContainer.Panel1
         // 
         this.mainSplitContainer.Panel1.BackColor = System.Drawing.SystemColors.Control;
         // 
         // mainSplitContainer.Panel2
         // 
         this.mainSplitContainer.Panel2.BackColor = System.Drawing.SystemColors.Control;
         this.mainSplitContainer.Size = new System.Drawing.Size(1965, 1020);
         this.mainSplitContainer.SplitterDistance = 1397;
         this.mainSplitContainer.SplitterWidth = 11;
         this.mainSplitContainer.TabIndex = 1;
         this.mainSplitContainer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainSplitContainer_MouseDown);
         this.mainSplitContainer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainSplitContainer_MouseMove);
         this.mainSplitContainer.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainSplitContainer_MouseUp);
         // 
         // mainMenuStrip
         // 
         this.mainMenuStrip.ImageScalingSize = new System.Drawing.Size(40, 40);
         this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.pluginsToolStripMenuItem,
            this.helpToolStripMenuItem});
         this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
         this.mainMenuStrip.Name = "mainMenuStrip";
         this.mainMenuStrip.Padding = new System.Windows.Forms.Padding(16, 5, 0, 5);
         this.mainMenuStrip.Size = new System.Drawing.Size(1965, 55);
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
         this.fileToolStripMenuItem.Size = new System.Drawing.Size(75, 45);
         this.fileToolStripMenuItem.Text = "File";
         // 
         // openToolStripMenuItem
         // 
         this.openToolStripMenuItem.Name = "openToolStripMenuItem";
         this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
         this.openToolStripMenuItem.Size = new System.Drawing.Size(335, 46);
         this.openToolStripMenuItem.Text = "Open...";
         this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
         // 
         // closeToolStripMenuItem
         // 
         this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
         this.closeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
         this.closeToolStripMenuItem.Size = new System.Drawing.Size(335, 46);
         this.closeToolStripMenuItem.Text = "Close";
         this.closeToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItem_Click);
         // 
         // closeAllToolStripMenuItem
         // 
         this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
         this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(335, 46);
         this.closeAllToolStripMenuItem.Text = "Close All";
         this.closeAllToolStripMenuItem.Click += new System.EventHandler(this.CloseAllToolStripMenuItem_Click);
         // 
         // exitToolStripMenuItem
         // 
         this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
         this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
         this.exitToolStripMenuItem.Size = new System.Drawing.Size(335, 46);
         this.exitToolStripMenuItem.Text = "Exit";
         this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
         // 
         // pluginsToolStripMenuItem
         // 
         this.pluginsToolStripMenuItem.Name = "pluginsToolStripMenuItem";
         this.pluginsToolStripMenuItem.Size = new System.Drawing.Size(126, 45);
         this.pluginsToolStripMenuItem.Text = "Plugins";
         // 
         // helpToolStripMenuItem
         // 
         this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
         this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
         this.helpToolStripMenuItem.Size = new System.Drawing.Size(92, 45);
         this.helpToolStripMenuItem.Text = "Help";
         // 
         // aboutToolStripMenuItem
         // 
         this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
         this.aboutToolStripMenuItem.Size = new System.Drawing.Size(235, 46);
         this.aboutToolStripMenuItem.Text = "About...";
         this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
         // 
         // MainWindow
         // 
         this.AllowDrop = true;
         this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 30F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(1965, 1075);
         this.Controls.Add(this.mainSplitContainer);
         this.Controls.Add(this.mainMenuStrip);
         this.MainMenuStrip = this.mainMenuStrip;
         this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
         this.Name = "MainWindow";
         this.Text = "ImagingInterface";
         this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
         this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
         this.Load += new System.EventHandler(this.MainWindow_Load);
         this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainWindow_DragDrop);
         this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainWindow_DragEnter);
         ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
         this.mainSplitContainer.ResumeLayout(false);
         this.mainMenuStrip.ResumeLayout(false);
         this.mainMenuStrip.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

         }

      #endregion

      private System.Windows.Forms.ToolTip imagingInterfaceToolTip;
      private System.Windows.Forms.SplitContainer mainSplitContainer;
      private System.Windows.Forms.MenuStrip mainMenuStrip;
      private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem pluginsToolStripMenuItem;
      }
   }