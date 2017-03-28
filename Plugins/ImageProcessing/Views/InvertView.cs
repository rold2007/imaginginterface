namespace ImageProcessing.Views
   {
   using System;
   using System.Windows.Forms;
   using ImageProcessing.Controllers.EventArguments;

   public partial class InvertView : UserControl //, IInvertView
      {
      public InvertView()
         {
         this.InitializeComponent();
         }

      public event EventHandler<InvertEventArgs> Invert;

      public void Close()
         {
         }

      private void InvertCheckBox_CheckedChanged(object sender, EventArgs e)
         {
         if (this.Invert != null)
            {
            this.Invert(this, new InvertEventArgs(this.InvertCheckBox.Checked));
            }
         }
      }
   }
