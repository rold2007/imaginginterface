namespace ImagingInterface.Controllers
   {
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using ImagingInterface.Views;

   public class MainController : IMainController
      {
      private IMainView mainView;

      public MainController(IMainView mainView)
         {
         this.mainView = mainView;
         }

      public void AddImageManagerView(IImageManagerView imageManagerView)
         {
         this.mainView.AddImageManagerView(imageManagerView);
         }
      }
   }
