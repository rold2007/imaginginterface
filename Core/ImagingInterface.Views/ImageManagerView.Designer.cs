namespace ImagingInterface.Views
   {
   partial class ImageManagerView
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
         this.imagesTabControl = new System.Windows.Forms.TabControl();
         this.SuspendLayout();
         // 
         // imagesTabControl
         // 
         this.imagesTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
         this.imagesTabControl.ItemSize = new System.Drawing.Size(0, 18);
         this.imagesTabControl.Location = new System.Drawing.Point(0, 0);
         this.imagesTabControl.Name = "imagesTabControl";
         this.imagesTabControl.SelectedIndex = 0;
         this.imagesTabControl.Size = new System.Drawing.Size(746, 404);
         this.imagesTabControl.TabIndex = 1;
         // 
         // ImageManagerView
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.imagesTabControl);
         this.Name = "ImageManagerView";
         this.Size = new System.Drawing.Size(746, 404);
         this.ResumeLayout(false);

         }

      #endregion

      private System.Windows.Forms.TabControl imagesTabControl;
      }
   }
