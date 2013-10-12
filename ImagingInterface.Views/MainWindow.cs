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
   using ImagingInterface.Views.EventArguments;

   public partial class MainWindow : Form, IFileView, IImageViewManager
      {
      private static bool checkSingleton = false;
      private Dictionary<IImageView, TabPage> imageViewTabPage;
      private Dictionary<IImageView, ToolTip> imageViewToolTip;

      public MainWindow()
         {
         Debug.Assert(MainWindow.checkSingleton == false, "A singleton shoudn't be constructed twice.");

         MainWindow.checkSingleton = true;

         this.InitializeComponent();

         this.imageViewTabPage = new Dictionary<IImageView, TabPage>();
         this.imageViewToolTip = new Dictionary<IImageView, ToolTip>();
         }

      public event EventHandler FileOpen;
      public event EventHandler FileClose;
      public event EventHandler FileCloseAll;
      public event EventHandler<DragDropEventArgs> DragDropFile;

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

         this.UpdateTabPageProperties(imageView);

         this.imagesTabControl.Controls.Add(tabPage);
         }

      public IImageView GetActiveImage()
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

      private void MainWindow_DragDrop(object sender, DragEventArgs e)
         {
         if (this.DragEventValid(e))
            {
            string[] data = e.Data.GetData(DataFormats.FileDrop) as string[];

            if (data != null)
               {
               if (this.DragDropFile != null)
                  {
                  this.DragDropFile(sender, new DragDropEventArgs(data));
                  }
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

      private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
         {
         if (this.FileCloseAll != null)
            {
            this.FileCloseAll(this, EventArgs.Empty);
            }
         }

      private void UpdateTabPageProperties(IImageView imageView)
         {
         TabPage tabPage = this.imageViewTabPage[imageView];
         Size size = tabPage.ClientSize;
         Control imageViewControl = imageView as Control;

         imageViewControl.Size = size;
         }

      private void imagesTabControl_SizeChanged(object sender, EventArgs e)
         {
         if (this.imagesTabControl.TabCount != 0 && this.imagesTabControl.SelectedTab != null)
            {
            this.UpdateTabPageProperties(this.imagesTabControl.SelectedTab.Controls[0] as IImageView);
            }
         }

      // Based on http://stackoverflow.com/questions/6521731/refresh-the-panels-of-a-splitcontainer-as-the-splitter-moves
      private void mainSplitContainer_MouseDown(object sender, MouseEventArgs e)
         {
         SplitContainer splitContainer = (SplitContainer)sender;

         // This disables the normal move behavior
         splitContainer.IsSplitterFixed = true;
         }

      // Based on http://stackoverflow.com/questions/6521731/refresh-the-panels-of-a-splitcontainer-as-the-splitter-moves
      private void mainSplitContainer_MouseUp(object sender, MouseEventArgs e)
         {
         SplitContainer splitContainer = (SplitContainer)sender;

         // This allows the splitter to be moved normally again
         splitContainer.IsSplitterFixed = false;
         }

      // Based on http://stackoverflow.com/questions/6521731/refresh-the-panels-of-a-splitcontainer-as-the-splitter-moves
      private void mainSplitContainer_MouseMove(object sender, MouseEventArgs e)
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
               // If it isn't aligned vertically then it must be
               // horizontal
               else
                  {
                  // Only move the splitter if the mouse is within
                  // the appropriate bounds
                  if (e.Y > 0 && e.Y < splitContainer.Height)
                     {
                     // Move the splitter
                     splitContainer.SplitterDistance = e.Y;
                     }
                  }
               }
            // If a button other than left is pressed or no button
            // at all
            else
               {
               // This allows the splitter to be moved normally again
               splitContainer.IsSplitterFixed = false;
               }
            }
         }
      }
   }
