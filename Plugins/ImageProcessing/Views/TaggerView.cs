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

   public partial class TaggerView : UserControl, ITaggerView
      {
      public TaggerView()
         {
         this.InitializeComponent();
         }

      public void Close()
         {
         }
      }
   }
