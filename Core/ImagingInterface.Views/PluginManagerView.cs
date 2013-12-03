namespace ImagingInterface.Views
   {
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Diagnostics;
   using System.Drawing;
   using System.Data;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using System.Windows.Forms;
   using ImagingInterface.Models;
   using ImagingInterface.Plugins;
   using ImagingInterface.Views.EventArguments;

   public partial class PluginManagerView : UserControl, IPluginManagerView
      {
      private static bool checkSingleton = false;
      private Dictionary<IRawPluginView, TabPage> pluginViewTabPage;
      private Dictionary<IRawPluginView, ToolTip> pluginViewToolTip;

      public PluginManagerView()
         {
         // This help detect misconfiguration in IoC
         Debug.Assert(PluginManagerView.checkSingleton == false, "A singleton shoudn't be constructed twice.");

         PluginManagerView.checkSingleton = true;

         this.InitializeComponent();

         this.pluginViewTabPage = new Dictionary<IRawPluginView, TabPage>();
         this.pluginViewToolTip = new Dictionary<IRawPluginView, ToolTip>();

         this.Dock = DockStyle.Fill;
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

      private void UpdatePluginTabPageProperties(IRawPluginView pluginView)
         {
         TabPage tabPage = this.pluginViewTabPage[pluginView];
         Size size = tabPage.ClientSize;
         Control pluginViewControl = pluginView as Control;

         pluginViewControl.Size = size;
         }

      private void PluginsTabControl_SizeChanged(object sender, EventArgs e)
         {
         if (this.pluginsTabControl.TabCount != 0 && this.pluginsTabControl.SelectedTab != null)
            {
            this.UpdatePluginTabPageProperties(this.pluginsTabControl.SelectedTab.Controls[0] as IRawPluginView);
            }
         }
      }
   }
