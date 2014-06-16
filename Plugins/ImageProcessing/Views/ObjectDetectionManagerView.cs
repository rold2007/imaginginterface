namespace ImageProcessing.Views
   {
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Data;
   using System.Drawing;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using System.Windows.Forms;
   using ImagingInterface.Plugins;

   public partial class ObjectDetectionManagerView : UserControl, IObjectDetectionManagerView
      {
      public ObjectDetectionManagerView()
         {
         this.InitializeComponent();
         }

      public void Close()
         {
         }

      public void AddView(IRawPluginView rawPluginView)
         {
         Control pluginViewControl = rawPluginView as Control;

         this.flowLayoutPanel.Controls.Add(pluginViewControl);
         }
      }
   }
