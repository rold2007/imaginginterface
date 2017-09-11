namespace ImageProcessing.Views
   {
   partial class TaggerView
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
         this.labelTextBox = new System.Windows.Forms.TextBox();
         this.addButton = new System.Windows.Forms.Button();
         this.labelGroupBox = new System.Windows.Forms.GroupBox();
         this.removeButton = new System.Windows.Forms.Button();
         this.labelsListView = new System.Windows.Forms.ListView();
         this.columnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.imageList = new System.Windows.Forms.ImageList(this.components);
         this.labelGroupBox.SuspendLayout();
         this.SuspendLayout();
         // 
         // labelTextBox
         // 
         this.labelTextBox.Location = new System.Drawing.Point(16, 44);
         this.labelTextBox.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
         this.labelTextBox.Name = "labelTextBox";
         this.labelTextBox.Size = new System.Drawing.Size(460, 37);
         this.labelTextBox.TabIndex = 1;
         this.labelTextBox.TextChanged += new System.EventHandler(this.LabelTextBox_TextChanged);
         // 
         // addButton
         // 
         this.addButton.Enabled = false;
         this.addButton.Location = new System.Drawing.Point(499, 37);
         this.addButton.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
         this.addButton.Name = "addButton";
         this.addButton.Size = new System.Drawing.Size(200, 53);
         this.addButton.TabIndex = 2;
         this.addButton.Text = "Add";
         this.addButton.UseVisualStyleBackColor = true;
         this.addButton.Click += new System.EventHandler(this.AddButton_Click);
         // 
         // labelGroupBox
         // 
         this.labelGroupBox.Controls.Add(this.removeButton);
         this.labelGroupBox.Controls.Add(this.labelsListView);
         this.labelGroupBox.Controls.Add(this.labelTextBox);
         this.labelGroupBox.Controls.Add(this.addButton);
         this.labelGroupBox.Location = new System.Drawing.Point(8, 7);
         this.labelGroupBox.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
         this.labelGroupBox.Name = "labelGroupBox";
         this.labelGroupBox.Padding = new System.Windows.Forms.Padding(8, 7, 8, 7);
         this.labelGroupBox.Size = new System.Drawing.Size(1091, 505);
         this.labelGroupBox.TabIndex = 4;
         this.labelGroupBox.TabStop = false;
         this.labelGroupBox.Text = "Labels";
         // 
         // removeButton
         // 
         this.removeButton.Enabled = false;
         this.removeButton.Location = new System.Drawing.Point(499, 104);
         this.removeButton.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
         this.removeButton.Name = "removeButton";
         this.removeButton.Size = new System.Drawing.Size(200, 53);
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
         this.labelsListView.Location = new System.Drawing.Point(16, 104);
         this.labelsListView.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
         this.labelsListView.Name = "labelsListView";
         this.labelsListView.Size = new System.Drawing.Size(460, 382);
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
         // TaggerView
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 30F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.labelGroupBox);
         this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
         this.Name = "TaggerView";
         this.Size = new System.Drawing.Size(1107, 519);
         this.labelGroupBox.ResumeLayout(false);
         this.labelGroupBox.PerformLayout();
         this.ResumeLayout(false);

         }

      #endregion

      private System.Windows.Forms.TextBox labelTextBox;
      private System.Windows.Forms.Button addButton;
      private System.Windows.Forms.GroupBox labelGroupBox;
      private System.Windows.Forms.ListView labelsListView;
      private System.Windows.Forms.ColumnHeader columnHeader;
      private System.Windows.Forms.ImageList imageList;
      private System.Windows.Forms.Button removeButton;
   }
   }
