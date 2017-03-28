namespace ImagingInterface.Views
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows.Forms;
    using ImagingInterface.Controllers;
    using ImagingInterface.Controllers.EventArguments;
    using Microsoft.Practices.ServiceLocation;
    using Plugins;

    public partial class MainWindow : Form
    {
        private IServiceLocator serviceLocator;
        private FileOperationController fileOperationController;
        private ImageManagerView imageManagerView;
        private PluginManagerView pluginManagerView;

        public MainWindow(IServiceLocator serviceLocator, FileOperationController fileOperationController, ImageManagerView imageManagerView, PluginManagerView pluginManagerView)
        {
            this.serviceLocator = serviceLocator;
            this.fileOperationController = fileOperationController;
            this.imageManagerView = imageManagerView;
            this.pluginManagerView = pluginManagerView;

            this.InitializeComponent();

            this.mainSplitContainer.Panel1.Controls.Add(this.imageManagerView);
            this.mainSplitContainer.Panel2.Controls.Add(this.pluginManagerView);
        }

        public event CancelEventHandler ApplicationClosing;

        public event EventHandler<PluginCreateEventArgs> PluginCreate;

        public void AddPlugin(string name)
        {
            ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(name);

            toolStripMenuItem.Name = name;
            toolStripMenuItem.Click += this.PluginClick;

            this.pluginsToolStripMenuItem.DropDownItems.Add(toolStripMenuItem);
        }

        public new void Close()
        {
            base.Close();
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
            using (AboutBoxView aboutBoxView = this.serviceLocator.GetInstance<AboutBoxView>())
            {
                aboutBoxView.ShowDialog();
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //IList<IFileSource> fileSources;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Multiselect = true;

                DialogResult dialogResult = openFileDialog.ShowDialog();

                this.fileOperationController.OpenFiles(openFileDialog.FileNames);
            }

            //this.OpenAllImages(fileSources);
        }
        /*
        private void OpenAllImages(IList<IFileSource> fileSources)
        {
           foreach (IFileSource fileSource in fileSources)
           {
              //ImageView imageView = this.serviceLocator.GetInstance<ImageView>();

              //imageView.SetImageSource(fileSource);

              //this.imageManagerView.AddImageView(imageView);
           }
        }
        */
        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.fileOperationController.CloseActiveFile();
            //this.imageManagerView.RemoveActiveImageView();
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.fileOperationController.CloseAllFiles();
            //this.imageManagerView.RemoveAllImageViews();
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

            if (this.ApplicationClosing != null)
            {
                this.ApplicationClosing(this, cancelEventArgs);
            }

            e.Cancel = cancelEventArgs.Cancel;
        }
    }
}
