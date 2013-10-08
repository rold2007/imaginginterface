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

   public partial class ImageView : UserControl, IImageView
      {
      private readonly IImageViewManager imageViewManager;

      public ImageView(IImageViewManager imageViewManager)
         {
         InitializeComponent();
         
         this.imageViewManager = imageViewManager;
         }

      public void Show(IImageModel imageModel)
         {
         this.imageViewManager.AddImageView(imageModel);
         }
      }
   }
