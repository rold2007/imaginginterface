// <copyright file="MainWindow.cs" company="David Rolland">
// Copyright (c) David Rolland. All rights reserved.
// </copyright>

namespace ImagingInterface.Views
{
   using System;
   using System.ComponentModel;
   using System.Diagnostics;
   using System.Windows.Forms;
   using ImagingInterface.Controllers;
   using ImagingInterface.Controllers.Interfaces;
   using ImagingInterface.Plugins;

   public partial class MainWindow : Form
   {
      private static string closePluginName = "Close plugin"; // ncrunch: no coverage

      private FileOperationController fileOperationController;
      private PluginOperationController pluginOperationController;
      private ImageManagerView imageManagerView;
      private PluginManagerView pluginManagerView;
      private AboutBoxView aboutBoxView;
      private ApplicationLogic applicationLogic;

      public MainWindow(FileOperationController fileOperationController, PluginOperationController pluginOperationController, ImageManagerView imageManagerView, PluginManagerView pluginManagerView, AboutBoxView aboutBoxView, IApplicationLogic applicationLogic)
      {
         this.fileOperationController = fileOperationController;
         this.pluginOperationController = pluginOperationController;
         this.imageManagerView = imageManagerView;
         this.pluginManagerView = pluginManagerView;
         this.aboutBoxView = aboutBoxView;
         this.applicationLogic = applicationLogic as ApplicationLogic;
         this.InitializeComponent();

         this.mainSplitContainer.Panel1.Controls.Add(this.imageManagerView);
         this.mainSplitContainer.Panel2.Controls.Add(this.pluginManagerView);

         this.InitializePlugins();
      }

      public event CancelEventHandler ApplicationClosing;

      public new void Close()
      {
         base.Close();
      }

      private void InitializePlugins()
      {
         bool pluginPresent = false;

         foreach (string pluginName in this.pluginOperationController.PluginNames)
         {
            this.AddPlugin(pluginName);

            pluginPresent = true;
         }

         if (pluginPresent)
         {
            this.AddPlugin(closePluginName);
         }
      }

      private void AddPlugin(string name)
      {
         ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(name)
         {
            Name = name
         };
         toolStripMenuItem.Click += this.PluginClick;

         this.pluginsToolStripMenuItem.DropDownItems.Add(toolStripMenuItem);
      }

      private void PluginClick(object sender, EventArgs e)
      {
         ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;

         if (toolStripMenuItem.Name == MainWindow.closePluginName)
         {
            IPluginView pluginView = this.pluginManagerView.GetActivePlugin();

            this.pluginOperationController.ClosePlugin(pluginView);
         }
         else
         {
            this.pluginOperationController.CreatePlugin(toolStripMenuItem.Name);
         }
      }

      private void MainWindow_DragDrop(object sender, DragEventArgs e)
      {
         if (this.DragEventValid(e))
         {
            string[] data = e.Data.GetData(DataFormats.FileDrop) as string[];

            this.fileOperationController.OpenFiles(data);
         }
      }

      private void MainWindow_DragEnter(object sender, DragEventArgs e)
      {
         if (this.DragEventValid(e))
         {
            e.Effect = DragDropEffects.Copy;
         }
         else
         {
            e.Effect = DragDropEffects.None;
         }
      }

      private bool DragEventValid(DragEventArgs e)
      {
         if ((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
         {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
               string[] data = e.Data.GetData(DataFormats.FileDrop) as string[];

               if (data.Length != 0)
               {
                  return true;
               }
            }
         }

         return false;
      }

      private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
      {
         this.aboutBoxView.ShowDialog();
      }

      private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
      {
         base.Close();
      }

      private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
      {
         using (OpenFileDialog openFileDialog = new OpenFileDialog())
         {
            openFileDialog.Multiselect = true;

            DialogResult dialogResult = openFileDialog.ShowDialog();

            this.fileOperationController.OpenFiles(openFileDialog.FileNames);
         }
      }

      private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
      {
         if (this.imageManagerView.HasActiveImageView)
         {
            ImageView imageView = this.imageManagerView.ActiveImageView;

            this.fileOperationController.CloseFile(imageView);
         }
      }

      private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
      {
         this.fileOperationController.CloseFiles(this.imageManagerView.ImageViews);
      }

      // Based on http://stackoverflow.com/questions/6521731/refresh-the-panels-of-a-splitcontainer-as-the-splitter-moves
      private void MainSplitContainer_MouseDown(object sender, MouseEventArgs e)
      {
         SplitContainer splitContainer = (SplitContainer)sender;

         // This disables the normal move behavior
         splitContainer.IsSplitterFixed = true;
      }

      // Based on http://stackoverflow.com/questions/6521731/refresh-the-panels-of-a-splitcontainer-as-the-splitter-moves
      private void MainSplitContainer_MouseUp(object sender, MouseEventArgs e)
      {
         SplitContainer splitContainer = (SplitContainer)sender;

         // This allows the splitter to be moved normally again
         splitContainer.IsSplitterFixed = false;
      }

      // Based on http://stackoverflow.com/questions/6521731/refresh-the-panels-of-a-splitcontainer-as-the-splitter-moves
      private void MainSplitContainer_MouseMove(object sender, MouseEventArgs e)
      {
         SplitContainer splitContainer = (SplitContainer)sender;

         // Check to make sure the splitter won't be updated by the
         // normal move behavior also
         if (splitContainer.IsSplitterFixed)
         {
            // Make sure that the button used to move the splitter
            // is the left mouse button
            if (e.Button.Equals(MouseButtons.Left))
            {
               // Checks to see if the splitter is aligned Vertically
               if (splitContainer.Orientation.Equals(Orientation.Vertical))
               {
                  // Only move the splitter if the mouse is within
                  // the appropriate bounds
                  if (e.X > 0 && e.X < splitContainer.Width)
                  {
                     // Move the splitter
                     splitContainer.SplitterDistance = e.X;
                  }
               }
               else
               {
                  Debug.Assert(splitContainer.Orientation.Equals(Orientation.Horizontal), "If it isn't aligned vertically then it must be horizontal");

                  // Only move the splitter if the mouse is within
                  // the appropriate bounds
                  if (e.Y > 0 && e.Y < splitContainer.Height)
                  {
                     // Move the splitter
                     splitContainer.SplitterDistance = e.Y;
                  }
               }
            }
            else
            {
               // This allows the splitter to be moved normally again
               splitContainer.IsSplitterFixed = false;
            }
         }
      }

      private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
      {
         CancelEventArgs cancelEventArgs = new CancelEventArgs();

         this.ApplicationClosing?.Invoke(this, cancelEventArgs);

         e.Cancel = cancelEventArgs.Cancel;
      }

      private void MainWindow_Load(object sender, EventArgs e)
      {
         this.applicationLogic.AddImageManagerView(this.imageManagerView);
         this.applicationLogic.AddPluginManagerView(this.pluginManagerView);
      }
   }
}
