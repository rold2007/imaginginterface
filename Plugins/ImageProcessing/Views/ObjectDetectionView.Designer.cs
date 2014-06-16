namespace ImageProcessing.Views
   {
   partial class ObjectDetectionView
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
         this.trainButton = new System.Windows.Forms.Button();
         this.detectButton = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // trainButton
         // 
         this.trainButton.Location = new System.Drawing.Point(21, 181);
         this.trainButton.Name = "trainButton";
         this.trainButton.Size = new System.Drawing.Size(75, 23);
         this.trainButton.TabIndex = 0;
         this.trainButton.Text = "Train";
         this.trainButton.UseVisualStyleBackColor = true;
         // 
         // detectButton
         // 
         this.detectButton.Location = new System.Drawing.Point(102, 181);
         this.detectButton.Name = "detectButton";
         this.detectButton.Size = new System.Drawing.Size(75, 23);
         this.detectButton.TabIndex = 1;
         this.detectButton.Text = "Detect";
         this.detectButton.UseVisualStyleBackColor = true;
         // 
         // ObjectDetectionView
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.detectButton);
         this.Controls.Add(this.trainButton);
         this.Name = "ObjectDetectionView";
         this.Size = new System.Drawing.Size(471, 229);
         this.ResumeLayout(false);

         }

      #endregion

      private System.Windows.Forms.Button trainButton;
      private System.Windows.Forms.Button detectButton;
      }
   }
