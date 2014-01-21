namespace Video.Views
   {
   partial class CaptureView
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
         this.startCaptureButton = new System.Windows.Forms.Button();
         this.stopCaptureButton = new System.Windows.Forms.Button();
         this.snapshotButton = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // startCaptureButton
         // 
         this.startCaptureButton.Location = new System.Drawing.Point(0, 0);
         this.startCaptureButton.Name = "startCaptureButton";
         this.startCaptureButton.Size = new System.Drawing.Size(75, 23);
         this.startCaptureButton.TabIndex = 0;
         this.startCaptureButton.Text = "Start";
         this.startCaptureButton.UseVisualStyleBackColor = true;
         this.startCaptureButton.Click += new System.EventHandler(this.StartCaptureButton_Click);
         // 
         // stopCaptureButton
         // 
         this.stopCaptureButton.Location = new System.Drawing.Point(81, 0);
         this.stopCaptureButton.Name = "stopCaptureButton";
         this.stopCaptureButton.Size = new System.Drawing.Size(75, 23);
         this.stopCaptureButton.TabIndex = 1;
         this.stopCaptureButton.Text = "Stop";
         this.stopCaptureButton.UseVisualStyleBackColor = true;
         this.stopCaptureButton.Click += new System.EventHandler(this.StopCaptureButton_Click);
         // 
         // snapshotButton
         // 
         this.snapshotButton.Location = new System.Drawing.Point(162, 0);
         this.snapshotButton.Name = "snapshotButton";
         this.snapshotButton.Size = new System.Drawing.Size(75, 23);
         this.snapshotButton.TabIndex = 2;
         this.snapshotButton.Text = "SnapShot";
         this.snapshotButton.UseVisualStyleBackColor = true;
         this.snapshotButton.Click += new System.EventHandler(this.SnapshotButton_Click);
         // 
         // CaptureView
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.snapshotButton);
         this.Controls.Add(this.stopCaptureButton);
         this.Controls.Add(this.startCaptureButton);
         this.Name = "CaptureView";
         this.Size = new System.Drawing.Size(265, 150);
         this.ResumeLayout(false);

         }

      #endregion

      private System.Windows.Forms.Button startCaptureButton;
      private System.Windows.Forms.Button stopCaptureButton;
      private System.Windows.Forms.Button snapshotButton;
      }
   }
