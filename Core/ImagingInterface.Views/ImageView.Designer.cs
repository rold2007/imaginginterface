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
         this.glControl = new OpenTK.GLControl();
         this.SuspendLayout();
         // 
         // glControl
         // 
         this.glControl.BackColor = System.Drawing.Color.Black;
         this.glControl.Location = new System.Drawing.Point(0, 0);
         this.glControl.Name = "glControl";
         this.glControl.Size = new System.Drawing.Size(375, 375);
         this.glControl.TabIndex = 0;
         this.glControl.VSync = false;
         this.glControl.Load += new System.EventHandler(this.GLControl_Load);
         this.glControl.Paint += new System.Windows.Forms.PaintEventHandler(this.GLControl_Paint);
         this.glControl.Resize += new System.EventHandler(this.GLControl_Resize);
         // 
         // ImageView
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.AutoScroll = true;
         this.Controls.Add(this.glControl);
         this.Name = "ImageView";
         this.Size = new System.Drawing.Size(82, 82);
         this.ResumeLayout(false);

         }

      #endregion

      private OpenTK.GLControl glControl;

      }
   }
