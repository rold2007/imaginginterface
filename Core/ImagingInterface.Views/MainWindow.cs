namespace ImagingInterface.Views
   {
   using System;
   using System.Collections.Generic;
   using System.Diagnostics;
   using System.Drawing;
   using System.Linq;
   using System.Windows.Forms;
   using Emgu.CV;
   using Emgu.CV.Structure;
   using ImagingInterface.Models;
   using ImagingInterface.Plugins;
   using ImagingInterface.Views.EventArguments;

   public partial class MainWindow : Form, IMainView, IFileOperationView, IPluginOperationsView
      {
      private static bool checkSingleton = false;

      public MainWindow()
         {
         // This help detect misconfiguration in IoC
         Debug.Assert(MainWindow.checkSingleton == false, "A singleton shoudn't be constructed twice.");

         MainWindow.checkSingleton = true;

         this.InitializeComponent();
         }

      public event EventHandler FileOpen;

      public event EventHandler FileClose;

      public event EventHandler FileCloseAll;

      public event EventHandler<DragDropEventArgs> DragDropFile;

      public event EventHandler<PluginCreateEventArgs> PluginCreate;

      public string[] OpenFile()
         {
         OpenFileDialog openFileDialog = new OpenFileDialog();

         DialogResult dialogResult = openFileDialog.ShowDialog();

         return openFileDialog.FileNames;
         }

      public void AddPlugin(string name)
         {
         ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(name);

         toolStripMenuItem.Name = name;
         toolStripMenuItem.Click += this.PluginClick;

         this.pluginsToolStripMenuItem.DropDownItems.Add(toolStripMenuItem);
         }

      public void AddImageManagerView(IImageManagerView imageManagerView)
         {
         this.mainSplitContainer.Panel1.Controls.Add(imageManagerView as Control);
         }

      public void AddPluginManagerView(IPluginManagerView pluginManagerView)
         {
         this.mainSplitContainer.Panel2.Controls.Add(pluginManagerView as Control);
         }

      private void PluginClick(object sender, EventArgs e)
         {
         if (this.PluginCreate != null)
            {
            ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;

            this.PluginCreate(sender, new PluginCreateEventArgs(toolStripMenuItem.Name));
            }
         }

      private void MainWindow_DragDrop(object sender, DragEventArgs e)
         {
         if (this.DragEventValid(e))
            {
            if (this.DragDropFile != null)
               {
               string[] data = e.Data.GetData(DataFormats.FileDrop) as string[];

               this.DragDropFile(sender, new DragDropEventArgs(data));
               }
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
         AboutBox aboutBox = new AboutBox();

         aboutBox.ShowDialog(this);
         }

      private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
         {
         Application.Exit();
         }

      private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
         {
         if (this.FileOpen != null)
            {
            this.FileOpen(this, EventArgs.Empty);
            }
         }

      private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
         {
         if (this.FileClose != null)
            {
            this.FileClose(this, EventArgs.Empty);
            }
         }

      private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
         {
         if (this.FileCloseAll != null)
            {
            this.FileCloseAll(this, EventArgs.Empty);
            }
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
                     Trace.WriteLine(string.Format("X: {0} Distance: {1}", e.X, splitContainer.SplitterDistance));
                     }
                  else
                     {
                     Trace.WriteLine(string.Format("Out X: {0} Distance {1}", e.X, splitContainer.SplitterDistance));
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
      }
   }
