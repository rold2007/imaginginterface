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
   using ImageProcessing.Views;

   public partial class InvertView : UserControl, IInvertView
      {
      public InvertView()
         {
         this.InitializeComponent();
         }

      public event EventHandler Invert;

      public void Close()
         {
         }

      private void InvertButton_Click(object sender, EventArgs e)
         {
         if (this.Invert != null)
            {
            this.Invert(this, EventArgs.Empty);
            }
         }
      }
   }
