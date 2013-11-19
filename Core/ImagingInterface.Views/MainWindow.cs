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

   public partial class MainWindow : Form, IFileView, IImageManagerView, IPluginOperationsView, IPluginManagerView
      {
      private static bool checkSingleton = false;
      private Dictionary<IImageView, TabPage> imageViewTabPage;
      private Dictionary<IImageView, ToolTip> imageViewToolTip;
      private Dictionary<IRawPluginView, TabPage> pluginViewTabPage;
      private Dictionary<IRawPluginView, ToolTip> pluginViewToolTip;

      public MainWindow()
         {
         // This help detect misconfiguration in IoC
         Debug.Assert(MainWindow.checkSingleton == false, "A singleton shoudn't be constructed twice.");

         MainWindow.checkSingleton = true;

         this.InitializeComponent();

         this.imageViewTabPage = new Dictionary<IImageView, TabPage>();
         this.imageViewToolTip = new Dictionary<IImageView, ToolTip>();
         this.pluginViewTabPage = new Dictionary<IRawPluginView, TabPage>();
         this.pluginViewToolTip = new Dictionary<IRawPluginView, ToolTip>();
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

      public void AddImageView(IImageView imageView, IImageModel imageModel)
         {
         TabPage tabPage = new TabPage(imageModel.DisplayName);
         ToolTip toolTip = new ToolTip();

         this.imageViewTabPage.Add(imageView, tabPage);
         this.imageViewToolTip.Add(imageView, toolTip);

         // Attach a new ToolTip because there's no way to detach a global (form) ToolTip
         // when closing the image
         toolTip.SetToolTip(tabPage, imageModel.DisplayName);

         Control imageViewControl = imageView as Control;

         tabPage.Controls.Add(imageViewControl);

         Size tabPageSize = this.imagesTabControl.DisplayRectangle.Size;

         tabPageSize.Height -= this.imagesTabControl.ItemSize.Height;

         tabPage.Size = tabPageSize;

         this.UpdateImageTabPageProperties(imageView);

         this.imagesTabControl.Controls.Add(tabPage);
         }

      public IImageView GetActiveImageView()
         {
         if (this.imagesTabControl.SelectedTab != null)
            {
            return this.imagesTabControl.SelectedTab.Controls[0] as IImageView;
            }
         else
            {
            return null;
            }
         }

      public void RemoveImageView(IImageView imageView)
         {
         TabPage tabPage = this.imageViewTabPage[imageView];
         ToolTip toolTip = this.imageViewToolTip[imageView];

         this.imagesTabControl.Controls.Remove(tabPage);
         this.imageViewTabPage.Remove(imageView);
         this.imageViewToolTip.Remove(imageView);

         tabPage.Dispose();
         toolTip.Dispose();
         }

      public void AddPlugin(string name)
         {
         ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(name);

         toolStripMenuItem.Name = name;
         toolStripMenuItem.Click += this.PluginClick;

         this.pluginsToolStripMenuItem.DropDownItems.Add(toolStripMenuItem);
         }

      public void AddPluginView(IRawPluginView rawPluginView, IRawPluginModel rawPluginModel)
         {
         TabPage tabPage = new TabPage(rawPluginModel.DisplayName);
         ToolTip toolTip = new ToolTip();

         this.pluginViewTabPage.Add(rawPluginView, tabPage);
         this.pluginViewToolTip.Add(rawPluginView, toolTip);

         // Attach a new ToolTip because there's no way to detach a global (form) ToolTip
         // when closing the plugin
         toolTip.SetToolTip(tabPage, rawPluginModel.DisplayName);

         Control pluginViewControl = rawPluginView as Control;

         tabPage.Controls.Add(pluginViewControl);

         Size tabPageSize = this.pluginsTabControl.DisplayRectangle.Size;

         tabPageSize.Height -= this.pluginsTabControl.ItemSize.Height;

         tabPage.Size = tabPageSize;

         this.UpdatePluginTabPageProperties(rawPluginView);

         this.pluginsTabControl.Controls.Add(tabPage);
         }

      public IRawPluginView GetActivePluginView()
         {
         if (this.pluginsTabControl.SelectedTab != null)
            {
            return this.pluginsTabControl.SelectedTab.Controls[0] as IRawPluginView;
            }
         else
            {
            return null;
            }
         }

      public void RemovePluginView(IRawPluginView rawPluginView)
         {
         TabPage tabPage = this.pluginViewTabPage[rawPluginView];
         ToolTip toolTip = this.pluginViewToolTip[rawPluginView];

         this.pluginsTabControl.Controls.Remove(tabPage);
         this.pluginViewTabPage.Remove(rawPluginView);
         this.pluginViewToolTip.Remove(rawPluginView);

         tabPage.Dispose();
         toolTip.Dispose();
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

      private void UpdateImageTabPageProperties(IImageView imageView)
         {
         TabPage tabPage = this.imageViewTabPage[imageView];
         Size size = tabPage.ClientSize;
         Control imageViewControl = imageView as Control;

         imageViewControl.Size = size;
         }

      private void UpdatePluginTabPageProperties(IRawPluginView pluginView)
         {
         TabPage tabPage = this.pluginViewTabPage[pluginView];
         Size size = tabPage.ClientSize;
         Control imageViewControl = pluginView as Control;

         imageViewControl.Size = size;
         }

      private void ImagesTabControl_SizeChanged(object sender, EventArgs e)
         {
         if (this.imagesTabControl.TabCount != 0 && this.imagesTabControl.SelectedTab != null)
            {
            this.UpdateImageTabPageProperties(this.imagesTabControl.SelectedTab.Controls[0] as IImageView);
            }
         }

      private void PluginsTabControl_SizeChanged(object sender, EventArgs e)
         {
         if (this.pluginsTabControl.TabCount != 0 && this.pluginsTabControl.SelectedTab != null)
            {
            this.UpdateImageTabPageProperties(this.pluginsTabControl.SelectedTab.Controls[0] as IImageView);
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
