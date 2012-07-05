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
         this.mainImageBoxToolTip = new System.Windows.Forms.ToolTip(this.components);
         ((System.ComponentModel.ISupportInitialize)(this.mainImageBox)).BeginInit();
         this.SuspendLayout();
         // 
         // mainImageBox
         // 
         this.mainImageBox.Dock = System.Windows.Forms.DockStyle.Fill;
         this.mainImageBox.Location = new System.Drawing.Point(0, 0);
         this.mainImageBox.Name = "mainImageBox";
         this.mainImageBox.Size = new System.Drawing.Size(304, 266);
         this.mainImageBox.TabIndex = 0;
         this.mainImageBox.TabStop = false;
         // 
         // mainWindow
         // 
         this.AllowDrop = true;
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(304, 266);
         this.Controls.Add(this.mainImageBox);
         this.Name = "mainWindow";
         this.Text = "ImagingInterface";
         this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
         this.DragDrop += new System.Windows.Forms.DragEventHandler(this.mainWindow_DragDrop);
         this.DragEnter += new System.Windows.Forms.DragEventHandler(this.mainWindow_DragEnter);
         ((System.ComponentModel.ISupportInitialize)(this.mainImageBox)).EndInit();
         this.ResumeLayout(false);

         }

      #endregion

      private Emgu.CV.UI.ImageBox mainImageBox;
      private System.Windows.Forms.ToolTip mainImageBoxToolTip;
      }
   }

