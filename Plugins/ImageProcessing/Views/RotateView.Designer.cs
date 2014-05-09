namespace ImageProcessing.Views
   {
   partial class RotateView
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
         this.rotationAngleTrackBar = new System.Windows.Forms.TrackBar();
         this.rotationAngleNumericUpDown = new System.Windows.Forms.NumericUpDown();
         ((System.ComponentModel.ISupportInitialize)(this.rotationAngleTrackBar)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.rotationAngleNumericUpDown)).BeginInit();
         this.SuspendLayout();
         // 
         // rotationAngleTrackBar
         // 
         this.rotationAngleTrackBar.LargeChange = 500;
         this.rotationAngleTrackBar.Location = new System.Drawing.Point(3, 3);
         this.rotationAngleTrackBar.Maximum = 36000;
         this.rotationAngleTrackBar.Name = "rotationAngleTrackBar";
         this.rotationAngleTrackBar.Size = new System.Drawing.Size(268, 45);
         this.rotationAngleTrackBar.SmallChange = 10;
         this.rotationAngleTrackBar.TabIndex = 1;
         this.rotationAngleTrackBar.TickFrequency = 4500;
         this.rotationAngleTrackBar.Scroll += new System.EventHandler(this.RotationAngleTrackBar_Scroll);
         // 
         // rotationAngleNumericUpDown1
         // 
         this.rotationAngleNumericUpDown.DecimalPlaces = 5;
         this.rotationAngleNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
         this.rotationAngleNumericUpDown.Location = new System.Drawing.Point(6, 54);
         this.rotationAngleNumericUpDown.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
         this.rotationAngleNumericUpDown.Name = "rotationAngleNumericUpDown1";
         this.rotationAngleNumericUpDown.Size = new System.Drawing.Size(265, 20);
         this.rotationAngleNumericUpDown.TabIndex = 3;
         this.rotationAngleNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.rotationAngleNumericUpDown.ValueChanged += new System.EventHandler(this.RotationAngleNumericUpDown_ValueChanged);
         // 
         // RotateView
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.rotationAngleNumericUpDown);
         this.Controls.Add(this.rotationAngleTrackBar);
         this.Name = "RotateView";
         this.Size = new System.Drawing.Size(274, 172);
         ((System.ComponentModel.ISupportInitialize)(this.rotationAngleTrackBar)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.rotationAngleNumericUpDown)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

         }

      #endregion

      private System.Windows.Forms.TrackBar rotationAngleTrackBar;
      private System.Windows.Forms.NumericUpDown rotationAngleNumericUpDown;
      }
   }
