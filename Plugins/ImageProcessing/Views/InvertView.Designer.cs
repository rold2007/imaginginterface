namespace ImageProcessing.Views
   {
   partial class InvertView
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
         this.InvertCheckBox = new System.Windows.Forms.CheckBox();
         this.SuspendLayout();
         // 
         // InvertCheckBox
         // 
         this.InvertCheckBox.AutoSize = true;
         this.InvertCheckBox.Location = new System.Drawing.Point(0, 7);
         this.InvertCheckBox.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
         this.InvertCheckBox.Name = "InvertCheckBox";
         this.InvertCheckBox.Size = new System.Drawing.Size(123, 34);
         this.InvertCheckBox.TabIndex = 1;
         this.InvertCheckBox.Text = "Invert";
         this.InvertCheckBox.UseVisualStyleBackColor = true;
         this.InvertCheckBox.CheckedChanged += new System.EventHandler(this.InvertCheckBox_CheckedChanged);
         // 
         // InvertView
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 30F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.InvertCheckBox);
         this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
         this.Name = "InvertView";
         this.Size = new System.Drawing.Size(400, 346);
         this.ResumeLayout(false);
         this.PerformLayout();

         }

      #endregion

      private System.Windows.Forms.CheckBox InvertCheckBox;
   }
   }
