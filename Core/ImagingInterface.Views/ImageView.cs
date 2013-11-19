namespace ImagingInterface.Views
   {
   using System.Windows.Forms;
   using Emgu.CV.UI;
   using ImagingInterface.Models;

   public partial class ImageView : UserControl, IImageView
      {
      public ImageView()
         {
         this.InitializeComponent();
         }

      public void AssignImageModel(IImageModel imageModel)
         {
         this.imageBox.Image = imageModel.Image;

         if (imageModel.Image != null)
            {
            this.imageBox.Size = imageModel.Image.Size;
            }
         }

      public void Close()
         {
         this.Dispose();
         }
      }
   }
