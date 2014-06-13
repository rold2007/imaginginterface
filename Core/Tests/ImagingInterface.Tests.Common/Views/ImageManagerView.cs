namespace ImagingInterface.Tests.Common.Views
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Plugins;
   using ImagingInterface.Views;

   public class ImageManagerView : IImageManagerView
      {
      private List<IRawImageView> allRawImageViews;

      public ImageManagerView()
         {
         this.allRawImageViews = new List<IRawImageView>();
         }

      public event EventHandler ActiveImageChanged;

      public void AddImage(IRawImageView rawImageView, IRawImageModel rawImageModel)
         {
         this.allRawImageViews.Add(rawImageView);

         if (this.allRawImageViews.Count == 1)
            {
            this.TriggerActiveImageChanged();
            }
         }

      public IRawImageView GetActiveImageView()
         {
         if (this.allRawImageViews.Count == 0)
            {
            return null;
            }
         else
            {
            return this.allRawImageViews[0];
            }
         }

      public void RemoveImage(IRawImageView rawImageView)
         {
         this.allRawImageViews.Remove(rawImageView);

         this.TriggerActiveImageChanged();
         }

      private void TriggerActiveImageChanged()
         {
         if (this.ActiveImageChanged != null)
            {
            this.ActiveImageChanged(this, EventArgs.Empty);
            }
         }
      }
   }
