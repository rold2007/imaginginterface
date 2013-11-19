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
         this.rotateRightButton = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // rotateRightButton
         // 
         this.rotateRightButton.Location = new System.Drawing.Point(0, 0);
         this.rotateRightButton.Name = "rotateRightButton";
         this.rotateRightButton.Size = new System.Drawing.Size(75, 23);
         this.rotateRightButton.TabIndex = 0;
         this.rotateRightButton.Text = "Rotate right";
         this.rotateRightButton.UseVisualStyleBackColor = true;
         this.rotateRightButton.Click += new System.EventHandler(this.RotateRightButton_Click);
         // 
         // RotateView
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.rotateRightButton);
         this.Name = "RotateView";
         this.ResumeLayout(false);

         }

      #endregion

      private System.Windows.Forms.Button rotateRightButton;
      }
   }
