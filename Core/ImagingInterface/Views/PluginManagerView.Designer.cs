﻿namespace ImagingInterface.Views
   {
   partial class PluginManagerView
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
         this.pluginsTabControl = new System.Windows.Forms.TabControl();
         this.SuspendLayout();
         // 
         // pluginsTabControl
         // 
         this.pluginsTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
         this.pluginsTabControl.Location = new System.Drawing.Point(0, 0);
         this.pluginsTabControl.Name = "pluginsTabControl";
         this.pluginsTabControl.SelectedIndex = 0;
         this.pluginsTabControl.Size = new System.Drawing.Size(618, 393);
         this.pluginsTabControl.TabIndex = 1;
         this.pluginsTabControl.SizeChanged += new System.EventHandler(this.PluginsTabControl_SizeChanged);
         // 
         // PluginManagerView
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.pluginsTabControl);
         this.Name = "PluginManagerView";
         this.Size = new System.Drawing.Size(618, 393);
         this.ResumeLayout(false);

         }

      #endregion

      private System.Windows.Forms.TabControl pluginsTabControl;
      }
   }
