namespace ImagingInterface.Controllers.Tests.Views
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Plugins;
   using ImagingInterface.Views;

   public class PluginManagerView : IPluginManagerView
      {
      private IRawPluginView activeRawPluginView;

      public PluginManagerView()
         {
         this.RawPluginViews = new List<IRawPluginView>();
         }

      public List<IRawPluginView> RawPluginViews
         {
         get;
         private set;
         }

      public void AddPluginView(IRawPluginView rawPluginView, IRawPluginModel rawPluginModel)
         {
         this.RawPluginViews.Add(rawPluginView);

         this.activeRawPluginView = rawPluginView;
         }

      public IRawPluginView GetActivePluginView()
         {
         return this.activeRawPluginView;
         }

      public void RemovePluginView(IRawPluginView rawPluginView)
         {
         this.RawPluginViews.Add(rawPluginView);

         if (this.activeRawPluginView == rawPluginView)
            {
            if (this.RawPluginViews.Count != 0)
               {
               this.activeRawPluginView = this.RawPluginViews[0];
               }
            else
               {
               this.activeRawPluginView = null;
               }
            }
         }
      }
   }
