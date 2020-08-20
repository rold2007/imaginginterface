namespace ImageProcessing.Views
   {
   partial class ObjectDetectionManagerView
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
         this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
         this.labelGroupBox = new System.Windows.Forms.GroupBox();
         this.removeButton = new System.Windows.Forms.Button();
         this.labelsListView = new System.Windows.Forms.ListView();
         this.columnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.imageList = new System.Windows.Forms.ImageList(this.components);
         this.labelTextBox = new System.Windows.Forms.TextBox();
         this.addButton = new System.Windows.Forms.Button();
         this.trainButton = new System.Windows.Forms.Button();
         this.detectButton = new System.Windows.Forms.Button();
         this.flowLayoutPanel.SuspendLayout();
         this.labelGroupBox.SuspendLayout();
         this.SuspendLayout();
         // 
         // flowLayoutPanel
         // 
         this.flowLayoutPanel.Controls.Add(this.labelGroupBox);
         this.flowLayoutPanel.Controls.Add(this.trainButton);
         this.flowLayoutPanel.Controls.Add(this.detectButton);
         this.flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
         this.flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
         this.flowLayoutPanel.Location = new System.Drawing.Point(0, 0);
         this.flowLayoutPanel.Name = "flowLayoutPanel";
         this.flowLayoutPanel.Size = new System.Drawing.Size(420, 268);
         this.flowLayoutPanel.TabIndex = 0;
         // 
         // labelGroupBox
         // 
         this.labelGroupBox.Controls.Add(this.removeButton);
         this.labelGroupBox.Controls.Add(this.labelsListView);
         this.labelGroupBox.Controls.Add(this.labelTextBox);
         this.labelGroupBox.Controls.Add(this.addButton);
         this.labelGroupBox.Location = new System.Drawing.Point(3, 3);
         this.labelGroupBox.Name = "labelGroupBox";
         this.labelGroupBox.Size = new System.Drawing.Size(409, 202);
         this.labelGroupBox.TabIndex = 5;
         this.labelGroupBox.TabStop = false;
         this.labelGroupBox.Text = "Labels";
         // 
         // removeButton
         // 
         this.removeButton.Enabled = false;
         this.removeButton.Location = new System.Drawing.Point(187, 42);
         this.removeButton.Name = "removeButton";
         this.removeButton.Size = new System.Drawing.Size(75, 21);
         this.removeButton.TabIndex = 5;
         this.removeButton.Text = "Remove";
         this.removeButton.UseVisualStyleBackColor = true;
         this.removeButton.Click += new System.EventHandler(this.RemoveButton_Click);
         // 
         // labelsListView
         // 
         this.labelsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader});
         this.labelsListView.FullRowSelect = true;
         this.labelsListView.HideSelection = false;
         this.labelsListView.Location = new System.Drawing.Point(6, 42);
         this.labelsListView.Name = "labelsListView";
         this.labelsListView.Size = new System.Drawing.Size(175, 155);
         this.labelsListView.SmallImageList = this.imageList;
         this.labelsListView.TabIndex = 4;
         this.labelsListView.UseCompatibleStateImageBehavior = false;
         this.labelsListView.View = System.Windows.Forms.View.SmallIcon;
         this.labelsListView.SelectedIndexChanged += new System.EventHandler(this.LabelsListView_SelectedIndexChanged);
         // 
         // columnHeader
         // 
         this.columnHeader.Width = 0;
         // 
         // imageList
         // 
         this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
         this.imageList.ImageSize = new System.Drawing.Size(16, 16);
         this.imageList.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // labelTextBox
         // 
         this.labelTextBox.Location = new System.Drawing.Point(6, 18);
         this.labelTextBox.Name = "labelTextBox";
         this.labelTextBox.Size = new System.Drawing.Size(175, 19);
         this.labelTextBox.TabIndex = 1;
         this.labelTextBox.TextChanged += new System.EventHandler(this.LabelTextBox_TextChanged);
         // 
         // addButton
         // 
         this.addButton.Enabled = false;
         this.addButton.Location = new System.Drawing.Point(187, 15);
         this.addButton.Name = "addButton";
         this.addButton.Size = new System.Drawing.Size(75, 21);
         this.addButton.TabIndex = 2;
         this.addButton.Text = "Add";
         this.addButton.UseVisualStyleBackColor = true;
         this.addButton.Click += new System.EventHandler(this.AddButton_Click);
         // 
         // trainButton
         // 
         this.trainButton.Location = new System.Drawing.Point(3, 211);
         this.trainButton.Name = "trainButton";
         this.trainButton.Size = new System.Drawing.Size(75, 21);
         this.trainButton.TabIndex = 2;
         this.trainButton.Text = "Train";
         this.trainButton.UseVisualStyleBackColor = true;
         this.trainButton.Click += new System.EventHandler(this.TrainButton_Click);
         // 
         // detectButton
         // 
         this.detectButton.Location = new System.Drawing.Point(3, 238);
         this.detectButton.Name = "detectButton";
         this.detectButton.Size = new System.Drawing.Size(75, 21);
         this.detectButton.TabIndex = 3;
         this.detectButton.Text = "Detect";
         this.detectButton.UseVisualStyleBackColor = true;
         // 
         // ObjectDetectionManagerView
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.flowLayoutPanel);
         this.Name = "ObjectDetectionManagerView";
         this.Size = new System.Drawing.Size(420, 268);
         this.flowLayoutPanel.ResumeLayout(false);
         this.labelGroupBox.ResumeLayout(false);
         this.labelGroupBox.PerformLayout();
         this.ResumeLayout(false);

         }

      #endregion

      private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
      private System.Windows.Forms.GroupBox labelGroupBox;
      private System.Windows.Forms.Button removeButton;
      private System.Windows.Forms.ListView labelsListView;
      private System.Windows.Forms.ColumnHeader columnHeader;
      private System.Windows.Forms.ImageList imageList;
      private System.Windows.Forms.TextBox labelTextBox;
      private System.Windows.Forms.Button addButton;
      private System.Windows.Forms.Button trainButton;
      private System.Windows.Forms.Button detectButton;
   }
   }
