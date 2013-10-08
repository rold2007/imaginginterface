namespace ImagingInterface.Views
   {
   using System.Windows.Forms;
   using ImagingInterface.Models;

   public partial class ImageView : UserControl, IImageView
      {
      private readonly IImageViewManager imageViewManager;

      public ImageView(IImageViewManager imageViewManager)
         {
         this.InitializeComponent();
         
         this.imageViewManager = imageViewManager;
         }

      public void Show(IImageModel imageModel)
         {
         this.imageViewManager.AddImageView(imageModel);
         }
      }
   }
