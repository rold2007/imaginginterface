namespace ImagingInterface.Views
{
   using System;
   using System.Collections.Generic;
   using System.Drawing;
   using System.Windows.Forms;
   using ImagingInterface.Controllers;
   using ImagingInterface.Plugins;

   public partial class PluginManagerView : UserControl
      {
      private PluginManagerController pluginManagerController;

      private Dictionary<IRawPluginView, TabPage> pluginViewTabPage;
      private Dictionary<IRawPluginView, ToolTip> pluginViewToolTip;

      public PluginManagerView(PluginManagerController pluginManagerController)
         {
         this.pluginManagerController = pluginManagerController;

         this.InitializeComponent();

         this.pluginViewTabPage = new Dictionary<IRawPluginView, TabPage>();
         this.pluginViewToolTip = new Dictionary<IRawPluginView, ToolTip>();

         this.Dock = DockStyle.Fill;
         }

      public void AddPlugin(IPluginView pluginView)
         {
         TabPage tabPage = new TabPage(pluginView.DisplayName);
         ToolTip toolTip = new ToolTip();

         this.pluginViewTabPage.Add(pluginView, tabPage);
         this.pluginViewToolTip.Add(pluginView, toolTip);

         // Attach a new ToolTip because there's no way to detach a global (form) ToolTip
         // when closing the plugin
         toolTip.SetToolTip(tabPage, pluginView.DisplayName);

         Control pluginViewControl = pluginView as Control;

         tabPage.Controls.Add(pluginViewControl);

         Size tabPageSize = this.pluginsTabControl.DisplayRectangle.Size;

         tabPageSize.Height -= this.pluginsTabControl.ItemSize.Height;

         tabPage.Size = tabPageSize;

         this.UpdatePluginTabPageProperties(pluginView);

         this.pluginsTabControl.Controls.Add(tabPage);
         }

      public IRawPluginView GetActivePlugin()
         {
         if (this.pluginsTabControl.SelectedTab != null)
            {
            if (this.pluginsTabControl.SelectedTab.Controls.Count > 0)
               {
               return this.pluginsTabControl.SelectedTab.Controls[0] as IRawPluginView;
               }
            }

         return null;
         }

      public void RemovePlugin(IRawPluginView rawPluginView)
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
