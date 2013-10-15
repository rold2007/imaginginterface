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
         this.components = new System.ComponentModel.Container();
         this.imageBox = new Emgu.CV.UI.ImageBox();
         ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
         this.SuspendLayout();
         // 
         // imageBox
         // 
         this.imageBox.BackColor = System.Drawing.SystemColors.Control;
         this.imageBox.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.PanAndZoom;
         this.imageBox.Location = new System.Drawing.Point(0, 0);
         this.imageBox.Name = "imageBox";
         this.imageBox.Size = new System.Drawing.Size(375, 375);
         this.imageBox.TabIndex = 3;
         this.imageBox.TabStop = false;
         // 
         // ImageView
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.AutoScroll = true;
         this.Controls.Add(this.imageBox);
         this.Name = "ImageView";
         this.Size = new System.Drawing.Size(116, 116);
         ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
         this.ResumeLayout(false);

         }

      #endregion

      private Emgu.CV.UI.ImageBox imageBox;
      }
   }
